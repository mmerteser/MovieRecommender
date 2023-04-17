namespace MovieRecommender.Core.Entities
{
    public class Movie : BaseEntity
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

        public virtual IEnumerable<MovieRating> MovieRatings { get; set; }
    }
}
