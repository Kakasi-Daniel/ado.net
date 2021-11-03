using MoviesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDAL.Repositories.Interfaces
{
    public interface IActorsRepository
    {
        Task<List<ActorModel>> GetActorsAsync(); 
        Task<ActorModel> GetActorAsync(int id);
        Task<int> PostActorAsync(ActorModel actor);
        Task<int> DeleteActorAsync(int id);
        Task UpdateActorAsync(int id, ActorModel actor);


    }
}
