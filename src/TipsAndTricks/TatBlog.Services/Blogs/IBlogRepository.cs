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
        public Task<Post> GetPostAsync(
            int year,
            int month,
            string slug,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Tìm top n bài viết được nhiều người xem nhất
        public Task<IList<Post>> GetPopularArticleAsync(
            int numPosts,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Kiểm tra tên định danh bài viết đã có hay chưa
        public Task<bool> IPostSlugExistedAsync(
            int postId,
            string slug,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Tăng số lượt xem của một bài viết
        public Task IncreaseViewCountAsync(
            int postId,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Lấy danh sách chuyên mục và số lượng bài viết nằm từng chuyên mục/chủ đề
        public Task<IList<CategoryItem>> GetCategoriesAsync(
            bool showOnMenu = false,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Lấy danh sách các từ khóa/thẻ và phân trang theo các tham số pagingParams
        public Task<IPagedList<TagItem>> GetPagedTagsAsync(
            IPagingParams pagingParams,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Lấy danh sách các thẻ kèm theo số bài viết chứa thẻ đó
        public Task<IList<TagItem>> GetTagsListAsync(
           CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Lấy và phân trang danh sách chuyên mục
        public Task<IPagedList<CategoryItem>> GetPagedCategoriesAsync(
           IPagingParams pagingParams,
           CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //k.Lấy danh sách ngày theo N tháng tính từ tháng hiện tại
        public List<DateTime> GetDateListByMonth(int num)
        {
            throw new NotImplementedException();
        }

        //k.Đếm số lượng bài viết trong N tháng gần nhất
        public List<NumberPostByMonth> GetNumberPostByMonthAsync(int numMonth)
        {
            throw new NotImplementedException();
        }

        //Thay đổi trạng thái Published của một bài viết
        public Task ChangePublishedStatusAsync(
            int postId,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Lọc bài viết theo điều kiện
        private IQueryable<Post> FilterPosts(PostQuery condition)
        {
            throw new NotImplementedException();
        }

        //Lấy danh sách bài viết theo truy vấn
        public Task<IList<Post>> GetPostsAsync(
        PostQuery condition,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Đếm số lượng bài viết thỏa mãn điều kiện tìm kiếm
        public Task<int> CountPostsAsync(
        PostQuery condition, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //Tìm kiếm và phân trang bài viết
        public Task<IPagedList<Post>> GetPagedPostsAsync(
            PostQuery condition,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
