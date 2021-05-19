using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Core.Interfaces.Services;
using WEA.Core.Repositories;

namespace WEA.Presentation.Helpers.Identity
{
    public class AdditionalUserClaimsPrincipleFactory : UserClaimsPrincipalFactory<User,Role>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMenuService _menuService;

        public AdditionalUserClaimsPrincipleFactory(
		UserManager<User> userManager,
		RoleManager<Role> roleManager,
		IOptions<IdentityOptions> optionsAccessor,
        IOrganizationRepository organizationRepository,
        IMenuService menuService)
		: base(userManager, roleManager, optionsAccessor)
        {
            _organizationRepository = organizationRepository;
            _menuService = menuService;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;
            var claims = new List<Claim>();
            claims.Add(new Claim(CustomClaimTypes.UserId,user.Id.ToString()));
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
            var menus = _menuService.GetUserMenus(user.Id);
            if (menus.IsSucceed && menus.Data != null && menus.Data.Any())
            {
                var json = JsonConvert.SerializeObject(AddParentMenusIfNotAdded(menus.Data.Where(m=>m.IsVisible == true).ToList()), Formatting.None);
                if (menus.IsSucceed)
                    claims.Add(new Claim(CustomClaimTypes.Menus, json));
            }
            else
            {
                var json = JsonConvert.SerializeObject(new List<Menu>(), Formatting.None);
                if (menus.IsSucceed)
                    claims.Add(new Claim(CustomClaimTypes.Menus, json));
            }
           

            identity.AddClaims(claims);
            return principal;
        }
        private List<Menu> AddParentMenusIfNotAdded(List<Menu> allowedMenus)
        {
            try
            {
                var result = new List<Menu>();
                var allMenus = _menuService.GetAll();
                if (!allMenus.IsSucceed)
                    return result;

                foreach (var menu in allowedMenus)
                {
                    result.Add(menu);
                    if (menu.ParentId.HasValue && !allowedMenus.Any(m => m.Id == menu.ParentId) && !result.Any(m => m.Id == menu.ParentId))
                    {
                        Guid? parentGuid = allMenus.Data.Where(m => m.Id == menu.ParentId).Select(m => m.Id).FirstOrDefault();
                        while (parentGuid.HasValue && !allowedMenus.Any(m => m.Id == parentGuid) && !result.Any(m => m.Id == parentGuid))
                        {
                            var parentM = allMenus.Data.Where(m => m.Id == parentGuid.Value).FirstOrDefault();
                            parentGuid = parentM.ParentId;
                            result.Add(parentM);
                        }
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                return new List<Menu>();
            }
        }
    }
}
