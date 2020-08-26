using Bloggy.Backend.Models;
using Bloggy.BehaviourTesting.Utils;
using Flurl;
using Flurl.Http;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Services.BlogArticle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace Bloggy.BehaviourTesting.StepDefinitions
{
    [Binding]
    public class BlogArticleStepDefinitions
    {
        //all http requests are handled by the extension flurl

        private HttpResponseMessage response;
        private BlogArticleObj responseArticle;
        private BlogArticleTransferObj submissionArticle;
        private BlogArticlePackage allItems;

        //CREATE blog article
        [When("I submit a blog article with the title (.*) and (\\d*) sections with (\\d*) images and (\\d*) paragraphs each")]
        public void WhenISubmitABlogArticle(string title, int sectionCount, int imageCount, int paragraphCount)
        {
            submissionArticle = BlogArticleUtils.Generate(title, sectionCount, imageCount, paragraphCount);
            response = "http://localhost:5000".AppendPathSegments("api", "v1", "blog-articles").PostJsonAsync(submissionArticle).Result;
        }

        [Then("My blog article should exist within bloggy with the title (.*)")]
        public void MyBlogArticleShouldExistWithinBloggyAsync(string title)
        {
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            responseArticle = JsonConvert.DeserializeObject<BlogArticleObj>(response.Content.ReadAsStringAsync().Result);

            Assert.IsNotNull(responseArticle.BlogArticleId);
            Assert.IsNotNull(responseArticle);
            Assert.AreEqual(title, responseArticle.Title);

            TestingContext.BlogArticleId = responseArticle.BlogArticleId;
        }

        [Then("My blog article has (\\d*) sections with (\\d*) images and (\\d*) paragraphs each")]
        public void BlogArticleHasCorrectSizedData(int sectionCount, int imageCount, int paragraphCount)
        {
            Assert.IsNotNull(responseArticle);
            Assert.IsNotNull(responseArticle.Sections);
            Assert.AreEqual(sectionCount, responseArticle.Sections.Count);

            foreach (var section in responseArticle.Sections)
            {
                Assert.AreEqual(imageCount, section.Images.Count);
                Assert.AreEqual(paragraphCount, section.Paragraphs.Count);
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


        //UPDATE blog article
        [Given("I have a blog article with the id (.*) and sectionId (.*)")]
        public void GivenIHaveABlogArticle(string blogArticleId, string sectionId)
        {
            allItems = "http://localhost:5000".AppendPathSegments("api", "v1", "blog-articles").GetJsonAsync<BlogArticlePackage>().Result;

            Assert.IsNotNull(allItems);
            Assert.AreNotEqual(allItems.BlogArticles.Count, 0);
          
            var currentBlogArticle = allItems.BlogArticles.SingleOrDefault(x => x.BlogArticleId == TestingContext.BlogArticleId);
            var currentSections = currentBlogArticle.Sections;

            Assert.IsNotNull(currentBlogArticle);
            Assert.IsNotNull(currentBlogArticle.Sections);
        }

        [When("I update the blog article with id (.*) and sectionId (.*) and paragraphId (.*) and set paragraphTextArea to (.*)")]
        public void WhenIUpdateABlogArticle(string blogArticleId, string sectionId, string paragraphId, string paragraphTextArea) 
        {
            var model = BlogArticleUtils.CreateCustomBlogArticle(blogArticleId, sectionId, paragraphId, paragraphTextArea);
            response = "http://localhost:5000".AppendPathSegments("api", "v1", "blog-articles").PutJsonAsync(model).Result;
        }

        [Then("The blog article with id (.*) and sectionId(.*) and paragraphId (.*) should reflect the updated info with paragraphTextArea set to (.*)")]
        public void ThenTheBlogArticleShouldReflectTheUpdate( string sectionId, string paragraphId, string paragraphTextArea)
        {
            response = "http://localhost:5000".AppendPathSegments("api", "v1", "blog-articles").GetJsonAsync().Result;
            allItems = JsonConvert.DeserializeObject<BlogArticlePackage>(response.Content.ReadAsStringAsync().Result);

            var currentBlogArticle = allItems.BlogArticles.SingleOrDefault(x => x.BlogArticleId == TestingContext.BlogArticleId);
            var currentSection = currentBlogArticle.Sections.SingleOrDefault(x => x.Paragraphs.Any(x  => x.ParagraphId == Guid.Parse(paragraphId)));

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(paragraphTextArea, currentSection.Paragraphs.SingleOrDefault(x => x.ParagraphId == Guid.Parse(paragraphId)).ParagraphTextArea);
        }
    }
}
