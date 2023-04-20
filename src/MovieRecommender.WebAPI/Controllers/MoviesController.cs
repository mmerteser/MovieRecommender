using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRecommender.Application.AbstractServices;
using MovieRecommender.Application.Models.RequestModels.MovieModels;

namespace MovieRecommender.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _movieService.GetAll();

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("Id={id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _movieService.GetById(id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("vote")]
        public async Task<IActionResult> VoteMovie([FromBody] MovieVoteRequest request)
        {
            var result = await _movieService.VoteMovie(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("recommend")]
        public async Task<IActionResult> RecommendMovie([FromBody] RecommendMovieRequest request)
        {
            var result = await _movieService.SendMovieRecommendMail(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
