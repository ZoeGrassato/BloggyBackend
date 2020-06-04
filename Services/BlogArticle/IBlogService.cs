using Services.BlogArticle.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
    public interface IBlogService
    {
        BlogArticlePackageTransferObj GetBlogArticles();
        void Add(BlogArticleTransferObj blogArticle);
        void Update(BlogArticleTransferObj blogArticle, Guid blogArticleId);
        void Delete(Guid blogArticleId);
    }
}
