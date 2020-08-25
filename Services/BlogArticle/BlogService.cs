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
using System.Linq;

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
            var createdIds = new IdsForBlogArticlePackage(); // this is so we can return the id's of the items we have just added
            var mappedBlogArticle = _accessObjectMapper.MapToBlogArticleAccessObj(blogArticle);
            var mappedSections = new List<SectionJson>();
            
            var blogUniqueIdentifier = Guid.NewGuid();

            //add mapped blog article
            _dbConnnection.AddBlogArticle(mappedBlogArticle, blogUniqueIdentifier);
            createdIds.BlogArticleId = blogUniqueIdentifier;

            foreach (var item in blogArticle.Sections)
            {
                var sectionUniqueIdentifier = Guid.NewGuid();
                createdIds.SectionIds.Add(sectionUniqueIdentifier); //add the list of section IDs to map back to the API
                createdIds.ParagraphIds.Add(item.SectionId, item.Paragraphs.Select(x => x.ParagraphId).ToList()); //add the dictionairy of paragraph IDs for each section

                //add mapped section
                _dbConnnection.AddSection(_accessObjectMapper.MapToJsonSectionAccessObj(item), blogUniqueIdentifier);

                //add mapped paragraphs for this section
                _dbConnnection.AddParagraphs(_accessObjectMapper.MapToParagraphAccessObj(item.Paragraphs), sectionUniqueIdentifier);

                //add mapped images for this section
                _dbConnnection.AddImages(_accessObjectMapper.MapToImageAccessObj(item.Images));
            }
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

            var temp = new BlogArticlePackage() { BlogArticles = new List<BlogArticleObj>() };
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
