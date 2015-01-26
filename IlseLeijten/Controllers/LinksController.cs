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
    public class LinksController : Controller
    {
        private ISiteDataRepository _siteDefinitionManager;
        private SiteData _siteDefinition;

        public LinksController(ISiteDataRepository siteDefinitionManager)
        {
            _siteDefinitionManager = siteDefinitionManager;
            _siteDefinition = siteDefinitionManager.SiteData;
        }

        public ActionResult Manage()
        {
            var links = _siteDefinition.Links;
            return View(links);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(WebLink link)
        {
            _siteDefinitionManager.AddLink(link);
            return RedirectToAction("Manage", "Links");
        }

        public ActionResult Delete(int id)
        {
            _siteDefinitionManager.RemoveLink(id);
            return RedirectToAction("Manage", "Links");
        }

        public ActionResult MoveUp(int id)
        {
            _siteDefinitionManager.PromoteLink(id);
            return RedirectToAction("Manage", "Links");
        }

        public ActionResult MoveDown(int id)
        {
            _siteDefinitionManager.DemoteLink(id);
            return RedirectToAction("Manage", "Links");
        }
    }
}
