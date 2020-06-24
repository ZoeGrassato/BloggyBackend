using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggy.Backend.Exceptions
{
    public class BloggyException : Exception
    {
        public BloggyException(string message, Exception exception) : base(message, exception)
        {

        }

        public BloggyException(string message) : base(message)
        {

        }
    }
}
