using Microsoft.Extensions.DependencyInjection;
using MoviesDAL.Repositories;
using MoviesDAL.Repositories.Interfaces;

namespace MoviesDAL
{
    public static class Configuration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IMoviesRepository, MoviesRepository>();
            services.AddTransient<IActorsRepository, ActorsRepository>();
            services.AddTransient<IRolesRepository,RolesRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericDapperRepository<>));
        }
    }
}
