using AutoMapper;
using BloggyBackend.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggyBackend.AutoMapper
{
    public class BlogArticleMapping
    {
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
