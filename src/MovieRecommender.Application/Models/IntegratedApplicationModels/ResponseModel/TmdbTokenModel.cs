﻿namespace MovieRecommender.Application.Models.IntegratedApplicationModels.ResponseModel
{
    public class TmdbTokenModel
    {
        public bool Success { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string RequestToken { get; set; }
    }
}
