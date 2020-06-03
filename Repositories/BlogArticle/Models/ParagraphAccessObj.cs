using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.BlogArticle.Models
{
    public class ParagraphAccessObj
    {
        public string ParagraphTextArea { get; set; }
        public Guid ParagraphId { get; set; }
        public Guid SectionId { get; set; }
    }
}
