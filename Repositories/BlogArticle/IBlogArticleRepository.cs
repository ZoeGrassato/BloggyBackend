using Repositories.BlogArticle.Models;
using Repositories.BlogArticle.Models.JsonMappingModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Repositories.BlogArticle
{
    public interface IBlogArticleRepository
    {
        void AddBlogArticle(BlogArticleAccessObj blogArticle);
        void AddParagraphs(List<ParagraphAccessObj> paragraphs, Guid sectionId);
        void AddImages(List<ImageAccessObj> images);
        void DeleteBlogArticle(Guid blogArticleId, Guid sectionId);
        List<BlogArticleAccessObj> GetAllBlogArticles();
        List<SectionAccessObj> GetAllSections(Func<List<SectionAccessObj>, bool> query = null);
        List<ParagraphAccessObj> GetAllParagraphs(Func<List<ParagraphAccessObj>, bool> query = null);
        void UpdateItem(Guid blogArticleId, BlogArticleAccessObj blogArticle);
        void AddSections(List<SectionJsonAccessObj> sections, Guid currentBlogId);
    }
}
