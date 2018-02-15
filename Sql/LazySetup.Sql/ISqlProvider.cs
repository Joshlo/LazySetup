using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Dapper;

namespace LazySetup.Sql
{
    public interface ISqlProvider
    {
        string ConnectionString { get; }

        Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object parameters = null);

        Task<IEnumerable<TResult>> QueryAsync<TFirst, TSecond, TResult>(string sql, Func<TFirst, TSecond, TResult> func,
            string splitOn, object parameters = null);
        Task<IEnumerable<TResult>> QueryAsync<TFirst, TSecond, TThird, TResult>(string sql, Func<TFirst, TSecond, TThird, TResult> func,
            string splitOn, object parameters = null);
        Task<IEnumerable<TResult>> QueryAsync<TFirst, TSecond, TThird, TFourth, TResult>(string sql, Func<TFirst, TSecond, TThird, TFourth, TResult> func,
            string splitOn, object parameters = null);
        Task<TResult> FirstOrDefaultAsync<TResult>(string sql, object parameters = null);
        Task<TResult> QueryMultipleAsync<TResult>(string sql, Func<SqlMapper.GridReader, TResult> func, object parameters = null);

        Task<int> ExecuteAsync(string sql, object parameters = null);

        IEnumerable<TResult> Query<TResult>(string sql, object parameters = null);
        IEnumerable<TResult> Query<TFirst, TSecond, TResult>(string sql, Func<TFirst, TSecond, TResult> func,
            string splitOn, object parameters = null);
        IEnumerable<TResult> Query<TFirst, TSecond, TThird, TResult>(string sql, Func<TFirst, TSecond, TThird, TResult> func,
            string splitOn, object parameters = null);
        IEnumerable<TResult> Query<TFirst, TSecond, TThird, TFourth, TResult>(string sql, Func<TFirst, TSecond, TThird, TFourth, TResult> func,
            string splitOn, object parameters = null);
        TResult FirstOrDefault<TResult>(string sql, object parameters = null);

        TResult QueryMultiple<TResult>(string sql, Func<SqlMapper.GridReader, TResult> func,
            object parameters = null);

        int Execute(string sql, object parameters = null);
    }
}