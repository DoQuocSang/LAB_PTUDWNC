using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.WebApp.Areas.Admin.Models;

namespace TatBlog.WebApp.Areas.Admin.Controllers
{
    public class TagsController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public TagsController(
           IBlogRepository blogRepository,
           IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(
           TagFilterModel model,
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


            ViewBag.TagsList = await _blogRepository
                .GetPagedTagsAsync(pageNumber, pageSize);

            //await PopulatePostFilterModelAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id = 0)
        {
            var tag = id > 0
                ? await _blogRepository.GetTagByIdAsync(id)
                : null;

            var model = tag == null
                ? new TagEditModel()
                : _mapper.Map<TagEditModel>(tag);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TagEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var tag = model.Id > 0
                ? await _blogRepository.GetTagByIdAsync(model.Id) : null;

            if (tag == null)
            {
                tag = _mapper.Map<Tag>(model);
                tag.Id = 0;
            }
            else
            {
                _mapper.Map(model, tag);
            }

            await _blogRepository.CreateOrUpdateTagAsync(tag);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteTag(int id)
        {
            await _blogRepository.DeleteTagAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
