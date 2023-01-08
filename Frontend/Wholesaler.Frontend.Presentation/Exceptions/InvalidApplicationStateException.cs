using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wholesaler.Frontend.Presentation.Exceptions
{
    internal class InvalidApplicationStateException : Exception
    {
        public InvalidApplicationStateException(string message) 
            : base(message)
        {

        }
    }
}
