using FluentValidation;
using MovieRecommender.Application.Constants;
using MovieRecommender.Application.Models.RequestModels.MovieModels;

namespace MovieRecommender.Application.Validators.MovieValidators
{
    public class MovieVoteValidator : AbstractValidator<MovieVoteRequest>
    {
        public MovieVoteValidator()
        {
            RuleFor(i => i.MovieId)
                .NotEmpty()
                .NotNull()
                .WithMessage($"MovieId {ValidationMessages.CannotBeEmpty}");

            RuleFor(i => i.Note)
                .MaximumLength(4000)
                .WithMessage($"Not 4000 karakterden uzun olamaz");

            RuleFor(i => i.Vote)
                .InclusiveBetween(1,10)
                .WithMessage("Oyunuz 1-10 arasında olmalıdır");
        }
    }
}
