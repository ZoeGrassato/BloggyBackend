using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.BlogArticle.Models
{
    public class SectionAccessObj
    {
        public Guid BlogId { get; set; }
        public Guid SectionId { get; set; }
        public HeaderAccessObj Header { get; set; }
        public SubHeaderAccessObj SubHeader { get; set; }
        public List<ParagraphAccessObj> Paragraphs { get; set; }
        public List<ImageAccessObj> Images { get; set; }
    }
}
