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
        private BlogArticleTransferObj responseArticle;
        private BlogArticleTransferObj submissionArticle;

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

            responseArticle = JsonConvert.DeserializeObject<BlogArticleTransferObj>(response.Content.ReadAsStringAsync().Result);

            Assert.IsNotNull(responseArticle.ArticleId);
            Assert.IsNotNull(responseArticle);
            Assert.AreEqual(title, responseArticle.Title);

            TestingContext.BlogArticleId = responseArticle.ArticleId;
            TestingContext.SectionId = responseArticle.Sections[0].SectionId; //we only want to update the first section
            TestingContext.ParagraphId = responseArticle.Sections[0].Paragraphs[0].ParagraphId; // and we only want to update the first paragraph

            var allItems = "http://localhost:5000".AppendPathSegments("api", "v1", "blog-articles").GetJsonAsync<BlogArticlePackageTransferObj>().Result;

            Assert.IsNotNull(allItems);
            Assert.IsNotNull(allItems.BlogArticles.SingleOrDefault(x => x.ArticleId == TestingContext.BlogArticleId));

            TestingContext.AllBlogsObject = allItems;
        }

        [Then("My blog article has (\\d*) sections with (\\d*) images and (\\d*) paragraphs each")]
        public void BlogArticleHasCorrectSizedData(int sectionCount, int imageCount, int paragraphCount)
        {
            Assert.IsNotNull(responseArticle);
            Assert.IsNotNull(responseArticle.Sections);
            Assert.AreEqual(sectionCount, responseArticle.Sections.Count);

            foreach (var section in responseArticle.Sections)
            {
                Assert.AreEqual(paragraphCount, section.Paragraphs.Count);
            }
        }

        [Then("My blog article has the correct data")]
        public void MyBlogArticleHasTheCorrectData()
        {
            Assert.IsNotNull(responseArticle);
            Assert.IsFalse(string.IsNullOrWhiteSpace(responseArticle.ArticleId.ToString()));

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
        [Given("I have a blog article with a blogArticle id and a sectionId")]
        public void GivenIHaveABlogArticle()
        {
            Assert.IsNotNull(TestingContext.AllBlogsObject);
            Assert.AreNotEqual(TestingContext.AllBlogsObject.BlogArticles.Count, 0);
          
            var currentBlogArticle = TestingContext.AllBlogsObject.BlogArticles.SingleOrDefault(x => x.ArticleId == TestingContext.BlogArticleId);
            var currentSections = currentBlogArticle.Sections;

            Assert.IsNotNull(currentBlogArticle);
            Assert.IsNotNull(currentBlogArticle.Sections);
        }

        [When("I update the blog article with the blogArticle id and section id and paragraphId set paragraphTextArea to (.*)")]
        public void WhenIUpdateABlogArticle(string paragraphTextArea) 
        {
            var model = BlogArticleUtils.CreateCustomBlogArticle(TestingContext.BlogArticleId, TestingContext.SectionId, TestingContext.ParagraphId, paragraphTextArea);
            response = "http://localhost:5000".AppendPathSegments("api", "v1", "blog-articles").PutJsonAsync(model).Result;
        }

        [Then("The blog article with the blogArticleId and sectionId and paragraphId should reflect the updated info with paragraphTextArea set to (.*)")]
        public void ThenTheBlogArticleShouldReflectTheUpdate(string paragraphTextArea)
        {
            //after we have updated a record get an updated model of what getall looks like
            TestingContext.AllBlogsObject = "http://localhost:5000".AppendPathSegments("api", "v1", "blog-articles").GetJsonAsync<BlogArticlePackageTransferObj>().Result;

            var currentItem = JsonConvert.DeserializeObject<UpdateBlogArticleTransferObj>(response.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(currentItem.ArticleId, TestingContext.BlogArticleId);

            var currentBlogArticle = TestingContext.AllBlogsObject.BlogArticles.SingleOrDefault(x => x.ArticleId == TestingContext.BlogArticleId);
            var currentSection = currentBlogArticle.Sections.SingleOrDefault(x => x.Paragraphs.Any(x  => x.ParagraphId == TestingContext.ParagraphId));

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(paragraphTextArea, currentSection.Paragraphs.SingleOrDefault(x => x.ParagraphId == TestingContext.ParagraphId).ParagraphTextArea);
        }


        //DELETE blog article
        [Given("I have a blog article with a blogArticleId")]
        public void GivenIHaveABlogArticleToDelete()
        {
            TestingContext.AllBlogsObject = "http://localhost:5000".AppendPathSegments("api", "v1", "blog-articles").GetJsonAsync<BlogArticlePackageTransferObj>().Result;
            var currentBlogArticle = TestingContext.AllBlogsObject.BlogArticles.SingleOrDefault(x => x.ArticleId == TestingContext.BlogArticleId);

            Assert.IsNotNull(currentBlogArticle);
        }

        [When("I delete the blog article with the blogArticleId")]
        public void WhenIDeleteTheBlogArticle()
        {
            response = "http://localhost:5000".AppendPathSegments("api", "v1", "blog-articles", TestingContext.BlogArticleId.ToString()).DeleteAsync().Result;

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);

            TestingContext.AllBlogsObject = "http://localhost:5000".AppendPathSegments("api", "v1", "blog-articles").GetJsonAsync<BlogArticlePackageTransferObj>().Result;
            //var currentBlogArticle = TestingContext.AllBlogsObject.BlogArticles.SingleOrDefault(x => x.ArticleId == TestingContext.BlogArticleId);

            ////ensure entry does not exist
            //Assert.IsNull(currentBlogArticle);
        }
    }
}
