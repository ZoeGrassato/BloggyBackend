using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggy.Backend.Models
{
    public class ImageTransferObj
    {
        public byte[] BytesImages { get; set; }
        public Guid ImageId { get; set; }
        public Guid SectionId { get; set; }
    }
}
