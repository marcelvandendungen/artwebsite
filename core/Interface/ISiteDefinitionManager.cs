using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Model;

namespace Core.Interface
{
    public interface ISiteDefinitionManager
    {
        SiteDefinition SiteDefinitions { get; set; }
        void Load();
        void Save();
        void AddLink(WebLink link);
        void RemoveLink(int id);
        void DemoteLink(int id);
        void PromoteLink(int id);
    }
}