namespace MovieRecommender.Application.IntegratedApplicationModels.ResponseModel
{
    public class TmdbGetMovieModel : TmdbBaseResponse
    {
        public int Page { get; set; }
        public List<Result> Results { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }

        public class Result
        {
            public bool Adult { get; set; }
            public string BackdropPath { get; set; }
            public int Id { get; set; }
            public string OriginalLanguage { get; set; }
            public string OriginalTitle { get; set; }
            public string Overview { get; set; }
            public double Popularity { get; set; }
            public string PosterPath { get; set; }
            public string ReleaseDate { get; set; }
            public string Title { get; set; }
            public bool Video { get; set; }
        }

    }
}
