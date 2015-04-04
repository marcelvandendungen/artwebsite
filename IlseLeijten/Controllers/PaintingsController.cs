using Core.Interface;
using Core.Model;
using IlseLeijten.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IlseLeijten.Controllers
{
    [AuthorizeUsers]
    [RequireHttps]
    public class PaintingsController : Controller
    {
        private IArtRepository _artRepository;
        private ArtCollection _artCollection;

        public PaintingsController(IArtRepository artRepository)
        {
            _artRepository = artRepository;
            _artCollection = _artRepository.Read();
        }

        [HttpGet]
        public ActionResult Manage()
        {
            int idx = 0;

            var viewmodel = _artCollection.Paintings.Select(p => new ArtWorkViewModel
                {
                    Id = idx++,
                    Name = p.Name,
                    FileName = p.FileName,
                    Year = p.Year,
                    Notes = p.Notes
                });
            return View(viewmodel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Painting painting)
        {
            _artCollection.Paintings.Add(painting);
            _artRepository.Save(_artCollection);

            return RedirectToAction("Manage", "Paintings");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var painting = _artCollection.Paintings[id];
            return View(painting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Painting painting)
        {
            if (ModelState.IsValid)
            {
                int idx = _artCollection.Paintings.FindIndex(p => p.Name == painting.Name);
                _artCollection.Paintings.RemoveAt(idx);
                _artCollection.Paintings.Insert(idx, painting);
                _artRepository.Save(_artCollection);
            }
            return RedirectToAction("Manage", "Paintings");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var painting = _artCollection.Paintings[id];
            
            return View(painting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Painting painting)
        {
            _artCollection.Paintings.RemoveAt(id);
            _artRepository.Save(_artCollection);

            return RedirectToAction("Manage", "Paintings");
        }

        public ActionResult MoveUp(int id)
        {
            if (id > 0)
            {
                _artCollection.Paintings.PromoteEntry(id);
                _artRepository.Save(_artCollection);
            }

            return RedirectToAction("Manage", "Paintings");
        }

        public ActionResult MoveDown(int id)
        {
            if (id < _artCollection.Paintings.Count - 1)
            {
                _artCollection.Paintings.DemoteEntry(id);
                _artRepository.Save(_artCollection);
            }

            return RedirectToAction("Manage", "Paintings");
        }
    }
}
