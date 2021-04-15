using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Core.Services;
using WEA.SharedKernel;
using WEA.SharedKernel.Interfaces;

namespace WEA.Core.Interfaces.Services
{
    public interface IOrganizationService : IBaseService<Organization>
    {
        Task<Result<int>> ChangeTitleAsync(string title);
    }
}
