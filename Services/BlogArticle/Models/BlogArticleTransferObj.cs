using System;
using System.Collections.Generic;
using System.Text;

namespace Services.BlogArticle.Models
{
    public class BlogArticleTransferObj
    {
        public Guid ArticleId { get; set; }
        public List<SectionTransferObj> Sections { get; set; }
        public string Title { get; set; }
    }
}
