using Generics;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repositories.BlogArticle;
using Services.BlogArticle.Models;
using Services.BlogArticle.Models.JsonMappingModels;
using Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class BlogService : IBlogService
    {
        private JsonMapping _jsonMapping;
        private IBlogArticleRepository _dbConnnection;
        private ILogger _logger;
        public BlogService(ILogger<IBlogService> logger, IBlogArticleRepository dbConnection)
        {
            _logger = logger;
            _jsonMapping = new JsonMapping();
            _dbConnnection = dbConnection;
        }
        public void Add(BlogArticleTransferObj blogArticle)
        {
            var mappedBlogArticleItem = _jsonMapping.MapToBlogArticleJson(blogArticle);
            _dbConnnection.AddBlogArticle(mappedBlogArticleItem);

            var mappedSections = new List<SectionJsonTransferObj>();
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

        public BlogArticlePackageTransferObj GetBlogArticles(Func<BlogArticleTransferObj, bool> query)
        {
            var blogArticles = _dbConnnection.GetAllBlogArticles();
            var sectionItems = _dbConnnection.GetAllSections();
            var paragraphItems = _dbConnnection.GetAllParagraphs();

            var itemsModel = new BlogArticlePackageTransferObj
            {
                Sections = null, //sectionItems,
                Paragraphs= null, ///paragraphItems,
                BlogArticles = null, // blogArticles
            };
            return itemsModel.BlogArticles.Where(query);
        }

        public void Update(BlogArticleTransferObj blogArticle, Guid blogArticleId)
        {
            _dbConnnection.UpdateItem(blogArticleId, blogArticle);
        }
    }
}
