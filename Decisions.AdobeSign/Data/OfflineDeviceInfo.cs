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
    public class OfflineDeviceInfo
    {
        /// <summary>
        /// Application Description
        /// </summary>
        [DataMember]
        [JsonProperty(PropertyName = "applicationDescription")]
        public string ApplicationDescription { get; set; }

        /// <summary>
        /// Device  Description
        /// </summary>
        [DataMember]
        [JsonProperty(PropertyName = "deviceDescription ")]
        public string DeviceDescription { get; set; }

        /// <summary>
        /// The device local time. The device time provided should not be before 30 days of current date.
        /// </summary>
        [DataMember]
        [JsonProperty(PropertyName = "deviceTime")]
        public DateTime? DeviceTime { get; set; }
    }
}
