using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Presentation.Areas.Administration.Services;
using WEA.Presentation.Areas.Catalog.Services;
using WEA.Presentation.Services;

namespace WEA.Presentation.Helpers.Extensions
{
    public static class StartupExtension
    {
        public static void AddServiceFacades(this IServiceCollection services)
        {
            services.AddScoped<MenuServiceFacade>();
            services.AddScoped<RoleServiceFacade>();
            services.AddScoped<RoleMenuServiceFacade>();
            services.AddScoped<UserServiceFacade>();
            services.AddScoped<AccountServiceFacade>();
            services.AddScoped<GridViewServiceFacade>();
            services.AddScoped<DictionaryServiceFacade>();
            services.AddScoped<OrganizationServiceFacade>();
            services.AddScoped<FileServiceFacade>();
            services.AddScoped<CarBrandServiceFacade>();
            services.AddScoped<CarModelServiceFacade>();
        }
    }
}
