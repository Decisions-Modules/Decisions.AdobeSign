using Decisions.AdobeSign.Data;
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

        public static string CreateAgreement(AdobeSignConnection connection, AgreementInfo agreementInfo)
        {
            AgreementCreationResponse res = PostRequest<AgreementCreationResponse, AgreementInfo>(connection, "agreements", agreementInfo);
            return res.Id;
        }

        public static AgreementInfo GetAgreementInfo(AdobeSignConnection connection, string agreementId)
        {
            AgreementInfo res = GetRequest<AgreementInfo>(connection, $"agreements/{agreementId}");
            return res;
        }

        public static void GetTransientDocument(AdobeSignConnection connection, string agreementId, string filePath)
        {
            HttpClient httpClient = GetClient(connection);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/PDF");
            // httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync($"agreements/{agreementId}/combinedDocument").Result;
            CheckResponse(response);

            byte[] bytes = response.Content.ReadAsByteArrayAsync().Result;
            System.IO.File.WriteAllBytes(filePath, bytes);
        }

    }
}
