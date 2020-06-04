using AutoMapper;
using Repositories.BlogArticle.Models;
using Services.BlogArticle.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.AutoMapping
{
    public class AccessObjectMapping
    {
        public List<ParagraphTransferObj> MapToParagraphTransferObj (List<ParagraphAccessObj> paragraphAccessObj)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<List<ParagraphAccessObj>, List<ParagraphTransferObj>>();
            });

            IMapper mapper = config.CreateMapper();
            var source = paragraphAccessObj;
            var final = mapper.Map<List<ParagraphAccessObj>, List<ParagraphTransferObj>>(source);
            return final;
        }

        public List<ImageTransferObj> MapToImageTransferObj (List<ImageAccessObj> paragraphAccessObj)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<List<ImageAccessObj>, List<ImageTransferObj>>();
            });

            IMapper mapper = config.CreateMapper();
            var source = paragraphAccessObj;
            var final = mapper.Map<List<ImageAccessObj>, List<ImageTransferObj>>(source);
            return final;
        }

        public List<SectionTransferObj> MapToSectionTransferObj (List<SectionAccessObj> paragraphAccessObj)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<List<SectionAccessObj>, List<SectionTransferObj>>();
            });

            IMapper mapper = config.CreateMapper();
            var source = paragraphAccessObj;
            var final = mapper.Map<List<SectionAccessObj>, List<SectionTransferObj>>(source);
            return final;
        }

        public List<BlogArticleTransferObj> MapToBlogArticleTransferObj (List<BlogArticleAccessObj> paragraphAccessObj)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<List<BlogArticleAccessObj>, List<BlogArticleTransferObj>>();
            });

            IMapper mapper = config.CreateMapper();
            var source = paragraphAccessObj;
            var final = mapper.Map<List<BlogArticleAccessObj>, List<BlogArticleTransferObj>>(source);
            return final;
        }
    }
}
