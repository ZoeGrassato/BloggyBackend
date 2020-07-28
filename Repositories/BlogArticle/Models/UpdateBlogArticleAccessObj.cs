﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.BlogArticle.Models
{
    public class UpdateBlogArticleAccessObj
    {
        public Guid ArticleId { get; set; }
        public List<SectionAccessObj> Sections { get; set; }
        public string Title { get; set; }
    }
}