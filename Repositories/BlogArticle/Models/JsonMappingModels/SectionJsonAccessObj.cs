using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.BlogArticle.Models.JsonMappingModels
{
    public class SectionJsonAccessObj
    {
        public Guid SectionId { get; set; }
        public string Header { get; set; }
        public string SubHeader { get; set; }
    }
}
