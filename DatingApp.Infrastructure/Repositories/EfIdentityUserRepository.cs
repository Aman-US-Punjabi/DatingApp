using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Core.Entities.BaseEntities;
using DatingApp.Core.Interfaces.Repositories;
using DatingApp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Infrastructure.Repositories
{
    public class EfIdentityUserRepository<T> : IRepository<T>, IAsyncRepository<T> where T : IdentityUser<int>
    {
        private readonly CatalogContext _dbContext;

        public EfIdentityUserRepository(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public T GetSingleBySpec(ISpecification<T> spec)
        {
            return List(spec).FirstOrDefault();
        }

        public IEnumerable<T> List(ISpecification<T> spec)
        {
            return ApplySpecification(spec).AsEnumerable();
        }

        public IEnumerable<T> ListAll()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public int Count(ISpecification<T> spec)
        {
            return ApplySpecification(spec).Count();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public bool Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges() > 0;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public bool Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return _dbContext.SaveChanges() > 0;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync() > 0;
        }

         private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluatorIdentityUser<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }
    }
}