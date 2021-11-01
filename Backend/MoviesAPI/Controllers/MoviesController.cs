using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMoviesRepository moviesRepo;

        public MoviesController(IMoviesRepository moviesRepo)
        {
            this.moviesRepo = moviesRepo;
        }

        [HttpGet]
        public List<MovieModel> GetAllMovies()
        {
            List<MovieModel> movieNames = moviesRepo.GetMovies();

            return movieNames;
        }
    }
}
