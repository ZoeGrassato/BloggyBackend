using AutoMapper;
using BloggyBackend.Models;
using Models;
using Models.Mapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMappingLogic
{
    public class BlogArticleAutoMapper
    {
        public BlogArticleJson MapToBlogArticleJson(BlogArticle blogArticle)
        {
            var final = new BlogArticleJson { BlogArticleId = blogArticle.ArticleId, Title = blogArticle.Title };
            return final;
        }

        public SectionJson MapToSectionJson(Section section)
        {
            var final = new SectionJson
            {
                SectionId = section.SectionId,
                Header = JsonConvert.SerializeObject(section.Header),
                SubHeader = JsonConvert.SerializeObject(section.SubHeader)
            };
            return final;
        }

        public BlogArticle MapToBlogArticle(BlogArticleViewModel blogArticleViewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogArticleViewModel, BlogArticle>();
            });

            IMapper mapper = config.CreateMapper();
            var source = blogArticleViewModel;
            var final = mapper.Map<BlogArticleViewModel, BlogArticle>(source);
            return final;
        }
    }
}
