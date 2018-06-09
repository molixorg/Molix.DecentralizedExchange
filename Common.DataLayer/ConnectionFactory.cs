using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Common.DataLayer
{
    public class ConnectionFactory : IConnectionFactory
    {
        private string m_ConnectionString;

        public ConnectionFactory(string connectionString)
        {
            m_ConnectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            var result = new SqlConnection(m_ConnectionString);
            result.Open();

            return result;
        }

        public async Task<IDbConnection> CreateAsyncConnection()
        {
            var result = new SqlConnection(m_ConnectionString);
            await result.OpenAsync();

            return result;
        }
    }
}
