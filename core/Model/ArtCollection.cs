using AOOR.Utilities;
using Core.Interface;
using Core.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Core.Model
{
    public class ArtCollection : IArtCollection
    {
        private IPaintingRepository _paintingRepository;
        private IEnumerable<Painting> _paintings;

        public ArtCollection(IPaintingRepository paintingRepository)
        {
            _paintingRepository = paintingRepository;
        }

        public IEnumerable<Painting> Paintings
        {
            get
            {
                if (_paintings == null)
                {
                    _paintings = _paintingRepository.Read();
                }
                return _paintings;
            }
        }
    }
}
