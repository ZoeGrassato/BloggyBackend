using AutoMapper;
using Bloggy.Backend.Models;
using Services.BlogArticle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggy.Backend.AutoMapper
{
    public class BlogArticleMapping
    {
        public BlogArticleTransferObj MapToBlogArticle(BlogArticleViewModel blogArticleViewModel)
        {
            var finalItem = new BlogArticleTransferObj()
            {
                Title = blogArticleViewModel.Title,
                BlogArticleId = blogArticleViewModel.ArticleId,
                Sections = MapSections(blogArticleViewModel.Sections)
            };

            return finalItem;
        }

        public UpdateBlogArticleTransferObj MapToUpdateBlogArticle(UpdateBlogArticleViewModel blogArticleViewModel)
        {
            var finalItem = new UpdateBlogArticleTransferObj()
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

        public List<SectionTransferObj> MapSections(List<SectionViewModel> sectionViewModels)
        {
            var finalList = new List<SectionTransferObj>();
            foreach(var item in sectionViewModels)
            {
                var current = new SectionTransferObj()
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

        public List<Image> MapImages(List<ImageViewModel> imageModels)
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

        public List<ParagraphTransferObj> MapParagraphs(List<ParagraphViewModel> paragraphViewModels)
        {
            var finalList = new List<ParagraphTransferObj>();
            foreach(var item in paragraphViewModels)
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

        public HeaderTransferObj MapHeader(HeaderViewModel headerViewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HeaderViewModel, HeaderTransferObj>();
            });

            IMapper mapper = config.CreateMapper();
            var source = headerViewModel;
            var final = mapper.Map<HeaderViewModel, HeaderTransferObj>(source);
            return final;
        }

        public SubHeaderTransferObj MapSubheader(SubHeaderViewModel subHeaderViewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SubHeaderViewModel, SubHeaderTransferObj>();
            });

            IMapper mapper = config.CreateMapper();
            var source = subHeaderViewModel;
            var final = mapper.Map<SubHeaderViewModel, SubHeaderTransferObj>(source);
            return final;
        }
    }
}
