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
                ArticleId = Guid.Empty,
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
                    SectionId = Guid.Empty,
                    BlogId = Guid.Empty,
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
                    ParagraphId = Guid.Empty,
                    ParagraphTextArea = paragraph.Generate(),
                    SectionId = Guid.Empty
                });
            }
            return paragraphs;
        }

        private static bool RandomBool()
        {
            var rand = new Random();
            return rand.Next(0, 2) == 0;
        }

        public static UpdateBlogArticleTransferObj CreateCustomBlogArticle(Guid blogArticleId, Guid sectionId, Guid paragraphId, string paragraphTextArea)
        {
            var paragraphs = new List<ParagraphTransferObj>()
            {
                new ParagraphTransferObj()
                {
                    ParagraphId = paragraphId,
                    ParagraphTextArea = paragraphTextArea,
                    SectionId = sectionId
                }
            };

            var sections = new List<SectionTransferObj>()
            {
                new SectionTransferObj()
                {
                    SectionId = sectionId,
                    BlogId = blogArticleId,
                    Paragraphs = paragraphs,
                    Images = new List<ImageTransferObj>()
                }
            };

            var final = new UpdateBlogArticleTransferObj()
            {
                ArticleId = blogArticleId,
                Sections = sections,
                HasTitleChanged = false,
                HasParagraphChanged = true,
                HasSectionChanged = true,
                Title = "Some title"
            };

            return final;
        }
    }
}
