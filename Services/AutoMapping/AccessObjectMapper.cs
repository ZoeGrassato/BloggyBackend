using AutoMapper;
using Repositories.BlogArticle.Models;
using Repositories.BlogArticle.Models.JsonMappingModels;
using Services.BlogArticle.Models;
using Services.BlogArticle.Models.JsonMappingModels;
using Services.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mapping
{
    //for items that need to be mapped in order to go into the db
    //mapping direction--> from transferObjects to accessObjects
    public class AccessObjectMapper
    {
        public BlogArticleAccessObj MapToBlogArticleAccessObj(BlogArticleObj blogArticleTransferObj)
        {
            var final = new BlogArticleAccessObj() { BlogId = blogArticleTransferObj.ArticleId, Title = blogArticleTransferObj.Title };
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<BlogArticleTransferObj, BlogArticleAccessObj>();
            //});

            //IMapper mapper = config.CreateMapper();
            //var source = blogArticleTransferObj;
            //var final = mapper.Map<BlogArticleTransferObj, BlogArticleAccessObj>(source);
            return final;
        }

        public UpdateBlogArticleAccessObj MapToUpdateBlogArticleAccessObj(UpdateBlogArticle blogArticleTransferObj)
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

        public List<ParagraphAccessObj> MapToParagraphAccessObj(List<Paragraph> paragraphTransferObj)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Paragraph, ParagraphAccessObj>();
            });

            IMapper mapper = config.CreateMapper();
            var source = paragraphTransferObj;
            var final = mapper.Map<List<Paragraph>, List<ParagraphAccessObj>>(source);
            return final;
        }

        public List<ImageAccessObj> MapToImageAccessObj(List<Image> imageItems)
        {
            var final = new List<ImageAccessObj>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Image, ImageAccessObj>();
            });

            foreach (var item in imageItems)
            {
                IMapper mapper = config.CreateMapper();
                var source = item;
                final.Add(mapper.Map<Image, ImageAccessObj>(source));
            }
            return final;
        }

        public List<SectionAccessObj> MapToSectionAccessObj(List<Section> sections)
        {
            var final = new List<SectionAccessObj>();

            for (int i = 0; i < sections.Count; i++)
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

        public HeaderAccessObj MapToHeader(Header header)
        {
            var final = new HeaderAccessObj();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Header, HeaderAccessObj>();
            });
            IMapper mapper = config.CreateMapper();
            var source = header;
            final = mapper.Map<Header, HeaderAccessObj>(source);
            return final;
        }

        public SubHeaderAccessObj MapToSubheader(SubHeader subHeader)
        {
            var final = new SubHeaderAccessObj();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SubHeader, SubHeaderAccessObj>();
            });
            IMapper mapper = config.CreateMapper();
            var source = subHeader;
            final = mapper.Map<SubHeader, SubHeaderAccessObj>(source);
            return final;
        }

        public List<SectionJsonAccessObj> MapToJsonSectionsAccessObj(List<SectionJson> sections)
        {
            var final = new List<SectionJsonAccessObj>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SectionJson, SectionJsonAccessObj>();
            });

            foreach (var item in sections)
            {
                IMapper mapper = config.CreateMapper();
                var source = item;
                final.Add(mapper.Map<SectionJson, SectionJsonAccessObj>(source));
            }
            return final;
        }

        public SectionJsonAccessObj MapToJsonSectionAccessObj(Section section)
        {
            var final = new SectionJsonAccessObj();

            final.SectionId = section.SectionId;
            final.Header = SerializationManager.Serialize(section.Header);
            final.SubHeader = SerializationManager.Serialize(section.SubHeader);

            return final;
        }
    }
}
