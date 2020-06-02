using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
    public interface IBlogService
    {
        BlogArticlePackage GetBlogArticles(Func<List<BlogArticle>, bool> customFunc = null);
        void Add(BlogArticle blogArticle);
        void Update(BlogArticle blogArticle, Guid blogArticleId);
        void Delete(Guid blogArticleId);
    }
}
