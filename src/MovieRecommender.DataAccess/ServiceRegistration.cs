using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieRecommender.Application;
using MovieRecommender.Application.Repositories;
using MovieRecommender.DataAccess.Context;
using MovieRecommender.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommender.DataAccess
{
    public static class ServiceRegistration
    {
        public static void AddDataAccessService(this IServiceCollection services)
        {
            services.AddDbContext<MovieDbContext>(i => i.UseSqlServer(Configuration.ConnectionString));

            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieRatingRepository, MovieRatingRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            AddDataSeeding();
        }

        public static void AddDataSeeding()
        {
            var seedData = new DataSeeding();
            seedData.SeedAsync().GetAwaiter().GetResult();
        }
    }
}
