using MovieRecommender.Application.Utilities.Result;

namespace MovieRecommender.Application.Utilities.HttpService
{
    public interface IHttpService
    {
        Task<IDataResult<TResponse>> GetAsync<TResponse>(string url, HttpAuthentication authentication = null);
        Task<IDataResult<TResponse>> PostAsync<TResponse>(string url,
                                                          object requestBody,
                                                          IDictionary<string, IEnumerable<string>> customHeaders = null,
                                                          HttpAuthentication authentication = null);
    }
}
