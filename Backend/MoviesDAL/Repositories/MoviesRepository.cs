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
        public List<MovieModel> GetMovies()
        {
            List<MovieModel> Movies = new List<MovieModel>();
            string sqlQuerry = "select * from Movies";

            using (SqlConnection cnn = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;"))
            {
                using(SqlCommand cmd = new SqlCommand(sqlQuerry, cnn))
                {
                    cnn.Open();
                    using(SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            var movie = new MovieModel();

                            movie.Name = dr["Name"].ToString();
                            movie.ReleaseDate = Convert.ToDateTime(dr["ReleaseDate"]);

                            Movies.Add(movie);
                        }
                    }
                }
            }

            return Movies;
        }
    }
}
