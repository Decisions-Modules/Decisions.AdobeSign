using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

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

    }
}
