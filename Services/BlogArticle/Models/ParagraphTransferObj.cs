﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Services.BlogArticle.Models
{
    public class ParagraphTransferObj
    {
        public string ParagraphTextArea { get; set; }
        public Guid ParagraphId { get; set; }
        public Guid SectionId { get; set; }
    }
}
