using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Header : SharedTextInfo
    {
        public string HeaderText { get; set; }
        public bool IsUnderlined { get; set; }
        public bool IsItalic { get; set; }
    }
}
