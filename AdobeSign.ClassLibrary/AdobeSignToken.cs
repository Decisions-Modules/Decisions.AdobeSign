using System;
using System.Collections.Generic;
using System.Text;

namespace AdobeSign.ClassLibrary
{
    public class AdobeSignToken
    {
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpiration { get; set; }
        public string ApiAccessPoint { get; set; }
    }
}
