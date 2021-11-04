using Dapper;
using MoviesDAL.Repositories.Interfaces;
using MoviesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDAL.Repositories
{
    class DapperMoviesRepository : IMoviesRepository
    {

        private readonly IDbConnection db;

        public DapperMoviesRepository()
        {
            this.db = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;");
        }

        public async Task<int> DeleteMovieAsync(int id)
        {
           var rows = await db.ExecuteAsync("DELETE FROM movies WHERE ID=@Id", new { Id = id });

            return rows;
        }

        public async Task<MovieModel> GetMovieAsync(int movieID)
        {
            var result = await db.QueryAsync<MovieModel>("SELECT * FROM movies WHERE ID=@Id", new { Id = movieID});
            var movie = result.SingleOrDefault();
            
            return movie;
     
        }

        public async Task<List<MovieModel>> GetMoviesAsync()
        {
            var queryes = await db.QueryAsync<MovieModel>("SELECT * FROM movies");
            var queryesList = queryes.ToList();

            return queryesList;
        }

        public async Task<int> PostMovieAsync(MovieModel movie)
        {
            string sql = "insert into movies(Name,ReleaseDate) values(@Name,@ReleaseDate) SELECT SCOPE_IDENTITY();";

            var query = await db.QueryAsync<int>(sql, movie);
            int insertedId = query.Single();

            return insertedId;

        }

        public async Task<int> UpdateMovieAsync(int movieID, MovieModel movie)
        {
            string sql = @"UPDATE movies SET 
                                  Name = @Name,
                                  ReleaseDate = @ReleaseDate
                                  WHERE ID=@Id";

            movie.Id = movieID;

            var rows = await db.ExecuteAsync(sql, movie);

            return rows;
        }
    }
}
