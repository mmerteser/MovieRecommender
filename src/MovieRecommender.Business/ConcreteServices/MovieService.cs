using AutoMapper;
using Microsoft.Extensions.Logging;
using MovieRecommender.Application;
using MovieRecommender.Application.AbstractServices;
using MovieRecommender.Application.Constants;
using MovieRecommender.Application.Models.IntegratedApplicationModels.ResponseModel;
using MovieRecommender.Application.Models.RequestModels.MovieModels;
using MovieRecommender.Application.Models.ViewModels;
using MovieRecommender.Application.Repositories;
using MovieRecommender.Application.Utilities;
using MovieRecommender.Application.Utilities.HttpService;
using MovieRecommender.Application.Utilities.Result;
using MovieRecommender.Core.Entities;
using System.Text;

namespace MovieRecommender.Business.ConcreteServices
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMovieRatingRepository _movieRatingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpService _httpService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly ILogger<MovieService> _logger;
        public MovieService(IMovieRepository movieRepository,
                            IMovieRatingRepository movieRatingRepository,
                            IHttpService httpService,
                            IMapper mapper,
                            ILogger<MovieService> logger,
                            IUserRepository userRepository,
                            IEmailService emailService)
        {
            _movieRepository = movieRepository;
            _httpService = httpService;
            _mapper = mapper;
            _logger = logger;
            _userRepository = userRepository;
            _movieRatingRepository = movieRatingRepository;
            _emailService = emailService;
        }

        /// <summary>
        /// Db'deki tüm movie kayıtlarını ratingleri ile birlikte döner.
        /// </summary>
        public async Task<IDataResult<GetMovieVM>> GetAll()
        {
            var movieVm = new GetMovieVM();
            try
            {
                _logger.LogInformation("GetAllMovies was started");

                var movies = await _movieRepository.GetAllMoviesAsync();

                movieVm.Movies = movies;

                return new SuccessDataResult<GetMovieVM>(movieVm);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"SaveMovies Error: {ex.Message}");

                return new ErrorDataResult<GetMovieVM>(movieVm);
            }
        }

        public async Task<IDataResult<MovieVM>> GetById(int id)
        {
            var movieVm = new MovieVM();
            try
            {
                _logger.LogInformation("GetAllMovies was started");

                var movies = await _movieRepository.GetMovieByIdAsync(id);

                movieVm = movies;

                return new SuccessDataResult<MovieVM>(movieVm);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"SaveMovies Error: {ex.Message}");

                return new ErrorDataResult<MovieVM>(movieVm);
            }
        }

        /// <summary>
        /// Themoviedb'den http request ile populer filmler alınıyor ve class'a maplenerek return ediliyor
        /// </summary>
        public async Task<IDataResult<TmdbGetMovieModel>> GetMoviesFromTmdb()
        {
            string api_key = Configuration.Api_Key;
            int record_count = Configuration.MovieRecordCount;

            StringBuilder @url = new StringBuilder();
            @url.Append("https://api.themoviedb.org/3/movie/popular?api_key=")
                .Append(api_key)
                .Append("&language=tr-TR&page=")
                .Append(record_count.ToString());

            return await _httpService.GetAsync<TmdbGetMovieModel>(url.ToString());
        }

        /// <summary>
        /// Themoviedb'den alınan filmler entity'e maplenerek local db'ye kaydedilir.
        /// </summary>
        /// <param name="tmdbMovies">Themoviedb'den alınan veri modeli</param>
        public async Task<IResult> SaveMovies(TmdbGetMovieModel tmdbMovies)
        {
            try
            {
                var mappedData = _mapper.Map<List<Movie>>(tmdbMovies.Results);

                await _movieRepository.AddRangeAsync(mappedData);
                int rowCount = await _movieRepository.SaveAsync();

                if (rowCount <= 0)
                    return new ErrorResult();

                return new SuccessResult();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"SaveMovies Error: {ex.Message}");
                return new ErrorResult(Messages.UnexpectedError);
            }
        }
        /// <summary>
        /// Film için oy ve yorum kaydeder.
        /// </summary>
        /// <param name="movieVote">Filme verilen oy ve yorum girilmelidir</param>
        public async Task<IResult> VoteMovie(MovieVoteRequest movieVote)
        {
            try
            {
                var user = _userRepository.GetCurrentUserIdFromContext();

                if (!user.Success)
                    return new ErrorResult(user.Message);

                var movie = await _movieRepository.FirstOrDefaultAsync(i => i.Id == movieVote.MovieId, includes: i => i.MovieRatings);

                var control = BusinessRules.Run(CheckIfExistsMovie(movie), CheckIfAlreadyVoted(movie, user.Data));

                if (control is not null)
                    return control;

                await _movieRatingRepository.AddAsync(new MovieRating
                {
                    Note = movieVote.Note ?? String.Empty,
                    Rate = movieVote.Vote,
                    UserId = user.Data,
                    MovieId = movieVote.MovieId
                });

                int affectedRowCount = await _movieRatingRepository.SaveAsync();

                if(affectedRowCount <= 0) 
                    return new ErrorResult();

                return new SuccessResult();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Film db'de bulunuyor mu kontrolü yapılır.
        /// </summary>
        /// <param name="movie"></param>
        private IResult CheckIfExistsMovie(Movie movie)
        {
            if (movie is null)
                return new ErrorResult("Uygun film bulunamadı");

            return new SuccessResult();
        }

        /// <summary>
        /// Kullanıcı aynı filme daha önce yorumda bulunmuş mu kontrolü yapılır.
        /// </summary>
        /// <param name="movie"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IResult CheckIfAlreadyVoted(Movie movie, int userId)
        {
            if (movie.MovieRatings.Any(i => i.UserId == userId))
                return new ErrorResult("Aynı filme tekrar yorum yapamazsınız");

            return new SuccessResult();
        }

        public async Task<IResult> SendMovieRecommendMail(RecommendMovieRequest recommendMovie)
        {
            try
            {
                var movie = await _movieRepository.GetByIdAsync(recommendMovie.MovieId);

                if (movie is null)
                    return new ErrorResult("Film bulunamadı");

                return await _emailService.SendMovieRecommendingMailAsync(recommendMovie.ToEmail, movie);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"SendMovieRecommendMail Error: {ex.Message}");
                throw;
            }
        }
    }
}
