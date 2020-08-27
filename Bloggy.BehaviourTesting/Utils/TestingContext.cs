using Bloggy.Backend.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloggy.BehaviourTesting.Utils
{
    //a class that maintains variables that are used in the test to perform neccesary checks (such as id's)
    public static class TestingContext
    {
        public static Guid BlogArticleId { get; set; }
        public static Guid SectionId { get; set; }
        public static Guid ParagraphId { get; set; }
        public static BlogArticlePackageTransferObj AllBlogsObject { get; set; }
    }
}
