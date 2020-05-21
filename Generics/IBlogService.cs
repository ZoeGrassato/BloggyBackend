using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
    public interface IBlogService
    {
        List<BlogArticle> GetBlogArticles(Func<List<BlogArticle>, bool> customFunc);
        void Add(BlogArticle blogArticle);
        void Update(BlogArticle blogArticle, Guid blogArticleId);
        void Delete(BlogArticle blogArticle);
    }
}
