using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WEA.Core.Entities;
using WEA.Core.ViewEntities;
using WEA.SharedKernel.Interfaces;

namespace WEA.Core.Repositories
{
    public interface IMenuRepository : IRepository<Menu>
    {
        IQueryable<Menu> GetUserMenus(Guid id);
        Menu GetMenuByRouteDetails(string area , string controller , string action);
    }
}
