using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEA.SharedKernel;

namespace WEA.SharedKernel.Interfaces
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<Result> CreateAsync(T model);
        Task<Result> EditAsync(T model);
        Task<Result> RemoveAsync(Guid id);
        Task<Result<T>> GetByIdAsync(Guid id);

        Result<IQueryable<T>> GetAll();
        Result<IQueryable<T>> GetAll(ISpecification<T> spec);

    }
}
