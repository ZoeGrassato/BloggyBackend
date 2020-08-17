using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.BlogArticle.Models
{
    public class GetAllSectionsAccessObject
    {
        public Guid BlogId { get; set; }
        public Guid SectionId { get; set; }
        public string Header { get; set; }
        public string Subheader { get; set; }
        public List<ParagraphAccessObj> Paragraphs { get; set; }
        public List<ImageAccessObj> Images { get; set; }
    }
}
