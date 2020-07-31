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
    //for items that need to be mapped in order to go into the db
    //mapping direction--> from transferObjects to accessObjects
    public class AccessObjectMapper
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

        public UpdateBlogArticleAccessObj MapToUpdateBlogArticleAccessObj(UpdateBlogArticleTransferObj blogArticleTransferObj)
        {
            var final = new UpdateBlogArticleAccessObj()
            {
                ArticleId = blogArticleTransferObj.BlogArticleId,
                Title = blogArticleTransferObj.Title,
                Sections = MapToSectionAccessObj(blogArticleTransferObj.Sections),
                HasImageChanged = blogArticleTransferObj.HasImageChanged,
                HasParagraphChanged = blogArticleTransferObj.HasParagraphChanged,
                HasSectionChanged = blogArticleTransferObj.HasSectionChanged,
                HasTitleChanged = blogArticleTransferObj.HasTitleChanged
            };
            
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

        public List<SectionAccessObj> MapToSectionAccessObj(List<SectionTransferObj> sections)
        {
            var final = new List<SectionAccessObj>();
           
            for(int i =0; i < sections.Count;i++)
            {
                var source = sections[i];
                var current = new SectionAccessObj()
                {
                    SectionId = sections[i].SectionId,
                    Header = MapToHeader(sections[i].Header),
                    SubHeader = MapToSubheader(sections[i].SubHeader),
                    Paragraphs = MapToParagraphAccessObj(sections[i].Paragraphs),
                    Images = MapToImageAccessObj(sections[i].Images)
                };

                final.Add(current);
            }
            return final;
        }

        public HeaderAccessObj MapToHeader(HeaderTransferObj header)
        {
            var final = new HeaderAccessObj();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HeaderTransferObj, HeaderAccessObj>();
            });
            IMapper mapper = config.CreateMapper();
            var source = header;
            final = mapper.Map<HeaderTransferObj, HeaderAccessObj>(source);
            return final;
        }

        public SubHeaderAccessObj MapToSubheader(SubHeaderTransferObj subHeader)
        {
            var final = new SubHeaderAccessObj();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SubHeaderTransferObj, SubHeaderAccessObj>();
            });
            IMapper mapper = config.CreateMapper();
            var source = subHeader;
            final = mapper.Map<SubHeaderTransferObj, SubHeaderAccessObj>(source);
            return final;
        }

        public List<SectionJsonAccessObj> MapToJsonSectionAccessObj(List<SectionJsonTransferObj> sections)
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
