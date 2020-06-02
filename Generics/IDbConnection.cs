using Models;
using Models.Mapping;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Generics
{
    public interface IDbConnection
    {
        void AddBlogArticle(BlogArticleJson blogArticle);
        void AddParagraphs(List<Paragraph> paragraphs, Guid sectionId);
        void AddImages(List<Image> images);
        void DeleteBlogArticle(Guid blogArticleId, Guid sectionId);
        List<BlogArticle> GetAllBlogArticles<T>(Func<List<T>, bool> query = null);
        List<Section> GetAllSections<T>(Func<List<T>, bool> query = null);
        List<Paragraph> GetAllParagraphs<T>(Func<List<T>, bool> query = null);
        void UpdateItem(Guid blogArticleId, BlogArticle blogArticle);
        void AddSections(List<SectionJson> sections, Guid currentBlogId);
    }
}
