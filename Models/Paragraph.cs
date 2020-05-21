using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Paragraph : SharedTextInfo
    {
        public string ParagraphTextArea { get; set; }
        public Guid ParagraphId { get; set; }
        public Guid SectionId { get; set; }
    }
}
