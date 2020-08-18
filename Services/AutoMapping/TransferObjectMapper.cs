using AutoMapper;
using Repositories.BlogArticle.Models;
using Services.BlogArticle.Models;
using Services.Serialization;
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
                                                                    List<Image> images, 
                                                                    List<BlogArticleTransferObj> blogArticles )
        {
            var final = new BlogArticlePackageTransferObj();
            foreach(var blogArticle in blogArticles)
            {
                blogArticle.Sections = MapParagraphsAndImagesForSection(sections.Where(x => x.BlogId == blogArticle.BlogArticleId).ToList(), paragraphs, images);
                final.BlogArticles.Add(blogArticle);
            }
            return final;
        }

        public List<SectionTransferObj> MapParagraphsAndImagesForSection(List<SectionTransferObj> sections, List<ParagraphTransferObj> paragraphs, List<Image> images)
        {
            foreach(var section in sections)
            {
                section.Paragraphs = paragraphs.Where(x => x.SectionId == section.SectionId).ToList();
                section.Images = null;
            }
            return sections;
        }

        public List<ParagraphTransferObj> MapToParagraphTransferObj(List<ParagraphAccessObj> paragraphs)
        {
            var final = new List<ParagraphTransferObj>();

            foreach (var paragraph in paragraphs)
            {
                final.Add(new ParagraphTransferObj()
                { 
                    ParagraphId = paragraph.ParagraphId, 
                    ParagraphTextArea = paragraph.ParagraphTextArea, 
                    SectionId = paragraph.SectionId 
                });
            }
            return final;
        }

        public List<Image> MapToImageTransferObj(List<ImageAccessObj> images)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<List<ImageAccessObj>, List<Image>>();
            });

            IMapper mapper = config.CreateMapper();
            var source = images;
            var final = mapper.Map<List<ImageAccessObj>, List<Image>>(source);
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
            var final = new List<BlogArticleTransferObj>();
            foreach(var item in blogArticles)
            {
                final.Add(new BlogArticleTransferObj() { BlogArticleId = item.BlogId, Sections = null, Title = item.Title });
            }

            return final;
        }

        public List<SectionTransferObj> MapFromGetAllSectionAccessObj(List<GetAllSectionsAccessObject> sections)
        {
            var final = new List<SectionTransferObj>();
            foreach (var item in sections)
            {
                final.Add(new SectionTransferObj()
                {
                    SectionId = item.SectionId,
                    BlogId = item.BlogId,
                    Header = SerializationManager.Deserialize<HeaderTransferObj>(item.Header),
                    SubHeader = SerializationManager.Deserialize<SubHeaderTransferObj>(item.Subheader),
                    Images = null,
                    Paragraphs = null
                });
            }

            return final;
        }
    }
}
