using System;
using System.Collections.Generic;
using AdobeSign.ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdobeSign.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAgreementCreation()
        {
            AdobeSignConnection connection = new AdobeSignConnection();
            connection.ClientID = "CBJCHBCAABAAAsFf1llG3qvcrWUzkaPPpLZSkcJElARh";
            connection.ClientSecret = "cSd_djtK41Saa22Mw200Eva2p9nhDZEu";
            connection.Code = "CBNCKBAAHBCAABAA68kT-m_cqlhceilaiHmlx9AuXTdSg9G3";
            connection.LoginURL = "https://secure.na2.echosign.com/public/oauth?redirect_uri=https://localhost:1111&response_type=code&client_id=CBJCHBCAABAAAsFf1llG3qvcrWUzkaPPpLZSkcJElARh&scope=user_login:self+agreement_send:self+agreement_write:self+agreement_read:self";
            connection.RedirectURL = "https://localhost:1111";
            AdobeSignToken token = AdobeSignService.GetAuthorizationToken(connection);

            AgreementModel agreement = new AgreementModel();
            agreement.AgreementName = "Test Agreement";
            agreement.FileName = "dummy11.pdf";
            agreement.File = System.IO.File.ReadAllBytes(@"F:\Projects\Adobe Sign\AdobeSign.ClassLibrary\dummy11.pdf");
            agreement.Recipients = new List<Recipient>() { new Recipient() { Email = "utkarshchoudhary897@gmail.com", Name = "Utkarsh", Order = 1 } };
            string agreementID = AdobeSignService.SendAgreement(connection, token, agreement);
            Assert.IsNotNull(agreementID);
        }

        [TestMethod]
        public void TestAgreementInfo()
        {
            AdobeSignConnection connection = new AdobeSignConnection();
            connection.ClientID = "CBJCHBCAABAAAsFf1llG3qvcrWUzkaPPpLZSkcJElARh";
            connection.ClientSecret = "cSd_djtK41Saa22Mw200Eva2p9nhDZEu";
            connection.Code = "CBNCKBAAHBCAABAA68kT-m_cqlhceilaiHmlx9AuXTdSg9G3";
            connection.LoginURL = "https://secure.na2.echosign.com/public/oauth?redirect_uri=https://localhost:1111&response_type=code&client_id=CBJCHBCAABAAAsFf1llG3qvcrWUzkaPPpLZSkcJElARh&scope=user_login:self+agreement_send:self+agreement_write:self+agreement_read:self";
            connection.RedirectURL = "https://localhost:1111";
            AdobeSignToken token = AdobeSignService.GetAuthorizationToken(connection);

            string agreementID = "CBJCHBCAABAAJYoeJhQk4SZ6jbwnJrGS7_iHno0BZsbn";
            var info = AdobeSignService.GetAgreementInfo(connection, token, agreementID);
            Assert.IsNotNull(info);
        }

        [TestMethod]
        public void TestDownloadAgreement()
        {
            AdobeSignConnection connection = new AdobeSignConnection();
            connection.ClientID = "CBJCHBCAABAAAsFf1llG3qvcrWUzkaPPpLZSkcJElARh";
            connection.ClientSecret = "cSd_djtK41Saa22Mw200Eva2p9nhDZEu";
            connection.Code = "CBNCKBAAHBCAABAAH69ikQLhqJCgTtZhgHhPmdwU0n2SVmYB";
            connection.LoginURL = "https://secure.na2.echosign.com/public/oauth?redirect_uri=https://localhost:1111&response_type=code&client_id=CBJCHBCAABAAAsFf1llG3qvcrWUzkaPPpLZSkcJElARh&scope=user_login:self+agreement_send:self+agreement_write:self+agreement_read:self";
            connection.RedirectURL = "https://localhost:1111";
            AdobeSignToken token = AdobeSignService.GetAuthorizationToken(connection);

            string agreementID = "CBJCHBCAABAAJYoeJhQk4SZ6jbwnJrGS7_iHno0BZsbn";
            var bytes = AdobeSignService.GetTransientDocument(connection, token, agreementID);
            string filePath = @"G:\pdf\agreement.pdf";
            System.IO.File.WriteAllBytes(filePath, bytes);
            Assert.IsNotNull(bytes);
        }
    }
}
