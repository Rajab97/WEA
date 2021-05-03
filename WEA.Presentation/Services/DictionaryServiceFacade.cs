using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Presentation.Helpers.Web;
using WEA.SharedKernel.Interfaces;

namespace WEA.Presentation.Services
{
    public class DictionaryServiceFacade:BaseServiceFacade
    {
        private readonly IRepository<Menu> _menus;
        private readonly RoleManager<Role> _roles;

        public DictionaryServiceFacade(IRepository<Menu> menus,
                                        RoleManager<Role> roles)
        {
            _menus = menus;
            _roles = roles;
        }

        public IQueryable<SelectListItemGuid> Menus()
        {
            var result = _menus.GetAll().Select(m => new SelectListItemGuid() { Id = m.Id, Text = m.Title });
            return result;
        }

        public IQueryable<SelectListItemGuid> Roles()
        {
            var result = _roles.Roles.Select(m => new SelectListItemGuid() { Id = m.Id, Text = m.Name });
            return result;
        }
    }
}
