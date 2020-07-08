using System;
using System.Collections.Generic;
using System.Text;

namespace AdobeSign.ClassLibrary
{
    public class AdobeSignConnection
    {
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectURL { get; set; }
        public string LoginURL { get; set; }
        public string Code { get; set; }
    }
}
