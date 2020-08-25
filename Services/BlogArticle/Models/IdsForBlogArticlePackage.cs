using System;
using System.Collections.Generic;
using System.Text;

namespace Services.BlogArticle.Models
{
    public class IdsForBlogArticlePackage
    {
        public Guid BlogArticleId { get; set; }
        public List<Guid> SectionIds { get; set; }
        public Dictionary<Guid,List<Guid>> ParagraphIds { get; set; } // the first Guid is the section ID for the paragraph, and the second is the actual paragraph ID
        public Dictionary<Guid, List<Guid>> ImageIds { get; set; }  // the first Guid is the section ID for the image, and the second is the actual image ID
    }
}
