using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bloggy.Backend.AutoMapper;
using Bloggy.Backend.Exceptions;
using Bloggy.Backend.Models;
using Generics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bloggy.Backend.Controllers.v1
{
    [Route("api/v1/blog-articles")]
    [ApiController]
    public class BlogArticlesController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly BlogArticleMapping _blogArticleMapping;
        private readonly ILogger<BlogArticlesController> _logger;

        public BlogArticlesController(ILogger<BlogArticlesController> logger, IBlogService blogService, BlogArticleMapping blogArticleMapping)
        {
            _blogService = blogService;
            _logger = logger;
            _blogArticleMapping = blogArticleMapping;
        }

        [HttpGet]
        public IActionResult ReadAll()
        {
            var items = _blogService.GetBlogArticles();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult Read(string id)
        {
            var items = _blogService.GetBlogArticles();
            return Ok(items);
        }

        [HttpPost]
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