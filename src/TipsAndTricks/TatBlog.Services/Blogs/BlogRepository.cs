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
        public async Task<bool> IPostSlugExistedAsync(
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

        //Thay đổi trạng thái Published của một bài viết
        public async Task ChangePublishedStatusAsync(
            int postId,
            CancellationToken cancellationToken = default)
        {
            await _context.Set<Post>()
                .Where(x => x.Id == postId)
                .ExecuteUpdateAsync(p =>
                p.SetProperty(x => x.Published, x => !x.Published),
                cancellationToken);
        }
    }
}
