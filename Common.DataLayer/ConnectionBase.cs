using Common.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataLayer
{
    public abstract class ConnectionBase
    {
        private readonly IConnectionFactory m_ConnectionFactory;

        protected ConnectionBase(IConnectionFactory factory)
        {
            m_ConnectionFactory = factory;
        }

        protected async Task<T> ExecuteAsync<T>(Func<IDbConnection, T> action)
        {
            using (var connection = await m_ConnectionFactory.CreateAsyncConnection())
            {
                return action(connection);
            }
        }

        protected Task<IEnumerable<T>> ExecuteProcedureAsync<T>(string procedureName, object requestData = null)
        {
            return ExecuteAsync(connection =>
            {
                return connection.Query<T>(procedureName, requestData, commandType: CommandType.StoredProcedure);
            });
        }

        protected async Task<T> ExecuteProcedureForSingleResultAsync<T>(string procedureName, object requestData = null)
        {
            var results = await ExecuteProcedureForListResultAsync<T>(procedureName, requestData);

            return results.SingleOrDefault();
        }

        protected Task<IEnumerable<T>> ExecuteProcedureForListResultAsync<T>(string procedureName, object requestData = null)
        {
            return ExecuteAsync(connection =>
            {
                var lists = connection.Query<T>(procedureName, requestData, commandType: CommandType.StoredProcedure);

                return lists;
            });
        }

        protected Task<IEnumerablePage<T>> GetPagedAsync<T>(string procedureName, object requestData = null)
        {
            return ExecuteAsync(connection =>
            {
                IEnumerablePage<T> paged;
                using (var queries = connection.QueryMultiple(
                    procedureName,
                    requestData,
                    commandType: CommandType.StoredProcedure))
                {
                    IEnumerable<T> data = queries.Read<T>();
                    int totalCount = queries.Read<int>().Single();
                    paged = new EnumerablePage<T>
                    {
                        PageData = data,
                        TotalCount = totalCount
                    };
                }

                return paged;
            });
        }
    }
}
