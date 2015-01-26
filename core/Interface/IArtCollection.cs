using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Model;

namespace Core.Interface
{
    public interface IArtCollection
    {
        IEnumerable<Painting> Paintings { get; }
    }
}
