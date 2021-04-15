using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Core.Services;
using WEA.SharedKernel.Interfaces;

namespace WEA.Core.Repositories
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        Task<int> ChangeTitleAsync(string title);
    }
}
