using System;
using Decisions.AdobeSign.Utility;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Properties;
using System.IO;
using System.Linq;

namespace Decisions.AdobeSign
{
    [AutoRegisterStep("Create Agreement", AdobeSignCategory)]
    [Writable]
    public class CreateAgreement : AbstractStep
    {
        [PropertyHidden]
        public override DataDescription[] InputData => new[]
            { new DataDescription(typeof(AdobeSignAgreementCreationData), AgreementCreationDataLabel) };

        [PropertyHidden]
        protected override OutcomeScenarioData SuccessOutcomeScenarioData =>
            new(ResultOutcomeLabel, new DataDescription(typeof(string), AgreementIdLabel));

        protected override void ExecuteStep(StepStartData data)
        {
            AdobeSignAgreementCreationData agreementData = 
                (AdobeSignAgreementCreationData)data.Data[AgreementCreationDataLabel];
            string transientDocumentId = AdobeSignApi.CreateTransientDocument(
                accessTokenData: Token, 
                fileData: File.ReadAllBytes(agreementData.FilePath), 
                fileName: Path.GetFileName(agreementData.FilePath));
            AdobeSignApi.CreateAgreement(Token, ExtractCreationData(agreementData, transientDocumentId));
        }

        private static AdobeSignAgreementInfo ExtractCreationData(
            AdobeSignAgreementCreationData creationData, string transientDocumentId)
        {
            switch (creationData.InfoType)
            {
                case AdobeSignAgreementCreationType.Full:
                    return ExtractCreationDataFull(creationData, transientDocumentId);
                case AdobeSignAgreementCreationType.Simplified:
                    return ExtractCreationDataSimplified(creationData, transientDocumentId);
                default:
                    throw new NotImplementedException();
            }
        }

        private static AdobeSignAgreementInfo ExtractCreationDataFull(
            AdobeSignAgreementCreationData creationData, string transientDocumentId)
        {
            creationData.FullAgreementInfo.FileInfos = new[]
            {
                new AdobeSignFileInfo() { TransientDocumentId = transientDocumentId }
            };
            return creationData.FullAgreementInfo;
        }

        private static AdobeSignAgreementInfo ExtractCreationDataSimplified(
            AdobeSignAgreementCreationData creationData, string transientDocumentId)
        {
            var adobeSignAgreementInfo = new AdobeSignAgreementInfo
            {
                FileInfos = new[]
                {
                    new AdobeSignFileInfo() { TransientDocumentId = transientDocumentId }
                },
                Name = creationData.AgreementName,
                SignatureType = AgreementSignatureType.ESIGN,
                State = AgreementState.IN_PROCESS,
                EmailOption = new AdobeSignEmailOption() { 
                    SendOptions = new AdobeSignSendOptions() { 
                        InitEmails = AgreementEmailNotification.ALL,
                        InFlightEmails = AgreementEmailNotification.NONE,
                        CompletionEmails = AgreementEmailNotification.ALL 
                    } 
                }
            };
            if (creationData.Recipients != null)
            {
                adobeSignAgreementInfo.ParticipantSetsInfo = creationData.Recipients
                    .Select((recipient, i) => new AdobeSignParticipantSetInfo()
                        {
                            MemberInfos = new[] { new AdobeSignParticipantInfo() { Email = recipient.Email } }, 
                            Name = recipient.Name, 
                            Role = AgreementParticipantRole.SIGNER, 
                            Order = i + 1
                        })
                    .ToArray();
            }
            return adobeSignAgreementInfo;
        }
    }
}
