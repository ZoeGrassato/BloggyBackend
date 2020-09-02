using Generics;
using Microsoft.Extensions.Logging;
using Repositories.BlogArticle;
using Services.AutoMapping;
using Services.BlogArticle.Models;
using Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class BlogService : IBlogService
    {
        private AccessObjectMapper _accessObjectMapper = new AccessObjectMapper();
        private TransferObjectMapper _transferObjectMapper = new TransferObjectMapper();
        private IBlogArticleRepository _dbConnnection;
        private ILogger _logger;
        public BlogService(ILogger<IBlogService> logger, IBlogArticleRepository dbConnection)
        {
            _logger = logger;
            _dbConnnection = dbConnection;
        }
        public BlogArticleObj Add(BlogArticleObj blogArticle)
        {
            var blogUniqueIdentifier = Guid.NewGuid();
            var mappedBlogArticle = _accessObjectMapper.MapToBlogArticleAccessObj(blogArticle);
            var blogArticleObj = new BlogArticleObj() { Title = blogArticle.Title, ArticleId = blogUniqueIdentifier };

            //add mapped blog article
            _dbConnnection.AddBlogArticle(mappedBlogArticle, blogUniqueIdentifier);

            foreach (var item in blogArticle.Sections)
            {
                var sectionUniqueIdentifier = Guid.NewGuid();

                item.Paragraphs = _transferObjectMapper.MapSectionIdsForParagraph(item.Paragraphs, sectionUniqueIdentifier);
                item.SectionId = sectionUniqueIdentifier;
                item.BlogId = blogUniqueIdentifier;
                blogArticleObj.Sections.Add(item);

                //add a new ID for each paragraph
                item.Paragraphs.Select(x => { x.ParagraphId = Guid.NewGuid(); return x; }).ToList();

                //add mapped section
                _dbConnnection.AddSection(_accessObjectMapper.MapToJsonSectionAccessObj(item), blogUniqueIdentifier);

                //add mapped paragraphs for this section
                _dbConnnection.AddParagraphs(_accessObjectMapper.MapToParagraphAccessObj(item.Paragraphs), sectionUniqueIdentifier);

                //add mapped images for this section
                _dbConnnection.AddImages(_accessObjectMapper.MapToImageAccessObj(item.Images));
            }

            return blogArticleObj;
        }
        public void Delete(Guid blogArticleId)
        {
            var relevantIdsToDelete = RetrieveAllNestedIdsForDeletion(blogArticleId);
            _dbConnnection.DeleteBlogArticle(blogArticleId);
            _dbConnnection.DeleteParagraphs(relevantIdsToDelete.RelevantParagraphIds);
            _dbConnnection.DeleteSections(relevantIdsToDelete.RelevantSectionIds);
        }

        public DeletionIdsSectionAndParagraph RetrieveAllNestedIdsForDeletion(Guid blogArticleId)
        {
            var listOfRelevantSectionIds = _dbConnnection.GetAllSections().Where(x => x.BlogId == blogArticleId).Select(x => x.SectionId).ToList();
            var listOfRelevantParagraphIds = new List<Guid>();
            var allParagraphs = _dbConnnection.GetAllParagraphs().ToList().Select(x => x.ParagraphId);
            var final = new DeletionIdsSectionAndParagraph() { RelevantParagraphIds = listOfRelevantParagraphIds, RelevantSectionIds = listOfRelevantSectionIds };

            foreach (var currentSectionId in listOfRelevantSectionIds)
            {
                var paragraphsForCurrentSection = allParagraphs.Where(x => x == currentSectionId).ToList();
                paragraphsForCurrentSection.ForEach(x => listOfRelevantParagraphIds.Add(x));
            }

            return final;
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
