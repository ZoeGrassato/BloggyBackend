﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Services.BlogArticle.Models.JsonMappingModels
{
    public class SectionJsonTransferObj
    {
        public Guid SectionId { get; set; }
        public string Header { get; set; }
        public string SubHeader { get; set; }
    }
}
