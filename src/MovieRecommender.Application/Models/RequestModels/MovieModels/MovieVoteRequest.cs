namespace MovieRecommender.Application.Models.RequestModels.MovieModels
{
    public class MovieVoteRequest
    {
        public int MovieId { get; set; }
        public int Vote { get; set; }
        public string Note { get; set; }
    }
}
