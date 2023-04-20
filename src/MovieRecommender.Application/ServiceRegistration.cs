﻿using Microsoft.Extensions.DependencyInjection;

namespace MovieRecommender.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServiceRegistration));
        }
    }
}
