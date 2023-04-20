using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRecommender.Application.AbstractServices;
using MovieRecommender.Application.Models.RequestModels.AuthenticationModels;

namespace MovieRecommender.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _authService.LoginAsync(request.UsernameOrEmail, request.Password);

            if(!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
