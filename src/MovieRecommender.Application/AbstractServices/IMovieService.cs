using MovieRecommender.Application.IntegratedApplicationModels.ResponseModel;
using MovieRecommender.Application.Utilities.Results;

namespace MovieRecommender.Application.AbstractServices
{
    public interface IMovieService
    {
        Task<GetOneResult<TmdbGetMovieModel>> GetMovies();
        Task<Result> SaveMovies(TmdbGetMovieModel tmdbMovies);
    }
}
