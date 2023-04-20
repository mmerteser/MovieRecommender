using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRecommender.Application.IntegratedApplicationModels.ResponseModel;
using MovieRecommender.Application.Repositories;
using MovieRecommender.Application.Utilities.HttpService;
using MovieRecommender.Core.Entities;

namespace MovieRecommender.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IHttpService _httpService;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public MoviesController(IHttpService httpService, IMovieRepository movieRepository, IMapper mapper)
        {
            _httpService = httpService;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _httpService.GetAsync<TmdbGetMovieModel>("https://api.themoviedb.org/3/movie/popular?api_key=a40b675d40f769ca7dae6d54b2bc961c&language=tr-TR&page=20");

            var mappedData = _mapper.Map<List<Movie>>(result.Data.Results);

            await _movieRepository.AddRangeAsync(mappedData);
            await _movieRepository.SaveAsync();

            return Ok(result);
        }
    }
}
