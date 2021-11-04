using MoviesLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesDAL.Repositories.Interfaces
{
    public interface IRepository<T> where T:BaseModel
    {
        Task<List<T>> GetAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> AddAsync(T model);
        Task UpdateAsync(int id, T model);
        Task DeleteAsync(int id);
    }
}
