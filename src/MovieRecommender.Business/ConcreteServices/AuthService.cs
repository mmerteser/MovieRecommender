using Microsoft.Extensions.Logging;
using MovieRecommender.Application.AbstractServices;
using MovieRecommender.Application.Constants;
using MovieRecommender.Application.Models.ViewModels;
using MovieRecommender.Application.Repositories;
using MovieRecommender.Application.Utilities.Result;
using MovieRecommender.Application.Utilities.Security.Hashing;
using MovieRecommender.Application.Utilities.Security.JWT;

namespace MovieRecommender.Business.ConcreteServices
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository,
                           ITokenHelper tokenHelper,
                           ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
            _logger = logger;
        }

        public async Task<IDataResult<LoginVM>> LoginAsync(string usernameOrEmail, string password)
        {
            try
            {
                var user = await _userRepository.GetUserByUsernameOrEmailAsync(usernameOrEmail);

                if (!user.Success)
                    return new ErrorDataResult<LoginVM>(new LoginVM(), user.Message);

                bool isVerify = HashingHelper.VerifyPasswordHash(password, user.Data.PasswordHash, user.Data.PasswordSalt);

                if (!isVerify)
                    return new ErrorDataResult<LoginVM>(new LoginVM(), Messages.WrongPassword);

                var token = _tokenHelper.CreateToken(user.Data);

                var loginVM = new LoginVM
                {
                    FirstLastName = user.Data.FirstLastName,
                    Token = token,
                };

                return new SuccessDataResult<LoginVM>(loginVM, Messages.Success);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"{nameof(AuthService)} {nameof(LoginAsync)} - {ex.Message}");
                return new ErrorDataResult<LoginVM>(new LoginVM(), Messages.Error);
            }
        }
    }
}
