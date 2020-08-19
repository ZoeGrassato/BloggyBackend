using System;
using System.Collections.Generic;
using System.Text;

namespace Services.BlogArticle.Models
{
    public class UpdateBlogArticle
    {
        public Guid BlogArticleId { get; set; }
        public List<Section> Sections { get; set; }
        public string Title { get; set; }
        public bool HasParagraphChanged { get; set; }
        public bool HasSectionChanged { get; set; }
        public bool HasTitleChanged { get; set; }
        public bool HasImageChanged { get; set; }
    }
}
