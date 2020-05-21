using Generics;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class BlogService : IBlogService
    {
        public void Add(BlogArticle blogArticle) => throw new NotImplementedException();
        public void Delete(BlogArticle blogArticle) => throw new NotImplementedException();
        public List<BlogArticle> GetBlogArticles(Func<List<BlogArticle>, bool> customFunc) => throw new NotImplementedException();
        public void Update(BlogArticle blogArticle, Guid blogArticleId) => throw new NotImplementedException();
    }
}
