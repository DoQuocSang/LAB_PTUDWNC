using FluentValidation;
using FluentValidation.AspNetCore;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Printing;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.WebApp.Areas.Admin.Models;
using TatBlog.WebApp.Validations;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace TatBlog.WebApp.Areas.Admin.Controllers
{
    public class PostsController : Controller
    {
        private readonly ILogger<PostsController> _logger;
        private readonly IBlogRepository _blogRepository;
        private readonly IMediaManager _mediaManager;
        private readonly IMapper _mapper;
        private readonly IValidator<PostEditModel> _postValidator;
        public PostsController(
            IBlogRepository blogRepository, 
            IMediaManager mediaManager,
            IMapper mapper,
            ILogger<PostsController> logger)
        {
            _blogRepository = blogRepository;
            _mediaManager = mediaManager;
            _mapper = mapper;
            _postValidator = new PostValidator(blogRepository);
            _logger = logger;
        }

        private async Task PopulatePostFilterModelAsync(PostFilterModel model)
        {
            var authors = await _blogRepository.GetAuthorsAsync();
            var categories = await _blogRepository.GetCategoriesAsync();

            model.AuthorList = authors.Select(a => new SelectListItem()
            {
                Text = a.FullName,
                Value = a.Id.ToString()
            });

            model.CategoryList = categories.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
        }

        private async Task PopulatePostEditModelAsync(PostEditModel model)
        {
            var authors = await _blogRepository.GetAuthorsAsync();
            var categories = await _blogRepository.GetCategoriesAsync();

            model.AuthorList = authors.Select(a => new SelectListItem()
            {
                Text = a.FullName,
                Value = a.Id.ToString()
            });

            model.CategoryList = categories.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
        }

        //[Area("Admin")]
        public async Task<IActionResult> Index(
            PostFilterModel model,
            int pageNumber = 1,
            int pageSize = 10)
        {
            //var postQuery = new PostQuery()
            //{
            //    Keyword = model.Keyword,
            //    CategoryId = model.CategoryId,
            //    AuthorId = model.AuthorId,
            //    Year = model.Year,
            //    Month = model.Month
            //};
            _logger.LogInformation("Tạo điều kiện truy vấn");

            var postQuery = _mapper.Map<PostQuery>(model);

            _logger.LogInformation("Lấy danh sách bài viết từ CSDL");

            ViewBag.PostsList = await _blogRepository
                .GetPagedPostsAsync(postQuery, pageNumber, pageSize);

            _logger.LogInformation("Chuẩn bị dữ liệu cho ViewModel");

            await PopulatePostFilterModelAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id = 0)
        {
            var post = id > 0 
                ? await _blogRepository.GetPostByIdAsync(id, true)
                : null;

            var model = post == null
                ? new PostEditModel()
                : _mapper.Map<PostEditModel>(post);

            await PopulatePostEditModelAsync(model);

            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(
        //    IValidator<PostEditModel> postValidator,
        //    PostEditModel model)
        //{
        //    var validationResult = await postValidator.ValidateAsync(model);

        //    if (!validationResult.IsValid)
        //    {
        //        validationResult.AddToModelState(ModelState);
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        await PopulatePostEditModelAsync(model);
        //        return View(model);
        //    }

        //    var post = model.Id > 0
        //        ? await _blogRepository.GetPostByIdAsync(model.Id)
        //        : null;

        //    if (post == null)
        //    {
        //        post = _mapper.Map<Post>(model);

        //        post.Id = 0;
        //        post.PostedDate = DateTime.Now;
        //    }
        //    else
        //    {
        //        _mapper.Map(model, post);

        //        post.Category = null;
        //        post.ModifiedDate = DateTime.Now;
        //    }

        //    if (model.ImageFile?.Length > 0)
        //    {
        //        var newImagePath = await _mediaManager.SaveFileAsync(
        //            model.ImageFile.OpenReadStream(),
        //            model.ImageFile.FileName,
        //            model.ImageFile.ContentType);

        //        if (!string.IsNullOrWhiteSpace(newImagePath))
        //        {
        //            await _mediaManager.DeleteFileAsync(post.ImageUrl);
        //            post.ImageUrl = newImagePath;
        //        }
        //    }

        //    await _blogRepository.CreateOrUpdatePostAsync(
        //        post, model.GetSelectedTags());

        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public async Task<IActionResult> Edit(PostEditModel model)
        {
            var validationResult = await _postValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
            }

            if (!ModelState.IsValid)
            {
                await PopulatePostEditModelAsync(model);
                return View(model);
            }

            var post = model.Id > 0
                ? await _blogRepository.GetPostByIdAsync(model.Id) : null;


            if (post == null)
            {
                post = _mapper.Map<Post>(model);
                post.Id = 0;
                post.PostedDate = DateTime.Now;
            }
            else
            {
                _mapper.Map(model, post);
                post.Category = null;
                post.ModifiedDate = DateTime.Now;
            }

            // Nếu người dùng có upload hình ảnh minh họa cho bài viết
            if (model.ImageFile?.Length > 0)
            {
                // Thì thực hiện lưu vào thư mục uploads
                var newImagePath = await _mediaManager.SaveFileAsync(model.ImageFile.OpenReadStream(), model.ImageFile.FileName, model.ImageFile.ContentType);

                // Nếu thành công, xóa hình ảnh cũ nếu có
                if (!string.IsNullOrEmpty(newImagePath))
                {
                    await _mediaManager.DeleteFileAsync(post.ImageUrl);
                    post.ImageUrl = newImagePath;
                }
            }

            await _blogRepository.CreateOrUpdatePostAsync(post, model.GetSelectedTags());

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> VerifyPostSlug(
            int id, string urlSlug)
        {
            var slugExisted = await _blogRepository
                .IsPostSlugExistedAsync(id, urlSlug);

            return slugExisted
                ? Json("$Slug '{urlSlug}' đã được sử dụng")
                :Json(true);
        }

        public async Task<IActionResult> ChangePostPublishedState(int id)
        {
            await _blogRepository.TogglePublishedFlagAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeletePost(int id)
        {
            await _blogRepository.DeletePostAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
