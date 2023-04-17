namespace MovieRecommender.Core.Entities
{
    public class MovieRating : BaseEntity
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public int Rate { get; set; }
        public string Note { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual User User { get; set; }

    }
}
