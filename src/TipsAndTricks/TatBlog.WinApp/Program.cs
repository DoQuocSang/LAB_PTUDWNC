using Azure;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Data.Seeders;
using TatBlog.Services.Blogs;
using TatBlog.WinApp;

namespace TatBlog.WinApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Test danh sách tác giả
            //==================================================
            //var context = new BlogDbContext();

            //var seeder = new DataSeeder(context);

            //seeder.Initialize();

            //var authors = context.Authors.ToList();

            //Console.WriteLine("{0,-4}{1,-30}{2,-30}{3,12}",
            //    "ID", "Full Name", "Email", "Joined Date");

            //foreach (var author in authors) 
            //{
            //    Console.WriteLine("{0,-4}{1,-30}{2,-30}{3,12:MM/dd/yyyy}",
            //        author.Id, author.FullName, author.Email, author.JoinedDate);
            //}
            //==================================================

            //Hiển thị bài viết
            //==================================================
            //var context = new BlogDbContext();

            //var posts = context.Posts
            //    .Where(p => p.Published)
            //    .OrderBy(p => p.Title)
            //    .Select(p => new
            //    {
            //        Id = p.Id,
            //        Title = p.Title,
            //        ViewCount = p.ViewCount,
            //        PostedDate = p.PostedDate,
            //        Author = p.Author.FullName,
            //        Category = p.Category.Name
            //    })
            //    .ToList();

            //foreach (var post in posts)
            //{
            //    Console.WriteLine("ID        : {0}", post.Id);
            //    Console.WriteLine("Title     : {0}", post.Title);
            //    Console.WriteLine("View      : {0}", post.ViewCount);
            //    Console.WriteLine("Date      : {0:MM/dd/yyyy}", post.PostedDate);
            //    Console.WriteLine("Author    : {0}", post.Author);
            //    Console.WriteLine("Category  : {0}", post.Category);
            //    Console.WriteLine("".PadRight(80, '-'));
            //}
            //==================================================

            //Tìm 3 bài viết được xem nhiều nhất
            //==================================================
            //var context = new BlogDbContext();

            //IBlogRepository blogRepo = new BlogRepository(context);

            //var posts = await blogRepo.GetPopularArticleAsync(3);

            //foreach (var post in posts)
            //{
            //    Console.WriteLine("ID        : {0}", post.Id);
            //    Console.WriteLine("Title     : {0}", post.Title);
            //    Console.WriteLine("View      : {0}", post.ViewCount);
            //    Console.WriteLine("Date      : {0:MM/dd/yyyy}", post.PostedDate);
            //    Console.WriteLine("Author    : {0}", post.Author);
            //    Console.WriteLine("Category  : {0}", post.Category);
            //    Console.WriteLine("".PadRight(80, '-'));
            //}
            //==================================================

            //Tìm 3 bài viết được xem nhiều nhất
            //==================================================
            //var context = new BlogDbContext();

            //IBlogRepository blogRepo = new BlogRepository(context);

            //var categories = await blogRepo.GetCategoriesAsync();

            //Console.WriteLine("{0,-5}{1,-50}{2,10}",
            //    "ID", "Name", "Count");

            //foreach (var item in categories)
            //{
            //    Console.WriteLine("{0,-5}{1,-50}{2,10}",
            //        item.Id, item.Name, item.PostCount);
            //}
            //==================================================


            //Phân trang
            //==================================================
            //var context = new BlogDbContext();

            //IBlogRepository blogRepo = new BlogRepository(context);

            //var pagingParams = new PagingParams
            //{
            //    PageNumber = 1,
            //    PageSize = 5,
            //    SortColumn = "Name",
            //    SortOrder = "DESC"
            //};

            //var tagList = await blogRepo.GetPagedTagsAsync(pagingParams);

            //Console.WriteLine("{0,-5}{1,-50}{2,10}",
            //    "ID", "Name", "Count");

            //foreach (var item in tagList)
            //{
            //    Console.WriteLine("{0,-5}{1,-50}{2,10}",
            //        item.Id, item.Name, item.PostCount);
            //}
            //==================================================


            //Bài tập==========================================
            Console.OutputEncoding = Encoding.Unicode;
            var context = new BlogDbContext();

            IBlogRepository blogRepo = new BlogRepository(context);

            //1a) Tìm một thẻ (Tag) theo tên định danh (slug) 
            //==================================================
            //Console.Write("Nhập tên định danh của thẻ cần tìm: ");
            //string temp = Console.ReadLine().Trim();
            //var tags = context.Tags
            //    .Where(t => t.UrlSlug == temp)
            //    .OrderBy(t => t.Name)
            //    .Select(t => new
            //    {
            //        Id = t.Id,
            //        Name = t.Name,
            //        UrlSlug = t.UrlSlug,
            //        Description = t.Description
            //    })
            //    .ToList();


            //if(tags.Count > 0)
            //{
            //    Console.WriteLine("=> Kết quả tìm thấy:");
            //    foreach (var tag in tags)
            //    {
            //        Console.WriteLine("ID            : {0}", tag.Id);
            //        Console.WriteLine("Name          : {0}", tag.Name);
            //        Console.WriteLine("UrlSlug       : {0}", tag.UrlSlug);
            //        Console.WriteLine("Description   : {0}", tag.Description);
            //        Console.WriteLine("".PadRight(80, '-'));
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("=> Không tìm thấy thẻ cần tìm!");
            //}

            //==================================================

            //1c) Lấy danh sách tất cả các thẻ (Tag) kèm theo số bài viết chứa thẻ đó. Kết quả trả về kiểu IList<TagItem>
            //==================================================
            //Console.WriteLine("Lấy danh sách tất cả các thẻ (Tag) kèm theo số bài viết chứa thẻ đó:");
            //var tagList = await blogRepo.GetTagsListAsync();
            //var tags = context.Tags
            //    .OrderBy(t => t.Name)
            //    .Select(t => new
            //    {
            //        Id = t.Id,
            //        Name = t.Name,
            //        UrlSlug = t.UrlSlug,
            //        Description = t.Description,
            //        PostCount = t.Posts.Count()
            //    })
            //.ToList();

            //if (tagList.Count > 0)
            //{
            //    foreach (var tag in tagList)
            //    {
            //        Console.WriteLine("ID            : {0}", tag.Id);
            //        Console.WriteLine("Name          : {0}", tag.Name);
            //        Console.WriteLine("UrlSlug       : {0}", tag.UrlSlug);
            //        Console.WriteLine("Description   : {0}", tag.Description);
            //        Console.WriteLine("PostCount     : {0}", tag.PostCount);
            //        Console.WriteLine("".PadRight(80, '-'));
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("=> Không tìm thấy thẻ cần tìm!");
            //}

            //==================================================

            //1d) Xóa một thẻ theo mã cho trước
            //==================================================
            //Console.Write("Nhập mã của thẻ cần xóa: ");
            //int temp = Convert.ToInt32(Console.ReadLine());
            //var itemRemove = context.Tags.SingleOrDefault(x => x.Id == temp);
            //if(itemRemove != null)
            //{
            //    context.Tags.Remove(itemRemove);
            //    context.SaveChanges();
            //    Console.WriteLine("=> Xóa thẻ thành công!");
            //}
            //else
            //{
            //    Console.WriteLine("=> Không tìm thấy thẻ cần xóa!");
            //}

            //var tags = context.Tags
            //    .OrderBy(t => t.Name)
            //    .Select(t => new
            //    {
            //        Id = t.Id,
            //        Name = t.Name,
            //        UrlSlug = t.UrlSlug,
            //        Description = t.Description,
            //        PostCount = t.Posts.Count()
            //    })
            //.ToList();

            //if (tags.Count > 0)
            //{
            //    Console.WriteLine("=> Danh sách thẻ hiện tại:");
            //    foreach (var tag in tags)
            //    {
            //        Console.WriteLine("ID            : {0}", tag.Id);
            //        Console.WriteLine("Name          : {0}", tag.Name);
            //        Console.WriteLine("UrlSlug       : {0}", tag.UrlSlug);
            //        Console.WriteLine("Description   : {0}", tag.Description);
            //        Console.WriteLine("PostCount     : {0}", tag.PostCount);
            //        Console.WriteLine("".PadRight(80, '-'));
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("=> Không tìm thấy thẻ cần tìm!");
            //}
            //==================================================

            //1e) Tìm một chuyên mục (Category) theo tên định danh (slug) 
            //==================================================
            //Console.Write("Nhập tên định danh của chuyên mục cần tìm: ");
            //string temp = Console.ReadLine().Trim();
            //var categories = context.Categories
            //    .Where(c => c.UrlSlug == temp)
            //    .OrderBy(c => c.Name)
            //    .Select(c => new
            //    {
            //        Id = c.Id,
            //        Name = c.Name,
            //        UrlSlug = c.UrlSlug,
            //        Description = c.Description,
            //        PostCount = c.Posts.Count()
            //    })
            //    .ToList();


            //if (categories.Count > 0)
            //{
            //    Console.WriteLine("=> Kết quả tìm thấy:");
            //    foreach (var category in categories)
            //    {
            //        Console.WriteLine("ID            : {0}", category.Id);
            //        Console.WriteLine("Name          : {0}", category.Name);
            //        Console.WriteLine("UrlSlug       : {0}", category.UrlSlug);
            //        Console.WriteLine("Description   : {0}", category.Description);
            //        Console.WriteLine("PostCount     : {0}", category.PostCount);
            //        Console.WriteLine("".PadRight(80, '-'));
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("=> Không tìm thấy chuyên mục cần tìm!");
            //}
            //==================================================

            //1f) Tìm một chuyên mục theo mã số cho trước. 
            //==================================================
            //Console.Write("Nhập mã số của chuyên mục cần tìm: ");
            //int temp = Convert.ToInt32(Console.ReadLine());
            //var categories = context.Categories
            //    .Where(c => c.Id == temp)
            //    .OrderBy(c => c.Name)
            //    .Select(c => new
            //    {
            //        Id = c.Id,
            //        Name = c.Name,
            //        UrlSlug = c.UrlSlug,
            //        Description = c.Description,
            //        PostCount = c.Posts.Count()
            //    })
            //    .ToList();


            //if (categories.Count > 0)
            //{
            //    Console.WriteLine("=> Kết quả tìm thấy:");
            //    foreach (var category in categories)
            //    {
            //        Console.WriteLine("ID            : {0}", category.Id);
            //        Console.WriteLine("Name          : {0}", category.Name);
            //        Console.WriteLine("UrlSlug       : {0}", category.UrlSlug);
            //        Console.WriteLine("Description   : {0}", category.Description);
            //        Console.WriteLine("PostCount     : {0}", category.PostCount);
            //        Console.WriteLine("".PadRight(80, '-'));
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("=> Không tìm thấy chuyên mục cần tìm!");
            //}
            //==================================================

            //1g) Thêm một chuyên mục/chủ đề 
            //==================================================
            //Console.Write("Nhập thông tin chuyên mục cần thêm: ");
            //Console.WriteLine("ID            : ");
            //Console.WriteLine("Name          : ");
            //Console.WriteLine("UrlSlug       : ");
            //Console.WriteLine("Description   : ");

            //int temp = Convert.ToInt32(Console.ReadLine());
            //var categories = context.Categories
            //    .Where(c => c.Id == temp)
            //    .OrderBy(c => c.Name)
            //    .Select(c => new
            //    {
            //        Id = c.Id,
            //        Name = c.Name,
            //        UrlSlug = c.UrlSlug,
            //        Description = c.Description,
            //        PostCount = c.Posts.Count()
            //    })
            //    .ToList();


            //if (categories.Count > 0)
            //{
            //    Console.WriteLine("=> Kết quả tìm thấy:");
            //    foreach (var category in categories)
            //    {
            //        Console.WriteLine("ID            : {0}", category.Id);
            //        Console.WriteLine("Name          : {0}", category.Name);
            //        Console.WriteLine("UrlSlug       : {0}", category.UrlSlug);
            //        Console.WriteLine("Description   : {0}", category.Description);
            //        Console.WriteLine("PostCount     : {0}", category.PostCount);
            //        Console.WriteLine("".PadRight(80, '-'));
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("=> Không tìm thấy chuyên mục cần tìm!");
            //}
            //==================================================
        }
    }
}