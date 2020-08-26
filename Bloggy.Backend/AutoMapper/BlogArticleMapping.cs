using AutoMapper;
using Bloggy.Backend.Models;
using Services.BlogArticle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggy.Backend.AutoMapper
{
    //for items that need to be mapped for use of the API or the service layer
    //mapping direction--> from transferObjects to serviceLayerObjects and
    //                 --> from serviceLayerObjects to trasferObjects
    public class BlogArticleMapping
    {
        public BlogArticleObj MapToBlogArticle(BlogArticleTransferObj blogArticleViewModel)
        {
            var finalItem = new BlogArticleObj()
            {
                Title = blogArticleViewModel.Title,
                BlogArticleId = blogArticleViewModel.ArticleId,
                Sections = MapSections(blogArticleViewModel.Sections)
            };

            return finalItem;
        }

        public UpdateBlogArticle MapToUpdateBlogArticle(UpdateBlogArticleTransferObj blogArticleViewModel)
        {
            var finalItem = new UpdateBlogArticle()
            {
                Title = blogArticleViewModel.Title,
                BlogArticleId = blogArticleViewModel.ArticleId,
                Sections = MapSections(blogArticleViewModel.Sections),
                HasImageChanged = blogArticleViewModel.HasImageChanged,
                HasParagraphChanged = blogArticleViewModel.HasParagraphChanged,
                HasSectionChanged = blogArticleViewModel.HasSectionChanged,
                HasTitleChanged = blogArticleViewModel.HasTitleChanged
            };

            return finalItem;
        }

        public List<Section> MapSections(List<SectionTransferObj> sectionViewModels)
        {
            var finalList = new List<Section>();
            foreach(var item in sectionViewModels)
            {
                var current = new Section()
                {
                    Header = MapHeader(item.Header),
                    SubHeader = MapSubheader(item.SubHeader),
                    Paragraphs = MapParagraphs(item.Paragraphs),
                    Images = MapImages(item.Images),
                    SectionId = item.SectionId
                    
                };
                finalList.Add(current);
            }
            return finalList;
        }

        public List<Image> MapImages(List<ImageTransferObj> imageModels)
        {
            var finalList = new List<Image>();
            foreach (var item in imageModels)
            {
                var current = new Image()
                {
                   ImageId = item.ImageId,
                   BytesImages = item.BytesImages,
                   SectionId = item.SectionId
                };
                finalList.Add(current);
            }
            return finalList;
        }

        public List<Paragraph> MapParagraphs(List<ParagraphTransferObj> paragraphViewModels)
        {
            var finalList = new List<Paragraph>();
            foreach(var item in paragraphViewModels)
            {
                var current = new Paragraph()
                {
                    ParagraphId = item.ParagraphId,
                    ParagraphTextArea = item.ParagraphTextArea,
                    SectionId = item.SectionId
                };
                finalList.Add(current);
            }
            return finalList;
        }

        public Header MapHeader(HeaderTransferObj headerViewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HeaderTransferObj, Header>();
            });

            IMapper mapper = config.CreateMapper();
            var source = headerViewModel;
            var final = mapper.Map<HeaderTransferObj, Header>(source);
            return final;
        }

        public SubHeader MapSubheader(SubHeaderTransferObj subHeaderViewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SubHeaderTransferObj, SubHeader>();
            });

            IMapper mapper = config.CreateMapper();
            var source = subHeaderViewModel;
            var final = mapper.Map<SubHeaderTransferObj, SubHeader>(source);
            return final;
        }

        public BlogArticleTransferObj MapToBlogArticleTransferObj(BlogArticleObj blogArticleObj)
        {
            var final = new BlogArticleTransferObj()
            {
                ArticleId = blogArticleObj.BlogArticleId,
                Title = blogArticleObj.Title,
                Sections = MapToSectionTransferObj(blogArticleObj.Sections)
            };

            return final;
        }

        public List<SectionTransferObj> MapToSectionTransferObj(List<Section> sections)
        {
            var final = new List<SectionTransferObj>();
            
            foreach(var item in sections)
            {
                final.Add(new SectionTransferObj()
                {
                    BlogId = item.BlogId,
                    SectionId = item.SectionId,
                    Header = MapFromHeader(item.Header),
                    SubHeader = MapFromSubheader(item.SubHeader),
                    Paragraphs = MapFromParagraphs(item.Paragraphs)
                });
            }

            return final;
        }

        public HeaderTransferObj MapFromHeader(Header headerViewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Header, HeaderTransferObj>();
            });

            IMapper mapper = config.CreateMapper();
            var source = headerViewModel;
            var final = mapper.Map<Header, HeaderTransferObj>(source);
            return final;
        }

        public SubHeaderTransferObj MapFromSubheader(SubHeader subHeader)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SubHeader, SubHeaderTransferObj>();
            });

            IMapper mapper = config.CreateMapper();
            var source = subHeader;
            var final = mapper.Map<SubHeader, SubHeaderTransferObj>(source);
            return final;
        }

        public List<ParagraphTransferObj> MapFromParagraphs(List<Paragraph> paragraphs)
        {
            var finalList = new List<ParagraphTransferObj>();
            foreach (var item in paragraphs)
            {
                var current = new ParagraphTransferObj()
                {
                    ParagraphId = item.ParagraphId,
                    ParagraphTextArea = item.ParagraphTextArea,
                    SectionId = item.SectionId
                };
                finalList.Add(current);
            }
            return finalList;
        }
    }
}
