using MoviesLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDAL.Repositories.Interfaces
{
    public interface IRolesRepository
    {
        Task<List<RoleOut>> GetRolesAsync();
        Task<RoleOut> GetRoleByIdAsync(int id);
        Task<int> AddRoleAsync(RoleAddIn role);
        Task UpdateRoleAsync(int id, RoleUpdateIn role);
        Task DeleteRoleAsync(int id);

    }
}
