using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupMelanoma : DisplayGroupIHC
    {
        public DisplayGroupMelanoma()
        {
            this.m_GroupName = "Melanoma";
            
            this.m_List.Add(new S100());
            this.m_List.Add(new MelanA());
            this.m_List.Add(new HMB45());
            this.m_List.Add(new MITF());            
        }
    }
}
