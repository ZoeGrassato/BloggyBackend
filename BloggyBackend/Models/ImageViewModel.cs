using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggyBackend.Models
{
    public class ImageViewModel
    {
        public byte[] BytesImages { get; set; }
        public Guid ImageId { get; set; }
        public Guid SectionId { get; set; }
    }
}
