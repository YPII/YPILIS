using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupNeural : DisplayGroupIHC
    {
        public DisplayGroupNeural()
        {
            this.m_GroupName = "Neural";
            
            this.m_List.Add(new CD56());
            this.m_List.Add(new S100());            
            this.m_List.Add(new Chromogranin());
            this.m_List.Add(new Synaptophysin());
            this.m_List.Add(new GFAP());            
        }
    }
}
