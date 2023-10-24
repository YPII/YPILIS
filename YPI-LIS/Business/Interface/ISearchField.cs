using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Interface
{
    public interface ISearchField
    {
        string SqlFieldName { get; }
        string Condition { get; }
    }
}
