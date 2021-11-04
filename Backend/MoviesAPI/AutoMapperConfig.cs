using AutoMapper;
using MoviesLibrary.DTOs;
using MoviesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<RoleModel, RoleOut>().ReverseMap();
            CreateMap<RoleModel, RoleAddIn>().ReverseMap();
            CreateMap<RoleModel, RoleUpdateIn>().ReverseMap();
        }
    }
}
