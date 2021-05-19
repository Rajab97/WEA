using Autofac;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WEA.Infrastructure.Data;
using WEA.SharedKernel.Interfaces;
using WEA.Core;
using WEA.Infrastructure.Data.Repositories;
using WEA.Core.Repositories;
using WEA.Core.Services;
using WEA.Core.Interfaces;
using WEA.Core.Common.Services;
using WEA.Core.CommonServices;

namespace WEA.Infrastructure
{
    public class DefaultInfrastructureModule : Autofac.Module
    {
        private bool _isDevelopment = false;
        private List<Assembly> _assemblies = new List<Assembly>();

        public DefaultInfrastructureModule(bool isDevelopment, Assembly callingAssembly = null)
        {
            _isDevelopment = isDevelopment;
            var coreAssembly = Assembly.GetAssembly(typeof(EmailService));
            var infrastructureAssembly = Assembly.GetAssembly(typeof(EfRepository));
            _assemblies.Add(coreAssembly);
            _assemblies.Add(infrastructureAssembly);
            if (callingAssembly != null)
            {
                _assemblies.Add(callingAssembly);
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_isDevelopment)
            {
                RegisterDevelopmentOnlyDependencies(builder);
            }
            else
            {
                RegisterProductionOnlyDependencies(builder);
            }
            RegisterCommonDependencies(builder);
        }

        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<EfRepository>().As<IRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>))
           .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(BaseService<>)).As(typeof(IBaseService<>))
                .InstancePerLifetimeScope();


            builder.RegisterType<UserService>().AsSelf().InstancePerLifetimeScope();
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();


            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(IRequestExceptionHandler<,,>),
                typeof(IRequestExceptionAction<,>),
                typeof(INotificationHandler<>),
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                .RegisterAssemblyTypes(_assemblies.ToArray())
                .AsClosedTypesOf(mediatrOpenType)
                .AsImplementedInterfaces();
            }

            builder
               .RegisterType<DbFactory>()
               .AsSelf()
               .InstancePerLifetimeScope();
            builder
              .RegisterType<UnitOfWork>()
              .As<IUnitOfWork>()
              .InstancePerLifetimeScope();
            //builder.Register<Func<AppDbContext>>(c => () => c.Resolve<AppDbContext>()).InstancePerLifetimeScope();


            //services.AddScoped<DbFactory>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<Func<AppDbContext>>((provider) => () => provider.GetService<AppDbContext>());

            builder.RegisterType<EmailService>().As<IEmailService>()
                .InstancePerLifetimeScope();
        }

        private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
        {
            // TODO: Add development only services
        }

        private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
        {
            // TODO: Add production only services
        }


       
    }
}
