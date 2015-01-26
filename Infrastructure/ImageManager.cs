using Core.Interface;
using Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Infrastructure
{
    public class ImageManager : IImageManager
    {
        private const string _imagesFolder = @"Content\img";
        private const string _thumbsFolder = @"Content\thumb";

        public void SaveImageFile(HttpPostedFileBase imageFile)
        {
            string imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _imagesFolder);
            string savedFileName = Path.Combine(imagesFolder, Path.GetFileName(imageFile.FileName));

            imageFile.SaveAs(savedFileName);
        }

        public string SaveThumbnail(string filename)
        {
            string imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _imagesFolder);
            string filepath = Path.Combine(imagesFolder, filename);

            var thumbnail = ResizeImage(filepath, 128, 128);

            string thumbsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _thumbsFolder);
            SaveImage(Path.Combine(thumbsFolder, filename), thumbnail);

            return filename;
        }

        public void DeleteImage(string filename)
        {
            string imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _imagesFolder);
            string filepath = Path.Combine(imagesFolder, filename);

            File.Delete(filepath);
        }

        public void DeleteThumbnail(string filename)
        {
            string thumbsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _thumbsFolder);
            string filepath = Path.Combine(thumbsFolder, filename);

            File.Delete(filepath);
        }

        virtual public WebImage ResizeImage(string filepath, int width, int height)
        {
            var image = new WebImage(filepath);
            
            image = image.Resize(width + 1, height + 1, true);
            image = image.Crop(1, 1);

            return image;
        }

        virtual public void SaveImage(string filepath, WebImage image)
        {
            image.Save(filepath);
        }
    }
}