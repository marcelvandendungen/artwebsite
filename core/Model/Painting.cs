using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Core.Model
{
    public class Painting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string FileName { get; set; }
        public string Notes { get; set; }
    }
}
