using System;
using System.Collections.Generic;
using System.Text;

namespace Services.BlogArticle.Models
{
    public class DeletionIdsSectionAndParagraph
    {
        public List<Guid> RelevantSectionIds { get; set; }
        public List<Guid> RelevantParagraphIds { get; set; }
    }
}
