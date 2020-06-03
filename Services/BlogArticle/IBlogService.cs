using Models;
using Services.BlogArticle.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
    public interface IBlogService
    {
        BlogArticlePackage GetBlogArticles(Func<List<BlogArticleTransferObj>, bool> customFunc = null);
        void Add(BlogArticleTransferObj blogArticle);
        void Update(BlogArticleTransferObj blogArticle, Guid blogArticleId);
        void Delete(Guid blogArticleId);
    }
}
