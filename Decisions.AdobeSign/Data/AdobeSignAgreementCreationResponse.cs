using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.AdobeSign
{
    //[DataContract]
    internal class AdobeSignAgreementCreationResponse
    {
        /// <summary>
        /// The unique identifier of the agreement
        /// </summary>
        /// <value>The unique identifier of the agreement</value>
        public string Id { get; set; }

    }
}
