using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TatBlog.WebApp.Areas.Admin.Models
{
    public class CategoryEditModel
    {
        public int Id { get; set; }

        [DisplayName("Tên chủ đề")]
        //[Required(ErrorMessage = "Tiêu đề không được để trống")]
        //[MaxLength(500, ErrorMessage = "Tiêu đề tối đa 500 ký tự")]
        public string Name { get; set; }

        [DisplayName("Mô tả")]
        //[Required(ErrorMessage = "Nội dung không được để trống")]
        //[MaxLength(5000, ErrorMessage = "Nội dung tối đa 5000 ký tự")]
        public string Description { get; set; }

        [DisplayName("Slug")]
        [Remote("VerifyPostSlug", "Posts", "Admin",
            HttpMethod = "POST", AdditionalFields = "Id")]
        //[Required(ErrorMessage = "URL slug không được để trống")]
        //[MaxLength(200, ErrorMessage = "Slug tối đa 200 ký tự")]
        public string UrlSlug { get; set; }

        [DisplayName("Hiển thị trên Menu")]
        public bool ShowOnMenu { get; set; }
    }
}
