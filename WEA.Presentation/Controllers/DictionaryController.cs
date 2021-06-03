using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Presentation.Services;

namespace WEA.Presentation.Controllers
{
    public class DictionaryController : BaseController
    {
        public const string Area = "";
        public const string Name = "Dictionary";
        private readonly DictionaryServiceFacade _dictionaryServiceFacade;

        public DictionaryController(DictionaryServiceFacade dictionaryServiceFacade)
        {
            _dictionaryServiceFacade = dictionaryServiceFacade;
        }
        
        public IActionResult Users(DataSourceLoadOptions options)
        {
            var result = _dictionaryServiceFacade.Users();
            return Load(result, options);
        }

        public IActionResult CarBrands(DataSourceLoadOptions options)
        {
            var result = _dictionaryServiceFacade.CarBrands();
            return Load(result, options);
        }
        public IActionResult Menus(DataSourceLoadOptions options)
        {
            var result = _dictionaryServiceFacade.Menus();
            return Load(result,options);
        }
        public IActionResult Roles(DataSourceLoadOptions options)
        {
            var result = _dictionaryServiceFacade.Roles();
            return Load(result, options);
        }
        public IActionResult ProductTypes(DataSourceLoadOptions options)
        {
            var result = _dictionaryServiceFacade.ProductTypes();
            return Load(result, options);
        }
    }
}
