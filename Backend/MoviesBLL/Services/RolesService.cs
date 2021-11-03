using MoviesDAL.Repositories.Interfaces;
using MoviesLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesBLL.Services
{
    public class RolesService
    {
        private readonly IRolesRepository rolesRepo;

        public RolesService(IRolesRepository rolesRepo)
        {
            this.rolesRepo = rolesRepo;
        }

        public async Task<List<RoleOut>> GetRolesAsync()
        {
            return await rolesRepo.GetRolesAsync();
        }
        
        public async Task<RoleOut> GetRoleByIdAsync(int id)
        {
            return await rolesRepo.GetRoleByIdAsync(id);
        }
        
        public async Task<int> AddRoleAsync(RoleAddIn role)
        {
            return await rolesRepo.AddRoleAsync(role);
        }
        
        public async Task DeleteRoleAsync(int id)
        {
            await rolesRepo.DeleteRoleAsync(id);
        }
        
        public async Task UpdateRoleAsync(int id,RoleUpdateIn role)
        {
            await rolesRepo.UpdateRoleAsync(id, role);
        }


    }
}
