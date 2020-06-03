using Services.BlogArticle.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.BlogArticle.Models
{
    public class HeaderTransferObj : SharedTextInfoTransferObj
    {
        public string HeaderText { get; set; }
        public bool IsUnderlined { get; set; }
        public bool IsItalic { get; set; }
    }
}
