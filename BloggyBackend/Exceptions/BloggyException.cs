using System;

namespace BloggyBackend.Controllers
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