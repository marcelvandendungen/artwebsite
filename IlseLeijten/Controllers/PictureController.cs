using Core.Interface;
using Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace IlseLeijten.Controllers
{
    public class PictureController : Controller
    {
        private IPictureRepository _pictureRepository;

        public PictureController(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }

        [HttpGet]
        public ActionResult List()
        {
            IEnumerable<Picture> pictures = _pictureRepository.GetPictures();

            return View(pictures);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(PictureFile pictureFile)
        {
            if (Request.Files.Count > 0)
            {
                var postedFile = Request.Files[0] as HttpPostedFileBase;
                string filename = Path.GetFileName(postedFile.FileName);

                string tempFolder = Path.GetTempPath();
                string tempPath = Path.Combine(tempFolder, filename);
                postedFile.SaveAs(tempPath);

                _pictureRepository.Add(filename, tempPath, "pictures");

                var thumbnail = ResizeImage(tempPath, 128, 128);

                tempPath = Path.Combine(tempFolder, "128x128_" + filename);
                thumbnail.Save(tempPath, "jpg", true);
                _pictureRepository.Add(filename, tempPath, "thumbnails");
            }

            return RedirectToAction("List", "Picture");
        }

        virtual public WebImage ResizeImage(string filepath, int width, int height)
        {
            var image = new WebImage(filepath);

            image = image.Resize(width + 1, height + 1, true);
            image = image.Crop(1, 1);

            return image;
        }
    }
}