using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Core.Interface
{
    public interface IImageManager
    {
        void SaveImageFile(HttpPostedFileBase postedFile);
        string SaveThumbnail(string filename);
        void DeleteImage(string filename);
        void DeleteThumbnail(string filename);
    }
}
