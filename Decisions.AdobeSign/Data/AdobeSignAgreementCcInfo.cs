﻿using System.Runtime.Serialization;

namespace Decisions.AdobeSign
{
    [DataContract]
    public class AdobeSignAgreementCcInfo
    {
        /// <summary>
        /// Email of the CC participant of the agreement
        /// </summary>
        /// <value>Email of the CC participant of the agreement</value>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Label of the CC list as returned in workflow description
        /// </summary>
        /// <value>Label of the CC list as returned in workflow description</value>
        [DataMember]
        public string Label { get; set; }

        /// <summary>
        /// When you enable limited document visibility (documentVisibilityEnabled), you can specify which file (fileInfo) should be made visible to which specific participant set.<br>Specify one or more label values of a fileInfos element.<br>Each signer participant sets must contain at least one required signature field in at least one visible file included in this API call; if not a page with a signature field is automatically appended for any missing participant sets. If there is a possibility that one or more participant sets do not have a required signature field in the files included in the API call, all signer participant sets should include a special index value of '0' to make this automatically appended signature page visible to the signer. Not doing so may result in an error. For all other roles, you may omit this value to exclude this page.
        /// </summary>
        /// <value>When you enable limited document visibility (documentVisibilityEnabled), you can specify which file (fileInfo) should be made visible to which specific participant set.<br>Specify one or more label values of a fileInfos element.<br>Each signer participant sets must contain at least one required signature field in at least one visible file included in this API call; if not a page with a signature field is automatically appended for any missing participant sets. If there is a possibility that one or more participant sets do not have a required signature field in the files included in the API call, all signer participant sets should include a special index value of '0' to make this automatically appended signature page visible to the signer. Not doing so may result in an error. For all other roles, you may omit this value to exclude this page.</value>
        [DataMember]
        public string[] VisiblePages { get; set; }

    }
}
