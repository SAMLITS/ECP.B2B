using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ECP.Util.AutofacIoc
{
    public class AutofacHelp
    {
        public static IServiceProvider AutofacProviderBuilderCore(IServiceCollection services, IContainer ApplicationContainer, Module modules)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(modules); //new AutofacModule();
            builder.Populate(services);
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }
    }

    //public class AutofacModule : Module
    //{
    //    protected override void Load(ContainerBuilder builder)
    //    {
    //        //builder.RegisterType<DiTest>().As<IDiTest>();
    //    }
    //}
}
