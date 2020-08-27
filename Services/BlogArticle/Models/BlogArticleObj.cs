using System;
using System.Collections.Generic;
using System.Text;

namespace Services.BlogArticle.Models
{
    public class BlogArticleObj
    {
        public Guid ArticleId { get; set; }
        public List<Section> Sections = new List<Section>();
        public string Title { get; set; }
    }
}
