using MoviesDAL.Repositories;
using MoviesDAL.Repositories.Interfaces;
using MoviesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesBLL.Services
{
    public class RatingsService
    {
        private readonly IRepository<RatingModel> ratingRepos;

        public RatingsService(IRepository<RatingModel> ratingRepos)
        {
            this.ratingRepos = ratingRepos;
        }


        public async Task<List<RatingModel>> GetRatingsAsync()
        {
            return await ratingRepos.GetAsync();
        } 
        
        public async Task<RatingModel> GetRatingAsync(int id)
        {
            return await ratingRepos.GetByIdAsync(id);
        }
        
        public async Task<int> AddRatingAsync(RatingModel rating)
        {
            return await ratingRepos.AddAsync(rating);
        }
         
        public async Task UpdateRatingAsync(int id,RatingModel rating)
        {
            await ratingRepos.UpdateAsync(id,rating);
        }
        
        public async Task DeleteRatingAsync(int id)
        {
            await ratingRepos.DeleteAsync(id);
        }

        


    }
}
