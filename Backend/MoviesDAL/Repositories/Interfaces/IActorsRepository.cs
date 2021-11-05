using MoviesLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesDAL.Repositories.Interfaces
{
    public interface IActorsRepository : IRepository<ActorModel>
    {
        Task<List<ActorModel>> GetActorsFromMovie(int id);
    }
}
