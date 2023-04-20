using AutoMapper;
using MovieRecommender.Application.Models.IntegratedApplicationModels.ResponseModel;
using MovieRecommender.Application.Models.ViewModels;
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

            CreateMap<Movie, MovieVM>().ReverseMap();

        }
    }
}
