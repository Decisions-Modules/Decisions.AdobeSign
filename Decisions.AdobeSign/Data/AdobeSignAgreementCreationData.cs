using DecisionsFramework.Design.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.AdobeSign
{
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AdobeSignAgreementCreationType { Simplified, Full}

    [DataContract]
    public class AdobeSignAgreementCreationData
    {
        [DataMember]
        [PropertyClassification(2, "Agreement Info Type", "Settings") ]
        public AdobeSignAgreementCreationType InfoType { get; set; } = AdobeSignAgreementCreationType.Simplified;

        [DataMember]
        [PropertyClassification(0, "Agreement Name", "Settings")]
        public string AgreementName { get; set; }

        [DataMember]
        [PropertyClassification(0, "Agreement File Path", "Settings")]
        public string FilePath { get; set; }

        [DataMember]
        [PropertyHiddenByValue(nameof(InfoType), AdobeSignAgreementCreationType.Simplified, false)]
        [PropertyClassification(0, "Agreement Recipients", "Settings")]
        public AdobeSignRecipient[] Recipients { get; set; }

        [DataMember]
        [PropertyHiddenByValue(nameof(InfoType), AdobeSignAgreementCreationType.Full, false)]
        [PropertyClassification(0, "Agreement Info", "Settings")]
        public AdobeSignAgreementInfo FullAgreementInfo { get; set; }

    }

    [DataContract]
    public class AdobeSignRecipient
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Email { get; set; }
    }
}
