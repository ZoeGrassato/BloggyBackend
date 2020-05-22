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
        void AddSections(List<SectionJson> sections);
        void AddParagraphs(List<Paragraph> paragraphs);
        void AddImages(List<Image> images);
        void DeleteBlogArticle(Guid BlogArticleId);
        List<T> GetAll<T>(Func<List<T>, bool> query = null);
        void UpdateItem(Guid blogArticleId, BlogArticle blogArticle);
    }

}
