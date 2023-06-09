﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieRecommender.Application;
using MovieRecommender.Application.AbstractServices;
using MovieRecommender.Application.Repositories;

namespace MovieRecommender.BackgroundServices
{
    public class GetMovieBackgroundService : BackgroundService
    {
        private readonly ILogger<GetMovieBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;
        Timer timer;
        public GetMovieBackgroundService(IServiceProvider serviceProvider, ILogger<GetMovieBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogWarning("Worker running at: {time}", DateTimeOffset.Now);

                timer = new Timer(GetAsync, state: null, TimeSpan.Zero, TimeSpan.FromMinutes(Configuration.TimerTickFromMinute));

                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }

        private void GetAsync(object? state)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();

            IMovieService _movieService = scope.ServiceProvider.GetRequiredService<IMovieService>();
            IMovieRepository _movieRepository = scope.ServiceProvider.GetRequiredService<IMovieRepository>();

            _movieRepository = scope.ServiceProvider.GetRequiredService<IMovieRepository>();

            var movies = _movieService.GetMoviesFromTmdb().GetAwaiter().GetResult();

            if (!movies.Success)
                return;

            _movieService.SaveMovies(movies.Data).GetAwaiter().GetResult();
        }
    }
}
