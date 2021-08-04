using System;
using System.Threading.Tasks;

namespace Common.Data
{
    public interface IWriteRepository<T> where T : class
    {
        Task<int> Add(T entity);
        Task<int> Delete(Guid id);
        Task<int> Update(T entity);
    }
}