using Decisions.AdobeSign.Utility;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.AdobeSign.Steps
{
    [AutoRegisterStep("Download Document", adobeSignCategory)]
    [Writable]
    public class DownloadDocument : AbstractStep
    {
        [PropertyHidden]
        public override DataDescription[] InputData
        {
            get
            {
                var data = new DataDescription[] { new DataDescription(typeof(string), AbstractStep.AgreementIdLabel), new DataDescription(typeof(string), AbstractStep.FilePathLabel), };
                return base.InputData.Concat(data).ToArray();
            }
        }

        public override OutcomeScenarioData[] OutcomeScenarios
        {
            get
            {
                var data = new OutcomeScenarioData[] { new OutcomeScenarioData(resultOutcomeLabel) };
                return base.OutcomeScenarios.Concat(data).ToArray();
            }
        }

        protected override Object ExecuteStep(AdobeSignConnection conn, StepStartData data)
        {
            string agreementId = (string)data.Data[AbstractStep.AgreementIdLabel];
            string filePath = (string)data.Data[AbstractStep.FilePathLabel];
            AdobeSignApi.GetTransientDocument(conn, agreementId, filePath);
            return null;
        }
    }
}
