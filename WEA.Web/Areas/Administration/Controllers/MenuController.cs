using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Web.Areas.Administration.Models;
using WEA.Web.Areas.Administration.Services;
using WEA.Web.Controllers;
using WEA.Web.Services;

namespace WEA.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class MenuController : BaseController
    {
        public const string Name = "Menu";
        private readonly MenuServiceFacade _facade;
        private readonly DictionaryServiceFacade _dictionary;

        public MenuController(MenuServiceFacade facade,
                                DictionaryServiceFacade dictionary)
        {
            _facade = facade;
            _dictionary = dictionary;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new MenuViewModel();
            IntializeViewModel(model);
            return View("Form", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MenuViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", model);
            }
            var resulr = _facade.Save(model);
            return Json("Ok");
        }


        private void IntializeViewModel(MenuViewModel model)
        {
            model.Menus = _dictionary.Menus();
        }
    }
}
