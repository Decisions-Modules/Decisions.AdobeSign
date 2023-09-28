using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Decisions.OAuth;
using DecisionsFramework;
using DecisionsFramework.Utilities.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Decisions.AdobeSign.Utility
{
    static partial class AdobeSignApi
    {
        private const string BaseUrisUrl = "https://api.na3.adobesign.com:443/api/rest/v6/baseUris";

        private static JsonSerializerSettings JsonSettings => new()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() },
                DateFormatString = "yyyy-MM-dd'T'HH:mm:ssZ",
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };
        
        private static string FetchBaseUriFromWeb(OAuthToken token)
        {
            AdobeSignBaseUriInfo result;
            try
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, BaseUrisUrl);
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.TokenData);
                requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                requestMessage.Headers.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                HttpResponseMessage httpResponseMessage = SendAsync(requestMessage);
                result = ParseResponse<AdobeSignBaseUriInfo>(httpResponseMessage);
            }
            catch (Exception ex)
            {
                throw new LoggedException("Cannot extract AdobeSign's base URL", ex);
            }
            if (result == null || string.IsNullOrWhiteSpace(result.apiAccessPoint))
                throw new LoggedException($"No valid response from AdobeSign API, try refreshing your OAuth Token");
            return result.apiAccessPoint.TrimEnd('/');
        } 

        private static TR ParseResponse<TR>(HttpResponseMessage response) where TR : new()
        {
            if (!response.IsSuccessStatusCode)
                throw new AdobeSignException(response.ReasonPhrase, response.StatusCode);
            var responseString = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TR>(responseString, JsonSettings);
        }

        private static HttpResponseMessage SendAsync(HttpRequestMessage requestMessage)
        {
            HttpResponseMessage httpResponseMessage = HttpClients
                .GetHttpClient(HttpClientAuthType.Normal)
                .SendAsync(requestMessage).Result;
            if (!httpResponseMessage.IsSuccessStatusCode)
                throw new AdobeSignException(httpResponseMessage.ReasonPhrase, httpResponseMessage.StatusCode);
            return httpResponseMessage;
        }

        private static HttpRequestMessage BuildHttpGetRequestMessage(
            OAuthToken token,
            string url,
            string mediaType,
            string contentType = null)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.TokenData);
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            if (contentType != null)
                requestMessage.Headers.TryAddWithoutValidation("Content-Type", contentType);
            return requestMessage;
        }
        
        private static HttpRequestMessage BuildHttpPostRequestMessage(
            OAuthToken token,
            string url,
            string contentType = null)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.TokenData);
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (contentType != null)
                requestMessage.Headers.TryAddWithoutValidation("Content-Type", contentType);
            return requestMessage;
        } 
    }
}
