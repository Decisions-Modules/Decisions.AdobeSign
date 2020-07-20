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
    //https://secure.na1.echosign.com/public/docs/restapi/v6#!/agreements/createAgreement

    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AgreementSignatureType { ESIGN, WRITTEN }
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AgreementState { AUTHORING, DRAFT, IN_PROCESS }

    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AgreementReminderFrequency { DAILY_UNTIL_SIGNED, WEEKDAILY_UNTIL_SIGNED, EVERY_OTHER_DAY_UNTIL_SIGNED, EVERY_THIRD_DAY_UNTIL_SIGNED, EVERY_FIFTH_DAY_UNTIL_SIGNED, WEEKLY_UNTIL_SIGNED, ONCE }

    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AgreementStatus { OUT_FOR_SIGNATURE, OUT_FOR_DELIVERY, OUT_FOR_ACCEPTANCE, OUT_FOR_FORM_FILLING, OUT_FOR_APPROVAL, AUTHORING, CANCELLED, SIGNED, APPROVED, DELIVERED, ACCEPTED, FORM_FILLED, EXPIRED, ARCHIVED, PREFILL, WIDGET_WAITING_FOR_VERIFICATION, DRAFT, DOCUMENTS_NOT_YET_PROCESSED, WAITING_FOR_FAXIN, WAITING_FOR_VERIFICATION }

    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AgreementType { AGREEMENT, MEGASIGN_CHILD, WIDGET_INSTANCE }



    [DataContract]
    public class AgreementInfo
    {
        /// <summary>
        /// A list of one or more files (or references to files) that will be sent out for signature. If more than one file is provided, they will be combined into one PDF before being sent out. Note: Only one of the four parameters in every FileInfo object must be specified
        /// </summary>
        /// <value>A list of one or more files (or references to files) that will be sent out for signature. If more than one file is provided, they will be combined into one PDF before being sent out. Note: Only one of the four parameters in every FileInfo object must be specified</value>
        [DataMember]
        [JsonProperty(PropertyName = "fileInfos")]
        public FileInfo[] FileInfos { get; set; }

        /// <summary>
        /// The name of the agreement that will be used to identify it, in emails, website and other places
        /// </summary>
        /// <value>The name of the agreement that will be used to identify it, in emails, website and other places</value>
        [DataMember]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// A list of one or more participant set. A participant set may have one or more participant. If any member of the participant set takes the action that has been assigned to the set(Sign/Approve/Acknowledge etc ), the action is considered as the action taken by whole participation set. For regular (non-MegaSign) documents, there is no limit on the number of electronic signatures in a single document. Written signatures are limited to four per document
        /// </summary>
        /// <value>A list of one or more participant set. A participant set may have one or more participant. If any member of the participant set takes the action that has been assigned to the set(Sign/Approve/Acknowledge etc ), the action is considered as the action taken by whole participation set. For regular (non-MegaSign) documents, there is no limit on the number of electronic signatures in a single document. Written signatures are limited to four per document</value>
        [DataMember]
        [JsonProperty(PropertyName = "participantSetsInfo")]
        public ParticipantSetInfo[] ParticipantSetsInfo { get; set; }


        /// <summary>
        /// Specifies the type of signature you would like to request - written or e-signature. The possible values are <br> ESIGN : Agreement needs to be signed electronically <br>, WRITTEN : Agreement will be signed using handwritten signature and signed document will be uploaded into the system
        /// </summary>
        /// <value>Specifies the type of signature you would like to request - written or e-signature. The possible values are <br> ESIGN : Agreement needs to be signed electronically <br>, WRITTEN : Agreement will be signed using handwritten signature and signed document will be uploaded into the system</value>
        [DataMember(Name = "signatureType", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "signatureType")]
        public AgreementSignatureType SignatureType { get; set; }

        /// <summary>
        /// The state in which the agreement should land. The state field can only be provided in POST calls, will never get returned in GET /agreements/{ID} and will be ignored if provided in PUT /agreements/{ID} call. The eventual status of the agreement can be obtained from GET /agreements/ID
        /// </summary>
        /// <value>The state in which the agreement should land. The state field can only be provided in POST calls, will never get returned in GET /agreements/{ID} and will be ignored if provided in PUT /agreements/{ID} call. The eventual status of the agreement can be obtained from GET /agreements/ID</value>
        [DataMember]
        [JsonProperty(PropertyName = "state")]
        public AgreementState State { get; set; }

        /// <summary>
        /// A list of one or more CCs that will be copied in the agreement transaction. The CCs will each receive an email at the beginning of the transaction and also when the final document is signed. The email addresses will also receive a copy of the document, attached as a PDF file. Should not be provided in offline agreement creation.
        /// </summary>
        /// <value>A list of one or more CCs that will be copied in the agreement transaction. The CCs will each receive an email at the beginning of the transaction and also when the final document is signed. The email addresses will also receive a copy of the document, attached as a PDF file. Should not be provided in offline agreement creation.</value>
        [DataMember]
        [JsonProperty(PropertyName = "ccs")]
        public AgreementCcInfo[] Ccs { get; set; }

        /// <summary>
        /// Date when agreement was created. This is a server generated attributed and can not be provided in POST/PUT calls. Format would be yyyy-MM-dd'T'HH:mm:ssZ. For example, e.g 2016-02-25T18:46:19Z represents UTC time
        /// </summary>
        /// <value>Date when agreement was created. This is a server generated attributed and can not be provided in POST/PUT calls. Format would be yyyy-MM-dd'T'HH:mm:ssZ. For example, e.g 2016-02-25T18:46:19Z represents UTC time</value>
        [DataMember]
        [JsonProperty(PropertyName = "createdDate")]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Device info of the offline device. It should only be provided in case of offline agreement creation.
        /// </summary>
        /// <value>Device info of the offline device. It should only be provided in case of offline agreement creation.</value>
        [DataMember]
        [JsonProperty(PropertyName = "deviceInfo")]
        public OfflineDeviceInfo DeviceInfo { get; set; }


        /// <summary>
        /// If set to true, enable limited document visibility. Should not be provided in offline agreement creation.
        /// </summary>
        /// <value>If set to true, enable limited document visibility. Should not be provided in offline agreement creation.</value>
        [DataMember]
        [JsonProperty(PropertyName = "documentVisibilityEnabled")]
        public bool? DocumentVisibilityEnabled { get; set; }

        /// <summary>
        /// Email configurations for the agreement. Should not be provided in offline agreement creation.
        /// </summary>
        /// <value>Email configurations for the agreement. Should not be provided in offline agreement creation.</value>
        [DataMember]
        [JsonProperty(PropertyName = "emailOption")]
        public EmailOption EmailOption { get; set; }

        /// <summary>
        /// Time after which Agreement expires and needs to be signed before it. Format should be yyyy-MM-dd'T'HH:mm:ssZ. For example, e.g 2016-02-25T18:46:19Z represents UTC time. Should not be provided in offline agreement creation.
        /// </summary>
        /// <value>Time after which Agreement expires and needs to be signed before it. Format should be yyyy-MM-dd'T'HH:mm:ssZ. For example, e.g 2016-02-25T18:46:19Z represents UTC time. Should not be provided in offline agreement creation.</value>
        [DataMember]
        [JsonProperty(PropertyName = "expirationTime")]
        public DateTime? ExpirationTime { get; set; }

        /// <summary>
        /// An arbitrary value from your system, which can be specified at sending time and then later returned or queried. Should not be provided in offline agreement creation.
        /// </summary>
        /// <value>An arbitrary value from your system, which can be specified at sending time and then later returned or queried. Should not be provided in offline agreement creation.</value>
        [DataMember]
        [JsonProperty(PropertyName = "externalId")]
        public ExternalId ExternalId { get; set; }

        /// <summary>
        /// Integer which specifies the delay in hours before sending the first reminder.<br>This is an optional field. The minimum value allowed is 1 hour and the maximum value can’t be more than the difference of agreement creation and expiry time of the agreement in hours.<br>If this is not specified but the reminder frequency is specified, then the first reminder will be sent based on frequency.<br>i.e. if the reminder is created with frequency specified as daily, the firstReminderDelay will be 24 hours. Should not be provided in offline agreement creation.
        /// </summary>
        /// <value>Integer which specifies the delay in hours before sending the first reminder.<br>This is an optional field. The minimum value allowed is 1 hour and the maximum value can’t be more than the difference of agreement creation and expiry time of the agreement in hours.<br>If this is not specified but the reminder frequency is specified, then the first reminder will be sent based on frequency.<br>i.e. if the reminder is created with frequency specified as daily, the firstReminderDelay will be 24 hours. Should not be provided in offline agreement creation.</value>
        [DataMember]
        [JsonProperty(PropertyName = "firstReminderDelay")]
        public int? FirstReminderDelay { get; set; }

        /// <summary>
        /// Specifies the form field layer template or source of form fields to apply on the files in this transaction. If specified, the FileInfo for this parameter must refer to a form field layer template via libraryDocumentId or libraryDocumentName, or if specified via transientDocumentId or documentURL, it must be of a supported file type. Note: Only one of the four parameters in every FileInfo object must be specified
        /// </summary>
        /// <value>Specifies the form field layer template or source of form fields to apply on the files in this transaction. If specified, the FileInfo for this parameter must refer to a form field layer template via libraryDocumentId or libraryDocumentName, or if specified via transientDocumentId or documentURL, it must be of a supported file type. Note: Only one of the four parameters in every FileInfo object must be specified</value>
        [DataMember]
        [JsonProperty(PropertyName = "formFieldLayerTemplates")]
        public FileInfo[] FormFieldLayerTemplates { get; set; }

        /// <summary>
        /// The unique identifier of the group to which the agreement belongs to. If not provided during agreement creation, primary group of the creator will be used
        /// </summary>
        [DataMember]
        [JsonProperty(PropertyName = "groupId")]
        public string GroupId { get; set; }

        /// <summary>
        /// The unique identifier of the agreement.If provided in POST, it will simply be ignored
        /// </summary>
        /// <value>The unique identifier of the agreement.If provided in POST, it will simply be ignored</value>
        [DataMember]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        ///  The date of the last event that occurred for this agreement
        /// </summary>
        [DataMember]
        [JsonProperty(PropertyName = "lastEventDate ")]
        public DateTime? LastEventDate { get; set; }

        /// <summary>
        /// The locale associated with this agreement - specifies the language for the signing page and emails, for example en_US or fr_FR. If none specified, defaults to the language configured for the agreement sender
        /// </summary>
        /// <value>The locale associated with this agreement - specifies the language for the signing page and emails, for example en_US or fr_FR. If none specified, defaults to the language configured for the agreement sender</value>
        [DataMember]
        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; set; }

        /// <summary>
        /// Optional default values for fields to merge into the document. The values will be presented to the signers for editable fields; for read-only fields the provided values will not be editable during the signing process. Merging data into fields is currently not supported when used with libraryDocumentId or libraryDocumentName. Only file and url are currently supported
        /// </summary>
        /// <value>Optional default values for fields to merge into the document. The values will be presented to the signers for editable fields; for read-only fields the provided values will not be editable during the signing process. Merging data into fields is currently not supported when used with libraryDocumentId or libraryDocumentName. Only file and url are currently supported</value>
        [DataMember]
        [JsonProperty(PropertyName = "mergeFieldInfo")]
        public MergefieldInfo[] MergeFieldInfo { get; set; }

        /// <summary>
        /// An optional message to the participants, describing what is being sent or why their signature is required
        /// </summary>
        /// <value>An optional message to the participants, describing what is being sent or why their signature is required</value>
        [DataMember(Name = "message", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// URL and associated properties for the success page the user will be taken to after completing the signing process. Should not be provided in offline agreement creation.
        /// </summary>
        /// <value>URL and associated properties for the success page the user will be taken to after completing the signing process. Should not be provided in offline agreement creation.</value>
        [DataMember]
        [JsonProperty(PropertyName = "postSignOption")]
        public PostSignOption PostSignOption { get; set; }

        /// <summary>
        /// Optional parameter that sets how often you want to send reminders to the participants. If it is not specified, the default frequency set for the account will be used. Should not be provided in offline agreement creation. If provided in PUT as a different value than the current one, an error will be thrown.
        /// </summary>
        /// <value>Optional parameter that sets how often you want to send reminders to the participants. If it is not specified, the default frequency set for the account will be used. Should not be provided in offline agreement creation. If provided in PUT as a different value than the current one, an error will be thrown.</value>
        [DataMember]
        [JsonProperty(PropertyName = "reminderFrequency")]
        public AgreementReminderFrequency ReminderFrequency { get; set; }

        /// <summary>
        /// Optional secondary security parameters for the agreement. Should not be provided in offline agreement creation.
        /// </summary>
        /// <value>Optional secondary security parameters for the agreement. Should not be provided in offline agreement creation.</value>
        [DataMember]
        [JsonProperty(PropertyName = "securityOption")]
        public SecurityOption SecurityOption { get; set; }

        /// <summary>
        /// Email of agreement sender. Only provided in GET. Can not be provided in POST/PUT request. If provided in POST/PUT, it will be ignored
        /// </summary>
        /// <value>Email of agreement sender. Only provided in GET. Can not be provided in POST/PUT request. If provided in POST/PUT, it will be ignored</value>
        [DataMember]
        [JsonProperty(PropertyName = "senderEmail")]
        public string SenderEmail { get; set; }

        /// <summary>
        /// This is a server generated attribute which provides the detailed status of an agreement.
        /// </summary>
        /// <value>This is a server generated attribute which provides the detailed status of an agreement.</value>
        [DataMember]
        [JsonProperty(PropertyName = "status")]
        public AgreementStatus Status { get; set; }

        /// <summary>
        /// This is a server generated attribute which provides the detailed status of an agreement.
        /// </summary>
        /// <value>This is a server generated attribute which provides the detailed status of an agreement.</value>
        [DataMember]
        [JsonProperty(PropertyName = "type")]
        public AgreementType Type { get; set; }

        /// <summary>
        /// Vaulting properties that allows Adobe Sign to securely store documents with a vault provider
        /// </summary>
        /// <value>Vaulting properties that allows Adobe Sign to securely store documents with a vault provider</value>
        [JsonProperty(PropertyName = "vaultingInfo")]
        public VaultingInfo VaultingInfo { get; set; }

        /// <summary>
        /// The identifier of custom workflow which defines the routing path of an agreement. Should not be provided in offline agreement creation.
        /// </summary>
        /// <value>The identifier of custom workflow which defines the routing path of an agreement. Should not be provided in offline agreement creation.</value>
        [DataMember(Name = "workflowId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "workflowId")]
        public string WorkflowId { get; set; }

    }

    [DataContract]
    public class ExternalId
    {
        /// <summary>
        /// An arbitrary value from your system, which can be specified at sending time and then later returned or queried
        /// </summary>
        /// <value>An arbitrary value from your system, which can be specified at sending time and then later returned or queried</value>
        [DataMember]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

    }

    [DataContract]
    public class MergefieldInfo
    {
        /// <summary>
        /// The default value of the field
        /// </summary>
        /// <value>The default value of the field</value>
        [DataMember]
        [JsonProperty(PropertyName = "defaultValue")]
        public string DefaultValue { get; set; }

        /// <summary>
        /// The name of the field
        /// </summary>
        /// <value>The name of the field</value>
        [DataMember]
        [JsonProperty(PropertyName = "fieldName")]
        public string FieldName { get; set; }
    }

    [DataContract]
    public class PostSignOption
    {
        /// <summary>
        /// The delay (in seconds) before the user is taken to the success page. If this value is greater than 0, the user will first see the standard Adobe Sign success message, and then after a delay will be redirected to your success page
        /// </summary>
        /// <value>The delay (in seconds) before the user is taken to the success page. If this value is greater than 0, the user will first see the standard Adobe Sign success message, and then after a delay will be redirected to your success page</value>
        [DataMember]
        [JsonProperty(PropertyName = "redirectDelay")]
        public int? RedirectDelay { get; set; }

        /// <summary>
        /// A publicly accessible url to which the user will be sent after successfully completing the signing process
        /// </summary>
        /// <value>A publicly accessible url to which the user will be sent after successfully completing the signing process</value>
        [DataMember]
        [JsonProperty(PropertyName = "redirectUrl")]
        public string RedirectUrl { get; set; }

    }

    [DataContract]
    public class VaultingInfo
    {
        /// <summary>
        /// For accounts set up for document vaulting and the option to enable per agreement, this determines whether the document is to be vaulted
        /// </summary>
        /// <value>For accounts set up for document vaulting and the option to enable per agreement, this determines whether the document is to be vaulted</value>
        [DataMember]
        [JsonProperty(PropertyName = "enabled")]
        public bool? Enabled { get; set; }

    }

    [DataContract]
    public class SecurityOption
    {
        /// <summary>
        /// The secondary password that will be used to secure the PDF document. Note that AdobeSign will never show this password to anyone, so you will need to separately communicate it to any relevant parties
        /// </summary>
        /// <value>The secondary password that will be used to secure the PDF document. Note that AdobeSign will never show this password to anyone, so you will need to separately communicate it to any relevant parties</value>
        [DataMember]
        [JsonProperty(PropertyName = "openPassword")]
        public string OpenPassword { get; set; }
    }

}
