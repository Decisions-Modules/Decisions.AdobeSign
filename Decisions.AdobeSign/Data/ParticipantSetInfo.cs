using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.AdobeSign.Data
{
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AgreementParticipantRole { SIGNER, APPROVER, ACCEPTOR, CERTIFIED_RECIPIENT, FORM_FILLER, DELEGATE_TO_SIGNER, DELEGATE_TO_APPROVER, DELEGATE_TO_ACCEPTOR, DELEGATE_TO_CERTIFIED_RECIPIENT, DELEGATE_TO_FORM_FILLER, SHARE}

    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AuthenticationMethod {NONE, PASSWORD, PHONE, KBA, WEB_IDENTITY, ADOBE_SIGN, GOV_ID}

    [DataContract]
    public class ParticipantSetInfo
    {
        /// <summary>
        /// Array of ParticipantInfo objects, containing participant-specific data (e.g. email). All participants in the array belong to the same set
        /// </summary>
        /// <value>Array of ParticipantInfo objects, containing participant-specific data (e.g. email). All participants in the array belong to the same set</value>
        [DataMember]
        [JsonProperty(PropertyName = "memberInfos")]
        public ParticipantInfo[] MemberInfos { get; set; }

        /// <summary>
        /// Index indicating position at which signing group needs to sign. Signing group to sign at first place is assigned a 1 index. Different signingOrder specified in input should form a valid consecutive increasing sequence of integers. Otherwise signingOrder will be considered invalid. No signingOrder should be specified for SHARE role
        /// </summary>
        /// <value>Index indicating position at which signing group needs to sign. Signing group to sign at first place is assigned a 1 index. Different signingOrder specified in input should form a valid consecutive increasing sequence of integers. Otherwise signingOrder will be considered invalid. No signingOrder should be specified for SHARE role</value>
        [DataMember]
        [JsonProperty(PropertyName = "order")]
        public int? Order { get; set; }

        /// <summary>
        /// Role assumed by all participants in the set (signer, approver etc.)
        /// </summary>
        /// <value>Role assumed by all participants in the set (signer, approver etc.)</value>
        [DataMember]
        [JsonProperty(PropertyName = "role")]
        public AgreementParticipantRole Role { get; set; }

        /// <summary>
        /// The unique label of a participant set.<br>For custom workflows, label specified in the participation set should map it to the participation step in the custom workflow.
        /// </summary>
        /// <value>The unique label of a participant set.<br>For custom workflows, label specified in the participation set should map it to the participation step in the custom workflow.</value>
        [DataMember]
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        /// <summary>
        /// Name of the participant set (it can be empty, but needs not to be unique in a single agreement). Maximum no of characters in participant set name is restricted to 255
        /// </summary>
        /// <value>Name of the participant set (it can be empty, but needs not to be unique in a single agreement). Maximum no of characters in participant set name is restricted to 255</value>
        [DataMember]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Participant set's private message - all participants in the set will receive the same message
        /// </summary>
        /// <value>Participant set's private message - all participants in the set will receive the same message</value>
        [DataMember]
        [JsonProperty(PropertyName = "privateMessage")]
        public string PrivateMessage { get; set; }

        /// <summary>
        /// When you enable limited document visibility (documentVisibilityEnabled), you can specify which file (fileInfo) should be made visible to which specific participant set.<br>Specify one or more label values of a fileInfos element.<br>Each signer participant sets must contain at least one required signature field in at least one visible file included in this API call; if not a page with a signature field is automatically appended for any missing participant sets. If there is a possibility that one or more participant sets do not have a required signature field in the files included in the API call, all signer participant sets should include a special index value of '0' to make this automatically appended signature page visible to the signer. Not doing so may result in an error. For all other roles, you may omit this value to exclude this page.
        /// </summary>
        /// <value>When you enable limited document visibility (documentVisibilityEnabled), you can specify which file (fileInfo) should be made visible to which specific participant set.<br>Specify one or more label values of a fileInfos element.<br>Each signer participant sets must contain at least one required signature field in at least one visible file included in this API call; if not a page with a signature field is automatically appended for any missing participant sets. If there is a possibility that one or more participant sets do not have a required signature field in the files included in the API call, all signer participant sets should include a special index value of '0' to make this automatically appended signature page visible to the signer. Not doing so may result in an error. For all other roles, you may omit this value to exclude this page.</value>
        [DataMember]
        [JsonProperty(PropertyName = "visiblePages")]
        public string[] VisiblePages { get; set; }

    }

    [DataContract]
    public class ParticipantInfo
    {

        /// <summary>
        /// Email of the particpant. In case of creating new Agreements(POST/PUT), this is a required field. In case of GET, this is the required field and will always be returned unless it is a fax workflow( legacy agreements) that were created using fax as input
        /// </summary>
        /// <value>Email of the particpant. In case of creating new Agreements(POST/PUT), this is a required field. In case of GET, this is the required field and will always be returned unless it is a fax workflow( legacy agreements) that were created using fax as input</value>
        [DataMember]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Security options that apply to the participant
        /// </summary>
        /// <value>Security options that apply to the participant</value>
        [DataMember]
        [JsonProperty(PropertyName = "securityOption")]
        public ParticipantSecurityOption SecurityOption { get; set; }

    }

    [DataContract]
    public class ParticipantSecurityOption
    {
        /// <summary>
        /// The authentication method for the participants to have access to view and sign the document
        /// </summary>
        /// <value>The authentication method for the participants to have access to view and sign the document</value>
        [DataMember]
        [JsonProperty(PropertyName = "authenticationMethod")]
        public AuthenticationMethod AuthenticationMethod { get; set; }

        /// <summary>
        /// The password required for the participant to view and sign the document. Note that AdobeSign will never show this password to anyone, so you will need to separately communicate it to any relevant parties. The password will not be returned in GET call. In case of PUT call, password associated with Agreement resource will remain unchanged if no password is specified but authentication method is provided as PASSWORD
        /// </summary>
        /// <value>The password required for the participant to view and sign the document. Note that AdobeSign will never show this password to anyone, so you will need to separately communicate it to any relevant parties. The password will not be returned in GET call. In case of PUT call, password associated with Agreement resource will remain unchanged if no password is specified but authentication method is provided as PASSWORD</value>
        [DataMember]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        /// <summary>
        /// The phoneInfo required for the participant to view and sign the document
        /// </summary>
        /// <value>The phoneInfo required for the participant to view and sign the document</value>
        [DataMember]
        [JsonProperty(PropertyName = "phoneInfo")]
        public PhoneInfo PhoneInfo { get; set; }
    }

    [DataContract]
    public class PhoneInfo
    {
        /// <summary>
        /// The phone Info country code required for the participant to view and sign the document if authentication method is PHONE
        /// </summary>
        /// <value>The phone Info country code required for the participant to view and sign the document if authentication method is PHONE</value>
        [DataMember]
        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// The country ISO Alpha-2 code required for the participant to view and sign the document if authentication method is PHONE,
        /// </summary>
        [DataMember]
        [JsonProperty(PropertyName = "countryIsoCode ")]
        public string CountryIsoCode { get; set; }

        /// <summary>
        /// The phone number required for the participant to view and sign the document if authentication method is PHONE
        /// </summary>
        /// <value>The phone number required for the participant to view and sign the document if authentication method is PHONE</value>
        [DataMember]
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }
    }

}
