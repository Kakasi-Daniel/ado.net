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
    public class MoviesRepository : IMoviesRepository
    {
        public async Task<int> DeleteMovieAsync(int id)
        {
            string sqlQuerry = "delete from movies where ID=@MovieID";
            int rows = 0;

            try
            {
                using (SqlConnection cnn = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;"))
                using (SqlCommand cmd = new SqlCommand(sqlQuerry, cnn))
                {
                    cnn.Open();
                    cmd.Parameters.Add(new SqlParameter("@MovieID", id));
                    cmd.CommandType = CommandType.Text;
                    rows = await cmd.ExecuteNonQueryAsync();
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return rows;
        }

        public async Task<MovieModel> GetMovieAsync(int movieID)
        {
            var movie = new MovieModel();
            string sqlQuerry = "select * from Movies where ID=@MovieID";

            try
            {
                using (SqlConnection cnn = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;"))
                using (SqlCommand cmd = new SqlCommand(sqlQuerry, cnn))
                {
                    cmd.Parameters.Add(new SqlParameter("@MovieID", movieID));
                    cnn.Open();
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            movie.Name = dr["Name"].ToString();
                            movie.Id = movieID;
                            movie.ReleaseDate = Convert.ToDateTime(dr["ReleaseDate"]);
                        }
                       
                    }
                }
                return movie;
            }
            catch(Exception exc)
            {
                return movie;
            }

            
        }

        public async Task<List<MovieModel>> GetMoviesAsync()
        {
            List<MovieModel> Movies = new List<MovieModel>();
            string sqlQuerry = "select * from Movies";

            using (SqlConnection cnn = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;"))
            using (SqlCommand cmd = new SqlCommand(sqlQuerry, cnn))
            {
                cnn.Open();
                using(SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {

                    while (dr.Read())
                    {
                        var movie = new MovieModel();

                        movie.Name = dr["Name"].ToString();
                        movie.ReleaseDate = Convert.ToDateTime(dr["ReleaseDate"]);
                        movie.Id = (int)dr["ID"];

                        Movies.Add(movie);
                    }
                    
                }
            }

            return Movies;
        }

        public async Task<int> PostMovieAsync(MovieModel movie)
        {
            string sqlQuery = "insert into movies(Name,ReleaseDate) values(@MovieName,@MovieReleaseDate) SELECT SCOPE_IDENTITY();";
            int insertedId = 0;

            using (SqlConnection cnn = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;"))
            using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
            {
                cnn.Open();
                cmd.Parameters.Add(new SqlParameter("@MovieName", movie.Name));
                cmd.Parameters.Add(new SqlParameter("@MovieReleaseDate", movie.ReleaseDate));
                cmd.CommandType = CommandType.Text;
                insertedId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }

            return insertedId;
        }

        public async Task<int> UpdateMovieAsync(int movieID, MovieModel movie)
        {

            int rows = 0;
            string nameUpdater = movie.Name != null ? "Name = @MovieName" : "";
            string dateUpdater = movie.ReleaseDate != DateTime.MinValue ? "ReleaseDate = @MovieReleaseDate" : "";
            string comma = (nameUpdater != "" && dateUpdater != "") ? "," : "";
            string sqlQuery = @$"UPDATE movies SET 
                                    Name = @MovieName,
                                    ReleaseDate = @MovieReleaseDate
                                WHERE ID=@MovieID";

            using (SqlConnection cnn = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;"))
            using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
            {
                cnn.Open();

                cmd.Parameters.Add(new SqlParameter("@MovieID", movieID));

                if (nameUpdater != "")
                {
                    cmd.Parameters.Add(new SqlParameter("@MovieName", movie.Name));
                }
                if(dateUpdater != "")
                {
                    cmd.Parameters.Add(new SqlParameter("@MovieReleaseDate", movie.ReleaseDate));
                }

                cmd.CommandType = CommandType.Text;
               rows =  await cmd.ExecuteNonQueryAsync();
            }

            return rows;




        }
    }
}
