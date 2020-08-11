using Bloggy.Backend.Models;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using System;
using System.Collections.Generic;

namespace Bloggy.BehaviourTesting.Utils
{
    public class BlogArticleUtils
    {
        public static BlogArticleViewModel Generate(string title, int sectionCount, int paragraphCount, int imageCount)
        {
            return new BlogArticleViewModel
            {
                ArticleId = Guid.Parse("0537b53f-67ab-4520-819d-394663934ddf"),
                Title = title,
                Sections = GenerateSections(sectionCount, paragraphCount, imageCount)
            };
        }

        private static List<SectionViewModel> GenerateSections(int count, int paragraphCount, int imageCount)
        {
            var headerText = RandomizerFactory.GetRandomizer(new FieldOptionsText());
            var subHeaderText = RandomizerFactory.GetRandomizer(new FieldOptionsText());
            var sections = new List<SectionViewModel>();
            for (var i = 0; i < count; i++)
            {
                sections.Add(new SectionViewModel
                {
                    SectionId = Guid.Parse("5f89a27c-676c-4575-a03f-de6f091a5fa5"),
                    BlogId = Guid.Parse("0537b53f-67ab-4520-819d-394663934ddf"),
                    Header = new HeaderViewModel
                    {
                        HeaderText = headerText.Generate(),
                        IsItalic = RandomBool(),
                        IsUnderlined = RandomBool()
                    },
                    SubHeader = new SubHeaderViewModel
                    {
                        SubHeaderText = subHeaderText.Generate(),
                        IsItalic = RandomBool(),
                        IsUnderlined = RandomBool()
                    },
                    Images = GenerateImages(imageCount),
                    Paragraphs = GenerateParagraphs(paragraphCount)
                }); 
            }
            return sections;
        }

        private static List<ImageViewModel> GenerateImages(int count)
        {
            var imageBytes = RandomizerFactory.GetRandomizer(new FieldOptionsBytes());
            var images = new List<ImageViewModel>();
            for (var i = 0; i < count; i++)
            {
                images.Add(new ImageViewModel
                {
                    BytesImages = imageBytes.Generate()
                });
            }
            return images;
        }

        private static List<ParagraphViewModel> GenerateParagraphs(int count)
        {
            var paragraph = RandomizerFactory.GetRandomizer(new FieldOptionsTextWords());
            var paragraphs = new List<ParagraphViewModel>();
            for (var i = 0; i < count; i++)
            {
                paragraphs.Add(new ParagraphViewModel
                {
                    ParagraphId = Guid.Parse("8622825b-b0e1-4d96-8ee5-6d449fab2873"),
                    ParagraphTextArea = paragraph.Generate(),
                    SectionId = Guid.Parse("5f89a27c-676c-4575-a03f-de6f091a5fa5")
                });
            }
            return paragraphs;
        }

        private static bool RandomBool()
        {
            var rand = new Random();
            return rand.Next(0, 2) == 0;
        }

        public static UpdateBlogArticleViewModel CreateCustomBlogArticle(string blogArticleId, string sectionId, string paragraphId, string paragraphTextArea)
        {
            var paragraphs = new List<ParagraphViewModel>()
            {
                new ParagraphViewModel()
                {
                    ParagraphId = Guid.Parse(paragraphId),
                    ParagraphTextArea = paragraphTextArea,
                    SectionId = Guid.Parse(sectionId)
                }
            };

            var sections = new List<SectionViewModel>()
            {
                new SectionViewModel()
                {
                    SectionId = Guid.Parse(sectionId),
                    BlogId = Guid.Parse(blogArticleId),
                    Paragraphs = paragraphs
                }
            };

            var final = new UpdateBlogArticleViewModel()
            {
                ArticleId = Guid.Parse(blogArticleId),
                Sections = sections,
                HasTitleChanged = false,
                HasParagraphChanged = true,
                HasSectionChanged = true
            };

            return final;
        }
    }
}
