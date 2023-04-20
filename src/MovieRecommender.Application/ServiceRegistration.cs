using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using MovieRecommender.Application.Models.RequestModels.AuthenticationModels;
using MovieRecommender.Application.Models.RequestModels.MovieModels;
using MovieRecommender.Application.Utilities.Security.JWT;
using MovieRecommender.Application.Validators.AuthValidatitors;
using MovieRecommender.Application.Validators.MovieValidators;

namespace MovieRecommender.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServiceRegistration));
            services.AddScoped<ITokenHelper, JwtHelper>();
            services.AddFluentValidation();
            services.AddValidators();
        }
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<LoginRequest>, LoginValidator>();
            services.AddSingleton<IValidator<MovieVoteRequest>, MovieVoteValidator>();
        }
    }
}
