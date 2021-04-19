using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.SharedKernel.Interfaces;

namespace WEA.Web.Services
{
    public class DictionaryServiceFacade:BaseServiceFacade
    {
        private readonly IRepository<Menu> _menus;
        public DictionaryServiceFacade(IRepository<Menu> menus)
        {
            _menus = menus;
        }

        public List<SelectListItem> Menus()
        {
            var result = _menus.GetAll().Select(m => new SelectListItem() { Value = m.Id.ToString(), Text = m.Title });
            return result.ToList();
        }
    }
}
