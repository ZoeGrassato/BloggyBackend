using AutoMapper;
using BloggyBackend.Models;
using Services.BlogArticle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggyBackend.AutoMapper
{
    public class BlogArticleMapping
    {
        public BlogArticleTransferObj MapToBlogArticle(BlogArticleViewModel blogArticleViewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogArticleViewModel, BlogArticleTransferObj>();
            });

            IMapper mapper = config.CreateMapper();
            var source = blogArticleViewModel;
            var final = mapper.Map<BlogArticleViewModel, BlogArticleTransferObj>(source);
            return final;
        }
    }
}
