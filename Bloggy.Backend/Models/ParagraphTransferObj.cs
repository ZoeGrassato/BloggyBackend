using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggy.Backend.Models
{
    public class ParagraphTransferObj
    {
        public string ParagraphTextArea { get; set; }
        public Guid ParagraphId { get; set; }
        public Guid SectionId { get; set; }
    }
}
