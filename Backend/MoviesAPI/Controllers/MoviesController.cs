using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesBLL.Services;
using MoviesDAL.Repositories.Interfaces;
using MoviesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<List<MovieModel>>> GetAllMovies()
        {
            List<MovieModel> movieNames = await movieService.GetMovieListAsync();

            return movieNames;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieID(int id)
        {
            MovieModel movie = await movieService.GetMovieAsync(id);

            return movie.Name == null ? NotFound() : Ok(movie); 
        }


        [HttpPost]
        public async Task<IActionResult> PostMovie([FromBody]MovieModel movie)
        {
            int id = await movieService.PostMovieAsync(movie);
            movie.Id = id;
            return Ok(movie);
            
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            int rows = await movieService.DeleteMovieByIdAsync(id);

            return rows == 0 ? NotFound() : Ok();
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MovieModel>> UpdateMovie([FromRoute]int id,[FromBody] MovieModel movie)
        {
            var result = await movieService.UpdateMovieAsync(id, movie);

            return result;

        }

        
    }
}