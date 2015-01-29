using Core.Interface;
using Core.Model;
using Infrastructure;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Mvc;

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

            container.RegisterType<IMembershipService, OpenIdMembershipService>();
            container.RegisterType<IImageManager, ImageManager>();
            container.RegisterType<IImageRepository, FilesystemImageRepository>();
            container.RegisterType<IArtCollection, ArtCollection>();

            string dataPath = httpServer.MapPath("~/App_Data/");
            container.RegisterInstance<IPaintingRepository>(new PaintingRepository(dataPath));

            IEnumerable<string> authorizedUsers = ConfigurationManager.AppSettings["authorizedusers"].Split(';');
            container.RegisterInstance<IAuthorizedUserManager>(new AuthorizedUserManager(authorizedUsers));
            container.RegisterInstance<ISiteDataRepository>(new SiteDataRepository(dataPath));

            string username = ConfigurationManager.AppSettings["emailusername"];
            string password = ConfigurationManager.AppSettings["emailpassword"];
            string smtpHost = ConfigurationManager.AppSettings["smtphost"];
            container.RegisterInstance<IEmailer>(new Emailer(username, password, smtpHost));

            return new UnityControllerFactory(container);
        }
    }
}