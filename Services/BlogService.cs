using AutoMapper;
using Generics;
using Microsoft.Extensions.Logging;
using Models;
using Models.Mapping;
using Newtonsoft.Json;
using Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class BlogService : IBlogService
    {
        private JsonMapping _jsonMapping;
        private IDbConnection _dbConnnection;
        private ILogger _logger;
        public BlogService(ILogger<IBlogService> logger, IDbConnection dbConnection)
        {
            _logger = logger;
            _jsonMapping = new JsonMapping();
            _dbConnnection = dbConnection;
        }
        public void Add(BlogArticle blogArticle)
        {
            var mappedBlogArticleItem = _jsonMapping.MapToBlogArticleJson(blogArticle);
            _dbConnnection.AddBlogArticle(mappedBlogArticleItem);

            var mappedSections = new List<SectionJson>();
            foreach (var item in blogArticle.Sections)
            {
                Guid blogArticleId = Guid.NewGuid();
                mappedSections.Add(_jsonMapping.MapToSectionJson(item));
                _dbConnnection.AddParagraphs(item.Paragraphs, blogArticleId);
                _dbConnnection.AddImages(item.Images);
            }
            _dbConnnection.AddSections(mappedSections, Guid.NewGuid());
        }
        public void Delete(Guid blogArticleId)
        {
            _dbConnnection.DeleteBlogArticle(blogArticleId, Guid.NewGuid());
        }

        public List<BlogArticle> GetBlogArticles(Func<List<BlogArticle>, bool> customFunc = null)
        {
            var items = _dbConnnection.GetAll<BlogArticle>(customFunc);
            return items;
        }

        public void Update(BlogArticle blogArticle, Guid blogArticleId)
        {
            _dbConnnection.UpdateItem(blogArticleId, blogArticle);
        }
    }
}
