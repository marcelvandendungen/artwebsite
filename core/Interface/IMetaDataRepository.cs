using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Interface
{
    public interface IMetaDataRepository
    {
        SiteMetaData Read();
        void Save(SiteMetaData metaData);
    }
}
