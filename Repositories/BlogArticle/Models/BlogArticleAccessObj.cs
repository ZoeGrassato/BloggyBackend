using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.BlogArticle.Models
{
    public class BlogArticleAccessObj
    {
        public Guid BlogId { get; set; }
        public string Title { get; set; }
        public List<SectionAccessObj> Sections { get; set; }
    }
}
