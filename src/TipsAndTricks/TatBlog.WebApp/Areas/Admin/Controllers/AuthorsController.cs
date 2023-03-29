using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.WebApp.Areas.Admin.Models;
using TatBlog.WebApp.Validations;

namespace TatBlog.WebApp.Areas.Admin.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly IMediaManager _mediaManager;

        public AuthorsController(
           IBlogRepository blogRepository,
           IMediaManager mediaManager,
           IMapper mapper,
           IAuthorRepository authorRepository)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _mediaManager = mediaManager;
            _authorRepository = authorRepository;
        }

        public async Task<IActionResult> Index(
           AuthorFilterModel model,
           int pageNumber = 1,
           int pageSize = 5)
        {
            //var postQuery = new PostQuery()
            //{
            //    Keyword = model.Keyword,
            //    CategoryId = model.CategoryId,
            //    AuthorId = model.AuthorId,
            //    Year = model.Year,
            //    Month = model.Month
            //};

            //var postQuery = _mapper.Map<PostQuery>(model);


            ViewBag.AuthorsList = await _authorRepository
                .GetPagedAuthorsAsync(pageNumber, pageSize);

            //await PopulatePostFilterModelAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id = 0)
        {
            var author = id > 0
                ? await _authorRepository.GetAuthorByIdAsync(id)
                : null;

            var model = author == null
                ? new AuthorEditModel()
                : _mapper.Map<AuthorEditModel>(author);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AuthorEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var author = model.Id > 0
                ? await _authorRepository.GetAuthorByIdAsync(model.Id) : null;


            if (author == null)
            {
                author = _mapper.Map<Author>(model);
                author.Id = 0;
                author.JoinedDate = DateTime.Now;
            }
            else
            {
                _mapper.Map(model, author);
            }

            // Nếu người dùng có upload hình ảnh minh họa cho bài viết
            if (model.ImageFile?.Length > 0)
            {
                // Thì thực hiện lưu vào thư mục uploads
                var newImagePath = await _mediaManager.SaveFileAsync(model.ImageFile.OpenReadStream(), model.ImageFile.FileName, model.ImageFile.ContentType);

                // Nếu thành công, xóa hình ảnh cũ nếu có
                if (!string.IsNullOrEmpty(newImagePath))
                {
                    await _mediaManager.DeleteFileAsync(author.ImageUrl);
                    author.ImageUrl = newImagePath;
                }
            }

            await _authorRepository.CreateOrUpdateAuthorAsync(author);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorRepository.DeleteAuthorAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
