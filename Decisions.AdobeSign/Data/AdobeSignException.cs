using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.AdobeSign.Data
{
    internal class AdobeSignException : Exception
    {
        public HttpStatusCode? HttpErrorCode { get; set; }
        public AdobeSignException(string message, HttpStatusCode? httpStatus = null) : base(message)
        {
            HttpErrorCode = httpStatus;
        }
    }
}
