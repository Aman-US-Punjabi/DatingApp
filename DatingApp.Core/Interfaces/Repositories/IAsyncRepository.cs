using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.Entities.BaseEntities;

namespace DatingApp.Core.Interfaces.Repositories
{
    public interface IAsyncRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}