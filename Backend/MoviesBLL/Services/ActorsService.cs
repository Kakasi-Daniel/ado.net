using AutoMapper;
using MoviesDAL.Repositories;
using MoviesDAL.Repositories.Interfaces;
using MoviesLibrary.DTOs;
using MoviesLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesBLL.Services
{
    public class ActorsService
    {
        private readonly IActorsRepository actorsRepo;
        private readonly IRolesRepository rolesRepo;
        private readonly IMapper mapper;

        public ActorsService(IActorsRepository actorsRepo, IRolesRepository rolesRepo, IMapper mapper)
        {
            this.actorsRepo = actorsRepo;
            this.rolesRepo = rolesRepo;
            this.mapper = mapper;
        }

        public async Task<List<ActorsRolesOut>> GetActorsFromMovie(int movieID)
        {
            var actors = await actorsRepo.GetActorsFromMovie(movieID);
            var roles = await rolesRepo.GetRolesFromMovie(movieID);

            var actorsList = new List<ActorsRolesOut>();

            foreach (var actor in actors)
            {
                actorsList.Add(mapper.Map<ActorsRolesOut>(actor));
            }

            foreach (var actor in actorsList)
            {
                actor.RoleName = roles.Find(role => role.ActorId == actor.Id).Name;
            }

            return actorsList;
        }

        public async Task<int> PostActorAsync(ActorIn actor)
        {
           return await actorsRepo.AddAsync(mapper.Map<ActorModel>(actor));
        }
        
        
        public async Task<ActorOut> GetActorByIDAsync(int id)
        {
            return mapper.Map<ActorOut>(await actorsRepo.GetByIdAsync(id));
        }
        
        public async Task<List<ActorOut>> GetActorsAsync()
        {
            var actors = await actorsRepo.GetAsync();

            return actors.Select(mapper.Map<ActorOut>).ToList();
        }

        public async Task DeleteActorAsync(int id)
        {
            await actorsRepo.DeleteAsync(id);
        }

        public async Task UpdateActorAsync(int id, ActorIn actor)
        {
            await actorsRepo.UpdateAsync(id, mapper.Map<ActorModel>(actor));
        }


    }
}
