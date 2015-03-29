using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IlseLeijten.Models
{
    public class PaintingViewModel
    {
        public string ImageRoot { get; set; }
        public string ThumbnailRoot { get; set; }
        public IEnumerable<Painting> Paintings { get; set; }
    }
}
