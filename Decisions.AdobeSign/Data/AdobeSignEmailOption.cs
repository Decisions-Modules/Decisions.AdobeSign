﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Decisions.AdobeSign
{

    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AgreementEmailNotification { ALL, NONE }

    [DataContract]
    public class AdobeSignEmailOption
    {
        /// <summary>
        /// Specify emails to be sent to different participants at different steps of the agreement process. Note: ALL means  emails for the events will be sent to all participants. NONE means emails for the events will not be sent to any participant
        /// </summary>
        /// <value>Specify emails to be sent to different participants at different steps of the agreement process. Note: ALL means  emails for the events will be sent to all participants. NONE means emails for the events will not be sent to any participant</value>
        [DataMember]
        public AdobeSignSendOptions SendOptions { get; set; }
    }

    [DataContract]
    public class AdobeSignSendOptions
    {
        /// <summary>
        /// Control notification mails for agreement completion events - COMPLETED, CANCELLED, EXPIRED and REJECTED
        /// </summary>
        /// <value>Control notification mails for agreement completion events - COMPLETED, CANCELLED, EXPIRED and REJECTED</value>
        [DataMember(Name = "completionEmails", EmitDefaultValue = false)]
        public AgreementEmailNotification? CompletionEmails { get; set; }

        /// <summary>
        /// Control notification mails for agreement-in-process events - DELEGATED, REPLACED
        /// </summary>
        /// <value>Control notification mails for agreement-in-process events - DELEGATED, REPLACED</value>
        [DataMember(Name = "inFlightEmails", EmitDefaultValue = false)]
        public AgreementEmailNotification? InFlightEmails { get; set; }

        /// <summary>
        /// Control notification mails for Agreement initiation events - ACTION_REQUESTED and CREATED
        /// </summary>
        /// <value>Control notification mails for Agreement initiation events - ACTION_REQUESTED and CREATED</value>
        [DataMember(Name = "initEmails", EmitDefaultValue = false)]
        public AgreementEmailNotification? InitEmails { get; set; }
    }
}
