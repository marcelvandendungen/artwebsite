using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IImageRepository
    {
        void SaveImage(string filename, string imagePath);
        void SaveThumbnail(string filename, string imagePath, string dimensions);
        void DeleteImage(string filename);
        void DeleteThumbnail(string filename, string dimensions);
    }
}
