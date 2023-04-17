using Microsoft.Extensions.Logging;
using MovieRecommender.Application.Utilities.HttpService;
using MovieRecommender.Application.Utilities.Results;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MovieRecommender.Business.ConcreteServices
{
    internal class HttpService : IHttpService
    {
        private readonly ILogger<HttpService> _logger;

        public HttpService(ILogger<HttpService> logger)
        {
            _logger = logger;
        }

        public async Task<GetOneResult<TResponse>> GetAsync<TResponse>(string url, HttpAuthentication authentication = null)
        {
            var result = new GetOneResult<TResponse>();

            ArgumentNullException.ThrowIfNullOrEmpty(url, nameof(url));

            using HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


            if (authentication != null)
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(authentication.AuthenticationSchemeName,
                                                                          authentication.AuthenticationKey);

            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                string content = JsonConvert.SerializeObject(response.Content);
                string reqcontent = JsonConvert.SerializeObject(response.RequestMessage.Content);
                string status = JsonConvert.SerializeObject(response.StatusCode);
                string uri = JsonConvert.SerializeObject(response.RequestMessage.RequestUri);

                _logger.LogCritical($"Request: {uri} - {reqcontent} \n\nResponse {status} - {content}");

                result.Message = "Unknown error";
                result.Success = false;

                if (typeof(TResponse) == typeof(string))
                {
                    result.Data = (TResponse)Convert.ChangeType(string.Empty, typeof(TResponse));
                }
                else
                    result.Data = (TResponse)Activator.CreateInstance<TResponse>();

                return result;
            }

            if (typeof(TResponse) == typeof(string))
                result.Data = (TResponse)Convert.ChangeType(await response.Content.ReadAsStringAsync(), typeof(TResponse));
            else
            {
                var dataObjects = await response.Content.ReadFromJsonAsync<TResponse>();
                result.Data = dataObjects;

            }

            return result;
        }
    }
}
