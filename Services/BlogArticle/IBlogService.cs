using Services.BlogArticle.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
    public interface IBlogService
    {
        /// <summary>
        /// retrieves all blog articles in the system
        /// </summary>
        /// <returns>all blog articles in the system</returns>
        BlogArticlePackageTransferObj GetBlogArticles();

        /// <summary>
        /// adds a new blog article to the system
        /// </summary>
        /// <param name="blogArticle"></param>
        void Add(BlogArticleTransferObj blogArticle);

        /// <summary>
        /// updates an existing blog article with new data based on a blog article id
        /// </summary>
        /// <param name="blogArticle">object containing the new data to update with</param>
        /// <param name="blogArticleId"></param>
        void Update(UpdateBlogArticleTransferObj blogArticle);

        /// <summary>
        /// deletes a blog article based on its unique id
        /// </summary>
        /// <param name="blogArticleId"></param>
        void Delete(Guid blogArticleId);
    }
}
