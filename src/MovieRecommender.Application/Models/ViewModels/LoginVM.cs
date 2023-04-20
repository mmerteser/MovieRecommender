using MovieRecommender.Application.Utilities.Security.JWT;

namespace MovieRecommender.Application.Models.ViewModels
{
    public class LoginVM
    {
        public string FirstLastName { get; set; }
        public AccessToken Token { get; set; } = new();
    }
}
