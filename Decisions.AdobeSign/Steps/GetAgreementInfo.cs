using Decisions.AdobeSign.Utility;
using Decisions.OAuth;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Properties;
using DecisionsFramework.ServiceLayer.Services.ContextData;

namespace Decisions.AdobeSign.Steps
{
    [AutoRegisterStep("Get Agreement Info", STEP_PARAMS_CATEGORY)]
    [Writable]
    public class GetAgreementInfo : AbstractStep
    {
        private const string RESULT_PATH = "Result";
        private const string INPUT_NAME_DATA = "Agreement Id";
        private const string OUTPUT_NAME_DATA = "Agreement Info";
        
        [PropertyHidden]
        public override DataDescription[] InputData => new DataDescription[]
        {
            new (typeof(string), INPUT_NAME_DATA)
        };

        protected override OutcomeScenarioData[] GetOutcomeScenarios()
        {
            return new OutcomeScenarioData[]
            {
                new (RESULT_PATH, new DataDescription(typeof(AdobeSignAgreementInfo), OUTPUT_NAME_DATA))
            };
        }  

        protected override ResultData ExecuteStep(StepStartData data, OAuthToken token)
        {
            var info = AdobeSignApi.GetAgreementInfo(
                token, 
                agreementId: (string)data.Data[INPUT_NAME_DATA]);
            return new ResultData(
                RESULT_PATH, 
                new[] { new DataPair(OUTPUT_NAME_DATA, info) });
        }
    }
}
