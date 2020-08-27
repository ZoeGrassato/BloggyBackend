using Bloggy.Backend.Models;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using System;
using System.Collections.Generic;

namespace Bloggy.BehaviourTesting.Utils
{
    // this class is dedicated to building a blog template for the testing engine to use while making http requests in order to conduct behaviour tests (dummy data). 
    public class BlogArticleUtils
    {
        public static BlogArticleTransferObj Generate(string title, int sectionCount, int paragraphCount, int imageCount)
        {
            return new BlogArticleTransferObj
            {
                ArticleId = Guid.Parse("0537b53f-67ab-4520-819d-394663934ddf"),
                Title = title,
                Sections = GenerateSections(sectionCount, paragraphCount, imageCount)
            };
        }

        private static List<SectionTransferObj> GenerateSections(int count, int paragraphCount, int imageCount)
        {
            var headerText = RandomizerFactory.GetRandomizer(new FieldOptionsText());
            var subHeaderText = RandomizerFactory.GetRandomizer(new FieldOptionsText());
            var sections = new List<SectionTransferObj>();
            for (var i = 0; i < count; i++)
            {
                sections.Add(new SectionTransferObj
                {
                    SectionId = Guid.Parse("5f89a27c-676c-4575-a03f-de6f091a5fa5"),
                    BlogId = Guid.Parse("0537b53f-67ab-4520-819d-394663934ddf"),
                    Header = new HeaderTransferObj
                    {
                        HeaderText = headerText.Generate(),
                        IsItalic = RandomBool(),
                        IsUnderlined = RandomBool()
                    },
                    SubHeader = new SubHeaderTransferObj
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

        private static List<ImageTransferObj> GenerateImages(int count)
        {
            var imageBytes = RandomizerFactory.GetRandomizer(new FieldOptionsBytes());
            var images = new List<ImageTransferObj>();
            for (var i = 0; i < count; i++)
            {
                images.Add(new ImageTransferObj
                {
                    BytesImages = imageBytes.Generate()
                });
            }
            return images;
        }

        private static List<ParagraphTransferObj> GenerateParagraphs(int count)
        {
            var paragraph = RandomizerFactory.GetRandomizer(new FieldOptionsTextWords());
            var paragraphs = new List<ParagraphTransferObj>();
            for (var i = 0; i < count; i++)
            {
                paragraphs.Add(new ParagraphTransferObj
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

        public static UpdateBlogArticleTransferObj CreateCustomBlogArticle(Guid blogArticleId, string sectionId, string paragraphId, string paragraphTextArea)
        {
            var paragraphs = new List<ParagraphTransferObj>()
            {
                new ParagraphTransferObj()
                {
                    ParagraphId = Guid.Parse(paragraphId),
                    ParagraphTextArea = paragraphTextArea,
                    SectionId = Guid.Parse(sectionId)
                }
            };

            var sections = new List<SectionTransferObj>()
            {
                new SectionTransferObj()
                {
                    SectionId = Guid.Parse(sectionId),
                    BlogId = blogArticleId,
                    Paragraphs = paragraphs
                }
            };

            var final = new UpdateBlogArticleTransferObj()
            {
                ArticleId = blogArticleId,
                Sections = sections,
                HasTitleChanged = false,
                HasParagraphChanged = true,
                HasSectionChanged = true
            };

            return final;
        }
    }
}
