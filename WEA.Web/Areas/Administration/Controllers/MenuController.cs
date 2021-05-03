using DevExtreme.AspNet.Mvc;
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

        public IActionResult GetData(DataSourceLoadOptions options)
        {
            var result = _facade.GetData();
            if (result.IsSucceed)
                return Load(result.Data,options);
            return AjaxFailureResult(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new MenuViewModel();
            return View("Form", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", model);
            }
            var result = await _facade.SaveAsync(model);
            if (result.IsSucceed)
            {
                return Json("Ok");
            }
            return AjaxFailureResult(result);
        }
       /* [AcceptVerbs("GET","POST")]
        public IActionResult CheckForValidUrl(string controller , string action)
        {
           return Json(true);
        }*/
    }
}
