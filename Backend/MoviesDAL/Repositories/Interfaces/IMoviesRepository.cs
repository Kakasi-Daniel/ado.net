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
        List<MovieModel> GetMovies();
    }
}
