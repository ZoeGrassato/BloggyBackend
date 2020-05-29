using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggyBackend.AutoMapper;
using BloggyBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace BloggyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogArticleController : ControllerBase
    {
        private readonly BlogService _blogService;
        private readonly BlogArticleMapping _blogArticleMapping;
        public BlogArticleController()
        {
            _blogService = new BlogService();
        }

        [HttpGet("Read")]
        public IActionResult Read()
        {
            var items = _blogService.GetBlogArticles();
            return Ok(items);
        }

        [HttpPost("Create")]
        public IActionResult Create(BlogArticleViewModel blogArticleViewModel)
        {
            var mappedItem = _blogArticleMapping.MapToBlogArticle(blogArticleViewModel);
            _blogService.Add(mappedItem);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(BlogArticleViewModel blogArticleViewModel)
        {
            return Ok();
        }
    }
}