using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Presentation.Areas.Administration.Models;
using WEA.Presentation.Services;
using WEA.SharedKernel;
using WEA.SharedKernel.Resources;

namespace WEA.Presentation.Areas.Administration.Services
{
    public class UserServiceFacade : BaseServiceFacade
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;

        public UserServiceFacade(UserManager<User> userManager,
                                    IMapper mapper,
                                    RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        public async Task<Result<UserViewModel>> GetModel(Guid id)
        {
            try
            {
                var result = await _userManager.FindByIdAsync(id.ToString());
                var model = _mapper.Map<UserViewModel>(result);
                

                return Succeed(model);
            }
            catch (ApplicationException ex)
            {
                ex.HandleException();
                return Result<UserViewModel>.Failure(ex);
            }
            catch (Exception ex)
            {
                ex.HandleException();
                return Result<UserViewModel>.Failure(ExceptionMessages.FatalError);
            }
        }
        public async Task<Result> SaveAsync(UserViewModel model)
        {
            try
            {
                var dto = await _userManager.FindByIdAsync(model.Id.ToString());
                throw new Exception();
             //   dto.Name = model.Name;
                var result = model.Id == Guid.Empty ? await _userManager.CreateAsync(dto) : await _userManager.UpdateAsync(dto);
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

        public Result<IQueryable<User>> GetData()
        {
            try
            {
                return Succeed(_userManager.Users);
            }
            catch (ApplicationException ex)
            {
                ex.HandleException();
                return Result<IQueryable<User>>.Failure(ex);
            }
            catch (Exception ex)
            {
                ex.HandleException();
                return Result<IQueryable<User>>.Failure(ExceptionMessages.FatalError);
            }

        }

        public async Task<Result> Remove(Guid id)
        {
            try
            {
                var role = await _userManager.FindByIdAsync(id.ToString());
                var result = await _userManager.DeleteAsync(role);
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
