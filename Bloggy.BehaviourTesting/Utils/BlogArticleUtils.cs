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
                    ParagraphTextArea = paragraph.Generate()
                });
            }
            return paragraphs;
        }

        private static bool RandomBool()
        {
            var rand = new Random();
            return rand.Next(0, 2) == 0;
        }
    }
}
