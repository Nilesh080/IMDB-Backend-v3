using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace IMDBApi_Assignment3.Repository
{
    public abstract class BaseRepository<T> where T : class
    {
        protected readonly string _connectionString;
        protected readonly string _tableName;

        protected BaseRepository(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            _tableName = tableName;
        }

        protected IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<T>($"SELECT * FROM {_tableName}");
            }
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                var query = $"SELECT * FROM {_tableName} WHERE Id = @Id";
                return await connection.QuerySingleOrDefaultAsync<T>(query, new { Id = id });
            }
        }

        public virtual async Task<int> CreateAsync(T entity, string insertQuery = null)
        {
            using (var connection = CreateConnection())
            {
                if (string.IsNullOrEmpty(insertQuery))
                {
                    insertQuery = $"INSERT INTO {_tableName} OUTPUT INSERTED.Id VALUES @entity";
                }
                return await connection.ExecuteScalarAsync<int>(insertQuery, entity);
            }
        }

        public virtual async Task<bool> UpdateAsync(T entity, string updateQuery = null)
        {
            using (var connection = CreateConnection())
            {
                if (string.IsNullOrEmpty(updateQuery))
                {
                    updateQuery = $"UPDATE {_tableName} SET @entity WHERE Id = @Id";
                }
                var result = await connection.ExecuteAsync(updateQuery, entity);
                return result > 0;
            }
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                var query = $"DELETE FROM {_tableName} WHERE Id = @Id";
                var result = await connection.ExecuteAsync(query, new { Id = id });
                return result > 0;
            }
        }

        public virtual async Task<bool> ExistsAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                var query = $"SELECT COUNT(1) FROM {_tableName} WHERE Id = @Id";
                var count = await connection.ExecuteScalarAsync<int>(query, new { Id = id });
                return count > 0;
            }
        }

        protected async Task<IEnumerable<T>> QueryAsync(string sql, object param = null)
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<T>(sql, param);
            }
        }

        protected async Task<T> QuerySingleOrDefaultAsync(string sql, object param = null)
        {
            using (var connection = CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<T>(sql, param);
            }
        }

        protected async Task<int> ExecuteAsync(string sql, object param = null)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecuteAsync(sql, param);
            }
        }

        protected async Task<T> ExecuteScalarAsync<T>(string sql, object param = null)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecuteScalarAsync<T>(sql, param);
            }
        }
    }
}
