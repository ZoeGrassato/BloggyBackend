using System;
using System.Collections.Generic;
using System.Text;

namespace Services.BlogArticle.Models
{
    public class BlogArticlePackageTransferObj
    {
        //public List<SectionTransferObj> Sections { get; set; }
        //public List<ParagraphTransferObj> Paragraphs { get; set; }
        //public List<ImageTransferObj> Images { get; set; }
        public List<BlogArticleTransferObj> BlogArticles { get; set; }
    }
}
