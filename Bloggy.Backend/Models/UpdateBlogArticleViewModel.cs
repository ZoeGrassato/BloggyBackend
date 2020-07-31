using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggy.Backend.Models
{
    public class UpdateBlogArticleViewModel
    {
        public Guid ArticleId { get; set; }
        public List<SectionViewModel> Sections { get; set; }
        public string Title { get; set; }
        public bool HasParagraphChanged { get; set; }
        public bool HasSectionChanged { get; set; }
        public bool HasTitleChanged { get; set; }
        public bool HasImageChanged { get; set; }

        public bool Validate()
        {
            return String.IsNullOrEmpty(Title) || Sections.Count <= 0;
        }
    }
}
