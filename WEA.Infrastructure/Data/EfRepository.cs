using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEA.SharedKernel;
using WEA.SharedKernel.Exceptions;
using WEA.SharedKernel.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
namespace WEA.Infrastructure.Data
{
    public class EfRepository : IRepository
    {
        protected readonly DbFactory _dbFactory;

        public EfRepository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public virtual T GetById<T>(Guid id) where T : BaseEntity, new()
        {
            return _dbFactory.DbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        }

        public virtual Task<T> GetByIdAsync<T>(Guid id) where T : BaseEntity, new()
        {
            return _dbFactory.DbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public virtual IQueryable<T> GetAll<T>() where T : BaseEntity, new()
        {
            return _dbFactory.DbContext.Set<T>().AsQueryable();
        }

        public virtual IQueryable<T> GetAll<T>(ISpecification<T> spec) where T : BaseEntity, new()
        {
            var specificationResult = ApplySpecification(spec);
            return specificationResult;
        }

        public virtual async Task<T> AddAsync<T>(T entity) where T : BaseEntity, new()
        {
            if (typeof(AuditEntity).IsAssignableFrom(typeof(T)))
            {
                ((AuditEntity)(object)entity).CreatedDate = DateTime.UtcNow;
            }
            await _dbFactory.DbContext.Set<T>().AddAsync(entity);
           // await _dbFactory.DbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task UpdateAsync<T>(T entity) where T : BaseEntity, new()
        {
            if (await GetVersionOfOriginalEntity<T>(entity.Id) != entity.Version)
            {
                throw new ConcurencyEditException(null);
            }
            if (typeof(AuditEntity).IsAssignableFrom(typeof(T)))
            {
                ((AuditEntity)(object)entity).UpdatedDate = DateTime.UtcNow;
            }
            entity.Version += 1;
            _dbFactory.DbContext.Entry(entity).State = EntityState.Modified;
           // return _dbFactory.DbContext.SaveChangesAsync();
        }

        public virtual Task DeleteAsync<T>(T entity) where T : BaseEntity, new()
        {
            if (typeof(DeleteEntity).IsAssignableFrom(typeof(T)))
            {
                ((DeleteEntity)(object)entity).IsDeleted = true;
                _dbFactory.DbContext.Update(entity);
            }
            else
                _dbFactory.DbContext.Remove(entity);
            return Task.CompletedTask;
            //return _dbFactory.DbContext.SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification<T>(ISpecification<T> spec) where T : BaseEntity, new()
        {
            var evaluator = new SpecificationEvaluator();
            return evaluator.GetQuery(_dbFactory.DbContext.Set<T>().AsQueryable(), spec);
        }
        protected async Task<int> GetVersionOfOriginalEntity<T>(Guid id) where T : BaseEntity, new()
        {
            var version = await _dbFactory.DbContext.Set<T>().Where(m => m.Id == id).Select(m => m.Version).SingleAsync();
            return version;
        }
    }

    public class EfRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        protected readonly DbFactory _dbFactory;

        public EfRepository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public virtual T GetById(Guid id)
        {
            return _dbFactory.DbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        }
        public virtual Task<T> GetByIdAsync(Guid id)
        {
            return _dbFactory.DbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }
        public virtual IQueryable<T> GetAll()
        {
            return _dbFactory.DbContext.Set<T>().AsQueryable();
        }
        public virtual IQueryable<T> GetAll(ISpecification<T> spec)
        {
            var specificationResult = ApplySpecification(spec);
            return specificationResult.AsQueryable();
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            if (typeof(AuditEntity).IsAssignableFrom(typeof(T)))
            {
                ((AuditEntity)(object)entity).CreatedDate = DateTime.UtcNow;
            }
            await _dbFactory.DbContext.Set<T>().AddAsync(entity);
            // await _dbFactory.DbContext.SaveChangesAsync();
            return entity;
        }
        public virtual async Task UpdateAsync(T entity)
        {
            if (await GetVersionOfOriginalEntity(entity.Id) != entity.Version)
            {
                throw new ConcurencyEditException(null);
            }
            if (typeof(AuditEntity).IsAssignableFrom(typeof(T)))
            {
                ((AuditEntity)(object)entity).UpdatedDate = DateTime.UtcNow;
            }
            entity.Version += 1;
            _dbFactory.DbContext.Entry(entity).State = EntityState.Modified;
            // return _dbFactory.DbContext.SaveChangesAsync();
        }
        public virtual Task DeleteAsync(T entity)
        {
            if (typeof(DeleteEntity).IsAssignableFrom(typeof(T)))
            {
                ((DeleteEntity)(object)entity).IsDeleted = true;
                _dbFactory.DbContext.Update(entity);
            }
            else
                _dbFactory.DbContext.Remove(entity);
            return Task.CompletedTask;
            //return _dbFactory.DbContext.SaveChangesAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            var evaluator = new SpecificationEvaluator();
            return evaluator.GetQuery(_dbFactory.DbContext.Set<T>().AsQueryable(), spec);
        }
        protected async Task<int> GetVersionOfOriginalEntity(Guid id)
        {
            var version = await _dbFactory.DbContext.Set<T>().Where(m => m.Id == id).Select(m => m.Version).SingleAsync();
            return version;
        }
    }
}
