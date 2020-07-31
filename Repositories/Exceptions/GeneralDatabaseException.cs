using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Exceptions
{
    public class GeneralDatabaseException : Exception
    {
        public GeneralDatabaseException(string message, Exception exception) : base(message, exception)
        {

        }

        public GeneralDatabaseException(string message): base(message)
        {

        }
    }
}
