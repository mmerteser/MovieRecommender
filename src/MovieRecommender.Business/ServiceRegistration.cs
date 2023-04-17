using Microsoft.Extensions.DependencyInjection;
using MovieRecommender.Application.Utilities.HttpService;
using MovieRecommender.Business.ConcreteServices;

namespace MovieRecommender.Business
{
    public static class ServiceRegistration
    {
        public static void AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped<IHttpService, HttpService>();
        }
    }
}
