using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRecommender.Application.IntegratedApplicationModels.ResponseModel;
using MovieRecommender.Application.Utilities.HttpService;

namespace MovieRecommender.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IHttpService _httpService;

        public MoviesController(IHttpService httpService)
        {
            _httpService = httpService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _httpService.GetAsync<TmdbGetMovieModel>("https://api.themoviedb.org/3/movie/popular?api_key=a40b675d40f769ca7dae6d54b2bc961c&language=tr-TR&page=20");
            return Ok(result);
        }
    }
}
