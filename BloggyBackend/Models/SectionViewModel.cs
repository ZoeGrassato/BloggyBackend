using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggyBackend.Models
{
    public class SectionViewModel
    {
        public Guid SectionId { get; set; }
        public Header Header { get; set; }
        public SubHeader SubHeader { get; set; }
        public List<Paragraph> Paragraphs { get; set; }
        public List<Image> Images { get; set; }
    }
}
