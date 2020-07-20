using Bloggy.Backend.Models;
using Bloggy.BehaviourTesting.Utils;
using Flurl;
using Flurl.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Services.BlogArticle.Models;
using System.Linq;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace Bloggy.BehaviourTesting.StepDefinitions
{
    [Binding]
    public class BlogArticleStepDefinitions
    {
        private HttpResponseMessage response;
        private BlogArticleTransferObj responseArticle;
        private BlogArticleViewModel submissionArticle;

        [When("I submit a blog article with the title (.*) and (\\d*) sections with (\\d*) images and (\\d*) paragraphs each")]
        public async void WhenISubmitABlogArticle(string title, int sectionCount, int imageCount, int paragraphCount)
        {
            submissionArticle = BlogArticleUtils.Generate(title, sectionCount, imageCount, paragraphCount);
            response = await "http://localhost:5000/".AppendPathSegments("api", "v1", "blog-article").PostJsonAsync(submissionArticle);

        }

        [Then("My blog article should exist within bloggy with the title (.*)")]
        public async void MyBlogArticleShouldExistWithinBloggyAsync(string title)
        {
            Assert.AreEqual(200, response.StatusCode);

            responseArticle = JsonConvert.DeserializeObject<BlogArticleTransferObj>(await response.Content.ReadAsStringAsync());

            Assert.IsNotNull(responseArticle);
            Assert.AreEqual(title, responseArticle.Title);
        }

        [Then("My blog article has (\\d*) sections with (\\d*) images and (\\d*) paragraphs each")]
        public void BlogArticleHasCorrectSizedData(int sectionCount, int imageCount, int paragraphCount)
        {
            Assert.IsNotNull(responseArticle);
            Assert.IsNotNull(responseArticle.Sections);
            Assert.AreEqual(sectionCount, responseArticle.Sections.Capacity);

            foreach (var section in responseArticle.Sections)
            {
                Assert.AreEqual(imageCount, section.Images.Capacity);
                Assert.AreEqual(paragraphCount, section.Paragraphs.Capacity);
            }
        }

        [Then("My blog article has the correct data")]
        public void MyBlogArticleHasTheCorrectData()
        {
            Assert.IsNotNull(responseArticle);
            Assert.IsFalse(string.IsNullOrWhiteSpace(responseArticle.BlogArticleId.ToString()));

            foreach (var section in responseArticle.Sections)
            {
                Assert.IsFalse(string.IsNullOrWhiteSpace(section.SectionId.ToString()));

                Assert.IsNotNull(section.Header);
                Assert.IsNotNull(section.SubHeader);

                Assert.IsFalse(string.IsNullOrWhiteSpace(section.Header.HeaderText));
                Assert.IsFalse(string.IsNullOrWhiteSpace(section.SubHeader.SubHeaderText));

                var headingDataExists = submissionArticle.Sections
                    .Select(section => section.Header.HeaderText)
                    .Contains(section.Header.HeaderText);

                var subHeadingDataExists = submissionArticle.Sections
                  .Select(section => section.SubHeader.SubHeaderText)
                  .Contains(section.SubHeader.SubHeaderText);

                foreach (var image in section.Images)
                {
                    Assert.IsFalse(string.IsNullOrWhiteSpace(image.SectionId.ToString()));
                    Assert.IsFalse(string.IsNullOrWhiteSpace(image.ImageId.ToString()));

                    Assert.IsTrue(image.BytesImages.Length > 0);
                }

                foreach (var paragraph in section.Paragraphs)
                {
                    Assert.IsFalse(string.IsNullOrWhiteSpace(paragraph.SectionId.ToString()));
                    Assert.IsFalse(string.IsNullOrWhiteSpace(paragraph.ParagraphId.ToString()));

                    Assert.IsFalse(string.IsNullOrWhiteSpace(paragraph.ParagraphTextArea));

                    var pargraphDataExists = submissionArticle.Sections
                        .SelectMany(section => section.Paragraphs)
                        .Select(paragraph => paragraph.ParagraphTextArea)
                        .Contains(paragraph.ParagraphTextArea);

                    Assert.IsTrue(pargraphDataExists);
                }
            }
        }
    }
}
