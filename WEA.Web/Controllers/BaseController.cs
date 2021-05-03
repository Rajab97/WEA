using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEA.SharedKernel;
using WEA.Web.Attributes;

namespace WEA.Web.Controllers
{
    [PaymentExpiredAuthAttribute]
    [Authorize]
    public class BaseController : Controller
    {
        protected new ActionResult Json(object data)
        {
            const string contentType = "application/json";
            var contentEncoding = Encoding.UTF8;
            var config = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            var json = JsonConvert.SerializeObject(data, config);
            return Content(json, contentType, contentEncoding);
        }
        protected ActionResult Load<T>(IQueryable<T> data, DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(data, loadOptions);
            return Json(result);
        }
        protected ActionResult Load<T>(IEnumerable<T> data, DataSourceLoadOptions loadOptions)
        {
            return Json(DataSourceLoader.Load(data, loadOptions));
        }
        protected ActionResult AjaxFailureResult(Result response, Action action = null)
        {
            action?.Invoke();
            Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;

            //https://docs.microsoft.com/en-us/dotnet/api/system.web.httpresponse.statusdescription?redirectedfrom=MSDN&view=netframework-4.8#System_Web_HttpResponse_StatusDescription
            //512 simboldan artiq ola bilmez
/*            var description = response.FailureResult.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(description)) description = string.Empty;
            if (description.Length > 500) description = description.Substring(0, 500) + "...";
            Response.StatusDescription = description;*/
            return Json(response.FailureResult);
        }
    }
}
