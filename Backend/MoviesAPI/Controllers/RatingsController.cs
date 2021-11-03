using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesBLL.Services;
using MoviesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly RatingsService ratings;

        public RatingsController(RatingsService ratings)
        {
            this.ratings = ratings;
        }

        [HttpGet]
        public async Task<List<RatingModel>> GetRatings()
        {
            return await ratings.GetRatingsAsync();
        }

        [HttpGet("{id}")]
        public async Task<RatingModel> GetRating([FromRoute] int id)
        {
            return await ratings.GetRatingAsync(id);
        }

        [HttpPost]
        public async Task<RatingModel> AddRating([FromBody] RatingModel rating)
        {
            int id = await ratings.AddRatingAsync(rating);

            return await GetRating(id);
        }

        [HttpPut("{id}")]
        public async Task<RatingModel> UpdateRating([FromRoute] int id, [FromBody] RatingModel rating)
        {
            await ratings.UpdateRatingAsync(id, rating);

            return await GetRating(id);
        }

        [HttpDelete("{id}")]
        public async Task DeleteRating([FromRoute] int id)
        {
            await ratings.DeleteRatingAsync(id);
        }
    }
}
