using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggyBackend.AutoMapper;
using BloggyBackend.Models;
using Generics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;

namespace BloggyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogArticleController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly BlogArticleMapping _blogArticleMapping;
        private readonly ILogger<BlogArticleController> _logger;

        public BlogArticleController(ILogger<BlogArticleController> logger, IBlogService blogService, BlogArticleMapping blogArticleMapping)
        {
            _blogService = blogService;
            _logger = logger;
            _blogArticleMapping = blogArticleMapping;
        }

        [HttpGet("Read")]
        public IActionResult Read()
        {
            var items = _blogService.GetBlogArticles();

            if (items == null || items.Count < 1)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpPost("Create")]
        public IActionResult Create(BlogArticleViewModel blogArticleViewModel)
        {
            if (!blogArticleViewModel.Validate())
            {
                throw new BloggyException("Requires at least one section and one title");
            }

            var mappedItem = _blogArticleMapping.MapToBlogArticle(blogArticleViewModel);
            _blogService.Add(mappedItem);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(BlogArticleViewModel blogArticleViewModel)
        {
            if (!blogArticleViewModel.Validate())
            {
                throw new BloggyException("Requires at least one section and one title");
            }
            return Ok();
        }
    }
}