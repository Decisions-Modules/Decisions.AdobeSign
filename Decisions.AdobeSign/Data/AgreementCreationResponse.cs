using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.AdobeSign.Data
{
    [DataContract]
    public class AgreementCreationResponse
    {
        /// <summary>
        /// The unique identifier of the agreement
        /// </summary>
        /// <value>The unique identifier of the agreement</value>
        [DataMember]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

    }
}
