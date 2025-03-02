using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CRMBilling.Core.Utils.Enums;

namespace CRMBilling.Core.Utils.ErrorHandling
{
    public class ApplicationException : Exception
    {
        public string ErrorCode { get; }
        public ErrorSeverity Severity { get; }

        public ApplicationException(string message, string errorCode, ErrorSeverity severity, Exception innerException = null)
          : base(message, innerException)
        {
            ErrorCode = errorCode;
            Severity = severity;
        }
    }
}
