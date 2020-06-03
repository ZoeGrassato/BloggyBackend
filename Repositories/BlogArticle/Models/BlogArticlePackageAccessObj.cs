using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.BlogArticle.Models
{
    public class BlogArticlePackageAccessObj
    {
        public List<SectionAccessObj> Sections { get; set; }
        public List<ParagraphAccessObj> Paragraphs { get; set; }
        public List<ImageAccessObj> Images { get; set; }
        public List<BlogArticleAccessObj> BlogArticles { get; set; }
    }
}
