using Core.Interface;
using Core.Model;
using IlseLeijten.Models;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IlseLeijten.Controllers
{
    [AuthorizeUsers]
    [RequireHttps]
    public class LinksController : Controller
    {
        private IMetaDataRepository _metadataRepostory;
        private SiteMetaData _siteMetaData;

        public LinksController(IMetaDataRepository metadataRepostory)
        {
            _metadataRepostory = metadataRepostory;
            _siteMetaData = _metadataRepostory.Read();
        }

        public ActionResult Manage()
        {
            int idx = 0;

            var viewmodel = _siteMetaData.Links.Select(l => new WebLinkViewModel 
            {
                Id = idx++,
                Caption = l.Caption,
                Address = l.Address
            });

            return View(viewmodel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(WebLink link)
        {
            _siteMetaData.Links.Add(link);
            _metadataRepostory.Save(_siteMetaData);
            return RedirectToAction("Manage", "Links");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var link = _siteMetaData.Links.ElementAt(id);
            return View(link);
        }

        [HttpPost]
        public ActionResult Edit(WebLink link, int id)
        {
            _siteMetaData.Links.RemoveAt(id);
            _siteMetaData.Links.Insert(id, link);
            _metadataRepostory.Save(_siteMetaData);
            return RedirectToAction("Manage", "Links");
        }

        public ActionResult Delete(int id)
        {
            _siteMetaData.Links.RemoveAt(id);
            _metadataRepostory.Save(_siteMetaData);
            return RedirectToAction("Manage", "Links");
        }

        public ActionResult MoveUp(int id)
        {
            if (id > 0)
            {
                _siteMetaData.Links.PromoteEntry(id);
                _metadataRepostory.Save(_siteMetaData);
            }

            return RedirectToAction("Manage", "Links");
        }

        public ActionResult MoveDown(int id)
        {
            if (id < _siteMetaData.Links.Count - 1)
            {
                _siteMetaData.Links.DemoteEntry(id);
                _metadataRepostory.Save(_siteMetaData);
            }

            return RedirectToAction("Manage", "Links");
        }
    }
}