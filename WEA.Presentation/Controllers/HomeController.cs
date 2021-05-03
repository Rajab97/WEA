using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WEA.Presentation.Models;
using WEA.SharedKernel.Exceptions;
using WEA.SharedKernel.Resources;

namespace WEA.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public const string Name = "Home";
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            var exceptionHandlerPathFeature =
                         HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var model = new ErrorPageModel()
            {
                StatusCode = 500,
            };
            if (exceptionHandlerPathFeature?.Error is BaseException e)
            {
                model.Message = e.Message;
            }
            else
            {
                model.Message = ExceptionMessages.FatalError;
            }

            return View(model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult HandleError(int statusCode)
        {
            var model = new ErrorPageModel() {
                StatusCode = statusCode
            };
            switch (statusCode)
            {
                case 404: {
                        model.Message = ExceptionMessages.PageNotFound;
                        break;
                    }
                default: {
                        model.Message = ExceptionMessages.FatalError;
                        break;
                    }
            }
            return View("Error", model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult NotAuthorize()
        {
            var model = new ErrorPageModel()
            {
                StatusCode = 403,
                Message = ExceptionMessages.UserAccessToThisPage
            };
            return View("Error",model);
        }
    }
}
