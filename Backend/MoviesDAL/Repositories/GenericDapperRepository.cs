using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;
using MoviesDAL.Repositories.Interfaces;
using MoviesLibrary;
using MoviesLibrary.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TableAttribute = System.ComponentModel.DataAnnotations.Schema.TableAttribute;

namespace MoviesDAL.Repositories
{
    public class GenericDapperRepository<T> : IRepository<T> where T:BaseModel, new() 
    {
        private readonly AppConfig AppConfig;
        private readonly IDbConnection Db;

        public GenericDapperRepository(IOptions<AppConfig> appConfig)
        {
            AppConfig = appConfig.Value;
            Db = new SqlConnection(AppConfig.ConnectionString);
        }

        public async Task<int> AddAsync(T model)
        {
            var insertedId = await Db.InsertAsync(model);
            return insertedId;
        }

        public async Task DeleteAsync(int id)
        {
            await Db.DeleteAsync(new T { Id = id });
        }

        public async Task<List<T>> GetAsync()
        {
            var res = await Db.GetAllAsync<T>();

            return res.ToList();
        }

        public async Task<List<T>> GetPaginatedAsync(int pageSize, int pageNumber)
        {
            var tableAttribute = typeof(T)
                .GetCustomAttributes(true)
                .Where(x => x.GetType() == typeof(TableAttribute))
                .Cast<TableAttribute>()
                .FirstOrDefault();

            var tableName = tableAttribute?.Name;
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new System.Exception("Model has no table name defined");
            }

            var options = new
            {
                offset = (pageNumber - 1) * pageSize,
                pageSize = pageSize
            };
            string sql = $@"SELECT *
                           FROM {tableName}
                           ORDER BY ID
                           OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY;";

            var res = await Db.QueryAsync<T>(sql, options);

            return res.ToList();
        }

        public async Task<List<T>> GetByIDs(List<int> IDs)
        {
            var res = await Db.GetAllAsync<T>();
            var filteredT = res.Where(ent => IDs.Contains(ent.Id));

            return filteredT.ToList();
        }



        public async Task<T> GetByIdAsync(int id)
        {
            return await Db.GetAsync<T>(id);
        }

        public async Task UpdateAsync(int id, T model)
        {
            model.Id = id;

            await Db.UpdateAsync<T>(model);
        }

        public async Task<int> GetNumberOfRows()
        {
            var tableAttribute = typeof(T)
                .GetCustomAttributes(true)
                .Where(x => x.GetType() == typeof(TableAttribute))
                .Cast<TableAttribute>()
                .FirstOrDefault();

            var tableName = tableAttribute?.Name;

            string sql = $"select count(*) from {tableName};";

            var res = await Db.QueryAsync<int>(sql);

            return res.FirstOrDefault();
        }
    }
}
