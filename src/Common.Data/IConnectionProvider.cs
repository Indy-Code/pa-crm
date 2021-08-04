using System.Data;
using System.Threading.Tasks;

namespace Common.Data
{
    public interface IConnectionProvider
    {
        IDbConnection GetDbConnection();
        Task<IDbConnection> GetOpenDbConnection();
    }
}