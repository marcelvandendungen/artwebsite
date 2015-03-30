using Core.Interface;
using Core.Model;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AzureBlobRepository : IPictureRepository
    {
        private CloudBlobClient _client;

        public AzureBlobRepository(string storageAccount, string accountKey)
        {
            StorageCredentials credentials = new StorageCredentials(storageAccount, accountKey);
            CloudStorageAccount account = new CloudStorageAccount(credentials, true);
            _client = account.CreateCloudBlobClient();
        }

        public IEnumerable<Picture> GetPictures()
        {
            List<Picture> pictures = new List<Picture>();
            var container = _client.GetContainerReference("pictures");

            foreach (IListBlobItem item in container.ListBlobs())
            {
                CloudBlockBlob blob = (CloudBlockBlob)item;
                pictures.Add(new Picture
                                 {
                                     Name = blob.Name,
                                     Location = blob.Uri.Segments[blob.Uri.Segments.Length-1]
                                 });
            }

            return pictures;
        }

        public void Add(string name, string path, string containerName)
        {
            var container = _client.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(name);
            blockBlob.Properties.ContentType = "image/jpeg";

            blockBlob.UploadFromFile(path, FileMode.Open);
        }

        public void Delete(string name, string containerName)
        {
            var container = _client.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(name);

            blockBlob.Delete();
        }
    }
}
