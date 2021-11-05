using AutoMapper;
using MoviesLibrary.DTOs;
using MoviesLibrary.Models;

namespace MoviesAPI
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<RoleOut, RoleOutWithNames>();
            CreateMap<RoleModel, RoleOutWithNames>();
            CreateMap<RoleModel, RoleOut>();
            CreateMap<RoleIn,RoleModel >();
            CreateMap<MovieModel, MovieOut>();
            CreateMap<MovieIn,MovieModel >();
            CreateMap<ActorModel, ActorOut>();
            CreateMap<ActorIn,ActorModel >();
            CreateMap<ActorModel,ActorsRolesOut>();
            CreateMap<RatingModel, RatingOut>();
            CreateMap<RatingIn,RatingModel >();
        }
    }
}
