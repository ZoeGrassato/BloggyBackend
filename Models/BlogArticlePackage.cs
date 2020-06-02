using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class BlogArticlePackage
    {
        public List<Section> Sections { get; set; }
        public List<Paragraph> Paragraphs { get; set; }
        public List<Image> Images { get; set; }
        public List<BlogArticle> BlogArticles { get; set; }
    }
}
