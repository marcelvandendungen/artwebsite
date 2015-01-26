using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Model
{
    public class SiteDefinition
    {
        public SiteDefinition()
        {
            this.FrontPageImage = string.Empty;
            this.AboutPage = new BioInfo {  Caption = string.Empty, Text = string.Empty, Picture = string.Empty };
            this.Links = new List<WebLink>();
        }

        public string FrontPageImage { get; set; }
        public BioInfo AboutPage { get; set; }
        public List<WebLink> Links { get; set; }
    }
}