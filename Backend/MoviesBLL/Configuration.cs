﻿using Microsoft.Extensions.DependencyInjection;
using MoviesBLL.Services;
using System.Data.SqlClient;

namespace MoviesBLL
{
    public static class Configuration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<MoviesService>();
            services.AddScoped<ActorsService>();
            services.AddScoped<RatingsService>();
            services.AddScoped<RolesService>();

            MoviesDAL.Configuration.RegisterServices(services);
        }
    }
}
