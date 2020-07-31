using AutoMapper;
using Generics;
using Microsoft.Extensions.Logging;
using Repositories.BlogArticle;
using Services.AutoMapping;
using Services.BlogArticle.Models;
using Services.BlogArticle.Models.JsonMappingModels;
using Services.Mapping;
using System;
using System.Collections.Generic;

namespace Services
{
    public class BlogService : IBlogService
    {
        private JsonMapping _jsonMapping;
        private AccessObjectMapper _transferObjectMapping = new AccessObjectMapper();
        private TransferObjectMapper _accessObjectMapping = new TransferObjectMapper();
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
            var mappedItem = _transferObjectMapping.MapToBlogArticleAccessObj(blogArticle);
            var mappedSections = new List<SectionJsonTransferObj>();

            _dbConnnection.AddBlogArticle(mappedItem);
            
            foreach (var item in blogArticle.Sections)
            {
                Guid blogArticleId = Guid.NewGuid();
                mappedSections.Add(_jsonMapping.MapToSectionJson(item));

                _dbConnnection.AddParagraphs(_transferObjectMapping
                    .MapToParagraphAccessObj(item.Paragraphs), blogArticleId);

                _dbConnnection.AddImages(_transferObjectMapping
                    .MapToImageAccessObj(item.Images));
            }
            _dbConnnection.AddSections(_transferObjectMapping
                .MapToJsonSectionAccessObj(mappedSections), Guid.NewGuid());
        }
        public void Delete(Guid blogArticleId)
        {
            _dbConnnection.DeleteBlogArticle(blogArticleId, Guid.NewGuid());
        }

        public BlogArticlePackageTransferObj GetBlogArticles()
        {
            var blogArticles = _dbConnnection.GetAllBlogArticles();
            var sectionItems = _dbConnnection.GetAllSections();
            var paragraphItems = _dbConnnection.GetAllParagraphs();

            var finalModel = new BlogArticlePackageTransferObj
            {
                Sections = _accessObjectMapping.MapToSectionTransferObj(sectionItems), 
                Paragraphs= _accessObjectMapping.MapToParagraphTransferObj(paragraphItems), 
                BlogArticles = _accessObjectMapping.MapToBlogArticleTransferObj(blogArticles)
            };
            return finalModel;
        }

        public void Update(UpdateBlogArticleTransferObj blogArticle)
        {
            var mappedItem = _transferObjectMapping.MapToUpdateBlogArticleAccessObj(blogArticle);
            _dbConnnection.UpdateItem(mappedItem);
        }
    }
}
