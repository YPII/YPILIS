using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain.Persistence
{
    public interface IPropertyWritable
    {
        void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter);
    }
}
