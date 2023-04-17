using MovieRecommender.Application.Repositories;
using MovieRecommender.Core.Entities;
using MovieRecommender.DataAccess.Context;

namespace MovieRecommender.DataAccess.Repositories
{
    public class MovieRatingRepository : GenericRepository<MovieRating>, IMovieRatingRepository
    {
        public MovieRatingRepository(MovieDbContext context) : base(context)
        {
        }
    }
}
