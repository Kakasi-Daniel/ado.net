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
    public class RatingsRepository : IRatingsRepository
    {
        public async Task<int> AddRatingAsync(RatingModel rating)
        {
            string sqlQuerry = @"insert into ratings(MovieId,Rating) values(@MovieId,@RatingValue)
                                 SELECT SCOPE_IDENTITY()";
            int insertedId = 0;

            using (SqlConnection cnn = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;"))
            using (SqlCommand cmd = new SqlCommand(sqlQuerry, cnn))
            {
                cnn.Open();
                cmd.Parameters.Add(new SqlParameter("@MovieId", rating.MovieId));
                cmd.Parameters.Add(new SqlParameter("@RatingValue", rating.Rating));
                
                cmd.CommandType = CommandType.Text;
                insertedId = Convert.ToInt32(await cmd.ExecuteScalarAsync());

            }

            return insertedId;
        }

        public async Task DeleteRatingAsync(int id)
        {
            string sql = "DELETE FROM ratings WHERE ID=@RatingId";
            
            using (SqlConnection cnn = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;"))
            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                cnn.Open();
                cmd.Parameters.Add(new SqlParameter("@RatingId", id));
                cmd.CommandType = CommandType.Text;
                int rows = await cmd.ExecuteNonQueryAsync();
                if(rows == 0)
                {
                    throw new Exception("No rows affected");
                }
            }
        }

        public async Task<RatingModel> GetRatingByIdAsync(int id)
        {
            string sql = "SELECT * FROM ratings WHERE ID=@RatingID";
            var rating = new RatingModel();

            using (SqlConnection cnn = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;"))
            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                cnn.Open();
                cmd.Parameters.Add(new SqlParameter("@RatingID",id));

                using (SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {
                    while (dr.Read())
                    {
                        rating.MovieId = Convert.ToInt32(dr["MovieId"].ToString());
                        rating.Rating = Convert.ToInt32(dr["Rating"].ToString());
                        rating.Id = id;
                    }
                }

            }

            return rating;
        }

        public async Task<List<RatingModel>> GetRatingsAsync()
        {
            string sql = "SELECT * FROM ratings";
            var ratings = new List<RatingModel>();

            using (SqlConnection cnn = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;"))
            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                cnn.Open();

                using (SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {
                    while (dr.Read())
                    {
                        ratings.Add(new RatingModel
                        {
                            Id = Convert.ToInt32(dr["ID"].ToString()),
                            MovieId = Convert.ToInt32(dr["MovieId"].ToString()),
                            Rating = Convert.ToInt32(dr["Rating"].ToString())
                        });
                    }
                }

            }

            return ratings;
        }

        public async Task UpdateRatingAsync(int id, RatingModel rating)
        {
            string sql = "UPDATE ratings SET MovieId=@MovieID,Rating=@Rating WHERE ID=@RatingID";

            using(SqlConnection cnn = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;"))
            using(SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                cnn.Open();

                cmd.Parameters.Add(new SqlParameter("@MovieId",rating.MovieId));
                cmd.Parameters.Add(new SqlParameter("@Rating",rating.Rating));
                cmd.Parameters.Add(new SqlParameter("@RatingID",id));

                cmd.CommandType = CommandType.Text;

                int rows = await cmd.ExecuteNonQueryAsync();

                if(rows == 0)
                {
                    throw new Exception("No row affected.");
                }
            }
        }
    }
}
