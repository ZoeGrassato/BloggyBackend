using AutoMapper;
using Generics;
using Models;
using Models.Mapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class BlogService : IBlogService
    {
        private BlogArticleAutoMapper _autoMapper;
        private IDbConnection _dbConnnection;
        public BlogService()
        {
            _autoMapper = new BlogArticleAutoMapper();
            _dbConnnection = new DbConnection();
        }
        public void Add(BlogArticle blogArticle)
        {
            var mappedBlogArticleItem = _autoMapper.MapToBlogArticleJson(blogArticle);
            _dbConnnection.AddBlogArticle(mappedBlogArticleItem);

            var mappedSections = new List<SectionJson>();
            foreach (var item in blogArticle.Sections)
            {
                mappedSections.Add(_autoMapper.MapToSectionJson(item));
                _dbConnnection.AddParagraphs(item.Paragraphs);
                _dbConnnection.AddImages(item.Images);
            }
            _dbConnnection.AddSections(mappedSections);
        }
        public void Delete(Guid blogArticleId)
        {
            _dbConnnection.DeleteBlogArticle(blogArticleId);
        }

        public List<BlogArticle> GetBlogArticles(Func<List<BlogArticle>, bool> customFunc = null)
        {
            var items = _dbConnnection.GetAll<BlogArticle>(customFunc);
            return items;
        }

        public void Update(BlogArticle blogArticle, Guid blogArticleId)
        {
            _dbConnnection.UpdateItem(blogArticleId, blogArticle);
        }
    }
}
