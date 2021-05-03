using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Core.Interfaces.Services;
using WEA.Core.Repositories;
using WEA.Core.ViewEntities;
using WEA.SharedKernel;
using WEA.SharedKernel.Exceptions;
using WEA.SharedKernel.Interfaces;

namespace WEA.Core.Services
{
    public class MenuService : BaseService<Menu> , IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IBaseService<RoleMenu> _roleMenuService;

        public MenuService(IMenuRepository repository ,
                            UserManager<User> userManager ,
                                RoleManager<Role> roleManager,
                                    IBaseService<RoleMenu> roleMenuService) : base(repository)
        {
            _menuRepository = repository;
            _userManager = userManager;
            _roleManager = roleManager;
            _roleMenuService = roleMenuService;
        }
        public override Task<Result> CreateAsync(Menu model)
        {
            return base.CreateAsync(model);
        }

        public Result<Menu> GetMenuByRouteDetails(string area, string controller, string action)
        {
            try
            {
                var res = _menuRepository.GetMenuByRouteDetails(area,controller,action);
                if (res != null)
                    return Result<Menu>.Succeed(res);

                return Result<Menu>.Failure();
            }
            catch (BaseException exc)
            {
                return Result<Menu>.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result<Menu>.Failure(fatalExc);
            }
        }

        public Result<IQueryable<Menu>> GetUserMenus(Guid userId)
        {
            try
            {
                var res = _menuRepository.GetUserMenus(userId);
                if (res.Any())
                    return Result<IQueryable<Menu>>.Succeed(res);

                return Result<IQueryable<Menu>>.Failure();
            }
            catch (BaseException exc)
            {
                return Result<IQueryable<Menu>>.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result<IQueryable<Menu>>.Failure(fatalExc);
            }
        }
    }
}
