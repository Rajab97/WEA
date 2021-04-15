using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEA.SharedKernel;
using WEA.SharedKernel.Exceptions;
using WEA.SharedKernel.Interfaces;

namespace WEA.Core.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity , new()
    {
        protected readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }
        public virtual async Task<Result> CreateAsync(T model)
        {
            try
            {
                await _repository.AddAsync(model);
                return Result.Succeed();
            }
            catch (BaseException exc)
            {
                return Result.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result.Failure(fatalExc);
            }
        }

        public virtual async Task<Result> EditAsync(T model)
        {
            try
            {
                await _repository.UpdateAsync(model);
                return Result.Succeed();
            }
            catch (BaseException exc)
            {
                return Result.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result.Failure(fatalExc);
            }
        }

        public virtual Result<IQueryable<T>> GetAll()
        {
            try
            {
                var result = _repository.GetAll();
                return Result<IQueryable<T>>.Succeed(result);
            }
            catch (BaseException exc)
            {
                return Result<IQueryable<T>>.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result<IQueryable<T>>.Failure(fatalExc);
            }
        }

        public virtual Result<IQueryable<T>> GetAll(ISpecification<T> spec)
        {
            try
            {
                var result = _repository.GetAll(spec);
                return Result<IQueryable<T>>.Succeed(result);
            }
            catch (BaseException exc)
            {
                return Result<IQueryable<T>>.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result<IQueryable<T>>.Failure(fatalExc);
            }
        }

        public virtual async Task<Result<T>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(id);
                return Result<T>.Succeed(result);
            }
            catch (BaseException exc)
            {
                return Result<T>.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result<T>.Failure(fatalExc);
            }
        }

        public virtual async Task<Result> RemoveAsync(Guid id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (!entity.IsSucceed)
                {
                    return Result.Failure(entity.ExceptionMessage);
                }
                await _repository.DeleteAsync(entity.Data);
                return Result.Succeed();
            }
            catch (BaseException exc)
            {
                return Result.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result.Failure(fatalExc);
            }
        }
    }
}
