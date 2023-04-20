using AutoMapper;
using MovieRecommender.Application.IntegratedApplicationModels.ResponseModel;
using MovieRecommender.Core.Entities;

namespace MovieRecommender.Application.Mapping
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            CreateMap<TmdbGetMovieModel.Result, Movie>()
                .ForMember(i => i.TmdbId, opt => opt.MapFrom(i => i.Id))
                .ForMember(i => i.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
