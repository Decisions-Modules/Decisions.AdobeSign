using Decisions.AdobeSign.Utility;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Properties; 

namespace Decisions.AdobeSign.Steps
{
    [AutoRegisterStep("Get Agreement Info", AdobeSignCategory)]
    [Writable]
    public class GetAgreementInfo : AbstractStep
    {
        [PropertyHidden]
        public override DataDescription[] InputData => new[]
            { new DataDescription(typeof(string), AgreementIdLabel) };

        [PropertyHidden]
        protected override OutcomeScenarioData SuccessOutcomeScenarioData => 
            new(ResultOutcomeLabel, new DataDescription(typeof(AdobeSignAgreementInfo), AgreementInfoLabel));

        protected override void ExecuteStep(StepStartData data)
        {
            string agreementId = (string)data.Data[AgreementIdLabel];
            AdobeSignApi.GetAgreementInfo(Token, agreementId);
        }
    }
}
