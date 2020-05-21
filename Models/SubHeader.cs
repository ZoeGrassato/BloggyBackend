using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class SubHeader : SharedTextInfo
    {
        public string SubHeaderText { get; set; }
        public bool IsUnderlined { get; set; }
        public bool IsItalic { get; set; }
    }
}
