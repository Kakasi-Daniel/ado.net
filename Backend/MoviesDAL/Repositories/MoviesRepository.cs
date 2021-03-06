using Dapper;
using Microsoft.Extensions.Options;
using MoviesDAL.Repositories.Interfaces;
using MoviesLibrary;
using MoviesLibrary.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesDAL.Repositories
{
    public class MoviesRepository : GenericDapperRepository<MovieModel> , IMoviesRepository
    {
        private readonly AppConfig AppConfig;
        private readonly IDbConnection Db;

        public MoviesRepository(IOptions<AppConfig> appConfig)
            :base(appConfig)
        {
            AppConfig = appConfig.Value;
            Db = new SqlConnection(AppConfig.ConnectionString);
        }

        public async Task<List<MovieModel>> GetByActorId(int id)
        {
            string sql = "select * from movies where Id in (select MovieId from roles where ActorID=@id)";

            var res = await Db.QueryAsync<MovieModel>(sql, new { id });

            return res.ToList();
        } 
        
       

        //public async Task<List<MovieModel>> GetPaginatedAsync(int pageSize,int pageNumber)
        //{
        //    var options = new
        //    {
        //        offset = (pageNumber - 1) * pageSize,
        //        pageSize = pageSize
        //    };
        //    string sql = @"SELECT *
        //                   FROM Movies
        //                   ORDER BY ID
        //                   OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY;";

        //    var res = await Db.QueryAsync<MovieModel>(sql,options);

        //    return res.ToList();
        //}

        


    }
}
