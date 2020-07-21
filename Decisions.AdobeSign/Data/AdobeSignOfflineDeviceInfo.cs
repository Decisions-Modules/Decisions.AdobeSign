using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.AdobeSign
{
    [DataContract]
    public class AdobeSignOfflineDeviceInfo
    {
        /// <summary>
        /// Application Description
        /// </summary>
        [DataMember]
        public string ApplicationDescription { get; set; }

        /// <summary>
        /// Device  Description
        /// </summary>
        [DataMember]
        public string DeviceDescription { get; set; }

        /// <summary>
        /// The device local time. The device time provided should not be before 30 days of current date.
        /// </summary>
        [DataMember]
        public DateTime? DeviceTime { get; set; }
    }
}
