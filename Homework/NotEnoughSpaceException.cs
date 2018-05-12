using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class NotEnoughSpaceException : Exception
    {
        public NotEnoughSpaceException()
        {
            
        }

        public NotEnoughSpaceException(string message) : base(message)
        {
        }

        public NotEnoughSpaceException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
