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
    public class FileInfo
    {
        /// <summary>
        /// A document that is associated with the agreement. This field cannot be provided in POST call. In case of GET call, this is the only field returned in the response
        /// </summary>
        /// <value>A document that is associated with the agreement. This field cannot be provided in POST call. In case of GET call, this is the only field returned in the response</value>
        [DataMember]
        [JsonProperty(PropertyName = "document")]
        public Document Document { get; set; }

        /// <summary>
        /// The unique label value of a file info element. In case of custom workflow this will map a file to corresponding file element in workflow definition. This must be specified in case of custom workflow agreement creation request 
        /// </summary>
        /// <value>The unique label value of a file info element. In case of custom workflow this will map a file to corresponding file element in workflow definition. This must be specified in case of custom workflow agreement creation request </value>
        [DataMember]
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        /// <summary>
        /// ID for an existing Library document that will be added to the agreement
        /// </summary>
        /// <value>ID for an existing Library document that will be added to the agreement</value>
        [DataMember]
        [JsonProperty(PropertyName = "libraryDocumentId")]
        public string LibraryDocumentId { get; set; }

        /// <summary>
        /// ID for a transient document that will be added to the agreement
        /// </summary>
        /// <value>ID for a transient document that will be added to the agreement</value>
        [DataMember]
        [JsonProperty(PropertyName = "transientDocumentId")]
        public string TransientDocumentId { get; set; }

        /// <summary>
        /// URL for an external document to add to the agreement
        /// </summary>
        /// <value>URL for an external document to add to the agreement</value>
        [DataMember(Name = "urlFileInfo", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "urlFileInfo")]
        public URLFileInfo UrlFileInfo { get; set; }

    }

    [DataContract]
    public class Document 
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
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Label of the document
        /// </summary>
        /// <value>Label of the document</value>
        [DataMember]
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        /// <summary>
        /// Number of pages in the document
        /// </summary>
        /// <value>Number of pages in the document</value>
        [DataMember]
        [JsonProperty(PropertyName = "numPages")]
        public int? NumPages { get; set; }

        /// <summary>
        /// mimeType of the original file. This is returned in GET but not accepted back in PUT
        /// </summary>
        /// <value>mimeType of the original file. This is returned in GET but not accepted back in PUT</value>
        [DataMember]
        [JsonProperty(PropertyName = "mimeType")]
        public string MimeType { get; set; }

        /// <summary>
        /// Name of the original document uploaded. This is returned in GET but not accepted back in PUT
        /// </summary>
        /// <value>Name of the original document uploaded. This is returned in GET but not accepted back in PUT</value>
        [DataMember]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    [DataContract]
    public class URLFileInfo
    {
        /// <summary>
        /// The mime type of the referenced file, used to determine if the file can be accepted and the necessary conversion steps can be performed
        /// </summary>
        /// <value>The mime type of the referenced file, used to determine if the file can be accepted and the necessary conversion steps can be performed</value>
        [DataMember]
        [JsonProperty(PropertyName = "mimeType")]
        public string MimeType { get; set; }

        /// <summary>
        /// The original system file name of the document being sent
        /// </summary>
        /// <value>The original system file name of the document being sent</value>
        [DataMember]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// A publicly accessible URL for retrieving the raw file content
        /// </summary>
        /// <value>A publicly accessible URL for retrieving the raw file content</value>
        [DataMember]
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

    }
}
