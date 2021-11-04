using AutoMapper;
using MoviesDAL.Repositories.Interfaces;
using MoviesLibrary.DTOs;
using MoviesLibrary.Models;
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
        private readonly IMapper mapper;

        public RolesService(IRolesRepository rolesRepo,IMapper mapper)
        {
            this.rolesRepo = rolesRepo;
            this.mapper = mapper;
        }

        public async Task<List<RoleOut>> GetRolesAsync()
        {
            var roleOutList = new List<RoleOut>();

            var roles = await rolesRepo.GetRolesAsync();

            foreach(var role in roles)
            {
                roleOutList.Add(mapper.Map<RoleOut>(role));
            }

            return roleOutList;
        }
        
        public async Task<RoleOut> GetRoleByIdAsync(int id)
        {
            return mapper.Map<RoleOut>(await rolesRepo.GetRoleByIdAsync(id));
        }
        
        public async Task<int> AddRoleAsync(RoleAddIn role)
        {
            return await rolesRepo.AddRoleAsync(mapper.Map<RoleModel>(role));
        }
        
        public async Task DeleteRoleAsync(int id)
        {
            await rolesRepo.DeleteRoleAsync(id);
        }
        
        public async Task UpdateRoleAsync(int id,RoleUpdateIn role)
        {
            await rolesRepo.UpdateRoleAsync(id, mapper.Map<RoleModel>(role));
        }


    }
}
