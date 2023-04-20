using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Data.Seeders;
using TatBlog.Services.Blogs;
using TatBlog.WinApp;
using static System.Net.Mime.MediaTypeNames;

namespace TatBlog.WinApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Test danh sách tác giả
            //==================================================
            var context = new BlogDbContext();

            var seeder = new DataSeeder(context);

            seeder.Initialize();

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
            //    PageNumber = 1, //Số thứ tự của trang
            //    PageSize = 5, //Số mẫu tin trong 1 trang
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
            //var context = new BlogDbContext();

            //IBlogRepository blogRepo = new BlogRepository(context);

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

            //1g) Thêm một chuyên mục
            //==================================================
            //Console.WriteLine("Nhập thông tin chuyên mục cần thêm: ");
            ////Console.WriteLine("ID            : ");
            ////int tempId = Convert.ToInt32(Console.ReadLine());
            //Console.Write("Name          : ");
            //string tempName = Console.ReadLine().Trim();
            //Console.Write("UrlSlug       : ");
            //string tempSlug = Console.ReadLine().Trim();
            //Console.Write("Description   : ");
            //string tempDesc = Console.ReadLine().Trim();

            //context.Categories.Add(new Category() { Name = tempName, UrlSlug = tempSlug, Description = tempDesc, ShowOnMenu = true });
            //context.SaveChanges();
            //Console.WriteLine("=> Thêm chuyên mục thành công!\n");

            //var categories = context.Categories
            //    .OrderBy(c => c.Id)
            //    .Select(c => new
            //    {
            //        Id = c.Id,
            //        Name = c.Name,
            //        UrlSlug = c.UrlSlug,
            //        Description = c.Description,
            //        PostCount = c.Posts.Count()
            //    })
            //    .ToList();

            //Console.WriteLine("=> Danh sách hiện tại:");
            //foreach (var category in categories)
            //{
            //    Console.WriteLine("ID            : {0}", category.Id);
            //    Console.WriteLine("Name          : {0}", category.Name);
            //    Console.WriteLine("UrlSlug       : {0}", category.UrlSlug);
            //    Console.WriteLine("Description   : {0}", category.Description);
            //    Console.WriteLine("PostCount     : {0}", category.PostCount);
            //    Console.WriteLine("".PadRight(80, '-'));
            //}
            //==================================================

            //1g) Cập nhật một chuyên mục
            //==================================================
            //Console.Write("Nhập tên chuyên mục cần cập nhật: ");
            //var temp = Console.ReadLine().Trim();

            //var updateItem = context.Categories.SingleOrDefault(c => c.Name == temp);

            //bool updateFlag = false;
            //if (updateItem != null)
            //{
            //    Console.WriteLine("[1] Name         : " + updateItem.Name);
            //    Console.WriteLine("[2] UrlSlug      : " + updateItem.UrlSlug);
            //    Console.WriteLine("[3] Description  : " + updateItem.Description);
            //    Console.Write("Vui lòng nhập 1 số để chọn thuộc tính cần cập nhật (Nhập số 0 để thoát): ");
            //    int num = Convert.ToInt32(Console.ReadLine());

            //    switch (num)
            //    {
            //        case 1:
            //            Console.Write("Nhập Name: ");
            //            updateItem.Name = Console.ReadLine().Trim();
            //            updateFlag = true;
            //            break;
            //        case 2:
            //            Console.Write("Nhập UrlSlug: ");
            //            updateItem.UrlSlug = Console.ReadLine().Trim();
            //            updateFlag = true;
            //            break;
            //        case 3:
            //            Console.Write("Nhập Description: ");
            //            updateItem.Description = Console.ReadLine().Trim();
            //            updateFlag = true;
            //            break;
            //        default:
            //            Console.WriteLine("Bạn đã chọn không thay đổi!");
            //            break;
            //    }
            //    context.SaveChanges();
            //    if (updateFlag)
            //    {
            //        Console.WriteLine("=> Cập nhật chuyên mục thành công!");
            //        Console.WriteLine("=> Thông tin chuyên mục {0} sau khi cập nhật:", temp);
            //        Console.WriteLine("ID            : {0}", updateItem.Id);
            //        Console.WriteLine("Name          : {0}", updateItem.Name);
            //        Console.WriteLine("UrlSlug       : {0}", updateItem.UrlSlug);
            //        Console.WriteLine("Description   : {0}", updateItem.Description);
            //        Console.WriteLine("".PadRight(80, '-'));
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Không tìm thấy chuyên mục!");
            //}
            //==================================================

            //1h) Xóa một chuyên mục theo mã số cho trước
            //==================================================
            //Console.Write("Nhập mã của chuyên mục cần xóa: ");
            //int temp = Convert.ToInt32(Console.ReadLine());
            //var itemRemove = context.Categories.SingleOrDefault(x => x.Id == temp);
            //if (itemRemove != null)
            //{
            //    context.Categories.Remove(itemRemove);
            //    context.SaveChanges();
            //    Console.WriteLine("=> Xóa chuyên mục thành công!");
            //}
            //else
            //{
            //    Console.WriteLine("=> Không tìm thấy chuyên mục cần xóa!");
            //}

            //var tags = context.Categories
            //    .OrderBy(t => t.Id)
            //    .Select(t => new
            //    {
            //        Id = t.Id,
            //        Name = t.Name,
            //        UrlSlug = t.UrlSlug,
            //        Description = t.Description,
            //        PostCount = t.Posts.Count()
            //    })
            //.ToList();

            //Console.WriteLine("=> Danh sách chuyên mục hiện tại:");
            //foreach (var tag in tags)
            //{
            //    Console.WriteLine("ID            : {0}", tag.Id);
            //    Console.WriteLine("Name          : {0}", tag.Name);
            //    Console.WriteLine("UrlSlug       : {0}", tag.UrlSlug);
            //    Console.WriteLine("Description   : {0}", tag.Description);
            //    Console.WriteLine("PostCount     : {0}", tag.PostCount);
            //    Console.WriteLine("".PadRight(80, '-'));
            //}
            //==================================================

            //1i) Kiểm tra tên định danh (slug) của một chuyên mục đã tồn tại hay chưa
            //==================================================
            //Console.WriteLine("[Kiểm tra tên định danh (slug) của một chuyên mục đã tồn tại hay chưa]");
            //Console.Write("Nhập tên định danh chuyên mục: ");
            //var temp = Console.ReadLine().Trim();
            //var itemCheck = context.Categories.SingleOrDefault(x => x.UrlSlug == temp);
            //if (itemCheck != null)
            //{
            //    Console.WriteLine("=> Tên định danh {0} đã tồn tại!", temp);
            //}
            //else
            //{
            //    Console.WriteLine("=> Tên định danh {0} chưa tồn tại!", temp);
            //}
            //==================================================

            //j.Lấy và phân trang danh sách chuyên mục, kết quả trả về kiểu IPagedList<CategoryItem>.
            //==================================================
            //var pagingParams = new PagingParams
            //{
            //    PageNumber = 1, //Số thứ tự của trang
            //    PageSize = 6, //Số mẫu tin trong 1 trang
            //    SortColumn = "Name",
            //    SortOrder = "ASC"
            //};

            //var categoryList = await blogRepo.GetPagedCategoriesAsync(pagingParams);

            //Console.WriteLine("{0,-5}{1,-50}{2,10}",
            //    "ID", "Name", "Count");

            //foreach (var item in categoryList)
            //{
            //    Console.WriteLine("{0,-5}{1,-50}{2,10}",
            //        item.Id, item.Name, item.PostCount);
            //}
            //==================================================

            //k.Đếm số lượng bài viết trong N tháng gần nhất. N là tham số đầu vào.
            //Kết quả là một danh sách các đối tượng chứa các thông tin sau: Năm, Tháng, Số bài viết.
            //==================================================
            //Console.WriteLine("Đếm số lượng bài viết trong N tháng gần nhất");
            //Console.Write("Nhập số tháng: ");
            //int temp = Convert.ToInt32(Console.ReadLine());

            //List<NumberPostByMonth> resultList = blogRepo.GetNumberPostByMonthAsync(temp);

            //Console.WriteLine("{0,-20}{1,-20}{2,10}",
            //    "Year", "Month", "PostCount");

            //foreach (var item in resultList)
            //{
            //    Console.WriteLine("{0,-20}{1,-20}{2,10}",
            //        item.Year, item.Month, item.PostCount);
            //}
            //==================================================

            //l.Tìm một bài viết theo mã số.
            //==================================================
            //Console.Write("Nhập mã số của bài viết cần tìm: ");
            //int temp = Convert.ToInt32(Console.ReadLine().Trim());
            //var posts = context.Posts
            //    .Where(p => p.Id == temp)
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


            //if (posts.Count > 0)
            //{
            //    Console.WriteLine("=> Kết quả tìm thấy:");
            //    foreach (var post in posts)
            //    {
            //        Console.WriteLine("ID        : {0}", post.Id);
            //        Console.WriteLine("Title     : {0}", post.Title);
            //        Console.WriteLine("View      : {0}", post.ViewCount);
            //        Console.WriteLine("Date      : {0:MM/dd/yyyy}", post.PostedDate);
            //        Console.WriteLine("Author    : {0}", post.Author);
            //        Console.WriteLine("Category  : {0}", post.Category);
            //        Console.WriteLine("".PadRight(80, '-'));
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("=> Không tìm thấy bài viết cần tìm!");
            //}
            //==================================================

            //m.Thêm một bài viết.
            //==================================================
            //Console.WriteLine("Nhập thông tin bài viết cần thêm: ");
            //Console.Write("Title                : ");
            //string tempTitle = Console.ReadLine().Trim();
            //Console.Write("ShortDescription     : ");
            //string tempShortDesc = Console.ReadLine().Trim();
            //Console.Write("Description          : ");
            //string tempDesc = Console.ReadLine().Trim();
            //Console.Write("Meta                 : ");
            //string tempMeta = Console.ReadLine().Trim();
            //Console.Write("UrlSlug              : ");
            //string tempUrlSlug = Console.ReadLine().Trim();
            //Console.Write("Author               : ");
            //string tempAuthor = Console.ReadLine().Trim();
            //Console.Write("Category             : ");
            //string tempCategory = Console.ReadLine().Trim();
            //Console.Write("Tags                 : ");
            //string tempTags = Console.ReadLine().Trim();
            //==================================================

            //n.Chuyển đổi trạng thái Published của bài viết. 
            //==================================================
            //Console.Write("Nhập mã số của bài viết cần thay đổi trạng thái Published: ");
            //int temp = Convert.ToInt32(Console.ReadLine().Trim());
            //var post = context.Set<Post>()
            //    .Include(x => x.Author)
            //    .Include(x => x.Category)
            //    .FirstOrDefault(p => p.Id == temp);

            //if (post != null)
            //{
            //    Console.WriteLine("=> Kết quả tìm thấy:");
            //    Console.WriteLine("ID        : {0}", post.Id);
            //    Console.WriteLine("Title     : {0}", post.Title);
            //    Console.WriteLine("Date      : {0:MM/dd/yyyy}", post.PostedDate);
            //    Console.WriteLine("Author    : {0}", post.Author.FullName);
            //    Console.WriteLine("Category  : {0}", post.Category.Name);
            //    Console.WriteLine("Published  : {0}", post.Published);
            //    Console.WriteLine("".PadRight(80, '-'));

            //    Console.WriteLine("=> Bạn có chắc chắn là muốn thay đổi trạng thái Published?");
            //    Console.Write("=> ");

            //    var answer = Console.ReadLine().Trim();
            //    if (string.Equals(answer, "co", StringComparison.OrdinalIgnoreCase))
            //    {
            //        post.Published = !post.Published;
            //        context.SaveChanges();
            //        Console.WriteLine("=> Thông tin bài viết sau khi thay đổi:");
            //        Console.WriteLine("ID        : {0}", post.Id);
            //        Console.WriteLine("Title     : {0}", post.Title);
            //        Console.WriteLine("Date      : {0:MM/dd/yyyy}", post.PostedDate);
            //        Console.WriteLine("Author    : {0}", post.Author.FullName);
            //        Console.WriteLine("Category  : {0}", post.Category.Name);
            //        Console.WriteLine("Published  : {0}", post.Published);
            //        Console.WriteLine("".PadRight(80, '-'));
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("=> Không tìm thấy bài viết cần tìm!");
            //}
            //==================================================

            //o.Lấy ngẫu nhiên N bài viết. N là tham số đầu vào. 
            //==================================================
            //Console.WriteLine("Lấy ngẫu nhiên N bài viết");
            //Console.Write("Nhập số bài viết: ");
            //int temp = Convert.ToInt32(Console.ReadLine().Trim());

            //var posts = context.Posts
            //    .Where(p => p.Published)
            //    .OrderBy(r => Guid.NewGuid())
            //    .Select(p => new
            //    {
            //        Id = p.Id,
            //        Title = p.Title,
            //        ViewCount = p.ViewCount,
            //        PostedDate = p.PostedDate,
            //        Author = p.Author.FullName,
            //        Category = p.Category.Name
            //    })
            //    .Take(temp)
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
            ////==================================================
        }
    }
}

