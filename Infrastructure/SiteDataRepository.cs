using AOOR.Utilities;
using Core.Interface;
using Core.Model;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Infrastructure
{
    /// <summary>
    /// Class managing all editable data except the artwork itself.
    /// </summary>
    public class SiteDataRepository : ISiteDataRepository
    {
        private const string filename = "SiteDefinition.en-US.xml";
        private string _location;

        public SiteData SiteData { get; set; }

        public SiteDataRepository(string path)
        {
            _location = Path.Combine(path, filename);
            Load();
        }

        public void AddLink(WebLink link)
        {
            if (link.Id == 0)
            {
                link.Id = Enumerable.Max(SiteData.Links, m => m.Id) + 1;
            }
            SiteData.Links.Add(link);
        }

        public void RemoveLink(int id)
        {
            var link = (from l in SiteData.Links
                        where l.Id == id
                        select l).First();
            SiteData.Links.Remove(link);
        }

        public void DemoteLink(int id)
        {
            SiteData.Links.DemoteEntry(p => p.Id == id);
        }

        public void PromoteLink(int id)
        {
            SiteData.Links.PromoteEntry(p => p.Id == id);
        }

        public void Load()
        {
            try
            {
                var _serializer = new ObjectSerializer(_location);
                SiteData = _serializer.Deserialize<SiteData>();
            }
            catch (FileNotFoundException)
            {
                // if the file is not there, start empty
                SiteData = new SiteData();
            }
        }

        public void Save()
        {
            var _serializer = new ObjectSerializer(_location);
            _serializer.Serialize<SiteData>(SiteData);
        }
    }
}
