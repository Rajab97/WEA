using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Presentation.Areas.Administration.Models;
using WEA.SharedKernel;
using WEA.SharedKernel.Resources;
using WEA.Presentation.Services;

namespace WEA.Presentation.Areas.Administration.Services
{
    public class RoleServiceFacade : BaseServiceFacade
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RoleServiceFacade(RoleManager<Role> roleManager , IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<Result<RoleViewModel>> GetModel(Guid id)
        {
            try
            {
                var result = await _roleManager.FindByIdAsync(id.ToString());
                var model = _mapper.Map<RoleViewModel>(result);
                return Succeed(model);
            }
            catch (ApplicationException ex)
            {
                ex.HandleException();
                return Result<RoleViewModel>.Failure(ex);
            }
            catch (Exception ex)
            {
                ex.HandleException();
                return Result<RoleViewModel>.Failure(ExceptionMessages.FatalError);
            }
        }
        public async Task<Result> SaveAsync(RoleViewModel model)
        {
            try
            {
                IdentityResult result;
                if (model.Id == Guid.Empty)
                {
                    var dto = _mapper.Map<Role>(model);
                    result = await _roleManager.CreateAsync(dto);
                }
                else
                {
                    var dto = await _roleManager.FindByIdAsync(model.Id.ToString());
                    _mapper.Map(model, dto);
                    result = await _roleManager.UpdateAsync(dto);
                }
                if (result.Succeeded)
                    return Succeed();

                return Result.Failure(result.Errors.FirstOrDefault().Description);
            }
            catch (ApplicationException ex)
            {
                ex.HandleException();
                return Result<RoleViewModel>.Failure(ex);
            }
            catch (Exception ex)
            {
                ex.HandleException();
                return Result<RoleViewModel>.Failure(ExceptionMessages.FatalError);
            }
        }

        public Result<IQueryable<Role>> GetData()
        {
            try
            {
                return Succeed(_roleManager.Roles);
            }
            catch (ApplicationException ex)
            {
                ex.HandleException();
                return Result<IQueryable<Role>>.Failure(ex);
            }
            catch (Exception ex)
            {
                ex.HandleException();
                return Result<IQueryable<Role>>.Failure(ExceptionMessages.FatalError);
            }
           
        }

        public async Task<Result> Remove(Guid id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id.ToString());
                var result = await _roleManager.DeleteAsync(role);
                return Succeed();
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
