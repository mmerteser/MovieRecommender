using MovieRecommender.Application.Utilities.Result;
using MovieRecommender.Core.Entities;

namespace MovieRecommender.Application.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IDataResult<User>> GetUserByUsernameOrEmailAsync(string usernameOrEmail);
        IDataResult<int> GetCurrentUserIdFromContext();
    }
}
