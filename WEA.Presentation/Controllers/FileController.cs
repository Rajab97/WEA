using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEA.Presentation.Controllers
{
    public class FileController : BaseController
    {
        public static string Name = "File";
        public IActionResult Index()
        {
            return View();
        }
    }
}
