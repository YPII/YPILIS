using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupBreast : DisplayGroupIHC
    {
        public DisplayGroupBreast()
        {
            this.m_GroupName = "Breast";
            
            this.m_List.Add(new EstrogenReceptor());
            this.m_List.Add(new ProgesteroneReceptor());
            this.m_List.Add(new HER2DISH());
            this.m_List.Add(new SmoothMuscleMyosin());
            this.m_List.Add(new Mammaglobin());
            this.m_List.Add(new GCDFP15());                        
        }
    }
}
