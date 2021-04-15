using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WEA.Core;
using WEA.Core.Services;

namespace WEA.Infrastructure
{
    public class ServicesModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assamblyCore = Assembly.GetAssembly(typeof(BaseService<>));
            builder.RegisterAssemblyTypes(assamblyCore)
                            .Where(m => NameSpaceChecking(m))
                                .As(t => InterfaceChecking(assamblyCore,t)).InstancePerLifetimeScope();
        }
        private bool NameSpaceChecking(Type m)
        {
            var result = m.Namespace.Contains("WEA.Core.Services");
            return result;
        }
        private Type InterfaceChecking(Assembly assamblyCore,Type t)
        {
            var result = assamblyCore.GetTypes().Where(m => m.IsInterface && m.Namespace.Contains("WEA.Core.Interfaces.Services")).FirstOrDefault(m => m.Name == "I" + t.Name);
            return result;
        }
    }
}
