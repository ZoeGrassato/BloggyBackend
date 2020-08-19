using System;
using System.Collections.Generic;
using System.Text;

namespace Services.BlogArticle.Models.JsonMappingModels
{
    public class BlogArticleJson
    {
        public Guid BlogArticleId { get; set; }
        public string Title { get; set; }
    }
}
