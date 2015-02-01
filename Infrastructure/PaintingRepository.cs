using AOOR.Utilities;
using Core.Interface;
using Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Infrastructure
{
    public class PaintingRepository : IPaintingRepository
    {
        private const string paintingsFilename = "paintings.xml";
        private List<Painting> _paintings = null;
        private ObjectSerializer _paintingSerializer;

        public PaintingRepository(string path)
        {
            _paintingSerializer = new ObjectSerializer(Path.Combine(path, paintingsFilename));
        }

        public void Create(Painting painting)
        {
            var newPainting = new Painting
                              {
                                  Id = GenerateUniqueId(_paintings),
                                  Name = painting.Name,
                                  Year = painting.Year,
                                  FileName = painting.FileName,
                                  Notes = string.Empty
                              };
            _paintings.Insert(_paintings.Count, newPainting);

            _paintingSerializer.Serialize<List<Painting>>(_paintings);
        }

        private int GenerateUniqueId(List<Painting> _paintings)
        {
            if (_paintings.Count == 0)
            {
                return 0;
            }

            return _paintings.Max(p => p.Id) + 1;
        }

        public IEnumerable<Painting> Read()
        {
            if (_paintings == null)
            {
                try
                {
                    var paintings = _paintingSerializer.Deserialize<List<Painting>>();

                    // generate id from index
                    _paintings = paintings.Select((p, i) => new Painting
                    {
                        Id = i,
                        Name = p.Name,
                        Year = p.Year,
                        FileName = p.FileName,
                        Notes = p.Notes
                    }).ToList();
                }
                catch (FileNotFoundException)
                {
                    // if file not found, start fresh
                    _paintings = new List<Painting>();
                }
            }

            return _paintings;
        }

        public void Update(Painting updatedPainting)
        {
            // find Painting with painting.Id
            var painting = (from p in _paintings
                              where p.Id == updatedPainting.Id
                              select p).First();

            // update fields that could have been changed
            painting.Name = updatedPainting.Name;
            painting.Year = updatedPainting.Year;
            painting.Notes = updatedPainting.Notes;

            _paintingSerializer.Serialize<List<Painting>>(_paintings);
        }

        public void Delete(int id)
        {
            int idx = _paintings.FindIndex(p => p.Id == id);
            _paintings.RemoveAt(idx);

            _paintingSerializer.Serialize<List<Painting>>(_paintings);
        }

        public void PromotePainting(int id)
        {
            _paintings.PromoteEntry(p => p.Id == id);
            _paintingSerializer.Serialize<List<Painting>>(_paintings);
        }

        public void DemotePainting(int id)
        {
            _paintings.DemoteEntry(p => p.Id == id);
            _paintingSerializer.Serialize<List<Painting>>(_paintings);
        }
    }
}
