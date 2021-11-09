using MoviesLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesDAL.Repositories.Interfaces
{
    public interface IMoviesRepository : IRepository<MovieModel>
    {
        Task<List<MovieModel>> GetByActorId(int id);
        Task<int> GetNumberOfRows();
    }
}
