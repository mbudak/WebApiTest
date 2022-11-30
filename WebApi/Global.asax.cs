using AutoMapper;
using BusinessLayer;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Services;
using DataAccessLayer.Database;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using DataAccessLayer.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Routing;
using WebApi.Filters;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Simple Injector's container
            var container = new Container();

            // Automapper Profile
            var profiles = Assembly.GetExecutingAssembly().GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
            var mConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BusinessProfile>();
                cfg.AddProfile<AppServicesProfile>();
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(Activator.CreateInstance(profile) as Profile);
                }
            });

            container.RegisterInstance(mConfig);
            container.RegisterSingleton(() => mConfig.CreateMapper(container.GetInstance));


            // Company
            container.RegisterSingleton<IDbWrapper<Company>, InMemoryDatabase<Company>>();
            container.RegisterSingleton<ICompanyService, CompanyService>();
            container.RegisterSingleton<ICompanyRepository, CompanyRepository>();

            // Employee
            container.RegisterSingleton<IDbWrapper<Employee>, InMemoryDatabase<Employee>>();
            container.RegisterSingleton<IEmployeeService, EmployeeService>();
            container.RegisterSingleton<IEmployeeRepository, EmployeeRepository>();

            // Logger
            container.RegisterSingleton<ILogDbWrapper<LogEntity>, LoggerDatabase<LogEntity>>();
            container.RegisterSingleton<ILoggerRepository, LoggerRepository>();



            // Config
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            // Filters
            GlobalConfiguration.Configuration.Filters.Add(new ExceptionHandlerFilter(container.GetInstance<ILoggerRepository>()));
            GlobalConfiguration.Configuration.Filters.Add(new ActivityLoggingFilter(container.GetInstance<ILoggerRepository>()));


            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            // Original
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
