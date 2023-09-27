using Decisions.OAuth;
using DecisionsFramework;
using DecisionsFramework.Data.ORMapper;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Properties;
using DecisionsFramework.Design.Properties.Attributes;
using DecisionsFramework.ServiceLayer.Services.ContextData;
using System;
using System.Linq;

namespace Decisions.AdobeSign
{
    [Writable]
    public abstract class AbstractStep : ISyncStep, IDataConsumer, IValidationSource
    {
        protected const string AdobeSignCategory = "Integration/AdobeSign";
        protected const string ResultOutcomeLabel = "Result";
        protected const string AgreementCreationDataLabel = "Agreement Data";
        protected const string AgreementIdLabel = "Agreement Id";
        protected const string AgreementInfoLabel = "Agreement Info";
        protected const string FilePathLabel = "File Path";
        private const string ErrorOutcomeLabel = "Error";
        private const string ErrorOutcomeDataLabel = "Error info";

        [PropertyHidden] 
        public abstract DataDescription[] InputData { get; }

        public OutcomeScenarioData[] OutcomeScenarios => 
            new[] { 
                new OutcomeScenarioData(
                    ErrorOutcomeLabel, 
                    new DataDescription(typeof(AdobeSignErrorInfo), ErrorOutcomeDataLabel)),
                SuccessOutcomeScenarioData
            };

        [PropertyHidden] 
        protected abstract OutcomeScenarioData SuccessOutcomeScenarioData { get; }

        [TokenPicker]
        [WritableValue]
        public string Token { get; set; }

        public ResultData Run(StepStartData data)
        {
            if (OutcomeScenarios.Length <= 1)
                throw new LoggedException("This step has an internal error; more than one outcome paths expected");
           
            try
            {
                ExecuteStep(data);
                return new ResultData(OutcomeScenarios.Last().ExitPointName);
            }
            catch (Exception ex)
            {
                AdobeSignErrorInfo errInfo = AdobeSignErrorInfo.FromException(ex);
                return new ResultData(
                    ErrorOutcomeLabel, 
                    new[] { new DataPair(ErrorOutcomeDataLabel, errInfo) });
            }
        }

        protected abstract void ExecuteStep(StepStartData data);

        public ValidationIssue[] GetValidationIssues()
        {
            if (Token == null)
                return new[] { new ValidationIssue("Token is missing / invalid") };

            try
            { 
                OAuthToken oAuthToken = new ORM<OAuthToken>().Fetch(Token);
                if (oAuthToken == null)
                    throw new EntityNotFoundException($"Can not find token with TokenId=\"{Token}\"");
                if (oAuthToken.TokenData == null)
                    throw new LoggedException($"Token Entity '{oAuthToken.EntityName}' has no AccessToken itself.");
            }
            catch (Exception ex)
            {
                return new[] { new ValidationIssue(ex.Message) };
            }
            
            return Array.Empty<ValidationIssue>();
        }

    }
}
