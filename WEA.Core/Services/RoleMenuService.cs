using System;
using System.Collections.Generic;
using System.Text;
using WEA.Core.Entities;
using WEA.Core.Interfaces.Services;
using WEA.Core.Repositories;
using WEA.SharedKernel;
using WEA.SharedKernel.Exceptions;

namespace WEA.Core.Services
{
    public class RoleMenuService : BaseService<RoleMenu>, IRoleMenuService
    {
        private readonly IRoleMenuRepository _roleMenuRepository;

        public RoleMenuService(IRoleMenuRepository roleMenuRepository):base(roleMenuRepository)
        {
            _roleMenuRepository = roleMenuRepository;
        }
        public Result HasAccessToMenu(Guid menuId, Guid roleId)
        {
            try
            {
                var res = _roleMenuRepository.HasAccessToMenu(menuId,roleId);
                return res == true ? Result.Succeed() : Result.Failure();
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

        public Result HasAccessToMenu(Guid menuId, IEnumerable<string> roleNames)
        {
            try
            {
                var res = _roleMenuRepository.HasAccessToMenu(menuId, roleNames);
                return res == true ? Result.Succeed() : Result.Failure();
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
