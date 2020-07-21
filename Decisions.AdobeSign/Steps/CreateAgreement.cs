using Decisions.AdobeSign.Utility;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.AdobeSign
{
    [AutoRegisterStep("Create Agreement", adobeSignCategory)]
    [Writable]
    public class CreateAgreement : AbstractStep
    {
        [PropertyHidden]
        public override DataDescription[] InputData
        {
            get
            {
                var data = new DataDescription[] { new DataDescription(typeof(AdobeSignAgreementCreationData), AbstractStep.agreementCreationDataLabel), };
                return base.InputData.Concat(data).ToArray();
            }
        }

        public override OutcomeScenarioData[] OutcomeScenarios
        {
            get
            {
                var data = new OutcomeScenarioData[] { new OutcomeScenarioData(resultOutcomeLabel, new DataDescription(typeof(string), AgreementIdLabel)) };
                return base.OutcomeScenarios.Concat(data).ToArray();
            }
        }

        protected override Object ExecuteStep(AdobeSignConnection conn, StepStartData data)
        {
            var agreementData = (AdobeSignAgreementCreationData)data.Data[AbstractStep.agreementCreationDataLabel];

            var fileData = File.ReadAllBytes(agreementData.FilePath);
            string transientDocumentId = AdobeSignApi.CreateTransientDocument(conn, fileData, Path.GetFileName(agreementData.FilePath));

            AdobeSignAgreementInfo agreementInfo = ExtractCreationData(agreementData, transientDocumentId);
            string id = AdobeSignApi.CreateAgreement(conn, agreementInfo);
            return id;
        }

        private AdobeSignAgreementInfo ExtractCreationData(AdobeSignAgreementCreationData creationData, string transientDocumentId)
        {
            if (creationData.InfoType == AdobeSignAgreementCreationType.Full)
            {
                creationData.FullAgreementInfo.FileInfos = new AdobeSignFileInfo[]
                        {
                            new AdobeSignFileInfo()  { TransientDocumentId = transientDocumentId }
                        };
                return creationData.FullAgreementInfo;
            }

            var res = new AdobeSignAgreementInfo();
            if (creationData.Recipients != null)
            {
                AdobeSignParticipantSetInfo[] participantsSetInfo = new AdobeSignParticipantSetInfo[creationData.Recipients.Length];

                for (int i = 0; i < creationData.Recipients.Length; i++)
                {
                    var recipient = creationData.Recipients[i];
                    var participantInfo = new AdobeSignParticipantSetInfo()
                    {
                        MemberInfos = new AdobeSignParticipantInfo[] { new AdobeSignParticipantInfo(){Email = recipient.Email} },
                        Name = recipient.Name,
                        Role = AgreementParticipantRole.SIGNER,
                        Order = i+1
                    };
                    participantsSetInfo[i] = participantInfo;

                }
                res.ParticipantSetsInfo = participantsSetInfo;
            }

            res.EmailOption = new AdobeSignEmailOption()
            {
                SendOptions = new AdobeSignSendOptions()
                {
                    InitEmails = AgreementEmailNotification.ALL,
                    InFlightEmails = AgreementEmailNotification.NONE,
                    CompletionEmails = AgreementEmailNotification.ALL,
                }
            };

            res.FileInfos = new AdobeSignFileInfo[]{ new AdobeSignFileInfo()  { TransientDocumentId = transientDocumentId } };
            res.Name = creationData.AgreementName;
            res.SignatureType = AgreementSignatureType.ESIGN;
            res.State = AgreementState.IN_PROCESS;

            return res;
        }

    }
}
