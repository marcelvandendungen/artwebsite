using Core.Model;
using System.Collections.Generic;

namespace Core.Interface
{
    public interface IPaintingRepository
    {
        void Create(Painting painting);
        IEnumerable<Painting> Read();
        void Update(Painting painting);
        void Delete(int id);
        void PromotePainting(int id);
        void DemotePainting(int id);
    }
}
