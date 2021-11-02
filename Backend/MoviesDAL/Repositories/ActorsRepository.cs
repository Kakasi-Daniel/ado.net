﻿using MoviesDAL.Repositories.Interfaces;
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
    class ActorsRepository : IActorsRepository
    {
        public Task DeleteActorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActorModel> GetActorAsync(int id)
        {
            string sqlQuerry = "select * from actors where ID=@ActorID";
            var actor = new ActorModel();

            try
            {
                using (SqlConnection cnn = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;"))
                using (SqlCommand cmd = new SqlCommand(sqlQuerry, cnn))
                {
                    cnn.Open();
                    cmd.Parameters.Add(new SqlParameter("@ActorID", id));

                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            actor.ID = (int)dr["ID"];
                            actor.Name = dr["Name"].ToString();
                            actor.Surname = dr["Surname"].ToString();
                            actor.BornDate = Convert.ToDateTime(dr["BornDate"]);
                        }


                    }
                }

                return actor;
            }catch(Exception ex)
            {
                return actor;
            }
        }

        public async Task<List<ActorModel>> GetActorsAsync()
        {
            List<ActorModel> Actors = new List<ActorModel>();
            string sqlQuerry = "select * from actors";

            using (SqlConnection cnn = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;"))
            using (SqlCommand cmd = new SqlCommand(sqlQuerry, cnn))
            {
                cnn.Open();
                using (SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {

                    while (dr.Read())
                    {
                        var actor = new ActorModel();

                        actor.ID = (int)dr["ID"];
                        actor.Name = dr["Name"].ToString();
                        actor.Surname = dr["Surname"].ToString();
                        actor.BornDate = Convert.ToDateTime(dr["BornDate"]);

                        Actors.Add(actor);
                    }

                }
            }

            return Actors;
        }

        public async Task<int> PostActorAsync(ActorModel actor)
        {
            string sqlQuerry = @"insert into actors(Name,Surname,BornDate) values(@ActorName,@ActorSurname,@ActorBornDate)
                                 SELECT SCOPE_IDENTITY()";
            int insertedId = 0;

            using (SqlConnection cnn = new SqlConnection("Server=localhost;Database=Movies;Trusted_Connection=True;"))
            using (SqlCommand cmd = new SqlCommand(sqlQuerry, cnn))
            {
                cnn.Open();
                cmd.Parameters.Add(new SqlParameter("@ActorName", actor.Name));
                cmd.Parameters.Add(new SqlParameter("@ActorSurname", actor.Surname));
                cmd.Parameters.Add(new SqlParameter("@ActorBornDate", actor.BornDate));
                cmd.CommandType = CommandType.Text;
                insertedId = Convert.ToInt32(await cmd.ExecuteScalarAsync());

            }

            return insertedId;
        }
    }
}
