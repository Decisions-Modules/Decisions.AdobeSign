using Decisions.AdobeSign.Utility;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Properties; 

namespace Decisions.AdobeSign.Steps
{
    [AutoRegisterStep("Download Document", AdobeSignCategory)]
    [Writable]
    public class DownloadDocument : AbstractStep
    {
        [PropertyHidden]
        public override DataDescription[] InputData => new[] { 
            new DataDescription(typeof(string), AgreementIdLabel), 
            new DataDescription(typeof(string), FilePathLabel), };

        [PropertyHidden]
        protected override OutcomeScenarioData SuccessOutcomeScenarioData => new(ResultOutcomeLabel);
        
        protected override void ExecuteStep(StepStartData data)
        {
            string agreementId = (string)data.Data[AgreementIdLabel];
            string filePath = (string)data.Data[FilePathLabel];
            AdobeSignApi.GetTransientDocument(Token, agreementId, filePath);
        }
    }
}
