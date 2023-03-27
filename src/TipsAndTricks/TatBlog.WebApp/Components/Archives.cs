﻿using Microsoft.AspNetCore.Mvc;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Components
{
    public class Archives : ViewComponent
    {
        private readonly IBlogRepository _blogRepository;

        public Archives(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
          
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _blogRepository.CountMonthlyPostsAsync();
            return View(categories);
        }
    }
}
