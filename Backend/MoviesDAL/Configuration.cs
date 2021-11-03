using Microsoft.Extensions.DependencyInjection;
using MoviesDAL.Repositories;
using MoviesDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDAL
{
    public static class Configuration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IMoviesRepository, MoviesRepository>();
            services.AddTransient<IActorsRepository, ActorsRepository>();
            services.AddTransient<IRatingsRepository, RatingsRepository>();
            services.AddTransient<IRolesRepository, RolesRepository>();
        }
    }
}
