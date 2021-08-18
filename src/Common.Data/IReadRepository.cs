using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Data
{
    public interface IReadRepository<T> where T : class
    {
        Task<T> GetById(Guid id);
        Task<T> GetById(Guid id, bool includeItems);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(int page, int pageSize);
    }
}