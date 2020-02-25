using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using DAL.EF;

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
                //cfg.AddProfile(new MappingProfile());
            });

            builder.RegisterType<AlgoLoanDbContext>().SingleInstance();
//            builder.RegisterType<IssueRepository>().As<IIssueRepository>()
//                .WithParameter(new TypedParameter(typeof(AlgoDeskContext), new AlgoDeskContext()))
//                .SingleInstance();

        }
    }
}