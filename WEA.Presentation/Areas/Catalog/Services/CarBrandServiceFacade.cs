using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Presentation.Areas.Catalog.Models;
using WEA.Presentation.Services;
using WEA.SharedKernel;
using WEA.SharedKernel.Interfaces;
using WEA.SharedKernel.Resources;

namespace WEA.Presentation.Areas.Catalog.Services
{
    public class CarBrandServiceFacade : BaseServiceFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseService<CarBrand> _service;
        private readonly IMapper _mapper;

        public CarBrandServiceFacade(IUnitOfWork unitOfWork,
                                    IBaseService<CarBrand> Service,
                                        IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._service = Service;
            this._mapper = mapper;
        }
        public async Task<Result<CarBrandViewModel>> GetModel(Guid id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result.IsSucceed)
                {
                    var model = _mapper.Map<CarBrandViewModel>(result.Data);
                    return Succeed(model);
                }
                return Failure<CarBrandViewModel>(result.Exception);
            }
            catch (ApplicationException ex)
            {
                ex.HandleException();
                return Result<CarBrandViewModel>.Failure(ex);
            }
            catch (Exception ex)
            {
                ex.HandleException();
                return Result<CarBrandViewModel>.Failure(ExceptionMessages.FatalError);
            }

        }
        public async Task<Result> SaveAsync(CarBrandViewModel model)
        {
            try
            {
                var dto = _mapper.Map<CarBrand>(model);
                var result = model.Id == Guid.Empty ? await _service.CreateAsync(dto) : await _service.EditAsync(dto);
                if (result.IsSucceed)
                {
                    await _unitOfWork.CommitAsync();
                    return Succeed();
                }
                return Failure(result.Exception);
            }
            catch (ApplicationException ex)
            {
                ex.HandleException();
                return Result.Failure(ex);
            }
            catch (Exception ex)
            {
                ex.HandleException();
                return Result.Failure(ExceptionMessages.FatalError);
            }

        }

        public Result<IQueryable<CarBrand>> GetData()
        {
            try
            {
                var result = _service.GetAll();
                if (result.IsSucceed)
                {
                    return Succeed(result.Data.AsQueryable());
                }
                return Failure<IQueryable<CarBrand>>(result.Exception);
            }
            catch (ApplicationException ex)
            {
                ex.HandleException();
                return Result<IQueryable<CarBrand>>.Failure(ex);
            }
            catch (Exception ex)
            {
                ex.HandleException();
                return Result<IQueryable<CarBrand>>.Failure(ExceptionMessages.FatalError);
            }
        }

        public async Task<Result> Remove(Guid id)
        {
            try
            {
                var result = await _service.RemoveAsync(id);
                if (result.IsSucceed)
                {
                    await _unitOfWork.CommitAsync();
                    return Succeed();
                }
                return Failure(result.Exception);
            }
            catch (ApplicationException ex)
            {
                ex.HandleException();
                return Result.Failure(ex);
            }
            catch (Exception ex)
            {
                ex.HandleException();
                return Result.Failure(ExceptionMessages.FatalError);
            }
        }
    }
}
