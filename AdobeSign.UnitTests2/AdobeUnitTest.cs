using System;
using System.Collections.Generic;
using AdobeSign.ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdobeSign.UnitTests
{

    [TestClass]
    public class AdobeUnitTest
    {
        private AdobeSignToken token = GetAdobeSignToken();
        private AdobeSignConnection connection = GetAdobeSignConnection();


        [TestMethod]
        public void TestAgreement()
        {
            string agreementID = CreateAgreement();
            DownloadAgreement(agreementID, @"C:\data\tmp\not_signed_agreement.pdf");

            var info = ReadAgreementInfo(agreementID);
            Agreements.Model.AgreementInfo info1;
            do
            {
                info1 = ReadAgreementInfo(agreementID);
            }
            while (info1.Status != "SIGNED");

            DownloadAgreement(agreementID, @"C:\data\tmp\agreement.pdf");
        }

        private static AdobeSignToken GetAdobeSignToken()
        {
            var res = new AdobeSignToken();
            res.AccessToken = AuthData.access_token;
            res.RefreshToken = AuthData.refresh_token;
            res.TokenExpiration = DateTime.Now.AddSeconds(3600);
            res.ApiAccessPoint = AuthData.EndPoint;

            return res;
        }
        private static AdobeSignConnection GetAdobeSignConnection()
        {
            var res = new AdobeSignConnection();

            res.ClientID = AuthData.AppId;
            res.ClientSecret = AuthData.ClientSecret;

            return res;
        }

        private string CreateAgreement()
        {
            AgreementModel agreement = new AgreementModel();
            agreement.AgreementName = "Test Agreement";
            agreement.FileName = "dummy11.pdf";
            agreement.File = System.IO.File.ReadAllBytes(@"C:\data\tmp\dummy11.pdf");
            agreement.Recipients = new List<Recipient>() { new Recipient() { Email = "kovalchuk_i_v@mail.ru", Name = "Utkarsh", Order = 1 } };
            string agreementID = AdobeSignService.SendAgreement(connection, token, agreement);
            Assert.IsNotNull(agreementID);
            return agreementID;
        }

        private Agreements.Model.AgreementInfo ReadAgreementInfo(string agreementID)
        {

            var info = AdobeSignService.GetAgreementInfo(connection, token, agreementID);
            Assert.IsNotNull(info);
            return info;
        }

        private void DownloadAgreement(string agreementID, string filePath)
        {
            var bytes = AdobeSignService.GetTransientDocument(connection, token, agreementID);
            System.IO.File.WriteAllBytes(filePath, bytes);
            Assert.IsNotNull(bytes);
        }
    }
}
