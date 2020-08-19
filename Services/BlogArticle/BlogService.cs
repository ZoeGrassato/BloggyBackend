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
        private AccessObjectMapper _accessObjectMapper = new AccessObjectMapper();
        private TransferObjectMapper _transferObjectMapper = new TransferObjectMapper();
        private IBlogArticleRepository _dbConnnection;
        private ILogger _logger;
        public BlogService(ILogger<IBlogService> logger, IBlogArticleRepository dbConnection)
        {
            _logger = logger;
            _jsonMapping = new JsonMapping();
            _dbConnnection = dbConnection;
        }
        public void Add(BlogArticleObj blogArticle)
        {
            var mappedBlogArticle = _accessObjectMapper.MapToBlogArticleAccessObj(blogArticle);
            var mappedSections = new List<SectionJson>();
            
            var blogUniqueIdentifier = Guid.NewGuid();
            var sectionUniqueIdentifier = Guid.NewGuid();

            //add mapped blog article
            _dbConnnection.AddBlogArticle(mappedBlogArticle, blogUniqueIdentifier);

            foreach (var item in blogArticle.Sections)
            {
                //add this mapped section to mapped list
                mappedSections.Add(_jsonMapping.MapToSectionJson(item));

                //add mapped paragraphs for this section
                _dbConnnection.AddParagraphs(_accessObjectMapper.MapToParagraphAccessObj(item.Paragraphs), sectionUniqueIdentifier);

                //add mapped images for this section
                _dbConnnection.AddImages(_accessObjectMapper.MapToImageAccessObj(item.Images));
            }
            //finally add the list of sections for this blog article
            var mappedSectionsForRepo = _accessObjectMapper.MapToJsonSectionAccessObj(mappedSections);
            _dbConnnection.AddSections(mappedSectionsForRepo, blogUniqueIdentifier, sectionUniqueIdentifier);
        }
        public void Delete(Guid blogArticleId)
        {
            _dbConnnection.DeleteBlogArticle(blogArticleId, Guid.NewGuid());
        }

        public BlogArticlePackage GetBlogArticles()
        {
            var blogArticles = _transferObjectMapper.MapToBlogArticleTransferObj(_dbConnnection.GetAllBlogArticles());
            var sections = _transferObjectMapper.MapFromGetAllSectionAccessObj(_dbConnnection.GetAllSections());
            var paragraphItems = _transferObjectMapper.MapToParagraphTransferObj(_dbConnnection.GetAllParagraphs());

            var temp = new BlogArticlePackage();
            temp = _transferObjectMapper.MapToPackageTransferObj(sections, paragraphItems, null, blogArticles);

            return temp;
        }

        public void Update(UpdateBlogArticle blogArticle)
        {
            var mappedItem = _accessObjectMapper.MapToUpdateBlogArticleAccessObj(blogArticle);
            _dbConnnection.UpdateItem(mappedItem);
        }
    }
}
