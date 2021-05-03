using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Presentation.Helpers.Identity;
using WEA.Presentation.Models;

namespace WEA.Presentation.ViewComponents
{
    public class SubMenu : ViewComponent
    {
        private readonly CurrentUser _currentUser;

        public SubMenu(CurrentUser currentUser)
        {
            _currentUser = currentUser;
        }
        public IViewComponentResult Invoke(Guid parentId)
        {
            var menus = new SubMenuComponentModel() {
                Menus = _currentUser.Menus,
                ParentId = parentId
            };
            return View(menus);
        }

        private List<Menu> AddChildrenMenus(Guid parentId)
        {
            var result = new List<Menu>();
            foreach (var subMenu in _currentUser.Menus.Where(m => m.ParentId == parentId))
            {
                result.Add(subMenu);
                if (_currentUser.Menus.Any(m => m.ParentId == subMenu.Id))
                    result.AddRange(AddChildrenMenus(subMenu.Id));
            }
            return result;
        }
    }
}
