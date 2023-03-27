using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Services.Extensions;
using static System.Net.Mime.MediaTypeNames;
using SlugGenerator;


namespace TatBlog.Services.Blogs
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDbContext _context;

        public BlogRepository(BlogDbContext context)
        {
            _context = context;
        }

        //Tìm bài viết có định danh slug và được đăng vào tháng month năm year
        public async Task<Post> GetPostAsync(
            int year,
            int month,
            string slug,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Post> postQuery = _context.Set<Post>()
                .Include(x => x.Category)
                .Include(x => x.Author);

            if(year > 0)
            {
                postQuery = postQuery.Where(x => x.PostedDate.Year == year);
            }

            if (month > 0)
            {
                postQuery = postQuery.Where(x => x.PostedDate.Month == month);
            }

            if (!string.IsNullOrWhiteSpace(slug))
            {
                postQuery = postQuery.Where(x => x.UrlSlug == slug);
            }

            return await postQuery.FirstOrDefaultAsync(cancellationToken);
        }

        //Tìm top n bài viết được nhiều người xem nhất
        public async Task<IList<Post>> GetPopularArticleAsync(
            int numPosts,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<Post>()
                .Include(x => x.Author)
                .Include(x => x.Category)
                .OrderByDescending(p  => p.ViewCount)
                .Take(numPosts)
                .ToListAsync(cancellationToken);
        }

        //Kiểm tra tên định danh bài viết đã có hay chưa
        public async Task<bool> IsPostSlugExistedAsync(
            int postId,
            string slug,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<Post>()
                .AnyAsync(x => x.Id != postId && x.UrlSlug == slug, 
                cancellationToken);
        }

        //Tăng số lượt xem của một bài viết
        public async Task IncreaseViewCountAsync(
            int postId,
            CancellationToken cancellationToken = default)
        {
            await _context.Set<Post>()
                .Where(x => x.Id == postId)
                .ExecuteUpdateAsync(p =>
                p.SetProperty(x => x.ViewCount, x => x.ViewCount + 1),
                cancellationToken);
        }

        //Lấy danh sách tác giả
        public async Task<IList<AuthorItem>> GetAuthorsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<Author>()
                .OrderBy(a => a.FullName)
                .Select(a => new AuthorItem()
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    Email = a.ToString(),
                    JoinedDate = a.JoinedDate,
                    ImageUrl = a.ImageUrl,
                    UrlSlug = a.UrlSlug,
                    Notes = a.Notes,
                    PostCount = a.Posts.Count(p => p.Published)
                })
                .ToListAsync(cancellationToken);
        }

        //Lấy danh sách chuyên mục và số lượng bài viết nằm từng chuyên mục/chủ đề
        public async Task<IList<CategoryItem>> GetCategoriesAsync(
            bool showOnMenu = false,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Category> categories = _context.Set<Category>();

            if (showOnMenu)
            {
                categories = categories.Where(x => x.ShowOnMenu);
            }

            return await categories
                .OrderBy(x => x.Name)
                .Select(x => new CategoryItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlSlug = x.UrlSlug,
                    Description = x.Description,
                    ShowOnMenu = x.ShowOnMenu,
                    PostCount = x.Posts.Count(p => p.Published)
                })
                .ToListAsync(cancellationToken);
        }

        //Lấy danh sách các từ khóa/thẻ và phân trang theo các tham số pagingParams
        public async Task<IPagedList<TagItem>> GetPagedTagsAsync(
           IPagingParams pagingParams,
           CancellationToken cancellationToken = default)
        {
            var tagQuery = _context.Set<Tag>()
                .Select(x => new TagItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlSlug = x.UrlSlug,
                    Description = x.Description,
                    PostCount = x.Posts.Count(p => p.Published)
                });

            return await tagQuery
                .ToPagedListAsync(pagingParams, cancellationToken);
        }

        //Lấy danh sách các thẻ kèm theo số bài viết chứa thẻ đó
        public async Task<IList<TagItem>> GetTagsListAsync(
           CancellationToken cancellationToken = default)
        {
            IQueryable<Tag> tags = _context.Set<Tag>();

            return await tags
                .OrderBy(x => x.Name)
                .Select(x => new TagItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlSlug = x.UrlSlug,
                    Description = x.Description,
                    PostCount = x.Posts.Count(p => p.Published)
                })
                .ToListAsync(cancellationToken);
        }

        //Lấy và phân trang danh sách chuyên mục
        public async Task<IPagedList<CategoryItem>> GetPagedCategoriesAsync(
           IPagingParams pagingParams,
           CancellationToken cancellationToken = default)
        {
            var categoryQuery = _context.Set<Category>()
                .Select(x => new CategoryItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlSlug = x.UrlSlug,
                    Description = x.Description,
                    PostCount = x.Posts.Count(p => p.Published)
                });

            return await categoryQuery
                .ToPagedListAsync(pagingParams, cancellationToken);
        }

        //k.Lấy danh sách ngày theo N tháng tính từ tháng hiện tại
        public List<DateTime> GetDateListByMonth(int num)
        {
            List<DateTime> result = new List<DateTime>();
            for (int i = 0; i < num; i++)
            {
                result.Add(DateTime.Now.AddMonths(-i));
            }
            return result;
        }

        //k.Đếm số lượng bài viết trong N tháng gần nhất
        public List<NumberPostByMonth> GetNumberPostByMonthAsync(int numMonth)
        {
            List<DateTime> list = GetDateListByMonth(numMonth);
            List<NumberPostByMonth> result = new List<NumberPostByMonth>();

            foreach (var item in list)
            {
                IQueryable<Post> postQuery = _context.Set<Post>();
                postQuery = postQuery.Where(x => x.PostedDate.Month == item.Month && x.PostedDate.Year == item.Year);
                if (postQuery.Count() > 0)
                {
                    NumberPostByMonth resultItem = new NumberPostByMonth();
                    resultItem.Month = item.Month;
                    resultItem.Year = item.Year;
                    resultItem.PostCount = postQuery.Count();
                    result.Add(resultItem);
                }
            }

            return result;
        }

        //Tìm bài viết theo id
        public async Task<Post> GetPostByIdAsync(
        int postId, bool includeDetails = false,
        CancellationToken cancellationToken = default)
        {
            if (!includeDetails)
            {
                return await _context.Set<Post>().FindAsync(postId);
            }

            return await _context.Set<Post>()
                .Include(x => x.Category)
                .Include(x => x.Author)
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == postId, cancellationToken);
        }

        //Lấy danh sách các bài viết theo chủ đề
        //public async Task<IList<Post>> GetPostListByCategoryAsync(
        //   CancellationToken cancellationToken = default)
        //{
        //    IQueryable<Post> posts = _context.Set<Post>();

        //    return await posts
        //        .OrderBy(x => x.Title)
        //        .Select(x => new Post()
        //        {
        //            Id = x.Id,
        //            Title = x.Title,
        //            UrlSlug = x.UrlSlug,
        //            Description = x.Description,
        //        })
        //        .ToListAsync(cancellationToken);
        //}

        //Thay đổi trạng thái Published của một bài viết
        //public async Task ChangePublishedStatusAsync(
        //    int postId,
        //    CancellationToken cancellationToken = default)
        //{
        //    await _context.Set<Post>()
        //        .Where(x => x.Id == postId)
        //        .ExecuteUpdateAsync(p =>
        //        p.SetProperty(x => x.Published, x => !x.Published),
        //        cancellationToken);
        //}

        //Thay đổi trạng thái published của 1 bài viết
        public async Task TogglePublishedFlagAsync(
            int id, 
            CancellationToken cancellationToken = default)
        {
            var post = await _context.Set<Post>().FindAsync(id);

            post.Published = !post.Published;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ToggleShowOnMenuFlagAsync(
         int id,
         CancellationToken cancellationToken = default)
        {
            var category = await _context.Set<Category>().FindAsync(id);

            category.ShowOnMenu = !category.ShowOnMenu;
            await _context.SaveChangesAsync(cancellationToken);
        }

        //Lọc bài viết theo điều kiện
        private IQueryable<Post> FilterPosts(PostQuery condition)
        {
            IQueryable<Post> posts = _context.Set<Post>()
                .Include(x => x.Category)
                .Include(x => x.Author)
                .Include(x => x.Tags);

            if (condition.PublishedOnly)
            {
                posts = posts.Where(x => x.Published);
            }

            if (condition.NotPublished)
            {
                posts = posts.Where(x => !x.Published);
            }

            if (condition.CategoryId > 0)
            {
                posts = posts.Where(x => x.CategoryId == condition.CategoryId);
            }

            if (!string.IsNullOrWhiteSpace(condition.CategorySlug))
            {
                posts = posts.Where(x => x.Category.UrlSlug == condition.CategorySlug);
            }

            if (condition.AuthorId > 0)
            {
                posts = posts.Where(x => x.AuthorId == condition.AuthorId);
            }

            if (!string.IsNullOrWhiteSpace(condition.AuthorSlug))
            {
                posts = posts.Where(x => x.Author.UrlSlug == condition.AuthorSlug);
            }

            if (!string.IsNullOrWhiteSpace(condition.TagSlug))
            {
                posts = posts.Where(x => x.Tags.Any(t => t.UrlSlug == condition.TagSlug));
            }

            if (!string.IsNullOrWhiteSpace(condition.Keyword))
            {
                posts = posts.Where(x => x.Title.Contains(condition.Keyword) ||
                                         x.ShortDescription.Contains(condition.Keyword) ||
                                         x.Description.Contains(condition.Keyword) ||
                                         x.Category.Name.Contains(condition.Keyword) ||
                                         x.Tags.Any(t => t.Name.Contains(condition.Keyword)));
            }

            if (condition.Year > 0)
            {
                posts = posts .Where(x => x.PostedDate.Year == condition.Year);
            }

            if (condition.Month > 0)
            {
                posts = posts.Where(x => x.PostedDate.Month == condition.Month);
            }

            if (!string.IsNullOrWhiteSpace(condition.TitleSlug))
            {
                posts = posts.Where(x => x.UrlSlug == condition.TitleSlug);
            }

            return posts;   
        }

        //Lấy danh sách bài viết theo truy vấn
        public async Task<IList<Post>> GetPostsAsync(
        PostQuery condition,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
        {
            return await FilterPosts(condition)
                .OrderByDescending(x => x.PostedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        //Đếm số lượng bài viết thỏa mãn điều kiện tìm kiếm
        public async Task<int> CountPostsAsync(
        PostQuery condition, CancellationToken cancellationToken = default)
        {
            return await FilterPosts(condition).CountAsync(cancellationToken: cancellationToken);
        }

        //Tìm kiếm và phân trang bài viết
        public async Task<IPagedList<Post>> GetPagedPostsAsync(
            PostQuery condition,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            return await FilterPosts(condition).ToPagedListAsync(
                pageNumber, pageSize,
                nameof(Post.PostedDate), "DESC",
                cancellationToken);
        }

        //Lấy và phân trang danh sách chuyên mục
        public async Task<IPagedList<Category>> GetPagedCategoriesAsync(
           int pageNumber = 1,
           int pageSize = 10,
           CancellationToken cancellationToken = default)
        {
            //var pagingParams = new PagingParams
            //{
            //    PageNumber = 1, //Số thứ tự của trang
            //    PageSize = 6, //Số mẫu tin trong 1 trang
            //    SortColumn = "Name",
            //    SortOrder = "ASC"
            //};
            var categoryQuery = _context.Set<Category>()
                .Include(x => x.Posts);

            return await categoryQuery.ToPagedListAsync(
                pageNumber, pageSize,
                nameof(Category.Name), "DESC",
                cancellationToken);
        }

        //Lấy danh sách thẻ
        public async Task<Tag> GetTagAsync(
        string slug, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Tag>()
                .FirstOrDefaultAsync(x => x.UrlSlug == slug, cancellationToken);
        }

        //private string GenerateSlug(string text)
        //{
        //    var array = text.Trim().ToLower().Split(' ');
        //    return string.Join("-", array);
        //}

        //Tạo mới hoặc cập nhật bài viết theo id 
        public async Task<Post> CreateOrUpdatePostAsync(
        Post post, IEnumerable<string> tags,
        CancellationToken cancellationToken = default)
        {
            if (post.Id > 0)
            {
                await _context.Entry(post).Collection(x => x.Tags).LoadAsync(cancellationToken);
            }
            else
            {
                post.Tags = new List<Tag>();
            }

            var validTags = tags.Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => new
                {
                    Name = x,
                    Slug = x.GenerateSlug()
                })
                .GroupBy(x => x.Slug)
                .ToDictionary(g => g.Key, g => g.First().Name);


            foreach (var kv in validTags)
            {
                if (post.Tags.Any(x => string.Compare(x.UrlSlug, kv.Key, StringComparison.InvariantCultureIgnoreCase) == 0)) continue;

                var tag = await GetTagAsync(kv.Key, cancellationToken) ?? new Tag()
                {
                    Name = kv.Value,
                    Description = kv.Value,
                    UrlSlug = kv.Key
                };

                post.Tags.Add(tag);
            }

            post.Tags = post.Tags.Where(t => validTags.ContainsKey(t.UrlSlug)).ToList();

            if (post.Id > 0)
                _context.Update(post);
            else
                _context.Add(post);

            await _context.SaveChangesAsync(cancellationToken);

            return post;
        }

        public async Task DeletePostAsync(
                int id, CancellationToken cancellationToken = default)
        {
            var post = await _context.Set<Post>().FindAsync(id);
            _context.Set<Post>().Remove(post);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Set<Category>().FindAsync(categoryId);
        }

        public async Task<Category> CreateOrUpdateCategoryAsync(
            Category category, CancellationToken cancellationToken = default)
        {
            if (category.Id > 0)
            {
                _context.Set<Category>().Update(category);
            }
            else
            {
                _context.Set<Category>().Add(category);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return category;
        }

        public async Task DeleteCategoryAsync(
            int categoryId, CancellationToken cancellationToken = default)
        {
            var category = await _context.Set<Category>().FindAsync(categoryId);

            _context.Set<Category>().Remove(category);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IPagedList<Author>> GetPagedAuthorsAsync(
         int pageNumber = 1,
         int pageSize = 10,
         CancellationToken cancellationToken = default)
        {
            //var pagingParams = new PagingParams
            //{
            //    PageNumber = 1, //Số thứ tự của trang
            //    PageSize = 6, //Số mẫu tin trong 1 trang
            //    SortColumn = "Name",
            //    SortOrder = "ASC"
            //};
            var authorQuery = _context.Set<Author>()
                .Include(x => x.Posts);

            return await authorQuery.ToPagedListAsync(
                pageNumber, pageSize,
                nameof(Author.FullName), "ASC",
                cancellationToken);
        }

        public async Task<Author> GetAuthorByIdAsync(int authorId)
        {
            return await _context.Set<Author>().FindAsync(authorId);
        }

        public async Task<Author> CreateOrUpdateAuthorAsync(
          Author author, CancellationToken cancellationToken = default)
        {
            if (author.Id > 0)
            {
                _context.Set<Author>().Update(author);
            }
            else
            {
                _context.Set<Author>().Add(author);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return author;
        }

        public async Task DeleteAuthorAsync(
            int authorId, CancellationToken cancellationToken = default)
        {
            var author = await _context.Set<Author>().FindAsync(authorId);

            _context.Set<Author>().Remove(author);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IPagedList<Tag>> GetPagedTagsAsync(
           int pageNumber = 1,
           int pageSize = 10,
           CancellationToken cancellationToken = default)
        {
            //var pagingParams = new PagingParams
            //{
            //    PageNumber = 1, //Số thứ tự của trang
            //    PageSize = 6, //Số mẫu tin trong 1 trang
            //    SortColumn = "Name",
            //    SortOrder = "ASC"
            //};
            var tagQuery = _context.Set<Tag>()
                .Include(x => x.Posts);

            return await tagQuery.ToPagedListAsync(
                pageNumber, pageSize,
                nameof(Tag.Name), "ASC",
                cancellationToken);
        }

        public async Task<Tag> GetTagByIdAsync(int tagId)
        {
            return await _context.Set<Tag>().FindAsync(tagId);
        }

        public async Task<Tag> CreateOrUpdateTagAsync(
          Tag tag, CancellationToken cancellationToken = default)
        {
            if (tag.Id > 0)
            {
                _context.Set<Tag>().Update(tag);
            }
            else
            {
                _context.Set<Tag>().Add(tag);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return tag;
        }

        public async Task DeleteTagAsync(
         int tagId, CancellationToken cancellationToken = default)
        {
            var tag = await _context.Set<Tag>().FindAsync(tagId);

            _context.Set<Tag>().Remove(tag);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
