using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class WrongTypeOfCarException : Exception
    {
        public WrongTypeOfCarException()
        {
        }

        public WrongTypeOfCarException(string message) : base(message)
        {
        }

        public WrongTypeOfCarException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongTypeOfCarException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
