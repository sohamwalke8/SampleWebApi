using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClassLibrary.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Add1Async(T entity);
        Task Update1Async(T entity);
        Task Delete1Async(T entity);

        Task<T> GetByIdsAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
    }
}
