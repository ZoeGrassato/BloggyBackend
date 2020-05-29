using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggyBackend.Models
{
    public class ParagraphViewModel
    {
        public string ParagraphTextArea { get; set; }
        public Guid ParagraphId { get; set; }
        public Guid SectionId { get; set; }
    }
}
