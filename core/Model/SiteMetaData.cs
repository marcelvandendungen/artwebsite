using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Model
{
    public class SiteMetaData
    {
        public string FrontPageImage { get; set; }
        public ArtistInfo ArtistInfo { get; set; }
        public List<WebLink> Links { get; set; }
    }
}