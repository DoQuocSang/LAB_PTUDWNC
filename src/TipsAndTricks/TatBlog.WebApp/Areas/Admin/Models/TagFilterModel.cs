using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Globalization;

namespace TatBlog.WebApp.Areas.Admin.Models
{
    public class TagFilterModel
    {
        [DisplayName("Từ khóa")]
        public string Keyword { get; set; }
    }
}
