using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Core.Repositories;

namespace WEA.Web.Helpers.Identity
{
    public class AdditionalUserClaimsPrincipleFactory : UserClaimsPrincipalFactory<User,Role>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public AdditionalUserClaimsPrincipleFactory(
		UserManager<User> userManager,
		RoleManager<Role> roleManager,
		IOptions<IdentityOptions> optionsAccessor,
        IOrganizationRepository organizationRepository)
		: base(userManager, roleManager, optionsAccessor)
        {
            _organizationRepository = organizationRepository;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;
            var claims = new List<Claim>();
            claims.Add(new Claim(CustomClaimTypes.IsSuperAdmin,user.IsAdmin.ToString()));

            var organization = await _organizationRepository.GetAll().Where(m => m.OwnerId == user.Id).FirstOrDefaultAsync();
            if (organization != null)
            {
                claims.Add(new Claim(CustomClaimTypes.IsOwner, true.ToString()));
                if (organization.ExpiredDate.HasValue)
                {
                    claims.Add(new Claim(CustomClaimTypes.PaymentExDate,organization.ExpiredDate.Value.ToString()));
                }
            }
            else
            {
                claims.Add(new Claim(CustomClaimTypes.IsOwner, false.ToString()));
                //claims.Add(new Claim(CustomClaimTypes.PaymentExDate,  null));
            }
            identity.AddClaims(claims);
            return principal;
        }
    }
}
