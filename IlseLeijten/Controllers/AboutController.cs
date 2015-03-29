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
    public class AboutController : Controller
    {
        private IMetaDataRepository _metadataRepostory;
        private SiteMetaData _siteMetaData;

        public AboutController(IMetaDataRepository metadataRepostory)
        {
            _metadataRepostory = metadataRepostory;
            _siteMetaData = _metadataRepostory.Read();
        }

        public ActionResult Manage()
        {
            return View(_siteMetaData.ArtistInfo);
        }

        [HttpPost]
        public ActionResult Save(ArtistInfo artistInfo)
        {
            _siteMetaData.ArtistInfo = artistInfo;
            _metadataRepostory.Save(_siteMetaData);

            return RedirectToAction("About", "Home");
        }
     }
}
