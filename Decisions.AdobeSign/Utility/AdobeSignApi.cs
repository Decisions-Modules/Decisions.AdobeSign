using System;
using System.Net.Http;
using System.Text;
using DecisionsFramework;
using Newtonsoft.Json;

namespace Decisions.AdobeSign.Utility
{
    public static partial class AdobeSignApi
    {
        public static string CreateTransientDocument(
            string accessTokenData, 
            byte[] fileData, 
            string fileName, 
            string mimeType = "application/PDF")
        {
            if (string.IsNullOrEmpty(accessTokenData)) 
                throw new ArgumentNullException(nameof(accessTokenData), $"{nameof(accessTokenData)} cannot be null or empty");
            if (fileData == null)
                throw new ArgumentNullException(nameof(fileData), $"{nameof(fileData)} is missing");
            if (string.IsNullOrEmpty(fileName)) 
                throw new ArgumentNullException(nameof(fileName), $"{nameof(fileName)} cannot be null or empty");
            if (string.IsNullOrEmpty(mimeType))
                throw new ArgumentNullException(nameof(mimeType), $"{nameof(mimeType)} cannot be null or empty");
            
            HttpRequestMessage requestMessage = BuildHttpPostRequestMessage(
                accessTokenData: accessTokenData,
                methodUri: "transientDocuments",
                contentType: mimeType);
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent(mimeType), "Mime-Type");
            content.Add(new StringContent(fileName), "File-Name");
            content.Add(new ByteArrayContent(fileData),"File", fileName);
            requestMessage.Content = content;

            HttpResponseMessage httpResponse = SendAsync(requestMessage);
            var response = ParseResponse<TransientDocumentResponse>(httpResponse);
            return response.TransientDocumentId;
        }

        public static string CreateAgreement(
            string accessTokenData, 
            AdobeSignAgreementInfo agreementInfo)
        {
            if (string.IsNullOrEmpty(accessTokenData)) 
                throw new ArgumentNullException(nameof(accessTokenData), $"{nameof(accessTokenData)} cannot be null or empty");
            if (agreementInfo == null)
                throw new ArgumentNullException(nameof(agreementInfo), $"{nameof(agreementInfo)} is required");
 
            HttpRequestMessage requestMessage = BuildHttpPostRequestMessage(
                accessTokenData: accessTokenData,
                methodUri: "agreements", 
                contentType: "application/json; charset=utf-8");
            requestMessage.Content = new StringContent(
                JsonConvert.SerializeObject(agreementInfo, Formatting.None, JsonSettings),
                Encoding.UTF8,
                "application/json");
            
            HttpResponseMessage httpResponse = SendAsync(requestMessage);
            var response = ParseResponse<AdobeSignAgreementCreationResponse>(httpResponse);
            return response.Id;
        }

        public static AdobeSignAgreementInfo GetAgreementInfo(
            string accessTokenData, 
            string agreementId)
        {
            if (string.IsNullOrEmpty(accessTokenData)) 
                throw new ArgumentNullException(nameof(accessTokenData), $"{nameof(accessTokenData)} cannot be null or empty");
            if (string.IsNullOrEmpty(agreementId)) 
                throw new ArgumentNullException(nameof(agreementId), $"{nameof(agreementId)} cannot be null or empty");

            HttpRequestMessage requestMessage = BuildHttpGetRequestMessage(
                accessTokenData: accessTokenData,
                methodUri: $"agreements/{agreementId}",
                mediaType: "application/json",
                contentType: "application/json; charset=utf-8");

            HttpResponseMessage httpResponse = SendAsync(requestMessage);
            var response = ParseResponse<AdobeSignAgreementInfo>(httpResponse);
            return response;
        }

        
        public static void GetTransientDocument(
            string accessTokenData, 
            string agreementId, 
            string filePath)
        {
            if (string.IsNullOrEmpty(accessTokenData)) 
                throw new ArgumentNullException(nameof(accessTokenData), $"{nameof(accessTokenData)} cannot be null or empty");
            if (string.IsNullOrEmpty(agreementId)) 
                throw new ArgumentNullException(nameof(agreementId), $"{nameof(agreementId)} cannot be null or empty");
            if (string.IsNullOrEmpty(filePath)) 
                throw new ArgumentNullException(nameof(filePath), $"{nameof(filePath)} cannot be null or empty");

            HttpRequestMessage requestMessage = BuildHttpGetRequestMessage(
                accessTokenData: accessTokenData,
                methodUri: $"agreements/{agreementId}/combinedDocument",
                mediaType: "application/PDF");

            HttpContent httpContent = SendAsync(requestMessage).Content;
            try
            {
                byte[] bytes = httpContent.ReadAsByteArrayAsync().Result;
                System.IO.File.WriteAllBytes(filePath, bytes);
            }
            catch (Exception ex)
            {
                throw new LoggedException($"Could not write resulting document to '{filePath}'", ex);
            }
        }
    }
}
