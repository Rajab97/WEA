using System;
using System.Collections.Generic;
using System.Text;
using WEA.Core.Entities;
using WEA.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WEA.Infrastructure.Data.Repositories
{
    public class OrganizationRepository : EfRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(DbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<int> ChangeTitleAsync(string title)
        {
            var query = $@"UPDATE ""Organizations"" SET ""OrganizationName""='{title}'";
            return await _dbFactory.DbContext.Database.ExecuteSqlRawAsync(query);
        }
    }
}
