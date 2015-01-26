using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace AOOR.Utilities
{
    public class ObjectSerializer
    {
        private string _location;

        public ObjectSerializer(string location)
        {
            _location = location;
        }

        public T Deserialize<T>()
        {
            var serializer = new XmlSerializer(typeof(T));
            var streamReader = new StreamReader(_location);

            var objects = (T)serializer.Deserialize(streamReader);

            streamReader.Close();

            return objects;
        }

        public void Serialize<T>(T objects)
        {
            var serializer = new XmlSerializer(typeof(T));
            var streamWriter = new StreamWriter(_location);

            serializer.Serialize(streamWriter, objects);
            streamWriter.Close();
        }
    }
}