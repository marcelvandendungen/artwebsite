using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Interface
{
    public interface IPictureRepository
    {
        void Add(string name, string path, string container);
        void Delete(string location, string container);
        IEnumerable<Picture> GetPictures();
    }
}
