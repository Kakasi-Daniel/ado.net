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
        private readonly IRepository<RoleModel> rolesRepo;
        private readonly IMapper mapper;

        public RolesService(IRepository<RoleModel> rolesRepo,IMapper mapper)
        {
            this.rolesRepo = rolesRepo;
            this.mapper = mapper;
        }

        public async Task<List<RoleOut>> GetRolesAsync()
        {
            var roles = await rolesRepo.GetAsync();

            return roles.Select(mapper.Map<RoleOut>).ToList();
        }
        
        public async Task<RoleOut> GetRoleByIdAsync(int id)
        {
            return mapper.Map<RoleOut>(await rolesRepo.GetByIdAsync(id));
        }
        
        public async Task<int> AddRoleAsync(RoleAddIn role)
        {
            return await rolesRepo.AddAsync(mapper.Map<RoleModel>(role));
        }
        
        public async Task DeleteRoleAsync(int id)
        {
            await rolesRepo.DeleteAsync(id);
        }
        
        public async Task UpdateRoleAsync(int id,RoleUpdateIn role)
        {
            await rolesRepo.UpdateAsync(id, mapper.Map<RoleModel>(role));
        }


    }
}
