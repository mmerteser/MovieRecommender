using Microsoft.Extensions.Logging;
using MovieRecommender.Application.Utilities.HttpService;
using MovieRecommender.Application.Utilities.Result;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace MovieRecommender.Business.ConcreteServices
{
    internal class HttpService : IHttpService
    {
        ILogger<HttpService> _logger;

        public HttpService(ILogger<HttpService> logger)
        {
            _logger = logger;
        }

        public async Task<IDataResult<TResponse>> GetAsync<TResponse>(string url, HttpAuthentication authentication = null)
        {
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

                if (typeof(TResponse) == typeof(string))
                    return new ErrorDataResult<TResponse>((TResponse)Convert.ChangeType(string.Empty, typeof(TResponse)), "Servisten hata alındı!", 400);

                return new ErrorDataResult<TResponse>((TResponse)Activator.CreateInstance<TResponse>(), "Servisten hata alındı!", 400);
            }

            if (typeof(TResponse) == typeof(string))
                return new SuccessDataResult<TResponse>((TResponse)Convert.ChangeType(await response.Content.ReadAsStringAsync(), typeof(TResponse)));

            var dataObjects = await response.Content.ReadFromJsonAsync<TResponse>();

            return new SuccessDataResult<TResponse>(dataObjects);

        }

        public async Task<IDataResult<TResponse>> PostAsync<TResponse>(string url, object requestBody
            , IDictionary<string, IEnumerable<string>> customHeaders = null
            , HttpAuthentication authentication = null)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(url, nameof(url));

            using var client = new HttpClient();

            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            if (authentication != null)
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(authentication.AuthenticationSchemeName,
                                                                          authentication.AuthenticationKey);

            if (customHeaders is not null)
            {
                foreach (var header in customHeaders)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            var data = JsonConvert.SerializeObject(requestBody);

            var content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);

            var stringResult = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                string @status = JsonConvert.SerializeObject(response.StatusCode);
                string @uri = JsonConvert.SerializeObject(response.RequestMessage.RequestUri);

                _logger.LogCritical($"Request: {@uri} \n\nResponse {@status} - {stringResult}");

                if (typeof(TResponse) == typeof(string))
                    return new ErrorDataResult<TResponse>((TResponse)Convert.ChangeType(string.Empty, typeof(TResponse)), $"Servisten hata alındı! {stringResult}", 400);

                var obj = await response.Content.ReadFromJsonAsync<TResponse>();
                return new ErrorDataResult<TResponse>((TResponse)Convert.ChangeType(obj, typeof(TResponse)), "Servisten hata alındı!", 400);
            }

            if (typeof(TResponse) == typeof(string))
            {
                return new SuccessDataResult<TResponse>((TResponse)Convert.ChangeType(stringResult, typeof(TResponse)));
            }

            var dataObjects = await response.Content.ReadFromJsonAsync<TResponse>();

            return new SuccessDataResult<TResponse>(dataObjects);
        }
    }
}
