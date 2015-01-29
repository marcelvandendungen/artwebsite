using Core.Interface;
using System;
using System.IO;

namespace Infrastructure
{
    /// <summary>
    /// Stores images and thumbnail in the filesystem
    /// </summary>
    public class FilesystemImageRepository : IImageRepository
    {
        private const string _imagesFolder = @"Content\img";
        private const string _thumbsFolder = @"Content\thumb";

        public void SaveImage(string filename, string imagePath)
        {
            string filepath = CreateImagePath(filename);

            File.Copy(imagePath, filepath, true);
        }

        public void SaveThumbnail(string filename, string imagePath, string dimensions)
        {
            string filepath = CreateThumbnailPath(filename, dimensions);

            File.Copy(imagePath, filepath, true);
        }

        public void DeleteImage(string filename)
        {
            string filepath = CreateImagePath(filename);

            File.Delete(filepath);
        }

        public void DeleteThumbnail(string filename, string dimensions)
        {
            string filepath = CreateThumbnailPath(filename, dimensions);

            File.Delete(filepath);
        }

        private static string CreateImagePath(string filename)
        {
            string imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _imagesFolder);
            string savedFileName = Path.Combine(imagesFolder, Path.GetFileName(filename));

            return savedFileName;
        }

        private static string CreateThumbnailPath(string filename, string dimensions)
        {
            string imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _imagesFolder);
            string savedFileName = Path.Combine(Path.Combine(imagesFolder, Path.GetFileName(filename)), dimensions);

            return savedFileName;
        }
    }
}
