﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggy.Backend.Models
{
    public class SectionViewModel
    {
        public Guid SectionId { get; set; }
        public HeaderViewModel Header { get; set; }
        public SubHeaderViewModel SubHeader { get; set; }
        public List<ParagraphViewModel> Paragraphs { get; set; }
        public List<ImageViewModel> Images { get; set; }
        public Guid BlogId { get; set; }
    }
}
