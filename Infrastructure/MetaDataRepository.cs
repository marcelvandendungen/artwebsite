using Core.Interface;
using Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Infrastructure
{
    public class MetaDataRepository : IMetaDataRepository
    {
        private string _dataDirectory;

        public MetaDataRepository(string dataDirectory)
        {
            _dataDirectory = dataDirectory;
        }

        public SiteMetaData Read()
        {
            string filePath = Path.Combine(_dataDirectory, Constants.MetaDataFileName);

            var serializer = new ObjectSerializer(filePath);

            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            try
            {
                return serializer.Deserialize<SiteMetaData>();
            }
            catch (FileNotFoundException)
            {
                // if the file is not there, start empty
                return new SiteMetaData
                    {
                        ArtistInfo = new ArtistInfo(),
                        Links = new List<WebLink>()
                    };
            }
        }

        public void Save(SiteMetaData metaData)
        {
            string filePath = Path.Combine(_dataDirectory, Constants.MetaDataFileName);
            var serializer = new ObjectSerializer(filePath);
            serializer.Serialize<SiteMetaData>(metaData);
        }
    }
}