using Microsoft.Extensions.DependencyInjection;
using MovieRecommender.Application.Utilities.HttpService;

namespace MovieRecommender.DependencyResolver
{
    public static class ServiceRegistrations
    {
        public static void AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped<IHttpService, HttpService>();
        }
    }
}
