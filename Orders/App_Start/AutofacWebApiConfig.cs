using Autofac;
using Autofac.Integration.WebApi;
using Orders.Infrastructure.Core;
using OrdersData;
using OrdersData.Infrastructure;
using OrdersData.Repository;
using OrdersService;
using OrdersService.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Orders.App_Start
{
    public class AutofacWebApiConfig
    {
        private static IContainer container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<OrdersContext>().As<DbContext>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterGeneric(typeof(EntityBaseRepository<>)).As(typeof(IEntityBaseRepository<>)).InstancePerRequest();
            builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerRequest();
            builder.RegisterType<MembershipService>().As<IMembership>();

            //Skirtas sumazinti repositoriu skaiciu kontroleryje
            builder.RegisterType<DataRepositoryFactory>().As<IDataRepositoryFactory>().InstancePerRequest();

            container = builder.Build();
            return container;
        }
    }
}