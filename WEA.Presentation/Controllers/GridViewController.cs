using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Presentation.Services;

namespace WEA.Presentation.Controllers
{
    public class GridViewController : BaseController
    {
        public const string Area = "";
        public const string Name = "GridView";
        private readonly GridViewServiceFacade _facade;

        public GridViewController(GridViewServiceFacade facade)
        {
            _facade = facade;
        }
        public IActionResult GetRoleMenuGridView(DataSourceLoadOptions loadOptions)
        {
            var result = _facade.GetRoleMenuGridView();
            if (result.IsSucceed)
                return Load(result.Data,loadOptions);
            return AjaxFailureResult(result);
        }
    }
}
