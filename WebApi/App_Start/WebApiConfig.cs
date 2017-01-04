using CustomerService.Utils.Helpers;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApi.Models;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web


            //var container = new UnityContainer();
            //container.RegisterType<IProductRepositorio, ProductRepositorio>(new HierarchicalLifetimeManager());
            //config.DependencyResolver = new UnityResolver(container);

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
