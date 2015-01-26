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
    public class PaintingsController : Controller
    {
        private IPaintingRepository _paintingRepository;
        private IArtCollection _artCollection;
        private IImageManager _imageManager;
        private IImageRepository _imageRepository;

        public PaintingsController(IArtCollection artCollection, IPaintingRepository paintingRepository, IImageManager imageManager, IImageRepository imageRepository)
        {
            _artCollection = artCollection;
            _paintingRepository = paintingRepository;
            _imageManager = imageManager;
            _imageRepository = imageRepository;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Manage()
        {
            var paintings = _artCollection.Paintings;
            return View(paintings);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Painting painting)
        {
            // if an image file was uploaded
            if (Request.Files.Count > 0)
            {
                var postedFile = Request.Files[0] as HttpPostedFileBase;
                string filename = Path.GetFileName(postedFile.FileName);

                string tempFolder = Path.GetTempPath();
                string tempPath = Path.Combine(tempFolder, filename);
                postedFile.SaveAs(tempPath);

                _imageRepository.SaveImage(filename, tempPath);

                painting.Filename = filename;

                var thumbnail = ResizeImage(tempPath, 128, 128);

                string tempThumbPath = Path.Combine(tempFolder, "128x128_" + filename);
                _imageRepository.SaveThumbnail(filename, tempThumbPath, "128x128");

                _paintingRepository.Create(painting);
            }

            return RedirectToAction("Manage", "Paintings");
        }

        virtual public WebImage ResizeImage(string filepath, int width, int height)
        {
            var image = new WebImage(filepath);

            image = image.Resize(width + 1, height + 1, true);
            image = image.Crop(1, 1);

            return image;
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var painting = FindPainting(id);
            return View(painting);
        }

        [HttpPost]
        public ActionResult Edit(Painting painting)
        {
            if (ModelState.IsValid)
            {
                _paintingRepository.Update(painting);
            }
            return RedirectToAction("Manage", "Paintings");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var painting = FindPainting(id);
            
            return View(painting);
        }

        [HttpPost]
        public ActionResult Delete(int id, Painting painting)
        {
            painting = FindPainting(id);
            // delete image files
            _imageManager.DeleteImage(painting.Filename);
            _imageManager.DeleteThumbnail(painting.Filename);

            // delete record from collection
            _paintingRepository.Delete(id);

            return RedirectToAction("Manage", "Paintings");
        }

        public ActionResult MoveUp(int id)
        {
            _paintingRepository.PromotePainting(id);

            return RedirectToAction("Manage", "Paintings");
        }

        public ActionResult MoveDown(int id)
        {
            _paintingRepository.DemotePainting(id);

            return RedirectToAction("Manage", "Paintings");
        }

        private Painting FindPainting(int id)
        {
            return (from p in _artCollection.Paintings
                    where p.Id == id
                    select p).First();
        }
    }
}
