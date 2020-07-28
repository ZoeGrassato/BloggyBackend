using System;
using System.Collections.Generic;
using System.Text;

namespace Services.BlogArticle.Models
{
    public class UpdateBlogArticleTransferObj
    {
        public Guid BlogArticleId { get; set; }
        public List<SectionTransferObj> Sections { get; set; }
        public string Title { get; set; }
    }
}
