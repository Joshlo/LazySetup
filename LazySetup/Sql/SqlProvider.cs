using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace LazySetup.Sql
{
    public class SqlProvider : ISqlProvider
    {
        public string ConnectionString { get; }

        public SqlProvider(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public async Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<TResult>(sql, parameters);
            }
        }

        public async Task<IEnumerable<TResult>> QueryAsync<TFirst, TSecond, TResult>(string sql, Func<TFirst, TSecond, TResult> func, string splitOn, object parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync(sql, func, parameters, splitOn: splitOn);
            }
        }

        public async Task<IEnumerable<TResult>> QueryAsync<TFirst, TSecond, TThird, TResult>(string sql, Func<TFirst, TSecond, TThird, TResult> func, string splitOn, object parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync(sql, func, parameters, splitOn: splitOn);
            }
        }

        public async Task<IEnumerable<TResult>> QueryAsync<TFirst, TSecond, TThird, TFourth, TResult>(string sql, Func<TFirst, TSecond, TThird, TFourth, TResult> func, string splitOn,
            object parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync(sql, func, parameters, splitOn: splitOn);
            }
        }

        public async Task<TResult> FirstOrDefaultAsync<TResult>(string sql, object parameters = null)
        {
            var result = await QueryAsync<TResult>(sql, parameters);
            return result.FirstOrDefault();
        }

        public async Task<TResult> QueryMultipleAsync<TResult>(string sql, Func<SqlMapper.GridReader, TResult> func, object parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryMultipleAsync(sql, parameters);
                return func(result);
            }
        }

        public IEnumerable<TResult> Query<TResult>(string sql, object parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return connection.Query<TResult>(sql, parameters);
            }
        }

        public IEnumerable<TResult> Query<TFirst, TSecond, TResult>(string sql, Func<TFirst, TSecond, TResult> func, string splitOn, object parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return connection.Query(sql, func, parameters, splitOn: splitOn);
            }
        }

        public IEnumerable<TResult> Query<TFirst, TSecond, TThird, TResult>(string sql, Func<TFirst, TSecond, TThird, TResult> func, string splitOn, object parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return connection.Query(sql, func, parameters, splitOn: splitOn);
            }
        }

        public IEnumerable<TResult> Query<TFirst, TSecond, TThird, TFourth, TResult>(string sql, Func<TFirst, TSecond, TThird, TFourth, TResult> func, string splitOn,
            object parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return connection.Query(sql, func, parameters, splitOn: splitOn);
            }
        }

        public TResult FirstOrDefault<TResult>(string sql, object parameters = null)
        {
            return Query<TResult>(sql, parameters).FirstOrDefault();
        }

        public TResult QueryMultiple<TResult>(string sql, Func<SqlMapper.GridReader, TResult> func, object parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return func(connection.QueryMultiple(sql, parameters));
            }
        }
    }
}
