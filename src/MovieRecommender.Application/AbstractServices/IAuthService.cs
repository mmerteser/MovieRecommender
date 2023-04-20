using MovieRecommender.Application.Models.ViewModels;
using MovieRecommender.Application.Utilities.Result;

namespace MovieRecommender.Application.AbstractServices
{
    public interface IAuthService
    {
        Task<IDataResult<LoginVM>> LoginAsync(string usernameOrEmail, string password);
    }
}
