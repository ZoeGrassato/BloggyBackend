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
            var mappedBlogArticle = _transferObjectMapping.MapToBlogArticleAccessObj(blogArticle);
            var mappedSections = new List<SectionJsonTransferObj>();
            
            var blogUniqueIdentifier = Guid.NewGuid();
            var sectionUniqueIdentifier = Guid.NewGuid();

            //add mapped blog article
            _dbConnnection.AddBlogArticle(mappedBlogArticle, blogUniqueIdentifier);

            foreach (var item in blogArticle.Sections)
            {
                //add this mapped section to mapped list
                mappedSections.Add(_jsonMapping.MapToSectionJson(item));

                //add mapped paragraphs for this section
                _dbConnnection.AddParagraphs(_transferObjectMapping.MapToParagraphAccessObj(item.Paragraphs), sectionUniqueIdentifier);

                //add mapped images for this section
                _dbConnnection.AddImages(_transferObjectMapping.MapToImageAccessObj(item.Images));
            }
            //finally add the list of sections for this blog article
            var mappedSectionsForRepo = _transferObjectMapping.MapToJsonSectionAccessObj(mappedSections);
            _dbConnnection.AddSections(mappedSectionsForRepo, blogUniqueIdentifier, sectionUniqueIdentifier);
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
