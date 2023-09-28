using System;
using Decisions.AdobeSign.Utility;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Properties;
using System.IO;
using System.Linq;
using Decisions.OAuth;
using DecisionsFramework.ServiceLayer.Services.ContextData;

namespace Decisions.AdobeSign
{
    [AutoRegisterStep("Create Agreement", STEP_PARAMS_CATEGORY)]
    [Writable]
    public class CreateAgreement : AbstractStep
    {
        private const string RESULT_PATH = "Result";
        private const string INPUT_NAME_DATA = "Agreement Data";
        private const string OUTCOME_NAME_DATA = "Agreement Id";

        [PropertyHidden]
        public override DataDescription[] InputData => new DataDescription[]
        {
            new (typeof(AdobeSignAgreementCreationData), INPUT_NAME_DATA)
        };
 
        protected override OutcomeScenarioData[] GetOutcomeScenarios()
        {
            return new OutcomeScenarioData[]
            {
                new (RESULT_PATH, new DataDescription(typeof(string), OUTCOME_NAME_DATA))
            };
        }  

        protected override ResultData ExecuteStep(
            StepStartData data, 
            OAuthToken token)
        {
            var agreementData = (AdobeSignAgreementCreationData)data.Data[INPUT_NAME_DATA];
            AdobeSignApi.ThrowIfNullOrEmpty(agreementData);
            
            string transientDocumentId = AdobeSignApi.CreateTransientDocument(
                token, 
                fileData: File.ReadAllBytes(agreementData.FilePath),
                fileName: Path.GetFileName(agreementData.FilePath));
            string agreementId = AdobeSignApi.CreateAgreement(
                token, 
                agreementInfo: ExtractCreationData(agreementData, transientDocumentId));
            
            return new ResultData(
                RESULT_PATH,
                new [] { new DataPair(OUTCOME_NAME_DATA, agreementId) });
        } 

        private static AdobeSignAgreementInfo ExtractCreationData(
            AdobeSignAgreementCreationData creationData, 
            string transientDocumentId)
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
            AdobeSignAgreementCreationData creationData, 
            string transientDocumentId)
        {
            creationData.FullAgreementInfo.FileInfos = new[]
            {
                new AdobeSignFileInfo() { TransientDocumentId = transientDocumentId }
            };
            return creationData.FullAgreementInfo;
        }

        private static AdobeSignAgreementInfo ExtractCreationDataSimplified(
            AdobeSignAgreementCreationData creationData, 
            string transientDocumentId)
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
