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
using System.Collections.Generic;

namespace Decisions.AdobeSign
{
    [Writable]
    public abstract class AbstractStep : ISyncStep, IDataConsumer, IValidationSource
    {
        protected const string STEP_PARAMS_CATEGORY = "Integration/AdobeSign";
        private const string ERROR_PATH = "Error";
        private const string ERROR_NAME_DATA = "Error Info"; 
        
        [PropertyHidden] 
        public abstract DataDescription[] InputData { get; }

        protected abstract OutcomeScenarioData[] GetOutcomeScenarios();

        public OutcomeScenarioData[] OutcomeScenarios 
        {
            get
            {
                List<OutcomeScenarioData> output = new List<OutcomeScenarioData>()
                { 
                    new (ERROR_PATH, new DataDescription(typeof(AdobeSignErrorInfo), ERROR_NAME_DATA))
                };
                output.AddRange(GetOutcomeScenarios());
                return output.ToArray();
            }
        }

        [TokenPicker]
        [WritableValue]
        public string Token { get; set; }

        protected abstract ResultData ExecuteStep(StepStartData data, OAuthToken token);

        public ResultData Run(StepStartData data)
        {
            if (string.IsNullOrWhiteSpace(Token))
                throw new ArgumentException("Token is missing / invalid");
            try
            {
                OAuthToken token = FetchOAuthToken(Token);
                return ExecuteStep(data, token);
            }
            catch (Exception ex)
            {
                return new ResultData(
                    resultPath: ERROR_PATH, 
                    values: new[] { new DataPair(ERROR_NAME_DATA, AdobeSignErrorInfo.FromException(ex)) });
            } 
        } 

        public ValidationIssue[] GetValidationIssues()
        {
            if (Token == null)
                return new[] { new ValidationIssue("Token cannot be null") };
            try
            {
                FetchOAuthToken(Token);
            }
            catch (Exception ex)
            {
                return new[] { new ValidationIssue(ex.Message) };
            }
            return Array.Empty<ValidationIssue>();
        }

        private static OAuthToken FetchOAuthToken(string tokenId)
        {
            OAuthToken token = new ORM<OAuthToken>().Fetch(tokenId);
            if (token == null)
                throw new EntityNotFoundException($"Can not find token with TokenId=\"{tokenId}\"");
            if (token.TokenData == null)
                throw new LoggedException($"Token Entity '{token.EntityName}' has no AccessToken itself.");
            return token; 
        }
    }
}
