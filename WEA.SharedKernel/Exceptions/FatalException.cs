using System;
using System.Collections.Generic;
using System.Text;
using WEA.SharedKernel.Resources;

namespace WEA.SharedKernel.Exceptions
{
    public class FatalException : BaseException
    {
        public FatalException(Exception sourceException)
            : base(ExceptionMessages.FatalError)
        {
            this.SourceException = sourceException;
            this.ErrorCause = ExceptionMessages.FatalError;
        }

        public Exception SourceException { get; }
    }
}
