using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.AdobeSign
{

    internal class TransientDocumentResponse
    {
        /// <summary>
        /// The unique identifier of the uploaded document that can be used in an agreement or a megaSign or widget creation call
        /// </summary>
        /// <value>The unique identifier of the uploaded document that can be used in an agreement or a megaSign or widget creation call</value>
        /*[DataMember]
        [JsonProperty(PropertyName = "transientDocumentId")]*/
        public string TransientDocumentId { get; set; }

    }
}
