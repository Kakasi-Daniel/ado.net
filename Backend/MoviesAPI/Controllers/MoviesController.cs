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
    public class MoviesController : ControllerBase
    {
        private readonly MoviesService movieService;

        public MoviesController(MoviesService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieOut>>> GetAllMovies()
        {
            var movies = await movieService.GetMovieListAsync();

            return movies;
        }

        [HttpGet("actor/{id}")]
        public async Task<ActionResult<List<MovieOut>>> GetByActor(int id)
        {
            var movies = await movieService.GetMoviesByActorId(id);

            return movies;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieOut>> GetMovieID(int id)
        {
            var movie = await movieService.GetMovieAsync(id);

            return movie == null ? NotFound() : Ok(movie); 
        }


        [HttpPost]
        public async Task<ActionResult<MovieOut>> PostMovie([FromBody]MovieIn movie)
        {
            int id = await movieService.PostMovieAsync(movie);
            return await GetMovieID(id);
            
        }
        
       

        [HttpPut("{id}")]
        public async Task<ActionResult<MovieOut>> UpdateMovie([FromRoute]int id,[FromBody] MovieIn movie)
        {
            await movieService.UpdateMovieAsync(id, movie);

            return await GetMovieID(id);

        }

        [HttpDelete("{id}")]
        public async Task DeleteMovie(int id)
        {
            await movieService.DeleteMovieByIdAsync(id);
        }


    }
}