using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Web.Services;

namespace WEA.Web.Controllers
{
    public class DictionaryController : BaseController
    {
        public const string Name = "Dictionary";
        private readonly DictionaryServiceFacade _dictionaryServiceFacade;

        public DictionaryController(DictionaryServiceFacade dictionaryServiceFacade)
        {
            _dictionaryServiceFacade = dictionaryServiceFacade;
        }
        public IActionResult Menus(DataSourceLoadOptions options)
        {
            var result = _dictionaryServiceFacade.Menus();
            return Load(result,options);
        }
    }
}
