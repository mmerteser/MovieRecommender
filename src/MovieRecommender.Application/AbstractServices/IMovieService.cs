using MovieRecommender.Application.Models.IntegratedApplicationModels.ResponseModel;
using MovieRecommender.Application.Models.RequestModels.MovieModels;
using MovieRecommender.Application.Models.ViewModels;
using MovieRecommender.Application.Utilities.Result;

namespace MovieRecommender.Application.AbstractServices
{
    public interface IMovieService
    {
        Task<IDataResult<TmdbGetMovieModel>> GetMoviesFromTmdb();
        Task<IDataResult<GetMovieVM>> GetAll();
        Task<IDataResult<MovieVM>> GetById(int id);
        Task<IResult> VoteMovie(MovieVoteRequest request);
        Task<IResult> SendMovieRecommendMail(RecommendMovieRequest recommendMovie);
        Task<IResult> SaveMovies(TmdbGetMovieModel tmdbMovies);
    }
}
