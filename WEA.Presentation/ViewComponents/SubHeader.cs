using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Core.Interfaces.Services;

namespace WEA.Presentation.ViewComponents
{
    public class SubHeader : ViewComponent
    {
        private readonly IOrganizationService _organizationService;

        public SubHeader(IOrganizationService organizationService)
        {
            this._organizationService = organizationService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var organzation = await _organizationService.GetAll().Data.FirstOrDefaultAsync();
            return View(organzation != null ? organzation : new Organization());
        }
    }
}
