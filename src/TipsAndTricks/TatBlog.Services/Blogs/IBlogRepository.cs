using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Services.Extensions;

namespace TatBlog.Services.Blogs
{
    public interface IBlogRepository
    {
        //Tìm bài viết có định danh slug và được đăng vào tháng month năm year
        Task<Post> GetPostAsync(
            int year,
            int month,
            int day,
            string slug,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Tìm top n bài viết được nhiều người xem nhất
        Task<IList<Post>> GetPopularArticleAsync(
            int numPosts,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Kiểm tra tên định danh bài viết đã có hay chưa
        Task<bool> IsPostSlugExistedAsync(
            int postId,
            string slug,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Tăng số lượt xem của một bài viết
        Task IncreaseViewCountAsync(
            int postId,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        ////Lấy danh sách chuyên mục và số lượng bài viết nằm từng chuyên mục/chủ đề
        //Task<IList<CategoryItem>> GetCategoriesAsync(
        //    bool showOnMenu = false,
        //    CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        //Lấy danh sách các từ khóa/thẻ và phân trang theo các tham số pagingParams
        Task<IPagedList<TagItem>> GetPagedTagsAsync(
            IPagingParams pagingParams,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Lấy danh sách các thẻ kèm theo số bài viết chứa thẻ đó
        Task<IList<TagItem>> GetTagsListAsync(
           CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Lấy và phân trang danh sách chuyên mục
        //Task<IPagedList<CategoryItem>> GetPagedCategoriesAsync(
        //   IPagingParams pagingParams,
        //   CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        //k.Lấy danh sách ngày theo N tháng tính từ tháng hiện tại
        List<DateTime> GetDateListByMonth(int num)
        {
            throw new NotImplementedException();
        }

        //k.Đếm số lượng bài viết trong N tháng gần nhất
        List<MonthlyPostCountItem> GetNumberPostByMonthAsync(int numMonth)
        {
            throw new NotImplementedException();
        }

        //Tìm bài viết theo id
        Task<Post> GetPostByIdAsync(
            int postId, bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        ////Thay đổi trạng thái Published của một bài viết
        //Task ChangePublishedStatusAsync(
        //    int postId,
        //    CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        //Lọc bài viết theo điều kiện
        IQueryable<Post> FilterPosts(PostQuery condition)
        {
            throw new NotImplementedException();
        }

        //Lấy danh sách chuyên mục
        //Task<IList<AuthorItem>> GetAuthorsAsync(CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        //Lấy danh sách bài viết theo truy vấn
        Task<IList<Post>> GetPostsAsync(
        PostQuery condition,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Đếm số lượng bài viết thỏa mãn điều kiện tìm kiếm
        Task<int> CountPostsAsync(
        PostQuery condition, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Tìm kiếm và phân trang bài viết
        Task<IPagedList<Post>> GetPagedPostsAsync(
            PostQuery condition,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Lấy danh sách thẻ
        Task<Tag> GetTagAsync(
        string slug, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //private string GenerateSlug(string text)
        //{
        //    throw new NotImplementedException();

        //}

        //Tạo mới hoặc cập nhật bài viết theo id 
        Task<Post> CreateOrUpdatePostAsync(
        Post post, IEnumerable<string> tags,
        CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Thay đổi trạng thái published của 1 bài viết
        Task TogglePublishedFlagAsync(
             int postId,
             CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        Task DeletePostAsync(
                int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Task<IPagedList<Category>> GetPagedCategoriesAsync(
        //   int pageNumber = 1,
        //   int pageSize = 10,
        //   CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<Category> GetCategoryByIdAsync(int categoryId)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<Category> CreateOrUpdateCategoryAsync(
        //   Category category, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        //Task DeleteCategoryAsync(
        //  int categoryId, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        Task ToggleShowOnMenuFlagAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Task<IPagedList<Author>> GetPagedAuthorsAsync(
        // int pageNumber = 1,
        // int pageSize = 10,
        // CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<Author> GetAuthorByIdAsync(int authorId)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<Author> CreateOrUpdateAuthorAsync(
        // Author author, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        //Task DeleteAuthorAsync(
        //   int authorId, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        Task<IPagedList<Tag>> GetPagedTagsAsync(
          int pageNumber = 1,
          int pageSize = 10,
          CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        Task<Tag> GetTagByIdAsync(int tagId)
        {
            throw new NotImplementedException();
        }

        Task<Tag> CreateOrUpdateTagAsync(
         Tag tag, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        Task DeleteTagAsync(
         int tagId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        Task<IList<TagItem>> GetTagsAsync(
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        Task<IList<Post>> GetRandomArticlesAsync(
            int numPosts, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Task<IList<Author>> GetPopularAuthorAsync(
        //   int numAuthors,
        //   CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        Task<IList<MonthlyPostCountItem>> CountMonthlyPostsAsync(
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        Task<IPagedList<Post>> GetPostAsync(
            int year,
            int month,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        Task<IPagedList<T>> GetPagedPostsAsync<T>(
           PostQuery condition,
           IPagingParams pagingParams,
           Func<IQueryable<Post>, IQueryable<T>> mapper);

        Task<IPagedList<PostItem>> GetPagedPostsAsync(
             IPagingParams pagingParams,
             string name = null,
             CancellationToken cancellationToken = default);

        Task<Post> GetCachedPostByIdAsync(int postId);

        Task<IPagedList<Post>> GetRandomPostsAsync(
          int numPosts,
          int pageSize = 30,
          int pageNumber = 1,
          CancellationToken cancellationToken = default);

        Task<IPagedList<PostItem>> GetPopularPostsAsync(
          int numPosts,
          int pageSize = 30,
          int pageNumber = 1,
        CancellationToken cancellationToken = default);
    }
}
