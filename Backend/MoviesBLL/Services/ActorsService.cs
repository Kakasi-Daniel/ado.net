using MoviesDAL.Repositories.Interfaces;
using MoviesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesBLL.Services
{
    public class ActorsService
    {
        private readonly IRepository<ActorModel> actorsRepo;

        public ActorsService(IRepository<ActorModel> actorsRepo)
        {
            this.actorsRepo = actorsRepo;
        }

        public async Task<int> PostActorAsync(ActorModel actor)
        {
           return await actorsRepo.AddAsync(actor);
        }
        
        
        public async Task<ActorModel> GetActorByIDAsync(int id)
        {
            return await actorsRepo.GetByIdAsync(id);
        }
        
        public async Task<List<ActorModel>> GetActorsAsync()
        {
            return await actorsRepo.GetAsync();
        }

        public async Task DeleteActorAsync(int id)
        {
            await actorsRepo.DeleteAsync(id);
        }

        public async Task UpdateActorAsync(int id, ActorModel actor)
        {
            await actorsRepo.UpdateAsync(id, actor);
        }


    }
}
