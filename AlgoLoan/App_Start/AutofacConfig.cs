using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using AlgoLoan.Models;
using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using DAL.EF;
using DAL.Repo;

namespace AlgoLoan
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            RegisterServices(builder);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            builder.RegisterInstance(config.CreateMapper())
                .As<IMapper>()
                .SingleInstance();

            builder.RegisterType<AlgoLoanDbContext>().InstancePerRequest();

            builder.RegisterType<ProviderRepository>().As<IProviderRepository>()
                .WithParameter(new TypedParameter(typeof(AlgoLoanDbContext), new AlgoLoanDbContext()))
                .InstancePerRequest();

            builder.RegisterType<SearchRepository>().As<ISearchRepository>()
                .WithParameter(new TypedParameter(typeof(AlgoLoanDbContext), new AlgoLoanDbContext()))
                .InstancePerRequest();

            builder.RegisterType<VisitRepository>().As<IVisitRepository>()
                .WithParameter(new TypedParameter(typeof(AlgoLoanDbContext), new AlgoLoanDbContext()))
                .InstancePerRequest();

            builder.RegisterType<SubscriptionRepository>().As<ISubscriptionRepository>()
                .WithParameter(new TypedParameter(typeof(AlgoLoanDbContext), new AlgoLoanDbContext()))
                .InstancePerRequest();

        }
    }
}