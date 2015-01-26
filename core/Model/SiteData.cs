using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Model
{
    public class SiteData
    {
        public SiteData()
        {
            this.FrontPageImage = string.Empty;
            this.AboutPage = new ArtistInfo { Caption = string.Empty, Text = string.Empty, Picture = string.Empty };
            this.Links = new List<WebLink>();
        }

        public string FrontPageImage { get; set; }
        public ArtistInfo AboutPage { get; set; }
        public List<WebLink> Links { get; set; }
    }
}