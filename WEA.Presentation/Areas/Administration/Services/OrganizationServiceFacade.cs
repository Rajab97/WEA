using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Core.Interfaces.Services;
using WEA.Presentation.Areas.Administration.Models;
using WEA.Presentation.Services;
using WEA.SharedKernel;
using WEA.SharedKernel.Interfaces;
using WEA.SharedKernel.Resources;

namespace WEA.Presentation.Areas.Administration.Services
{
    public class OrganizationServiceFacade : BaseServiceFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrganizationService _service;
        private readonly IMapper _mapper;

        public OrganizationServiceFacade(IUnitOfWork unitOfWork,
                                    IOrganizationService service,
                                        IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._service = service;
            this._mapper = mapper;
        }
        public async Task<Result<OrganizationViewModel>> GetModel(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result.IsSucceed)
            {
                var model = _mapper.Map<OrganizationViewModel>(result.Data);
                return Succeed(model);
            }
            return Failure<OrganizationViewModel>(result.Exception);
        }
        public async Task<Result> SaveAsync(OrganizationViewModel model)
        {
            try
            {
                Organization dto = null;
                if (model.Id == Guid.Empty)
                    dto = _mapper.Map<Organization>(model);
                else
                {
                    var dbModel = await _service.GetByIdAsync(model.Id);
                    if (dbModel.IsSucceed)
                    {
                        dto = _mapper.Map(model, dbModel.Data);
                    }
                    return Result.Failure(ExceptionMessages.ExOrganizationNotFound);
                }
                var result = model.Id == Guid.Empty ? await _service.CreateAsync(dto) : await _service.EditAsync(dto);
                if (result.IsSucceed)
                {
                    await _unitOfWork.CommitAsync();
                    return Succeed();
                }
                return Result.Failure(result.ExceptionMessage);
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

        public Result<IQueryable<Organization>> GetData()
        {
            return _service.GetAll();
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
