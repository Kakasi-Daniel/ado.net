using MoviesLibrary.DTOs;
using MoviesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDAL.Repositories.Interfaces
{
    public interface IRolesRepository
    {
        Task<List<RoleModel>> GetRolesAsync();
        Task<RoleModel> GetRoleByIdAsync(int id);
        Task<int> AddRoleAsync(RoleModel role);
        Task UpdateRoleAsync(int id, RoleModel role);
        Task DeleteRoleAsync(int id);

    }
}
