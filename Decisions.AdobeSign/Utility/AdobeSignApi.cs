using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Decisions.OAuth;
using DecisionsFramework;

namespace Decisions.AdobeSign.Utility
{
    public static partial class AdobeSignApi
    {

        public static string CreateTransientDocument(AdobeSignConnection connection, byte[] fileData, string fileName, string mimeType = "application/PDF")
        {
            HttpClient httpClient = GetClient(connection);

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(mimeType), "Mime-Type");
            content.Add(new StringContent(fileName), "File-Name");
            content.Add(new ByteArrayContent(fileData),"File", fileName);

            HttpResponseMessage response = httpClient.PostAsync("transientDocuments", content).Result;

            TransientDocumentResponse res = ParseResponse<TransientDocumentResponse>(response);
            return res.TransientDocumentId;
        }

        public static string CreateAgreement(AdobeSignConnection connection, AdobeSignAgreementInfo agreementInfo)
        {
            AdobeSignAgreementCreationResponse res = PostRequest<AdobeSignAgreementCreationResponse, AdobeSignAgreementInfo>(connection, "agreements", agreementInfo);
            return res.Id;
        }

        public static AdobeSignAgreementInfo GetAgreementInfo(AdobeSignConnection connection, string agreementId)
        {
            if (string.IsNullOrEmpty(agreementId))
                throw new ArgumentNullException("agreementId cannot be null nor empty");

            AdobeSignAgreementInfo res = GetRequest<AdobeSignAgreementInfo>(connection, $"agreements/{agreementId}");
            return res;
        }

        public static void GetTransientDocument(AdobeSignConnection connection, string agreementId, string filePath)
        {
            if (string.IsNullOrEmpty(agreementId))
                throw new ArgumentNullException("agreementId cannot be null nor empty");
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath cannot be null nor empty");

            HttpClient httpClient = GetClient(connection);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/PDF");
            HttpResponseMessage response = httpClient.GetAsync($"agreements/{agreementId}/combinedDocument").Result;
            CheckResponse(response);

            byte[] bytes = response.Content.ReadAsByteArrayAsync().Result;
            System.IO.File.WriteAllBytes(filePath, bytes);
        }

        // the region dynamic uri should not ever change for a token and therefore does not really need to be called more than once per token instance
        private static Dictionary<string, AdobeSignBaseUriInfo> baseUriTokenCache;
        
        private const string baseAddressUrisLookup = "https://api.na3.adobesign.com:443/";

        public static AdobeSignBaseUriInfo GetBaseUriInfo(string tokenId, string tokenData)
        {
            baseUriTokenCache ??= new Dictionary<string, AdobeSignBaseUriInfo>();
            
            // attempt to lookup in cache first. Since a token will never change dynamic region, we should not check more than once for a token per decisions instance
            if (baseUriTokenCache.TryGetValue(tokenId, out var info))
                return info;
            
            // else use the officially supplied region-agnostic url to pull the appropriate dynamic uri's info for this token
            AdobeSignConnection connection = new AdobeSignConnection()
            {
                BaseAddress = baseAddressUrisLookup,
                AccessToken = tokenData
            };
            AdobeSignBaseUriInfo result = GetRequest<AdobeSignBaseUriInfo>(connection, "baseUris");
            
            if (string.IsNullOrWhiteSpace(result.apiAccessPoint) || string.IsNullOrWhiteSpace(result.webAccessPoint))
                throw new LoggedException($"No valid response from {baseAddressUrisLookup}api/rest/v6/baseUris, try refreshing your OAuth Token");

            baseUriTokenCache.Add(tokenId, result);
            return result;
        }

    }
}
