using AutoMapper;
using MovieRecommender.Application;
using MovieRecommender.Application.AbstractServices;
using MovieRecommender.Application.IntegratedApplicationModels.ResponseModel;
using MovieRecommender.Application.Repositories;
using MovieRecommender.Application.Utilities.HttpService;
using MovieRecommender.Application.Utilities.Results;
using MovieRecommender.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MovieRecommender.Application.IntegratedApplicationModels.ResponseModel.TmdbGetMovieModel;

namespace MovieRecommender.Business.ConcreteServices
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IHttpService _httpService;
        private readonly IMapper _mapper;
        public MovieService(IMovieRepository movieRepository, IHttpService httpService, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _httpService = httpService;
            _mapper = mapper;
        }

        public async Task<GetOneResult<TmdbGetMovieModel>> GetMovies()
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
        public async Task<Application.Utilities.Results.Result> SaveMovies(TmdbGetMovieModel tmdbMovies)
        {
            var result = new Application.Utilities.Results.Result();

            var mappedData = _mapper.Map<List<Movie>>(tmdbMovies.Results);

            await _movieRepository.AddRangeAsync(mappedData);
            int rowCount = await _movieRepository.SaveAsync();

            result.Success = rowCount > 0;

            return result;
        }
    }
}
