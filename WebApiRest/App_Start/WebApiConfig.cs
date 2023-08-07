using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity.AspNet.WebApi;
using Unity;
using WebApiRest.Services;
using WebApiRest.Factory;
using DataAccess.Repository;
using DataAccess.Model;

namespace WebApiRest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de Web API
            // Create Unity container and register dependencies
            var container = new UnityContainer();
            container.RegisterType<LibreriaDBEntities>();
            container.RegisterType<IDetalleVentaRepository, DetalleVentaRepository>();
            container.RegisterType<IVentaRepository, VentaRepository>();
            container.RegisterType<IClienteRepository, ClienteRepository>();
            container.RegisterType<ILibroRepository, LibroRepository>();
            container.RegisterType<IFactory, Factory.Factory>();
            container.RegisterType<IDetalleVentaService, DetalleVentaService>();
            container.RegisterType<IVentaService, VentaService>();
            container.RegisterType<IClienteService, ClienteService>();
            container.RegisterType<ILibroService, LibroService>();
            
            // Register other dependencies for repositories as well

            // Set Unity as the dependency resolver for Web API
            config.DependencyResolver = new UnityDependencyResolver(container);

            // Rutas de Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
