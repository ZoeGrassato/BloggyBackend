using AutoMapper;
using Repositories.BlogArticle.Models;
using Repositories.BlogArticle.Models.JsonMappingModels;
using Services.BlogArticle.Models;
using Services.BlogArticle.Models.JsonMappingModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mapping
{
    public class TransferObjectMapping
    {
        public BlogArticleAccessObj MapToBlogArticleAccessObj(BlogArticleTransferObj blogArticleTransferObj)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogArticleTransferObj, BlogArticleAccessObj>();
            });

            IMapper mapper = config.CreateMapper();
            var source = blogArticleTransferObj;
            var final = mapper.Map<BlogArticleTransferObj, BlogArticleAccessObj>(source);
            return final;
        }

        public List<ParagraphAccessObj> MapToParagraphAccessObj(List<ParagraphTransferObj> paragraphTransferObj)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ParagraphTransferObj, ParagraphAccessObj>();
            });

            IMapper mapper = config.CreateMapper();
            var source = paragraphTransferObj;
            var final = mapper.Map<List<ParagraphTransferObj>, List<ParagraphAccessObj>>(source);
            return final;
        }

        public List<ImageAccessObj> MapToImageAccessObj(List<ImageTransferObj> imageItems)
        {
            var final = new List<ImageAccessObj>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ImageTransferObj, ImageAccessObj>();
            });
            
            foreach(var item in imageItems)
            {
                IMapper mapper = config.CreateMapper();
                var source = item;
                final.Add(mapper.Map<ImageTransferObj,ImageAccessObj>(source));
            }
            return final;
        }

        public List<SectionJsonAccessObj> MapToSectionAccessObj(List<SectionJsonTransferObj> sections)
        {
            var final = new List<SectionJsonAccessObj>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SectionJsonTransferObj, SectionJsonAccessObj>();
            });

            foreach (var item in sections)
            {
                IMapper mapper = config.CreateMapper();
                var source = item;
                final.Add(mapper.Map<SectionJsonTransferObj, SectionJsonAccessObj>(source));
            }
            return final;
        }
    }
}
