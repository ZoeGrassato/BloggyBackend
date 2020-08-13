using AutoMapper;
using Repositories.BlogArticle.Models;
using Services.BlogArticle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.AutoMapping
{
    //for items that need to be mapped in order to go through the API to the front end 
    //mapping direction--> from accessObjects to transferObjects
    public class TransferObjectMapper
    {
        public BlogArticlePackageTransferObj MapToPackageTransferObj(List<SectionTransferObj> sections, 
                                                                    List<ParagraphTransferObj> paragraphs, 
                                                                    List<ImageTransferObj> images, 
                                                                    List<BlogArticleTransferObj> blogArticles )
        {
            var final = new BlogArticlePackageTransferObj();
            foreach(var blogArticle in blogArticles)
            {
                var section = 
            }
        }

        public MapToPackageSection(List<SectionTransferObj>)
        {

        }

        public List<ParagraphTransferObj> MapToParagraphTransferObj(List<ParagraphAccessObj> paragraphs)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<List<ParagraphAccessObj>, List<ParagraphTransferObj>>();
            });

            IMapper mapper = config.CreateMapper();
            var source = paragraphs;
            var final = mapper.Map<List<ParagraphAccessObj>, List<ParagraphTransferObj>>(source);
            return final;
        }

        public List<ImageTransferObj> MapToImageTransferObj(List<ImageAccessObj> images)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<List<ImageAccessObj>, List<ImageTransferObj>>();
            });

            IMapper mapper = config.CreateMapper();
            var source = images;
            var final = mapper.Map<List<ImageAccessObj>, List<ImageTransferObj>>(source);
            return final;
        }

        public List<SectionTransferObj> MapToSectionTransferObj(List<SectionAccessObj> sections)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<List<SectionAccessObj>, List<SectionTransferObj>>();
            });

            IMapper mapper = config.CreateMapper();
            var source = sections;
            var final = mapper.Map<List<SectionAccessObj>, List<SectionTransferObj>>(source);
            return final;
        }

        public List<BlogArticleTransferObj> MapToBlogArticleTransferObj(List<BlogArticleAccessObj> blogArticles)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<List<BlogArticleAccessObj>, List<BlogArticleTransferObj>>();
            });

            IMapper mapper = config.CreateMapper();
            var source = blogArticles;
            var final = mapper.Map<List<BlogArticleAccessObj>, List<BlogArticleTransferObj>>(source);
            return final;
        }

        public List<SectionTransferObj> MapFromStringSectionTransferObj(List<GetAllSectionsAccessObject> sections)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<List<GetAllSectionsAccessObject>, List<SectionTransferObj>>();
            });

            IMapper mapper = config.CreateMapper();
            var source = sections;
            var final = mapper.Map<List<GetAllSectionsAccessObject>, List<SectionTransferObj>>(source);
            return final;
        }
    }
}
