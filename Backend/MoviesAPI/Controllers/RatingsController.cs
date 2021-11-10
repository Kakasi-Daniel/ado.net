using Microsoft.AspNetCore.Mvc;
using MoviesBLL.Services;
using MoviesLibrary.DTOs;
using MoviesLibrary.Models;
using System.Collections.Generic;
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
        public async Task<ActionResult<List<RatingOut>>> GetRatings()
        {
            return await ratings.GetRatingsAsync();
        }

        [HttpGet]
        [Route("paged/{pageSize}/{pageNumber}")]
        public async Task<ActionResult<PaginationResult<RatingOut>>> GetAllMoviesPaginated(int pageSize, int pageNumber)
        {
            var ratingsList = await ratings.GetPaginated(pageSize > 10 ? 10 : pageSize, pageNumber < 1 ? 1 : pageNumber);

            return ratingsList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RatingOut>> GetRating([FromRoute] int id)
        {   
            var rating = await ratings.GetRatingAsync(id);

            return rating != null ? Ok(rating) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<RatingOut>> AddRating([FromBody] RatingIn rating)
        {
            int id = await ratings.AddRatingAsync(rating);

            return await GetRating(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RatingOut>> UpdateRating([FromRoute] int id, [FromBody] RatingIn rating)
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
