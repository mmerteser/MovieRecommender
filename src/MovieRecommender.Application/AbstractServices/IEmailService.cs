using MovieRecommender.Application.Utilities.Result;
using MovieRecommender.Core.Entities;

namespace MovieRecommender.Application.AbstractServices
{
    public interface IEmailService
    {
        IResult SendEmail(string email,string mailBody);
        Task<IResult> SendMovieRecommendingMailAsync(string email, Movie movie);
    }
}
