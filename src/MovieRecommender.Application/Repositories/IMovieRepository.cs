using MovieRecommender.Application.Models.ViewModels;
using MovieRecommender.Core.Entities;

namespace MovieRecommender.Application.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<MovieVM>> GetAllMoviesAsync();
        Task<MovieVM> GetMovieByIdAsync(int id);
    }
}
