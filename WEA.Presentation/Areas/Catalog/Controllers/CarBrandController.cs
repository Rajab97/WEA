using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Presentation.Areas.Catalog.Models;
using WEA.Presentation.Areas.Catalog.Services;
using WEA.Presentation.Controllers;
using WEA.Presentation.Helpers.Statics;
using WEA.Presentation.Services;

namespace WEA.Presentation.Areas.Catalog.Controllers
{
    [Area(AreaConstants.Catalog)]
    public class CarBrandController : BaseController
    {
        public const string Name = "CarBrand";
        private readonly CarBrandServiceFacade _facade;
        private readonly DictionaryServiceFacade _dictionary;

        public CarBrandController(CarBrandServiceFacade facade,
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
            var model = new CarBrandViewModel();
            if (id.HasValue)
            {
                var result = await _facade.GetModel(id.Value);
                if (result.IsSucceed)
                    model = result.Data;
                else
                    return AjaxFailureResult(result);
            }
            return PartialView("_Form", model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CarBrandViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
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
