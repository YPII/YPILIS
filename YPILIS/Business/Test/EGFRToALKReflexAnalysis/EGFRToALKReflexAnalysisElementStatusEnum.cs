using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis
{
    public enum EGFRToALKReflexAnalysisElementStatusEnum
    {       
        Pending,
        NotIndicated,
        QNS,
        NotOrdered,
        OrderRequired,        
        Ordered,      
        Accepted,  
        Final,
        NotGoingToPerform
    }
}
