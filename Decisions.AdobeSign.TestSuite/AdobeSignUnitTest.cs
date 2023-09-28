using Decisions.AdobeSign;
using Decisions.AdobeSign.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading;
using AdobeSignFileInfo = Decisions.AdobeSign.AdobeSignFileInfo;

namespace AdobeSignature.UnitTests
{
    [TestClass]
    public class AdobeSignUnitTest
    { 
        
        [TestMethod]
        public void TestAgreement()
        {
            var fileData = File.ReadAllBytes(TestData.AgreementFileName);
            string docId = AdobeSignApi.CreateTransientDocument(AuthData.AccessToken, fileData, Path.GetFileName(TestData.AgreementFileName));
            Assert.IsNotNull(docId);

            string agreementId = AdobeSignApi.CreateAgreement(AuthData.AccessToken, CreateAgreementInfo(docId));
            Assert.IsNotNull(agreementId);

            AdobeSignAgreementInfo ai = AdobeSignApi.GetAgreementInfo(AuthData.AccessToken, agreementId);
            AdobeSignAgreementInfo agreementInfo;

            do
            {
                Thread.Sleep(10000);
                agreementInfo = AdobeSignApi.GetAgreementInfo(AuthData.AccessToken, agreementId);
            }
            while (agreementInfo.Status == AgreementStatus.OUT_FOR_SIGNATURE);

            AdobeSignApi.GetTransientDocument(AuthData.AccessToken, agreementId, $"{TestData.SignedAgreementFileName}_{agreementId}.pdf");
        }

        [TestMethod]
        public void TestDownloadAgreement()
        {
            string agreementId = "CBJCHBCAABAAii3YYjzAtGzuA1kCZaTrNdM0fGn31Qen";
            AdobeSignApi.GetTransientDocument(AuthData.AccessToken, agreementId, TestData.SignedAgreementFileName);
        }

        private AdobeSignAgreementInfo CreateAgreementInfo(string docId)
        {
            AdobeSignAgreementInfo agreementInfo = new AdobeSignAgreementInfo();
            agreementInfo.FileInfos = new AdobeSignFileInfo[] { new AdobeSignFileInfo() { TransientDocumentId = docId } };

            agreementInfo.EmailOption = new AdobeSignEmailOption()
            {
                SendOptions = new AdobeSignSendOptions()
                {
                    CompletionEmails = AgreementEmailNotification.ALL,
                    InFlightEmails = AgreementEmailNotification.NONE,
                    InitEmails = AgreementEmailNotification.ALL,
                }
            };

            agreementInfo.SignatureType = AgreementSignatureType.ESIGN;

            agreementInfo.State = AgreementState.IN_PROCESS;

            agreementInfo.Name = "Test Agreement Name";

            agreementInfo.ParticipantSetsInfo = new AdobeSignParticipantSetInfo[] { new AdobeSignParticipantSetInfo() {
                Role = AgreementParticipantRole.SIGNER,
                Name = "Ivan Kov",
                Order = 1,
                MemberInfos = new AdobeSignParticipantInfo[]{new AdobeSignParticipantInfo(){ Email = "kovalchuk_i_v@mail.ru"} }
            } };

            return agreementInfo;
        }
    }
}
