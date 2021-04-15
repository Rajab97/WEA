using System;
using System.Collections.Generic;
using System.Text;
using WEA.Core.Entities;
using WEA.Core.Interfaces.Services;
using WEA.Core.Repositories;
using WEA.SharedKernel.Interfaces;

namespace WEA.Core.Services
{
    public class DataService : BaseService<Organization> , IDataService
    {
        public DataService(IOrganizationRepository repository) : base(repository)
        {

        }
    }
}
