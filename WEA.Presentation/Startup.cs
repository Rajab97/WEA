using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WEA.Core.Entities;
using WEA.Core.Services;
using WEA.Infrastructure;
using WEA.Infrastructure.Data;
using WEA.Presentation.Helpers.Extensions;
using WEA.Presentation.Helpers.Identity;
using WEA.Presentation.Helpers.Identity.Authorization;
using WEA.Presentation.Helpers.Identity.Authorization.Handlers;

namespace WEA.Presentation
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(connectionString);
            });
            services.AddDbContext<ViewsDbContext>(options => {
                options.UseSqlServer(connectionString);
            });
            services.AddIdentity<User, Role>(options => {
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
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.LoginPath = "/Account/SignIn";
                options.AccessDeniedPath = "/Home/NotAuthorize";
                options.LogoutPath = "/Account/Logout";
                options.SlidingExpiration = false;
                //options.SlidingExpiration = true;
            });

            var profiles = this.GetType().Assembly.GetTypes().Where(m => m.IsClass && typeof(Profile).IsAssignableFrom(m));
            var mapperConfiguration = new AutoMapper.MapperConfiguration(mc => {
                foreach (var profile in profiles)
                {
                    mc.AddProfile(profile);
                }
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAuthentication();
            services.AddAuthorization(options =>
            {
                // [AllowAnonymus] attributu istifade olunmush actionlar istisna olmaqla digerlerinde login olmagi teleb edir
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            });
            services.AddScoped<IUserClaimsPrincipalFactory<User>, AdditionalUserClaimsPrincipleFactory>();
            services.AddScoped<CurrentUser>();
            services.AddSingleton<IAuthorizationPolicyProvider, CustomPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, ExpiredPaymentAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, AdminAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, HasMenuAccessAuthorizationHandler>();
            services.AddTransient<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();
            services.AddHttpContextAccessor();

            services.AddServiceFacades();
            // Add framework services.
            services
                .AddControllersWithViews()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DefaultInfrastructureModule(_env.EnvironmentName == "Development"));
            builder.RegisterModule(new RepositoriesModule());
            builder.RegisterModule(new ServicesModule());
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePagesWithReExecute("/Home/HandleError", "?statusCode={0}");
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                 name: "MyArea",
                 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
