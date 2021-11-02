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
        private readonly IMoviesRepository movieRepo;

        public MoviesService(IMoviesRepository movieRepo)
        {
            this.movieRepo = movieRepo;
        }
        public async Task<List<MovieModel>> GetMovieListAsync()
        {
            return await movieRepo.GetMoviesAsync();
        }
         public async Task<int> PostMovieAsync(MovieModel movie)
        {
           return await movieRepo.PostMovieAsync(movie);
        } 
        
        public async Task<MovieModel> GetMovieAsync(int movieID)
        {
            return await movieRepo.GetMovieAsync(movieID);
        }
        public async Task<int> DeleteMovieByIdAsync(int movieID)
        {
            return await movieRepo.DeleteMovieAsync(movieID);
        } 

        public async Task<MovieModel> UpdateMovieAsync(int movieId,MovieModel movie)
        {
            int rows = await movieRepo.UpdateMovieAsync(movieId, movie);
            if(rows != 0)
            {
                return await movieRepo.GetMovieAsync(movieId);
            }
            else
            {
                throw new Exception("Moviee not updated");
            }
        }
        
       


    }
}
