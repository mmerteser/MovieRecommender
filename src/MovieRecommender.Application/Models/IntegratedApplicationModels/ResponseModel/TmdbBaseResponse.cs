﻿namespace MovieRecommender.Application.Models.IntegratedApplicationModels.ResponseModel
{
    public class TmdbBaseResponse
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public bool Success { get; set; }
    }
}
