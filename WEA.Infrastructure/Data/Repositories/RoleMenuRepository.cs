using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WEA.Core.Entities;
using WEA.Core.Repositories;

namespace WEA.Infrastructure.Data.Repositories
{
    public class RoleMenuRepository : EfRepository<RoleMenu>, IRoleMenuRepository
    {
        public RoleMenuRepository(DbFactory dbFactory) : base(dbFactory)
        {

        }
        public bool HasAccessToMenu(Guid menuId, Guid roleId)
        {
            return _dbFactory.DbContext.Set<RoleMenu>().Where(m => m.RoleID == roleId && m.MenuID == menuId).FirstOrDefault() != null;
        }

        public bool HasAccessToMenu(Guid menuId, IEnumerable<string> roleNames)
        {
            var menuPermittedRoles = from rm in _dbFactory.DbContext.RoleMenus
                       join r in _dbFactory.DbContext.Roles on rm.RoleID equals r.Id
                       join m in _dbFactory.DbContext.Menus on rm.MenuID equals m.Id
                       where rm.MenuID == menuId
                       select new
                       {
                           r.Name
                       };
            foreach (var userRoleName in roleNames)
            {
                if (menuPermittedRoles.Any(m=> m.Name == userRoleName))
                    return true;
            }
            return false;
        }
    }
}
