using MovieRecommender.Application.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommender.Application.Utilities.HttpService
{
    public interface IHttpService
    {
        Task<GetOneResult<TResponse>> GetAsync<TResponse>(string url, HttpAuthentication authentication = null);
    }
}
