using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class BlogArticle
    {
        public Guid ArticleId { get; set; }
        public List<Section> Sections { get; set; }
        public string Title { get; set; }
    }
}
