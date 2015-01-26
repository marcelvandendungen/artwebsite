using AOOR.Utilities;
using Core.Interface;
using Core.Model;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Infrastructure
{
    public class SiteDefinitionManager : ISiteDefinitionManager
    {
        private const string filename = "SiteDefinition.en-US.xml";
        private string _location;

        public SiteDefinition SiteDefinitions { get; set; }

        public SiteDefinitionManager(string path)
        {
            _location = Path.Combine(path, filename);
            Load();
        }

        public void AddLink(WebLink link)
        {
            if (link.Id == 0)
            {
                link.Id = Enumerable.Max(SiteDefinitions.Links, m => m.Id) + 1;
            }
            SiteDefinitions.Links.Add(link);
        }

        public void RemoveLink(int id)
        {
            var link = (from l in SiteDefinitions.Links
                        where l.Id == id
                        select l).First();
            SiteDefinitions.Links.Remove(link);
        }

        public void DemoteLink(int id)
        {
            SiteDefinitions.Links.DemoteEntry(p => p.Id == id);
        }

        public void PromoteLink(int id)
        {
            SiteDefinitions.Links.PromoteEntry(p => p.Id == id);
        }

        public void Load()
        {
            try
            {
                var _serializer = new ObjectSerializer(_location);
                SiteDefinitions = _serializer.Deserialize<SiteDefinition>();
            }
            catch (FileNotFoundException)
            {
                // if the file is not there, start empty
                SiteDefinitions = new SiteDefinition();
            }
        }

        public void Save()
        {
            var _serializer = new ObjectSerializer(_location);
            _serializer.Serialize<SiteDefinition>(SiteDefinitions);
        }
    }
}
