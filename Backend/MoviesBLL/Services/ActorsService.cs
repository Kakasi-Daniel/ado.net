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
        private readonly IActorsRepository actorsRepo;

        public ActorsService(IActorsRepository actorsRepo)
        {
            this.actorsRepo = actorsRepo;
        }

        public async Task<int> PostActorAsync(ActorModel actor)
        {
           return await actorsRepo.PostActorAsync(actor);
        }
        
        
        public async Task<ActorModel> GetActorByIDAsync(int id)
        {
            return await actorsRepo.GetActorAsync(id);
        }
        
        public async Task<List<ActorModel>> GetActorsAsync()
        {
            return await actorsRepo.GetActorsAsync();
        }
    }
}
