using Core.Interface;
using IlseLeijten.Models;
using Infrastructure;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using IlseLeijten;
using Core.Model;
using System.IO;

namespace IlseLeijten
{
    public class DependencyInjectionConfig
    {
        internal static void Register(ControllerBuilder controllerBuilder, HttpServerUtility httpServer)
        {
            controllerBuilder.SetControllerFactory(ConfigureFactory(httpServer));
        }

        private static IControllerFactory ConfigureFactory(HttpServerUtility httpServer)
        {
            // composition root, register all types with IoC container here.
            var container = new UnityContainer();

            string dataPath = httpServer.MapPath("~/App_Data/");
            container.RegisterType<IMetaDataRepository, MetaDataRepository>(new InjectionConstructor(dataPath));
            container.RegisterType<IArtRepository, ArtRepository>(new InjectionConstructor(dataPath));

            string username = ConfigurationManager.AppSettings["emailusername"];
            string password = ConfigurationManager.AppSettings["emailpassword"];
            string smtpHost = ConfigurationManager.AppSettings["smtphost"];
            container.RegisterType<IEmailer, Emailer>(new InjectionConstructor(username, password, smtpHost));

            return new UnityControllerFactory(container);
        }
    }
}
