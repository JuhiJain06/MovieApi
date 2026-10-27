using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MovieAPI.Repositories
{
    
    public class BaseRepository<T> where T : class
    {
        private readonly string _connectionString;
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyConnection");
        }
        protected IDbConnection CreateConnection() => new SqlConnection(_connectionString);
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)//getall
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<T>(sql, param);
        }
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param)//get
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(sql, param);
        }
        public async Task<T> ExecuteScalarAsync<T>(string sql, object param)//add
        {
            using var connection = CreateConnection();
            return await connection.ExecuteScalarAsync<T>(sql, param);
        }
        public async Task<int> ExecuteAsync(string sql, object param)//delete/update
        {
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(sql, param);
        }
        public async Task<T> ExecuteSPAsync<T>(string spName, object param = null)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteScalarAsync<T>(spName, param, commandType: CommandType.StoredProcedure);
        }
    }
}
