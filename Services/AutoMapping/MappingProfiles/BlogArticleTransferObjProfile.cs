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
            CreateMap<BlogArticleAccessObj, BlogArticleTransferObj>()
            .ForMember(dest => dest.BlogArticleId, opt => opt.MapFrom(s => s.BlogId))
            .ReverseMap()
             .ForMember(dest => dest.BlogId, opt => opt.MapFrom(s => s.BlogArticleId));
        }

        private void Section()
        {
            CreateMap<SectionAccessObj, SectionTransferObj>();
        }

        private void Header()
        {
            CreateMap<HeaderAccessObj, HeaderTransferObj>();
        }
    }
}
