using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggy.Backend.Models
{
    public class BlogArticleTransferObj
    {
        public Guid ArticleId { get; set; }
        public List<SectionTransferObj> Sections { get; set; }
        public string Title { get; set; }

        public bool Validate()
        {
            return String.IsNullOrEmpty(Title) || Sections.Count <= 0;
        }
    }
}
