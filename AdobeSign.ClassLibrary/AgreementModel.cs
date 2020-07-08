using System;
using System.Collections.Generic;
using System.Text;

namespace AdobeSign.ClassLibrary
{
    public class AgreementModel
    {
        public string AgreementName { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }
        public List<Recipient> Recipients { get; set; }
    }

    public class Recipient
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Order { get; set; }
    }
}
