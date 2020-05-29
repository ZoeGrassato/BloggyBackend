using Models;
using Models.Mapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mapping
{
    public class JsonMapping
    {
        public BlogArticleJson MapToBlogArticleJson(BlogArticle blogArticle)
        {
            var final = new BlogArticleJson { BlogArticleId = blogArticle.ArticleId, Title = blogArticle.Title };
            return final;
        }

        public SectionJson MapToSectionJson(Section section)
        {
            var final = new SectionJson
            {
                SectionId = section.SectionId,
                Header = JsonConvert.SerializeObject(section.Header),
                SubHeader = JsonConvert.SerializeObject(section.SubHeader)
            };
            return final;
        }
    }
}
