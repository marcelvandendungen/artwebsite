using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Interface
{
    public interface IArtRepository
    {
        ArtCollection Read();
        void Save(ArtCollection artCollection);
    }
}
