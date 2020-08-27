using AutoMapper;
using Repositories.BlogArticle.Models;
using Services.BlogArticle.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.AutoMapping.MappingProfiles
{
    public class BlogArticleTransferObjProfile : Profile
    {
        public BlogArticleTransferObjProfile()
        {
            Main();
            Header();
            Section();
        }

        private void Main()
        {
            CreateMap<BlogArticleAccessObj, BlogArticleObj>()
            .ForMember(dest => dest.ArticleId, opt => opt.MapFrom(s => s.BlogId))
            .ReverseMap()
             .ForMember(dest => dest.BlogId, opt => opt.MapFrom(s => s.ArticleId));
        }

        private void Section()
        {
            CreateMap<SectionAccessObj, Section>();
        }

        private void Header()
        {
            CreateMap<HeaderAccessObj, Header>();
        }
    }
}
