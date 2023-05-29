using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieRecommender.Application.Models.RequestModels.MovieModels;
using MovieRecommender.Application.Models.ViewModels;
using MovieRecommender.Application.Repositories;
using MovieRecommender.Core.Entities;
using MovieRecommender.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommender.DataAccess.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly MovieDbContext _context;
        private readonly ILogger<MovieRepository> _logger;
        public MovieRepository(MovieDbContext context, ILogger<MovieRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<MovieVM>> GetAllMoviesAsync(GetMovieRequest request)
        {
            try
            {
                var query = await _context.Movies.Include(i => i.MovieRatings)
                                                 .ThenInclude(i => i.User)
                                                 .OrderBy(i => i.CreatedDate)
                                                 .Skip((request.PageNumber - 1) * request.PageSize)
                                                 .Take(request.PageSize)
                                                 .ToListAsync();

                if (query is null || !query.Any())
                    return new List<MovieVM>();

                var result = query.Select(i => new MovieVM
                {
                    MovieId = i.Id,
                    Adult = i.Adult,
                    BackdropPath = i.BackdropPath,
                    OriginalLanguage = i.OriginalLanguage,
                    OriginalTitle = i.OriginalTitle,
                    Overview = i.Overview,
                    Popularity = i.Popularity,
                    PosterPath = i.PosterPath,
                    ReleaseDate = i.ReleaseDate ?? DateTime.MinValue,
                    Title = i.Title,
                    TmdbId = i.TmdbId,
                    Video = i.Video,
                    VoteCount = !i.MovieRatings.Any() ? 0d : i.MovieRatings.Count(),
                    VoutAverage = !i.MovieRatings.Any() ? 0d : i.MovieRatings.Average(i => i.Rate),
                    MovieRatings = !i.MovieRatings.Any() ? new() : i.MovieRatings.Select(rating => new MovieRatingVM
                    {
                        Rate = rating.Rate,
                        Note = rating.Note,
                        VotedUsername = rating.User.Username
                    }).ToList()
                });

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                throw;
            }
        }

        public async Task<MovieVM> GetMovieByIdAsync(int id)
        {
            try
            {
                var query = await _context.Movies.Include(i => i.MovieRatings).ThenInclude(i => i.User).FirstOrDefaultAsync(i => i.Id == id);

                if (query is null)
                    return null;

                var result = new MovieVM
                {
                    MovieId = query.Id,
                    Adult = query.Adult,
                    BackdropPath = query.BackdropPath,
                    OriginalLanguage = query.OriginalLanguage,
                    OriginalTitle = query.OriginalTitle,
                    Overview = query.Overview,
                    Popularity = query.Popularity,
                    PosterPath = query.PosterPath,
                    ReleaseDate = query.ReleaseDate,
                    Title = query.Title,
                    TmdbId = query.TmdbId,
                    Video = query.Video,
                    VoteCount = !query.MovieRatings.Any() ? 0d : query.MovieRatings.Count(),
                    VoutAverage = !query.MovieRatings.Any() ? 0d : query.MovieRatings.Average(i => i.Rate),
                    MovieRatings = !query.MovieRatings.Any() ? new() : query.MovieRatings.Select(i => new MovieRatingVM
                    {
                        Rate = i.Rate,
                        Note = i.Note,
                        VotedUsername = i.User.Username
                    }).ToList()
                };

                return result;

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                throw;
            }

        }
    }
}
