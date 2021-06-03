using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Presentation.Helpers.Web;
using WEA.SharedKernel.Interfaces;
using WEA.Core.Helpers.Enums;
using WEA.SharedKernel.Extensions;
namespace WEA.Presentation.Services
{
    public class DictionaryServiceFacade:BaseServiceFacade
    {
        private readonly IRepository<Menu> _menus;
        private readonly RoleManager<Role> _roles;
        private readonly UserManager<User> _users;
        private readonly IRepository<CarBrand> _carBrands;

        public DictionaryServiceFacade(IRepository<Menu> menus,
                                        RoleManager<Role> roles,
                                        UserManager<User> users,
                                        IRepository<CarBrand> carBrands)
        {
            _menus = menus;
            _roles = roles;
            _users = users;
            _carBrands = carBrands;
        }

        public IQueryable<SelectListItemGuid> Menus()
        {
            var result = _menus.GetAll().Select(m => new SelectListItemGuid() { Id = m.Id, Text = m.Title });
            return result;
        }
        public IQueryable<SelectListItemGuid> CarBrands()
        {
            var result = _carBrands.GetAll().Select(m => new SelectListItemGuid() { Id = m.Id, Text = m.Name });
            return result;
        }
        public IQueryable<User> Users()
        {
            var result = _users.Users;
            return result;
        }

        public IQueryable<SelectListItemGuid> Roles()
        {
            var result = _roles.Roles.Select(m => new SelectListItemGuid() { Id = m.Id, Text = m.Name });
            return result;
        }

        public IEnumerable<SelectListItem> ProductTypes()
        {
            var result = ((ProductType[])Enum.GetValues(typeof(ProductType))).Select(m => new SelectListItem
            {
                                Text = m.GetDisplayName(),
                                Value = m.ToString()
                            }).ToList();
            return result;
        }
    }
}
