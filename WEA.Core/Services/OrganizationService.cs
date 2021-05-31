using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Core.Helpers.Enums;
using WEA.Core.Interfaces.Services;
using WEA.Core.Repositories;
using WEA.SharedKernel;
using WEA.SharedKernel.Exceptions;
using WEA.SharedKernel.Interfaces;
using WEA.SharedKernel.Resources;

namespace WEA.Core.Services
{
    public class OrganizationService : BaseService<Organization>, IOrganizationService
    {
        private readonly object lock1 = new object();
        private readonly IOrganizationRepository _organizationRepository;
        public OrganizationService(IOrganizationRepository repository) : base(repository)
        {
            _organizationRepository = repository;
        }

        public override async Task<Result> CreateAsync(Organization model)
        {
            lock (lock1)
            {
                return base.CreateAsync(model).Result;
            }
        }

    /*    private string GenerateIdentificationNumberForOrganization(Organization model)
        {
            StringBuilder result = new StringBuilder();

            if (model.ProductType == ProductType.CarParts.ToString())
                result.Append("CR");
            else
                result.Append("DF");

            result.Append(DateTime.Now.ToString("yyyyMMdd"));

            var lastOrganizationInThisMonth = _organizationRepository.GetAll()
                                                            .Where(m => m.CreatedDate.Year == DateTime.Now.Year && m.CreatedDate.Month == DateTime.Now.Month)
                                                                .OrderByDescending(m => m.CreatedDate)
                                                                    .FirstOrDefault();
            int incrementNum = 0;
            if (lastOrganizationInThisMonth != null)
            {
                var num = lastOrganizationInThisMonth.IdentificationNumber.Substring(lastOrganizationInThisMonth.IdentificationNumber.Length - 3, 3);
                incrementNum = Convert.ToInt32(num);
            }
            incrementNum++;
            result.Append(incrementNum.ToString().PadLeft(3, '0'));
            return result.ToString();
        }*/

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
