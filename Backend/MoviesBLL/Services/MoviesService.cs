using AutoMapper;
using MoviesDAL.Repositories.Interfaces;
using MoviesLibrary.DTOs;
using MoviesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesBLL.Services
{
    public class MoviesService
    {
        private readonly IMoviesRepository movieRepo;
        private readonly IMapper mapper;

        public MoviesService(IMoviesRepository movieRepo,IMapper mapper)
        {
            this.movieRepo = movieRepo;
            this.mapper = mapper;
        }

        public async Task<PaginationResult<MovieOut>> GetPaginated(int pageSize,int pageNumber)
        {
            var noOfMovies = await movieRepo.GetNumberOfRows();
            var noOfPages = Convert.ToInt32(Math.Ceiling(((decimal)noOfMovies / (decimal)pageSize)));
            var currentPage = pageNumber > noOfPages ? noOfPages : pageNumber;
            var movies = await movieRepo.GetPaginatedAsync(pageSize, currentPage);
            var moviesData = movies.Select(mapper.Map<MovieOut>).ToList();
            

            return new PaginationResult<MovieOut>(moviesData, noOfPages,currentPage);
        }
        
        public async Task<List<MovieOut>> GetMoviesByActorId(int id)
        {
            var movies = await movieRepo.GetByActorId(id);

            return movies.Select(mapper.Map<MovieOut>).ToList();
        }

        public async Task<List<MovieOut>> GetMovieListAsync()
        {
            var movies = await movieRepo.GetAsync();

            return movies.Select(mapper.Map<MovieOut>).ToList();
        }
         public async Task<int> PostMovieAsync(MovieIn movie)
        {
           return await movieRepo.AddAsync(mapper.Map<MovieModel>(movie));
        } 
        
        public async Task<MovieOut> GetMovieAsync(int movieID)
        {
            return mapper.Map<MovieOut>(await movieRepo.GetByIdAsync(movieID));
        }

        public async Task DeleteMovieByIdAsync(int movieID)
        {
            await movieRepo.DeleteAsync(movieID);
        } 

        public async Task UpdateMovieAsync(int movieId,MovieIn movie)
        {
            await movieRepo.UpdateAsync(movieId, mapper.Map<MovieModel>(movie));
        }
        
       


    }
}
