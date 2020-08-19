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
        public BlogArticlePackage MapToPackageTransferObj(List<Section> sections, 
                                                                    List<Paragraph> paragraphs, 
                                                                    List<Image> images, 
                                                                    List<BlogArticleObj> blogArticles )
        {
            var final = new BlogArticlePackage();
            foreach(var blogArticle in blogArticles)
            {
                blogArticle.Sections = MapParagraphsAndImagesForSection(sections.Where(x => x.BlogId == blogArticle.BlogArticleId).ToList(), paragraphs, images);
                final.BlogArticles.Add(blogArticle);
            }
            return final;
        }

        public List<Section> MapParagraphsAndImagesForSection(List<Section> sections, List<Paragraph> paragraphs, List<Image> images)
        {
            foreach(var section in sections)
            {
                section.Paragraphs = paragraphs.Where(x => x.SectionId == section.SectionId).ToList();
                section.Images = null;
            }
            return sections;
        }

        public List<Paragraph> MapToParagraphTransferObj(List<ParagraphAccessObj> paragraphs)
        {
            var final = new List<Paragraph>();

            foreach (var paragraph in paragraphs)
            {
                final.Add(new Paragraph()
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

        public List<Section> MapToSectionTransferObj(List<SectionAccessObj> sections)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<List<SectionAccessObj>, List<Section>>();
            });

            IMapper mapper = config.CreateMapper();
            var source = sections;
            var final = mapper.Map<List<SectionAccessObj>, List<Section>>(source);
            return final;
        }

        public List<BlogArticleObj> MapToBlogArticleTransferObj(List<BlogArticleAccessObj> blogArticles)
        {
            var final = new List<BlogArticleObj>();
            foreach(var item in blogArticles)
            {
                final.Add(new BlogArticleObj() { BlogArticleId = item.BlogId, Sections = null, Title = item.Title });
            }

            return final;
        }

        public List<Section> MapFromGetAllSectionAccessObj(List<GetAllSectionsAccessObject> sections)
        {
            var final = new List<Section>();
            foreach (var item in sections)
            {
                final.Add(new Section()
                {
                    SectionId = item.SectionId,
                    BlogId = item.BlogId,
                    Header = SerializationManager.Deserialize<Header>(item.Header),
                    SubHeader = SerializationManager.Deserialize<SubHeader>(item.Subheader),
                    Images = null,
                    Paragraphs = null
                });
            }

            return final;
        }
    }
}
