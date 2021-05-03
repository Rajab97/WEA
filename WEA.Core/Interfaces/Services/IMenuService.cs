using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Core.ViewEntities;
using WEA.SharedKernel;
using WEA.SharedKernel.Interfaces;

namespace WEA.Core.Interfaces.Services
{
    public interface IMenuService : IBaseService<Menu>
    {
        Result<IQueryable<Menu>> GetUserMenus(Guid userId);
        Result<Menu> GetMenuByRouteDetails(string area,string controller,string action);
    }
}
