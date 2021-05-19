using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.ViewEntities;
using WEA.Infrastructure.Data;
using WEA.SharedKernel;

namespace WEA.Presentation.Services
{
    public class GridViewServiceFacade : BaseServiceFacade
    {
        private readonly ViewsDbContext _context;

        public GridViewServiceFacade(ViewsDbContext context)
        {
            _context = context;
        }

        public Result<IQueryable<RoleMenuGridView>> GetRoleMenuGridView()
        {
            return Succeed(_context.RoleMenuGridView.AsQueryable());
        }
        public Result<IQueryable<OrganizationGridView>> GetOrganizationGridView()
        {
            return Succeed(_context.OrganizationGridView.AsQueryable());
        }
    }
}
