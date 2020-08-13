using System;
using System.Collections.Generic;
using System.Text;

namespace Services.BlogArticle.Models
{
    public class SectionTransferObj
    {
        public Guid SectionId { get; set; }
        public HeaderTransferObj Header { get; set; }
        public SubHeaderTransferObj SubHeader { get; set; }
        public List<ParagraphTransferObj> Paragraphs { get; set; }
        public List<ImageTransferObj> Images { get; set; }
        public Guid BlogId { get; set; }
    }
}
