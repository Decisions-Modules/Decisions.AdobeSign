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
using System.Collections.Specialized;
using System.Web;

namespace Decisions.AdobeSign
{
    [Writable]
    public abstract class AbstractStep : ISyncStep, IDataConsumer, IDataProducer, IValidationSource
    {
        public const string adobeSignCategory = "Integration/AdobeSign";

        protected const string errorOutcomeLabel = "Error";
        protected const string resultOutcomeLabel = "Result";
        protected const string doneOutcomeLabel = "Done";
        protected const string errorOutcomeDataLabel = "Error info";
        protected const string resultLabel = "RESULT";

        protected const string tokenLabel = "Token";
        protected const string agreementCreationDataLabel = "Agreement Data";
        protected const string AgreementIdLabel = "Agreement Id";
        protected const string AgreementInfoLabel = "Agreement Info";
        protected const string FilePathLabel = "File Path";

        [PropertyHidden]
        public virtual DataDescription[] InputData
        {
            get
            {
                return new DataDescription[] { };
            }
        }

        private const int errorOutcomeIndex = 0;
        private const int resultOutcomeIndex = 1;
        public virtual OutcomeScenarioData[] OutcomeScenarios
        {
            get
            {
                return new OutcomeScenarioData[] { new OutcomeScenarioData(errorOutcomeLabel, new DataDescription(typeof(AdobeSignErrorInfo), errorOutcomeDataLabel)) };
            }
        }

        [TokenPicker]
        [WritableValue]
        public string Token { get; set; }

        private AdobeSignConnection CreateConnection(string id)
        {
            ORM<OAuthToken> orm = new ORM<OAuthToken>();
            var token = orm.Fetch(id);
            if (token == null)
                throw new EntityNotFoundException($"Can not find token with TokenId=\"{id}\"");

            if (token.TokenData != null)
            {
                return new AdobeSignConnection() { BaseAddress = GetBaseApi(token), AccessToken = token.TokenData };
            };

            throw new LoggedException($"Token Entity '{token.EntityName}' has no AccessToken itself.");
        }
        private string GetBaseApi(OAuthToken oAuthToken)
        {
            string authorizationResponse = oAuthToken.FullAuthorizationResponse;
            if(String.IsNullOrEmpty(authorizationResponse))
                throw new LoggedException("Can't extract AdobeSign's base URL");
            int qIdx = authorizationResponse.IndexOf('?');
            if (qIdx == -1)
                throw new LoggedException("Can't extract AdobeSign's base URL");
            string queryString = authorizationResponse.Substring(qIdx + 1);
            NameValueCollection qs = HttpUtility.ParseQueryString(queryString);

            var endpoint = qs["api_access_point"];
            if (qs == null) throw new LoggedException("Can't extract AdobeSign's base URL");
            return endpoint;
        }

        public ResultData Run(StepStartData data)
        {
            try
            {
                AdobeSignConnection conn = CreateConnection(Token);

                Object res = ExecuteStep(conn, data);

                var outputData = OutcomeScenarios[resultOutcomeIndex].OutputData;
                var exitPointName = OutcomeScenarios[resultOutcomeIndex].ExitPointName;

                if (outputData != null && outputData.Length > 0)
                    return new ResultData(exitPointName, new DataPair[] { new DataPair(outputData[0].Name, res) });
                else
                    return new ResultData(exitPointName);
            }
            catch (Exception ex)
            {
                AdobeSignErrorInfo ErrInfo = AdobeSignErrorInfo.FromException(ex);
                return new ResultData(errorOutcomeLabel, new DataPair[] { new DataPair(errorOutcomeDataLabel, ErrInfo) });
            }
        }

        protected abstract Object ExecuteStep(AdobeSignConnection conn, StepStartData data);

        public ValidationIssue[] GetValidationIssues()
        {
            if (Token == null)
                return new ValidationIssue[] { new ValidationIssue("Token cannot be null") };

            try
            {
                var accessToken = CreateConnection(Token);
            }
            catch (Exception ex)
            {
                return new ValidationIssue[] { new ValidationIssue(ex.Message ?? ex.ToString()) };
            }
            return new ValidationIssue[0];
        }

    }
}
