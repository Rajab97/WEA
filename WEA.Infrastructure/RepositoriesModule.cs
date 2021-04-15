using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WEA.Core;
using WEA.Core.Repositories;
using WEA.Core.Services;
using WEA.Infrastructure.Data;
using WEA.Infrastructure.Data.Repositories;

namespace WEA.Infrastructure
{
    public class RepositoriesModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            /*   builder
                   .RegisterType<TestEntityRepository>()
                       .As<ITestEntityRepository>()
                           .InstancePerLifetimeScope();*/
            var assemblyInfrastructure = Assembly.GetAssembly(typeof(DefaultInfrastructureModule));
            var assamblyCore = Assembly.GetAssembly(typeof(BaseService<>));
            builder.RegisterAssemblyTypes(assemblyInfrastructure)
                            .Where(m => m.Namespace.Contains(nameof(WEA.Infrastructure.Data.Repositories)))
                                .As(t => assamblyCore.GetTypes().Where(m => m.IsInterface && m.Namespace.Contains(nameof(WEA.Core.Repositories))).FirstOrDefault(m => m.Name == "I" + t.Name)).InstancePerLifetimeScope();
        }
    }
}
