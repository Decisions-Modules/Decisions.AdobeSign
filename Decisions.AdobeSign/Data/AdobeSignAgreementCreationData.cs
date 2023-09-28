using DecisionsFramework.Design.Properties;
using System.Runtime.Serialization;

namespace Decisions.AdobeSign
{
    [DataContract]
    public enum AdobeSignAgreementCreationType
    {
        [EnumMember] Simplified, 
        [EnumMember] Full
    }

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
        [PropertyClassification(1, "Agreement File Path", "Settings")]
        public string FilePath { get; set; }

        [DataMember]
        [PropertyHiddenByValue(nameof(InfoType), AdobeSignAgreementCreationType.Simplified, false)]
        [PropertyClassification(3, "Agreement Recipients", "Settings")]
        public AdobeSignRecipient[] Recipients { get; set; }

        [DataMember]
        [PropertyHiddenByValue(nameof(InfoType), AdobeSignAgreementCreationType.Full, false)]
        [PropertyClassification(4, "Agreement Info", "Settings")]
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
