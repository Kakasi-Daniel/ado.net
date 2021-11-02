using MoviesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDAL.Repositories.Interfaces
{
    public interface IMoviesRepository
    {
        Task<List<MovieModel>> GetMoviesAsync();
        Task<MovieModel> GetMovieAsync(int movieID);
        Task<int> PostMovieAsync(MovieModel movie);
        Task<int> DeleteMovieAsync(int id);
        Task<int> UpdateMovieAsync(int movieID,MovieModel movie);
    }
}
