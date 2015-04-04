using Core.Interface;
using Core.Model;
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
    public class IndexController : Controller
    {
        private IMetaDataRepository _metadataRepository;
        private SiteMetaData _siteMetadata;

        public IndexController(IMetaDataRepository metadataRepository)
        {
            _metadataRepository = metadataRepository;
            _siteMetadata = _metadataRepository.Read();
        }

        public ActionResult Manage()
        {
            return View(_siteMetadata);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Save(SiteMetaData siteMetadata)
        {
            _siteMetadata.FrontPageImage = siteMetadata.FrontPageImage;
            _metadataRepository.Save(_siteMetadata);

            return RedirectToAction("Index", "Home");
        }
    }
}