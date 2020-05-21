using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Section
    {
        public Guid SectionId { get; set; }
        public Header Header { get; set; }
        public SubHeader SubHeader { get; set; }
        public List<Paragraph> Paragraphs { get; set; }
        public List<Image> Images { get; set; }
    }
}
