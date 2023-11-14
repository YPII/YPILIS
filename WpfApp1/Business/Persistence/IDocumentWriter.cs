using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public interface IDocumentWriter
    {
        bool ReadOnly { get; }
    }
}
