using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggyBackend.Models;
using Generics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloggyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogArticleController : ControllerBase
    {
        private IBlogService _blogService;
        private BlogArticleAutoMapper _autoMapperLogic;

        public BlogArticleController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public IActionResult Read()
        {
            var items = _blogService.GetBlogArticles();
            return Ok(items);
        }

        [HttpPost]
        public IActionResult Create(BlogArticleViewModel blogArticleViewModel)
        {
            var mappedItem = 
            _blogService.Add();
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(BlogArticleViewModel blogArticleViewModel)
        {
            return Ok();
        }
    }
}