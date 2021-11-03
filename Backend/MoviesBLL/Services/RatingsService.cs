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
        private readonly IRatingsRepository ratingsService;

        public RatingsService(IRatingsRepository ratingsService)
        {
            this.ratingsService = ratingsService;
        }


        public async Task<List<RatingModel>> GetRatingsAsync()
        {
            return await ratingsService.GetRatingsAsync();
        } 
        
        public async Task<RatingModel> GetRatingAsync(int id)
        {
            return await ratingsService.GetRatingByIdAsync(id);
        }
        
        public async Task<int> AddRatingAsync(RatingModel rating)
        {
            return await ratingsService.AddRatingAsync(rating);
        }
         
        public async Task UpdateRatingAsync(int id,RatingModel rating)
        {
            await ratingsService.UpdateRatingAsync(id,rating);
        }
        
        public async Task DeleteRatingAsync(int id)
        {
            await ratingsService.DeleteRatingAsync(id);
        }

        


    }
}
