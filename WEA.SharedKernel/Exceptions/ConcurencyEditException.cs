using System;
using System.Collections.Generic;
using System.Text;
using WEA.SharedKernel.Resources;

namespace WEA.SharedKernel.Exceptions
{
    public class ConcurencyEditException : BaseException
    {
        public ConcurencyEditException(Exception sourceException)
            : base(ExceptionMessages.ConcurencyEdit)
        {
            this.SourceException = sourceException;
            this.ErrorCause = ExceptionMessages.ConcurencyEdit;
        }

        public Exception SourceException { get; }
    }
}
