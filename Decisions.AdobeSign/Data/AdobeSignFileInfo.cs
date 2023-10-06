using System;
using System.Runtime.Serialization;

namespace Decisions.AdobeSign
{
    [DataContract]
    public class AdobeSignFileInfo
    {
        /// <summary>
        /// A document that is associated with the agreement. This field cannot be provided in POST call. In case of GET call, this is the only field returned in the response
        /// </summary>
        /// <value>A document that is associated with the agreement. This field cannot be provided in POST call. In case of GET call, this is the only field returned in the response</value>
        [DataMember]
        public AdobeSignDocument Document { get; set; }

        /// <summary>
        /// The unique label value of a file info element. In case of custom workflow this will map a file to corresponding file element in workflow definition. This must be specified in case of custom workflow agreement creation request 
        /// </summary>
        /// <value>The unique label value of a file info element. In case of custom workflow this will map a file to corresponding file element in workflow definition. This must be specified in case of custom workflow agreement creation request </value>
        [DataMember]
        public string Label { get; set; }

        /// <summary>
        /// ID for an existing Library document that will be added to the agreement
        /// </summary>
        /// <value>ID for an existing Library document that will be added to the agreement</value>
        [DataMember]
        public string LibraryDocumentId { get; set; }

        /// <summary>
        /// ID for a transient document that will be added to the agreement
        /// </summary>
        /// <value>ID for a transient document that will be added to the agreement</value>
        [DataMember]
        public string TransientDocumentId { get; set; }

        /// <summary>
        /// URL for an external document to add to the agreement
        /// </summary>
        /// <value>URL for an external document to add to the agreement</value>
        [DataMember(Name = "urlFileInfo", EmitDefaultValue = false)]
        public AdobeSignURLFileInfo UrlFileInfo { get; set; }

    }

    [DataContract]
    public class AdobeSignDocument 
    {
        /// <summary>
        /// The date the document was created
        /// </summary>
        [DataMember]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// ID of the document. In case of PUT call, this is the only field that is accepted in Document structure. Name and mimeType are ignored in case of PUT call
        /// </summary>
        /// <value>ID of the document. In case of PUT call, this is the only field that is accepted in Document structure. Name and mimeType are ignored in case of PUT call</value>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Label of the document
        /// </summary>
        /// <value>Label of the document</value>
        [DataMember]
        public string Label { get; set; }

        /// <summary>
        /// Number of pages in the document
        /// </summary>
        /// <value>Number of pages in the document</value>
        [DataMember]
        public int? NumPages { get; set; }

        /// <summary>
        /// mimeType of the original file. This is returned in GET but not accepted back in PUT
        /// </summary>
        /// <value>mimeType of the original file. This is returned in GET but not accepted back in PUT</value>
        [DataMember]
        public string MimeType { get; set; }

        /// <summary>
        /// Name of the original document uploaded. This is returned in GET but not accepted back in PUT
        /// </summary>
        /// <value>Name of the original document uploaded. This is returned in GET but not accepted back in PUT</value>
        [DataMember]
        public string Name { get; set; }
    }

    [DataContract]
    public class AdobeSignURLFileInfo
    {
        /// <summary>
        /// The mime type of the referenced file, used to determine if the file can be accepted and the necessary conversion steps can be performed
        /// </summary>
        /// <value>The mime type of the referenced file, used to determine if the file can be accepted and the necessary conversion steps can be performed</value>
        [DataMember]
        public string MimeType { get; set; }

        /// <summary>
        /// The original system file name of the document being sent
        /// </summary>
        /// <value>The original system file name of the document being sent</value>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// A publicly accessible URL for retrieving the raw file content
        /// </summary>
        /// <value>A publicly accessible URL for retrieving the raw file content</value>
        [DataMember]
        public string Url { get; set; }

    }
}
