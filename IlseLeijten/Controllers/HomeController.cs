using Core.Interface;
using Core.Model;
using IlseLeijten.Models;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IlseLeijten.Controllers
{
    public class HomeController : Controller
    {
        const string _imageRoot = "https://ilseleijten.blob.core.windows.net/pictures/";
        private SiteMetaData _siteMetaData;
        private ArtCollection _artCollection;
        private IMetaDataRepository _metadataRepostory;
        private IArtRepository _artRepository;

        public HomeController(IMetaDataRepository metadataRepostory, IArtRepository artRepository)
        {
            _metadataRepostory = metadataRepostory;
            _siteMetaData = _metadataRepostory.Read();

            _artRepository = artRepository;
            _artCollection = _artRepository.Read();
        }

        public ActionResult Index()
        {
            var messages = Request.QueryString.GetValues("alert");
            var alertMsg = string.Empty;

            if (messages != null)
            {
                alertMsg = messages[0];
            }

            var viewmodel = new IndexViewModel
            {
                FrontPageImage = _imageRoot + _siteMetaData.FrontPageImage,
                AlertMessage = alertMsg
            };

            return View(viewmodel);
        }

        public ActionResult About()
        {
            var viewModel = new AboutViewModel
            {
                Caption = _siteMetaData.ArtistInfo.Caption,
                Text = _siteMetaData.ArtistInfo.Text,
                Picture = _imageRoot + _siteMetaData.ArtistInfo.Picture
            };
            return View(viewModel);
        }

        public ActionResult Paintings()
        {
            var viewModel = new PaintingViewModel 
            {
                ImageRoot = "https://ilseleijten.blob.core.windows.net/pictures",
                ThumbnailRoot = "https://ilseleijten.blob.core.windows.net/thumbnails",
                Paintings = _artCollection.Paintings
            };

            return View(viewModel);
        }

        public ActionResult Links()
        {
            var viewModel = new LinksViewModel();
            viewModel.Links = _siteMetaData.Links;

            return View(viewModel);
        }

        public ActionResult Contact()
        {
            var viewModel = new ContactViewModel();

            return View(viewModel);
        }
    }
}
