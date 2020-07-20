using System;
using System.Collections.Generic;
using System.Threading;
using AdobeSign.Client;
using Decisions.AdobeSignature;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdobeSignature.UnitTests
{
    [TestClass]
    public class OldAdobeUnitTest
    {
        [TestMethod]
        public void TestAgreement()
        {
            string agreementID = CreateAgreement();
            DownloadAgreement(agreementID, @"C:\data\tmp\not_signed_agreement.pdf");

            var info = ReadAgreementInfo(agreementID);
            AdobeSign.Agreements.Model.AgreementInfo info1;
            do
            {
                Thread.Sleep(1000);
                info1 = ReadAgreementInfo(agreementID);
            }
            while (info1.Status != "SIGNED");

            DownloadAgreement(agreementID, @"C:\data\tmp\agreement.pdf");
        }

        private ApiClient GetApiClient()
        {
            ApiClient apiClient = new ApiClient(AuthData.AppId, AuthData.ClientSecret);

            apiClient.SetAccessToken(
                      AuthData.access_token,
                      AuthData.refresh_token,
                      DateTime.Now.AddSeconds(3600),
                      AuthData.BaseUrl);

            return apiClient;
        }

        private string CreateAgreement()
        {
            AgreementModel agreement = new AgreementModel();
            agreement.AgreementName = "Test Agreement";
            agreement.FilePath = @"C:\data\tmp\dummy11.pdf";
            //agreement.File = System.IO.File.ReadAllBytes(@"C:\data\tmp\dummy11.pdf");
            agreement.Recipients = new List<Recipient>() { new Recipient() { Email = "kovalchuk_i_v@mail.ru", Name = "Utkarsh", Order = 1 } };
            string agreementID = AdobeSignService.SendAgreement(GetApiClient(), agreement);
            Assert.IsNotNull(agreementID);
            return agreementID;
        }

        private AdobeSign.Agreements.Model.AgreementInfo ReadAgreementInfo(string agreementID)
        {

            var info = AdobeSignService.GetAgreementInfo(GetApiClient(), agreementID);
            Assert.IsNotNull(info);
            return info;
        }

        private void DownloadAgreement(string agreementID, string filePath)
        {
            var bytes = AdobeSignService.GetTransientDocument(GetApiClient(), agreementID);
            System.IO.File.WriteAllBytes(filePath, bytes);
            Assert.IsNotNull(bytes);
        }

    }
}
