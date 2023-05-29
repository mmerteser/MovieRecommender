using MovieRecommender.Application.Models.RequestModels.MovieModels;
using MovieRecommender.Application.Models.ViewModels;
using MovieRecommender.Core.Entities;

namespace MovieRecommender.Application.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<MovieVM>> GetAllMoviesAsync(GetMovieRequest request);
        Task<MovieVM> GetMovieByIdAsync(int id);
    }
}
