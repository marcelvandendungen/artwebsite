using Core.Interface;
using Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Infrastructure
{
    public class ArtRepository : IArtRepository
    {
        private string _dataDirectory;

        public ArtRepository(string dataDirectory)
        {
            _dataDirectory = dataDirectory;

            if (!Directory.Exists(_dataDirectory))
            {
                Directory.CreateDirectory(_dataDirectory);
            }
        }

        public ArtCollection Read()
        {
            string filePath = Path.Combine(_dataDirectory, Constants.ArtCollectionFileName);
            var serializer = new ObjectSerializer(filePath);

            try
            {
                var paintings = serializer.Deserialize<List<Painting>>();

                return new ArtCollection
                    {
                        Paintings = paintings
                    };
            }
            catch (FileNotFoundException)
            {
                // if file not found, start fresh
                return new ArtCollection
                    {
                        Paintings = new List<Painting>()
                    };
            }
        }
    
        public void Save(ArtCollection artCollection)
        {
            string filePath = Path.Combine(_dataDirectory, Constants.ArtCollectionFileName);
            var serializer = new ObjectSerializer(filePath);
            serializer.Serialize<List<Painting>>(artCollection.Paintings);
        }
    }
}
