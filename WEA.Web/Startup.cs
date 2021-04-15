using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Infrastructure;
using WEA.Infrastructure.Data;
using WEA.SharedKernel.Interfaces;
using WEA.Web.Filters;
using WEA.Web.Helpers;
using WEA.Web.Helpers.Identity;
using WEA.Web.Helpers.Identity.Authorization;
using WEA.Web.Helpers.Identity.Authorization.Handlers;
using WEA.Web.Helpers.Identity.Authorization.Requirements;
using STA = WEA.Web.Helpers.Statics;
namespace WEA.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(connectionString);
            });
            services.AddIdentity<User,Role>(options => {
                //options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Lockout.MaxFailedAccessAttempts = 5;
                //options.SignIn.RequireConfirmedPhoneNumber = true;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                options.LoginPath = "/Account/SignIn";
                options.AccessDeniedPath = "/Home/Error";
                options.LogoutPath = "/Account/Logout";
                //options.SlidingExpiration = true;
            });
            services.AddAuthorization(options => {
                // [AllowAnonymus] attributu istifade olunmush actionlar istisna olmaqla digerlerinde login olmagi teleb edir
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            });
            services.AddScoped<IUserClaimsPrincipalFactory<User>, AdditionalUserClaimsPrincipleFactory>();
            services.AddScoped<CurrentUser>();
            services.AddSingleton<IAuthorizationPolicyProvider, CustomPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, ExpiredPaymentAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, AdminAuthorizationHandler>();
            services.AddTransient<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();
            services.AddHttpContextAccessor();
            //services.AddScoped<PaymentExpiredAuthFilter>();
            //SeedAdminPW
            services.AddControllersWithViews();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DefaultInfrastructureModule(_env.EnvironmentName == "Development"));
            builder.RegisterModule(new RepositoriesModule());
            builder.RegisterModule(new ServicesModule());
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
