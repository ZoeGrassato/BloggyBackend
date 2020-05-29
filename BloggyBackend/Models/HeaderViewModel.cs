using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggyBackend.Models
{
    public class HeaderViewModel
    {
        public string HeaderText { get; set; }
        public bool IsUnderlined { get; set; }
        public bool IsItalic { get; set; }
    }
}
