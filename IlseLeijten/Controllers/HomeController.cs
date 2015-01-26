using Core.Model;
using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IlseLeijten.Controllers
{
    public class HomeController : Controller
    {
        private ISiteDataRepository _siteDefinitionManager;
        private IArtCollection _artCollection;

        public HomeController(IArtCollection artCollection, ISiteDataRepository siteDefinitionManager)
        {
            _artCollection = artCollection;
            _siteDefinitionManager = siteDefinitionManager;
        }

        public ActionResult Index()
        {
            ViewBag.frontimage = _siteDefinitionManager.SiteData.FrontPageImage;

            var alert = Request.QueryString.GetValues("alert");
            if (alert != null)
            {
                ViewBag.Alert = alert[0];
            }
            return View();
        }

        public ActionResult About()
        {
            var about = _siteDefinitionManager.SiteData.AboutPage;
            return View(about);
        }

        public ActionResult Paintings()
        {
            var paintings = _artCollection.Paintings;
            return View(paintings);
        }

        public ActionResult Links()
        {
            var links = _siteDefinitionManager.SiteData.Links;
            return View(links);
        }

        public ActionResult Contact()
        {
            return View();
        }

        [Authorize]
        public ActionResult Manage()
        {
            return View(_siteDefinitionManager.SiteData);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Save(SiteData site)
        {
            _siteDefinitionManager.SiteData.FrontPageImage = site.FrontPageImage;
            _siteDefinitionManager.Save();

            return RedirectToAction("Index", "Home");
        }
    }
}