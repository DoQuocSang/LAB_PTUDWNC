using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;

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
    }
}
