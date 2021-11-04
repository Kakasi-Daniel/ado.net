using MoviesDAL.Repositories.Interfaces;
using MoviesLibrary.DTOs;
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
    public class RolesRepository : IRolesRepository
    {
        public async Task<int> AddRoleAsync(RoleModel role)
        {
            string sqlQuerry = @"insert into roles(RoleName,ActorID,MovieID) values(@RoleName,@ActorID,@MovieID)
                                 SELECT SCOPE_IDENTITY()";
            int insertedId = 0;
            using (SqlConnection cnn = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;"))
            using (SqlCommand cmd = new SqlCommand(sqlQuerry, cnn))
            {
                cnn.Open();

                cmd.Parameters.Add(new SqlParameter("@RoleName", role.Name));
                cmd.Parameters.Add(new SqlParameter("@ActorID", role.ActorId));
                cmd.Parameters.Add(new SqlParameter("@MovieID", role.MovieId));

                cmd.CommandType = CommandType.Text;
                insertedId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }
            return insertedId;
        }

        public async Task DeleteRoleAsync(int id)
        {
            string sql = "DELETE FROM roles WHERE ID=@RoleId";

            using (SqlConnection cnn = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;"))
            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                cnn.Open();
                cmd.Parameters.Add(new SqlParameter("@RoleId", id));
                cmd.CommandType = CommandType.Text;
                int rows = await cmd.ExecuteNonQueryAsync();
                if (rows == 0)
                {
                    throw new Exception("No rows affected");
                }
            }
        }

        public async Task<RoleModel> GetRoleByIdAsync(int id)
        {
            string sql = "SELECT * FROM roles WHERE ID=@RoleID";
            var role = new RoleModel();

            using (SqlConnection cnn = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;"))
            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                cnn.Open();
                cmd.Parameters.Add(new SqlParameter("@RoleID", id));

                using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                {
                    while (dr.Read())
                    {
                       role.Id = id;
                       role.Name = dr["RoleName"].ToString();
                       role.MovieId = Convert.ToInt32(dr["MovieID"].ToString());
                       role.ActorId = Convert.ToInt32(dr["ActorID"].ToString());
                    }
                }

            }

            return role;
        }

        public async Task<List<RoleModel>> GetRolesAsync()
        {
            string sql = "SELECT * FROM roles";
            var roles = new List<RoleModel>();

            using (SqlConnection cnn = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;"))
            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                cnn.Open();

                using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                {
                    while (dr.Read())
                    {
                        roles.Add(new RoleModel
                        {
                            Id = Convert.ToInt32(dr["ID"].ToString()),
                            Name = dr["RoleName"].ToString(),
                            MovieId = Convert.ToInt32(dr["MovieID"].ToString()),
                            ActorId = Convert.ToInt32(dr["ActorID"].ToString()),
                        });
                    }
                }

            }

            return roles;
        }

        public async Task UpdateRoleAsync(int id, RoleModel role)
        {
            string sql = "UPDATE roles SET RoleName=@RoleName,ActorID=@ActorId,MovieID=@MovieId WHERE ID=@RoleId";

            using (SqlConnection cnn = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;"))
            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                cnn.Open();

                cmd.Parameters.Add(new SqlParameter("@MovieId", role.MovieId));
                cmd.Parameters.Add(new SqlParameter("@ActorId", role.ActorId));
                cmd.Parameters.Add(new SqlParameter("@RoleName", role.Name));
                cmd.Parameters.Add(new SqlParameter("@RoleId", id));
              

                cmd.CommandType = CommandType.Text;

                int rows = await cmd.ExecuteNonQueryAsync();

                if (rows == 0)
                {
                    throw new Exception("No row affected.");
                }
            }
        }
    }
}
