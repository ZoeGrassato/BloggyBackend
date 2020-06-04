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

        public List<ImageAccessObj> MapToImageAccessObj(List<ImageTransferObj> paragraphTransferObj)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ImageTransferObj, ImageAccessObj>();
            });

            IMapper mapper = config.CreateMapper();
            var source = paragraphTransferObj;
            var final = mapper.Map<List<ImageTransferObj>, List<ImageAccessObj>>(source);
            return final;
        }

        public List<SectionJsonAccessObj> MapToSectionAccessObj(List<SectionJsonTransferObj> sectionTransferObj)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<List<SectionJsonTransferObj>, List<SectionJsonAccessObj>>();
            });

            IMapper mapper = config.CreateMapper();
            var source = sectionTransferObj;
            var final = mapper.Map<List<SectionJsonTransferObj>, List<SectionJsonAccessObj>>(source);
            return final;
        }
    }
}
