using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggy.Backend.Models
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
