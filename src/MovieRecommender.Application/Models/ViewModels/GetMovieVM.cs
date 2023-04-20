using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommender.Application.Models.ViewModels
{
    public class GetMovieVM
    {
        public int Page { get; set; }
        public IEnumerable<MovieVM> Movies { get; set; }
    }

    public class MovieVM
    {
        public bool Adult { get; set; }
        public string? BackdropPath { get; set; }
        public int? TmdbId { get; set; }
        public string? OriginalLanguage { get; set; }
        public string? OriginalTitle { get; set; }
        public string? Overview { get; set; }
        public double Popularity { get; set; }
        public string? PosterPath { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Title { get; set; }
        public bool Video { get; set; }
        public double VoteCount { get; set; }
        public double VoutAverage { get; set; }
        public IEnumerable<MovieRatingVM>? MovieRatings { get; set; }
    }

    public class MovieRatingVM
    {
        public int Rate { get; set; }
        public string Note { get; set; }
        public string VotedUsername { get; set; }
    }
}
