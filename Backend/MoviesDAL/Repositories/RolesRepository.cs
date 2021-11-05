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
    public class RolesRepository : GenericDapperRepository<RoleModel>, IRolesRepository
    {

        private readonly AppConfig AppConfig;
        private readonly IDbConnection Db;

        public RolesRepository(IOptions<AppConfig> appConfig)
            : base(appConfig)
        {
            AppConfig = appConfig.Value;
            Db = new SqlConnection(AppConfig.ConnectionString);
        }

        public async Task<List<RoleModel>> GetRolesFromMovie(int id)
        {
            string sql = "select * from roles where MovieID=@id";

            var res = await Db.QueryAsync<RoleModel>(sql,new { id });

            return res.ToList();
        }
    }
}
