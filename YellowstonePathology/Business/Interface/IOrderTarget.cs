using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Interface
{
    public interface IOrderTarget
    {
        string GetId();
        string GetOrderedOnType();
        string GetDescription();
        IOrderTargetType GetTargetType();
    }
}
