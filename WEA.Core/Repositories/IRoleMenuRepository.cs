using System;
using System.Collections.Generic;
using System.Text;
using WEA.Core.Entities;
using WEA.SharedKernel.Interfaces;

namespace WEA.Core.Repositories
{
    public interface IRoleMenuRepository : IRepository<RoleMenu>
    {
        bool HasAccessToMenu(Guid menuId,Guid roleId);
        bool HasAccessToMenu(Guid menuId, IEnumerable<string> roleNames);
    }
}
