using FluentValidation;
using MovieRecommender.Application.Constants;
using MovieRecommender.Application.Models.RequestModels.AuthenticationModels;

namespace MovieRecommender.Application.Validators.AuthValidatitors
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(i => i.UsernameOrEmail)
            .NotEmpty()
                    .WithMessage(ValidationMessages.UsernameCannotBeEmpty)
            .NotNull()
                    .WithMessage(ValidationMessages.UsernameCannotBeEmpty);
            RuleFor(i => i.Password)
            .NotEmpty()
                    .WithMessage(ValidationMessages.PasswordCannotBeEmpty)
            .NotNull()
                    .WithMessage(ValidationMessages.PasswordCannotBeEmpty);
        }
    }
}
