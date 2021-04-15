using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEA.SharedKernel.Interfaces
{
    public interface IRepository
    {
        Task<T> GetByIdAsync<T>(Guid id) where T : BaseEntity, new();
        IQueryable<T> GetAll<T>() where T : BaseEntity , new();
        IQueryable<T> GetAll<T>(ISpecification<T> spec) where T : BaseEntity, new();
        Task<T> AddAsync<T>(T entity) where T : BaseEntity, new();
        Task UpdateAsync<T>(T entity) where T : BaseEntity, new();
        Task DeleteAsync<T>(T entity) where T : BaseEntity, new();
    }
    public interface IRepository<T> where T : BaseEntity, new()
    {
        Task<T> GetByIdAsync(Guid id);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
