using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.SharedKernel;
using WEA.SharedKernel.Interfaces;
using WEA.Presentation.Areas.Administration.Models;
using WEA.Presentation.Services;
using WEA.Core.Interfaces.Services;
using WEA.SharedKernel.Resources;

namespace WEA.Presentation.Areas.Administration.Services
{
    public class MenuServiceFacade : BaseServiceFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;

        public MenuServiceFacade(IUnitOfWork unitOfWork,
                                    IMenuService menuService,
                                        IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._menuService = menuService;
            this._mapper = mapper;
        }
        public async Task<Result<MenuViewModel>> GetModel(Guid id)
        {
            var result = await _menuService.GetByIdAsync(id);
            if (result.IsSucceed)
            {
                var model = _mapper.Map<MenuViewModel>(result.Data);
                return Succeed(model);
            }
            return Failure<MenuViewModel>(result.Exception);
        }
        public async Task<Result> SaveAsync(MenuViewModel model)
        {
            try
            {
                var dto = _mapper.Map<Menu>(model);
                var result = model.Id == Guid.Empty ? await _menuService.CreateAsync(dto) : await _menuService.EditAsync(dto);
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

        public Result<IQueryable<Menu>> GetData()
        {
            return _menuService.GetAll();
        }

        public async Task<Result> Remove(Guid id)
        {
            try
            {
                var result = await _menuService.RemoveAsync(id);
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
