using System;
using System.Collections.Generic;
using System.Text;

namespace Services.BlogArticle.Models
{
    public class BlogArticleObj
    {
        public Guid BlogArticleId { get; set; }
        public List<Section> Sections { get; set; }
        public string Title { get; set; }
    }
}
