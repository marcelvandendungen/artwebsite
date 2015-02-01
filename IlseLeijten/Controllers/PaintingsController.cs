using Core.Interface;
using Core.Model;
using System.Linq;
using System.Web.Mvc;

namespace IlseLeijten.Controllers
{
    public class PaintingsController : Controller
    {
        private IPaintingRepository _paintingRepository;
        private IArtCollection _artCollection;

        public PaintingsController(IArtCollection artCollection, IPaintingRepository paintingRepository)
        {
            _artCollection = artCollection;
            _paintingRepository = paintingRepository;
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
            _paintingRepository.Create(painting);

            return RedirectToAction("Manage", "Paintings");
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
