using MoviesDAL.Repositories.Interfaces;
using MoviesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesBLL.Services
{
   public class MoviesService
    {
        private readonly IRepository<MovieModel> movieRepo;

        public MoviesService(IRepository<MovieModel> movieRepo)
        {
            this.movieRepo = movieRepo;
        }
        public async Task<List<MovieModel>> GetMovieListAsync()
        {
            return await movieRepo.GetAsync();
        }
         public async Task<int> PostMovieAsync(MovieModel movie)
        {
           return await movieRepo.AddAsync(movie);
        } 
        
        public async Task<MovieModel> GetMovieAsync(int movieID)
        {
            return await movieRepo.GetByIdAsync(movieID);
        }

        public async Task DeleteMovieByIdAsync(int movieID)
        {
            await movieRepo.DeleteAsync(movieID);
        } 

        public async Task UpdateMovieAsync(int movieId,MovieModel movie)
        {
            
            await movieRepo.UpdateAsync(movieId, movie);
 
        }
        
       


    }
}
