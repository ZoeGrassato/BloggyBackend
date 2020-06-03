using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.BlogArticle.Models
{
    public class ImageAccessObj
    {
        public byte[] BytesImages { get; set; }
        public Guid ImageId { get; set; }
        public Guid SectionId { get; set; }
    }
}
