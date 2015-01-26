using Core.Interface;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IlseLeijten.Controllers
{
    [Authorize]
    public class AboutController : Controller
    {
        private ISiteDataRepository _siteDefinitionManager;

        public AboutController(ISiteDataRepository siteDefinitionManager)
        {
            _siteDefinitionManager = siteDefinitionManager;
        }

        public ActionResult Manage()
        {
            return View(_siteDefinitionManager.SiteData.AboutPage);
        }

        [HttpPost]
        public ActionResult Save(ArtistInfo info)
        {
            _siteDefinitionManager.SiteData.AboutPage = info;
            _siteDefinitionManager.Save();

            return RedirectToAction("About", "Home");
        }
    }
}