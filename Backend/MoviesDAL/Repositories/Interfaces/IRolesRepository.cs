using MoviesLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesDAL.Repositories.Interfaces
{
    public interface IRolesRepository : IRepository<RoleModel>
    {
        Task<List<RoleModel>> GetRolesFromMovie(int id);
    }
}
