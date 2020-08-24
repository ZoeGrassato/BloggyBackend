using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bloggy.Backend.AutoMapper;
using Bloggy.Backend.Exceptions;
using Bloggy.Backend.Models;
using Generics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace Bloggy.Backend.Controllers.v1
{
    [Route("api/v1/blog-articles")]
    [Produces("application/json")]
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
            if(items.BlogArticles == null)
            {
                throw new BloggyException("Items cannot be null");
            }
            return Ok(items);
        }

        [HttpGet("{id}")]
        public HttpResponseMessage Read(string id)
        {
            var items = _blogService.GetBlogArticles();
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");
            return response;
        }

        [HttpPost]
        public IActionResult Create(BlogArticleTransferObj blogArticleViewModel)
        {
            if (blogArticleViewModel.Validate())
            {
                throw new BloggyException("Requires at least one section and one title");
            }
            var mappedItem = _blogArticleMapping.MapToBlogArticle(blogArticleViewModel);
            _blogService.Add(mappedItem);
            return Created(string.Empty, blogArticleViewModel);
        }

        [HttpPut]
        public IActionResult Update(UpdateBlogArticleTransferObj blogArticleViewModel)
        {
            if (blogArticleViewModel.Validate())
            {
                throw new BloggyException("Requires at least one section and one title");
            }
            var mappedItem = _blogArticleMapping.MapToUpdateBlogArticle(blogArticleViewModel);
            _blogService.Update(mappedItem);
            return Ok(blogArticleViewModel);
        }
    }
}