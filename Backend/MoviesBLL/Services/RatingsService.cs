using AutoMapper;
using MoviesDAL.Repositories.Interfaces;
using MoviesLibrary.DTOs;
using MoviesLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesBLL.Services
{
    public class RatingsService
    {
        private readonly IRepository<RatingModel> ratingRepos;
        private readonly IMapper mapper;

        public RatingsService(IRepository<RatingModel> ratingRepos,IMapper mapper)
        {
            this.ratingRepos = ratingRepos;
            this.mapper = mapper;
        }


        public async Task<List<RatingOut>> GetRatingsAsync()
        {
            var ratings = await ratingRepos.GetAsync();

            return ratings.Select(mapper.Map<RatingOut>).ToList();
        } 
        
        public async Task<RatingOut> GetRatingAsync(int id)
        {
            return mapper.Map<RatingOut>(await ratingRepos.GetByIdAsync(id));
        }
        
        public async Task<int> AddRatingAsync(RatingIn rating)
        {
            return await ratingRepos.AddAsync(mapper.Map<RatingModel>(rating));
        }
         
        public async Task UpdateRatingAsync(int id,RatingIn rating)
        {
            await ratingRepos.UpdateAsync(id,mapper.Map<RatingModel>(rating));
        }
        
        public async Task DeleteRatingAsync(int id)
        {
            await ratingRepos.DeleteAsync(id);
        }

        


    }
}
