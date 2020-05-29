using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggyBackend.Models
{
    public class BlogArticleViewModel
    {
        public Guid ArticleId { get; set; }
        public List<Section> Sections { get; set; }
        public string Title { get; set; }
    }
}
