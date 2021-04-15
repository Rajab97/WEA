using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Core.Interfaces.Services;
using WEA.Core.Repositories;
using WEA.SharedKernel;
using WEA.SharedKernel.Exceptions;
using WEA.SharedKernel.Interfaces;

namespace WEA.Core.Services
{
    public class OrganizationService : BaseService<Organization>, IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;
        public OrganizationService(IOrganizationRepository repository) : base(repository)
        {
            _organizationRepository = repository;
        }

        public async Task<Result<int>> ChangeTitleAsync(string title)
        {
            try
            {
                var res =  await _organizationRepository.ChangeTitleAsync(title);
                return Result<int>.Succeed(res);
            }
            catch (BaseException exc)
            {
                return Result<int>.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result<int>.Failure(fatalExc);
            }
        }
    }
}
