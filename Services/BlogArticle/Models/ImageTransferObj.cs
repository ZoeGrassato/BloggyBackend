using System;
using System.Collections.Generic;
using System.Text;

namespace Services.BlogArticle.Models
{
    public class ImageTransferObj
    {
        public byte[] BytesImages { get; set; }
        public Guid ImageId { get; set; }
        public Guid SectionId { get; set; }
    }
}
