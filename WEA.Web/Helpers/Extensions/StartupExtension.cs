using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Web.Areas.Administration.Services;
using WEA.Web.Services;

namespace WEA.Web.Helpers.Extensions
{
    public static class StartupExtension
    {
        public static void AddServiceFacades(this IServiceCollection services)
        {
            services.AddScoped<MenuServiceFacade>();
            services.AddScoped<DictionaryServiceFacade>();
        }
    }
}
