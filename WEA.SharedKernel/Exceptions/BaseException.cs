using System;
using System.Collections.Generic;
using System.Text;

namespace WEA.SharedKernel.Exceptions
{
    public abstract class BaseException : Exception
    {

        public DateTime ErrorTime { get; set; }
        public string ErrorCause { get; set; }

        protected BaseException()
        {
        }

        protected BaseException(string message, string cause)
            : base(message)
        {
            ErrorTime = DateTime.Now;
            ErrorCause = cause;
        }

        protected BaseException(string message, string cause, DateTime time)
            : base(message)
        {
            ErrorTime = time;
            ErrorCause = cause;
        }

        protected BaseException(string message) : base(message)
        {
        }

        protected BaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BaseException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
