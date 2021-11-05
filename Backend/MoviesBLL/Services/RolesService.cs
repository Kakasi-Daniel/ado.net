using AutoMapper;
using MoviesDAL.Repositories.Interfaces;
using MoviesLibrary.DTOs;
using MoviesLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesBLL.Services
{
    public class RolesService
    {
        private readonly IRepository<RoleModel> rolesRepo;
        private readonly IRepository<ActorModel> actorsRepo;
        private readonly IRepository<MovieModel> moviesRepo;
        private readonly IMapper mapper;

        public RolesService(IRepository<RoleModel> rolesRepo, IRepository<ActorModel> actorsRepo, IRepository<MovieModel> moviesRepo, IMapper mapper)
        {
            this.rolesRepo = rolesRepo;
            this.actorsRepo = actorsRepo;
            this.moviesRepo = moviesRepo;
            this.mapper = mapper;
        }

        public async Task<List<RoleOut>> GetRolesAsync()
        {
            var roles = await rolesRepo.GetAsync();

            return roles.Select(mapper.Map<RoleOut>).ToList();
        }
        
        public async Task<RoleOutWithNames> GetRoleByIdAsync(int id)
        {
            var role = mapper.Map<RoleOut>(await rolesRepo.GetByIdAsync(id));
            var roleWithNames = mapper.Map<RoleOutWithNames>(role);
            var movie = await moviesRepo.GetByIdAsync(roleWithNames.MovieId);
            var actor = await actorsRepo.GetByIdAsync(roleWithNames.ActorId);
            roleWithNames.MovieName = movie.Name;
            roleWithNames.ActorName = actor.Name;

            return roleWithNames;
        }
        
        public async Task<int> AddRoleAsync(RoleIn role)
        {
            return await rolesRepo.AddAsync(mapper.Map<RoleModel>(role));
        }
        
        public async Task DeleteRoleAsync(int id)
        {
            await rolesRepo.DeleteAsync(id);
        }
        
        public async Task UpdateRoleAsync(int id,RoleIn role)
        {
            await rolesRepo.UpdateAsync(id, mapper.Map<RoleModel>(role));
        }


    }
}
