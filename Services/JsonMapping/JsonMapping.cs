using Newtonsoft.Json;
using Services.BlogArticle.Models;
using Services.BlogArticle.Models.JsonMappingModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mapping
{
    public class JsonMapping
    {
        public SectionJsonTransferObj MapToSectionJson(SectionTransferObj section)
        {
            var final = new SectionJsonTransferObj
            {
                SectionId = section.SectionId,
                Header = JsonConvert.SerializeObject(section.Header),
                SubHeader = JsonConvert.SerializeObject(section.SubHeader)
            };
            return final;
        }
    }
}
