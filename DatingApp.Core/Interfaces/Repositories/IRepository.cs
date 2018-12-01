using System.Collections.Generic;
using DatingApp.Core.Entities.BaseEntities;

namespace DatingApp.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        T GetSingleBySpec(ISpecification<T> spec);
        IEnumerable<T> ListAll();
        IEnumerable<T> List(ISpecification<T> spec);
        T Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        int Count(ISpecification<T> spec);
    }
}