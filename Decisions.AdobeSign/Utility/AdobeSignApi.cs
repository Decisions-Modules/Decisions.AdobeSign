using System;
using System.Net.Http;
using System.Text;
using Decisions.OAuth;
using DecisionsFramework;
using Newtonsoft.Json;

namespace Decisions.AdobeSign.Utility
{
    public static partial class AdobeSignApi
    {
        private const string URI_API_PART = "/api/rest/v6/";
        
        public static string CreateTransientDocument(
            OAuthToken token, 
            byte[] fileData, 
            string fileName, 
            string mimeType = "application/PDF")
        {
            ThrowIfNullOrEmpty(token);
            ThrowIfNullOrEmpty(fileData);
            ThrowIfNullOrEmpty(fileName);
            ThrowIfNullOrEmpty(mimeType); 
            
            HttpRequestMessage requestMessage = BuildHttpPostRequestMessage(
                token,
                url: $"{FetchBaseUriFromWeb(token)}{URI_API_PART}transientDocuments",
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
            OAuthToken token, 
            AdobeSignAgreementInfo agreementInfo)
        {
            ThrowIfNullOrEmpty(token);
            ThrowIfNullOrEmpty(agreementInfo);
 
            HttpRequestMessage requestMessage = BuildHttpPostRequestMessage(
                token,
                url: $"{FetchBaseUriFromWeb(token)}{URI_API_PART}agreements", 
                contentType: "application/json; charset=utf-8");
            requestMessage.Content = new StringContent(
                JsonConvert.SerializeObject(agreementInfo, Formatting.None, JsonSettings),
                Encoding.UTF8,
                "application/json");
            
            HttpResponseMessage httpResponse = SendAsync(requestMessage);
            return ParseResponse<AdobeSignAgreementCreationResponse>(httpResponse).Id;
        }

        public static AdobeSignAgreementInfo GetAgreementInfo(
            OAuthToken token,
            string agreementId)
        {
            ThrowIfNullOrEmpty(token);
            ThrowIfNullOrEmpty(agreementId); 

            HttpRequestMessage requestMessage = BuildHttpGetRequestMessage(
                token,
                url: $"{FetchBaseUriFromWeb(token)}{URI_API_PART}agreements/{agreementId}",
                mediaType: "application/json",
                contentType: "application/json; charset=utf-8");

            HttpResponseMessage httpResponse = SendAsync(requestMessage);
            var response = ParseResponse<AdobeSignAgreementInfo>(httpResponse);
            return response;
        }

        
        public static void GetTransientDocument(
            OAuthToken token,
            string agreementId, 
            string filePath)
        {
            ThrowIfNullOrEmpty(token);
            ThrowIfNullOrEmpty(agreementId);
            ThrowIfNullOrEmpty(filePath);

            try
            {
                HttpRequestMessage requestMessage = BuildHttpGetRequestMessage(
                    token, 
                    url: $"{FetchBaseUriFromWeb(token)}{URI_API_PART}agreements/{agreementId}/combinedDocument",
                    mediaType: "application/PDF");
                HttpContent httpContent = SendAsync(requestMessage).Content;
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
