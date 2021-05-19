using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Presentation.Areas.Administration.Models;
using WEA.Presentation.Areas.Administration.Services;
using WEA.Presentation.Controllers;
using WEA.Presentation.Helpers.Statics;
using WEA.Presentation.Services;

namespace WEA.Presentation.Areas.Administration.Controllers
{
    [Area(AreaConstants.Admin)]
    public class OrganizationController : BaseController
    {
        public const string Name = "Organization";
        private readonly OrganizationServiceFacade _facade;
        private readonly DictionaryServiceFacade _dictionary;

        public OrganizationController(OrganizationServiceFacade facade,
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
                return Load(result.Data, options);
            return AjaxFailureResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> Add(Guid? id)
        {
            var model = new OrganizationViewModel();
            if (id.HasValue)
            {
                var result = await _facade.GetModel(id.Value);
                if (result.IsSucceed)
                    model = result.Data;
                else
                    return AjaxFailureResult(result);
            }
            return View("Form", model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(OrganizationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", model);
            }
            var result = await _facade.SaveAsync(model);
            if (result.IsSucceed)
            {
                return RedirectToAction(nameof(Index));
            }
            return AjaxFailureResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await _facade.Remove(id);
            if (result.IsSucceed)
                return Json("Ok");
            return AjaxFailureResult(result);
        }
    }
}
