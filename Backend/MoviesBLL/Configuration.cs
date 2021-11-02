﻿using Microsoft.Extensions.DependencyInjection;
using MoviesBLL.Services;
using System.Data.SqlClient;

namespace MoviesBLL
{
    public static class Configuration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ReportsService>();
            services.AddScoped<MoviesService>();
            services.AddScoped<ActorsService>();

            MoviesDAL.Configuration.RegisterServices(services);
        }
    }
}
