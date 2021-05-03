using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Presentation.Helpers.Identity;

namespace WEA.Presentation.ViewComponents
{
    public class Aside : ViewComponent
    {
        private readonly CurrentUser _currentUser;

        public Aside(CurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public IViewComponentResult Invoke()
        {
            var menus = _currentUser.Menus;
            return View(menus);
        }
    }
}
