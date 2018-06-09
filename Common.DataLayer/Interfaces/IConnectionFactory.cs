using System.Data;
using System.Threading.Tasks;

namespace Common.DataLayer
{
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();
        Task<IDbConnection> CreateAsyncConnection();
    }
}
