namespace TatBlog.WebApi.Models
{
    public class AuthorEditModel
    {
        public string FullName { get; set; }

        public string UrlSlug { get; set; }

        public DateOnly JoinedDate { get; set; }

        public string Email { get; set; }

        public string Notes { get; set; }
    }
}
