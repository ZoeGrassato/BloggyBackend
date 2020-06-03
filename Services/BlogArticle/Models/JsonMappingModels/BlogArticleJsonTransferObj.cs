using System;
using System.Collections.Generic;
using System.Text;

namespace Services.BlogArticle.Models.JsonMappingModels
{
    public class BlogArticleJsonTransferObj
    {
        public Guid BlogArticleId { get; set; }
        public string Title { get; set; }
    }
}
