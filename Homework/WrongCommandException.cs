using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class WrongCommandException : Exception
    {
        public WrongCommandException()
        {
        }

        public WrongCommandException(string message) : base(message)
        {
        }

        public WrongCommandException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
