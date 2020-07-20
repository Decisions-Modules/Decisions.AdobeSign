using Decisions.AdobeSign.Data;
using Decisions.AdobeSign.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FileInfo = Decisions.AdobeSign.Data.FileInfo;

namespace AdobeSignature.UnitTests
{
    [TestClass]
    public class AdobeSignUnitTest
    {

        private AdobeSignConnection getConnection()
        {
            return new AdobeSignConnection()
            {
                AccessToken = AuthData.access_token,
                BaseAddress = AuthData.BaseUrl
            };

        }
        [TestMethod]
        public void TestAgreement()
        {
            var conn = getConnection();
            var fileData = File.ReadAllBytes(TestData.AgreementFileName);
            string docId = AdobeSignApi.CreateTransientDocument(conn, fileData, Path.GetFileName(TestData.AgreementFileName));
            Assert.IsNotNull(docId);

            string agreementId = AdobeSignApi.CreateAgreement(conn, CreateAgreementInfo(docId));
            Assert.IsNotNull(agreementId);

            AgreementInfo ai = AdobeSignApi.GetAgreementInfo(conn, agreementId);
            AgreementInfo agreementInfo;

            do
            {
                Thread.Sleep(10000);
                agreementInfo = AdobeSignApi.GetAgreementInfo(conn, agreementId);
            }
            while (agreementInfo.Status == AgreementStatus.OUT_FOR_SIGNATURE);

            AdobeSignApi.GetTransientDocument(conn, agreementId, $"{TestData.SignedAgreementFileName}_{agreementId}.pdf");
        }

        [TestMethod]
        public void TestDownloadAgreement()
        {
            string agreementId = "CBJCHBCAABAAii3YYjzAtGzuA1kCZaTrNdM0fGn31Qen";
            AdobeSignApi.GetTransientDocument(getConnection(), agreementId, TestData.SignedAgreementFileName);
        }

        private AgreementInfo CreateAgreementInfo(string docId)
        {
            AgreementInfo agreementInfo = new AgreementInfo();
            agreementInfo.FileInfos = new FileInfo[] { new FileInfo() { TransientDocumentId = docId } };

            agreementInfo.EmailOption = new EmailOption()
            {
                SendOptions = new SendOptions()
                {
                    CompletionEmails = AgreementEmailNotification.ALL,
                    InFlightEmails = AgreementEmailNotification.NONE,
                    InitEmails = AgreementEmailNotification.ALL,
                }
            };

            agreementInfo.SignatureType = AgreementSignatureType.ESIGN;

            agreementInfo.State = AgreementState.IN_PROCESS;

            agreementInfo.Name = "Test Agreement Name";

            agreementInfo.ParticipantSetsInfo = new ParticipantSetInfo[] { new ParticipantSetInfo() {
                Role = AgreementParticipantRole.SIGNER,
                Name = "Ivan Kov",
                Order = 1,
                MemberInfos = new ParticipantInfo[]{new ParticipantInfo(){ Email = "kovalchuk_i_v@mail.ru"} }
            } };

            return agreementInfo;
        }
    }
}
