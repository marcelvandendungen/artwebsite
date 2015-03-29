using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IlseLeijten.Models
{
    public class ArtWorkViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string FileName { get; set; }
        public string Notes { get; set; }
    }
}
