using Decisions.AdobeSign.Utility;
using Decisions.OAuth;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Properties;
using DecisionsFramework.ServiceLayer.Services.ContextData;

namespace Decisions.AdobeSign.Steps
{
    [AutoRegisterStep("Download Document", STEP_PARAMS_CATEGORY)]
    [Writable]
    public class DownloadDocument : AbstractStep
    {
        private const string RESULT_PATH = "Done";
        private const string INPUT_NAME_FILE_PATH = "File Path";
        private const string INPUT_NAME_AGREEMENT_ID = "Agreement Id";
        
        
        [PropertyHidden]
        public override DataDescription[] InputData => new DataDescription[] 
        { 
            new (typeof(string), INPUT_NAME_AGREEMENT_ID), 
            new (typeof(string), INPUT_NAME_FILE_PATH)
        };

        protected override OutcomeScenarioData[] GetOutcomeScenarios()
        {
            return new OutcomeScenarioData[]
            {
                new (RESULT_PATH)
            };
        }
        
        protected override ResultData ExecuteStep(StepStartData data, OAuthToken token)
        {
            string agreementId = (string)data.Data[INPUT_NAME_AGREEMENT_ID];
            AdobeSignApi.GetTransientDocument(
                token, 
                agreementId, 
                filePath: (string)data.Data[INPUT_NAME_FILE_PATH]);
            return new ResultData(
                resultPath: RESULT_PATH, 
                values: new [] { new DataPair(RESULT_PATH, agreementId) });
        }
    }
}
