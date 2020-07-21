using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.AdobeSign
{
    [DataContract]
    public class AdobeSignErrorInfo
    {
        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public HttpStatusCode? HttpErrorCode { get; set; }

        internal static AdobeSignErrorInfo FromException(Exception ex)
        {
            return new AdobeSignErrorInfo()
            {
                ErrorMessage = (ex.Message ?? ex.ToString()),
                HttpErrorCode = (ex as AdobeSignException)?.HttpErrorCode
            };
        }

        override public String ToString()
        {
            if (HttpErrorCode == null) return ErrorMessage;
            else return ErrorMessage + "\nHttpErrorCode = " + HttpErrorCode;
        }
    }

    internal class AdobeSignException : Exception
    {
        public HttpStatusCode? HttpErrorCode { get; set; }
        public AdobeSignException(string message, HttpStatusCode? httpStatus = null) : base(message)
        {
            HttpErrorCode = httpStatus;
        }
    }
}
