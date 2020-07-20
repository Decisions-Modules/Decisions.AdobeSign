using Decisions.AdobeSign.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.AdobeSign.Utility
{
    static partial class AdobeSignApi
    {
        private static AuthenticationHeaderValue GetAuthHeader(string accessToken)
        {
            return new AuthenticationHeaderValue("Bearer", accessToken);
        }

        private static HttpClient GetClient(AdobeSignConnection connection)
        {
            string baseAddr = connection.BaseAddress.TrimEnd('/') + "/api/rest/v6/";
            HttpClient httpClient = new HttpClient { BaseAddress = new Uri(baseAddr) };
            httpClient.DefaultRequestHeaders.Authorization = GetAuthHeader(connection.AccessToken);
            return httpClient;
        }

        private static HttpClient GetJsonClient(AdobeSignConnection connection)
        {
            HttpClient httpClient = GetClient(connection);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            return httpClient;
        }

        private static string ParseRequestContent<T>(T content)
        {
                var jsonSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
                };
                string data = JsonConvert.SerializeObject(content, Formatting.None, jsonSettings);
                return data;
        }
        private static void CheckResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new AdobeSignException(response.ReasonPhrase, response.StatusCode);
            };
        }

        private static R ParseResponse<R>(HttpResponseMessage response) where R : new()
        {
            CheckResponse(response);
            var responseString = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<R>(responseString);

            return result;
        }


        private static R GetRequest<R>(AdobeSignConnection connection, string requestUri) where R : new()
        {
            HttpResponseMessage response = GetJsonClient(connection).GetAsync(requestUri).Result;
            return ParseResponse<R>(response);
        }

        private static R PostRequest<R,T>(AdobeSignConnection connection, string requestUri, T content) where R : new()
        {
            string data = ParseRequestContent(content);
            var contentStr = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = GetJsonClient(connection).PostAsync(requestUri, contentStr).Result;
            return ParseResponse<R>(response);
        }

    }
}
