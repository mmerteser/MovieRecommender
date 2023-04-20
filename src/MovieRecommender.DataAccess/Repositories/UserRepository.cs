using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MovieRecommender.Application.Constants;
using MovieRecommender.Application.Repositories;
using MovieRecommender.Application.Utilities.Result;
using MovieRecommender.Core.Entities;
using MovieRecommender.DataAccess.Context;

namespace MovieRecommender.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserRepository(MovieDbContext context,
                              ILogger<UserRepository> logger,
                              IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Http context'i içerisinden UserId değerini alır
        /// </summary>
        /// <returns>Int tipinde UserId geriye döner</returns>
        public IDataResult<int> GetCurrentUserIdFromContext()
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.UserId).Value;

            if (String.IsNullOrEmpty(userId))
                return new ErrorDataResult<int>(int.MinValue);

            return new SuccessDataResult<int>(Convert.ToInt32(userId));
        }

        public async Task<IDataResult<User>> GetUserByUsernameOrEmailAsync(string usernameOrEmail)
        {
            _logger.LogInformation("GetUserByUsernameOrEmailAsync running for {usernameOrEmail}", usernameOrEmail);

            var user = await base.FirstOrDefaultAsync(i => i.Username.Equals(usernameOrEmail) || i.Email.Equals(usernameOrEmail));

            if (user is null)
                return new ErrorDataResult<User>(new User(), Messages.UserNotFound);
            else
            {
                if (user.IsBlocked)
                    return new ErrorDataResult<User>(new User(), Messages.Blocked);
                else
                    return new SuccessDataResult<User>(user, Messages.Success);
            }
        }
    }
}
