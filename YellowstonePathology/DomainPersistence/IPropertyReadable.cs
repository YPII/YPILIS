using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain.Persistence
{
    public interface IPropertyReadable
    {
        void ReadProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyReader propertyReader);
    }
}
