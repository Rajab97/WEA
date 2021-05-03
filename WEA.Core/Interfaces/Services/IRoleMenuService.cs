using System;
using System.Collections.Generic;
using System.Text;
using WEA.Core.Entities;
using WEA.SharedKernel;
using WEA.SharedKernel.Interfaces;

namespace WEA.Core.Interfaces.Services
{
    public interface IRoleMenuService : IBaseService<RoleMenu>
    {
        Result HasAccessToMenu(Guid menuId, Guid roleId);
        Result HasAccessToMenu(Guid menuId, IEnumerable<string> roleNames);
    }
}
