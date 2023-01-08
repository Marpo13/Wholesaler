using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wholesaler.Frontend.Presentation.Exceptions
{
    public class InvalidDataProvidedException : Exception
    {

        public InvalidDataProvidedException(string message) : base(message)
        {

        }
    }
}
