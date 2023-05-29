namespace MovieRecommender.Application.Models.RequestModels.MovieModels
{
    public class RecommendMovieRequest
    {
        public int MovieId { get; set; }
        public string ToEmail { get; set; }
        public int UserId;
    }
}
