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
    [AutoRegisterStep("Get Agreement Info", adobeSignCategory)]
    [Writable]
    public class GetAgreementInfo : AbstractStep
    {
        [PropertyHidden]
        public override DataDescription[] InputData
        {
            get
            {
                var data = new DataDescription[] { new DataDescription(typeof(string), AbstractStep.AgreementIdLabel), };
                return base.InputData.Concat(data).ToArray();
            }
        }

        public override OutcomeScenarioData[] OutcomeScenarios
        {
            get
            {
                var data = new OutcomeScenarioData[] { new OutcomeScenarioData(resultOutcomeLabel, new DataDescription(typeof(AdobeSignAgreementInfo), AbstractStep.AgreementInfoLabel)) };
                return base.OutcomeScenarios.Concat(data).ToArray();
            }
        }

        protected override Object ExecuteStep(AdobeSignConnection conn, StepStartData data)
        {
            string  agreementId = (string)data.Data[AbstractStep.AgreementIdLabel];
            AdobeSignAgreementInfo res = AdobeSignApi.GetAgreementInfo(conn, agreementId);
            return res;
        }
    }
}
