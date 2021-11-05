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
    public class ActorsRepository : GenericDapperRepository<ActorModel>, IActorsRepository
    {

        private readonly AppConfig AppConfig;
        private readonly IDbConnection Db;

        public ActorsRepository(IOptions<AppConfig> appConfig)
            : base(appConfig)
        {
            AppConfig = appConfig.Value;
            Db = new SqlConnection(AppConfig.ConnectionString);
        }

        public async Task<List<ActorModel>> GetActorsFromMovie(int id)
        {
            string sql = "select * from actors where(ID in(select ActorID from roles where MovieId = @id))";

            var res = await Db.QueryAsync<ActorModel>(sql,new { id });

            return res.ToList();
        }
    }
}
