using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Interface
{
    public interface ISubmitable
    {
        List<object> GetInserted();
        List<object> GetDeleted();
        List<object> GetUpdated();
		List<object> GetChanged();
        void Reset();
    }
}
