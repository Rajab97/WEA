using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Presentation.Areas.Administration.Models;
using WEA.Presentation.Services;
using WEA.SharedKernel;
using WEA.SharedKernel.Interfaces;
using WEA.SharedKernel.Resources;

namespace WEA.Presentation.Areas.Administration.Services
{
    public class RoleMenuServiceFacade : BaseServiceFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseService<RoleMenu> _roleMenuService;
        private readonly IMapper _mapper;

        public RoleMenuServiceFacade(IUnitOfWork unitOfWork,
                                    IBaseService<RoleMenu> roleMenuService,
                                        IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._roleMenuService = roleMenuService;
            this._mapper = mapper;
        }
        public async Task<Result<RoleMenuViewModel>> GetModel(Guid id)
        {
            try
            {
                var result = await _roleMenuService.GetByIdAsync(id);
                if (result.IsSucceed)
                {
                    var model = _mapper.Map<RoleMenuViewModel>(result.Data);
                    return Succeed(model);
                }
                return Failure<RoleMenuViewModel>(result.Exception);
            }
            catch (ApplicationException ex)
            {
                ex.HandleException();
                return Result<RoleMenuViewModel>.Failure(ex);
            }
            catch (Exception ex)
            {
                ex.HandleException();
                return Result<RoleMenuViewModel>.Failure(ExceptionMessages.FatalError);
            }
   
        }
        public async Task<Result> SaveAsync(RoleMenuViewModel model)
        {
            try
            {
                var dto = _mapper.Map<RoleMenu>(model);
                var result = model.Id == Guid.Empty ? await _roleMenuService.CreateAsync(dto) : await _roleMenuService.EditAsync(dto);
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

        public Result<IQueryable<RoleMenu>> GetData()
        {
            try
            {
                var result = _roleMenuService.GetAll();
                if (result.IsSucceed)
                {
                    return Succeed(result.Data.Include(m => m.Menu).Include(m => m.Role).AsQueryable());
                }
                return Failure<IQueryable<RoleMenu>>(result.Exception);
            }
            catch (ApplicationException ex)
            {
                ex.HandleException();
                return Result<IQueryable<RoleMenu>>.Failure(ex);
            }
            catch (Exception ex)
            {
                ex.HandleException();
                return Result<IQueryable<RoleMenu>>.Failure(ExceptionMessages.FatalError);
            }
        }

        public async Task<Result> Remove(Guid id)
        {
            try
            {
                var result = await _roleMenuService.RemoveAsync(id);
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
