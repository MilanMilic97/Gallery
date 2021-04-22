using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;
using Microsoft.Practices.Unity;
using MilanMilicIspit.Resolver;
using AutoMapper;
using MilanMilicIspit.Interfaces;
using MilanMilicIspit.Repository;
using MilanMilicIspit.Models;

namespace MilanMilicIspit
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //var cors = new EnableCorsAttribute("*", "*", "*");

            //config.EnableCors(cors);
            config.EnableSystemDiagnosticsTracing();

            var container = new UnityContainer();
            container.RegisterType<IGalleryRepo, GalleryRepo>(new HierarchicalLifetimeManager());
            container.RegisterType<IPictureRepo, PictureRepo>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityResolver(container);

            Mapper.Initialize(cfg =>
            {
               cfg.CreateMap<Gallery, GalleryDTO>(); // automatski će mapirati Author.Name u AuthorName
               //.ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name)); // ako želimo eksplicitno zadati mapranje
                cfg.CreateMap<Picture, PictureDTO>() // automatski će mapirati Author.Name u AuthorName
                .ForMember(dest => dest.GalleryName, opt => opt.MapFrom(src => src.Galery.Name)); // ako želimo eksplicitno zadati mapiranje
            });
        }
    }
}
