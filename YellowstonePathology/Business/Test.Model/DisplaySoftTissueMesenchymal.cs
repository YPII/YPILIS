using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplaySoftTissueMesenchymal : DisplayGroupIHC
    {
        public DisplaySoftTissueMesenchymal()
        {
            this.m_GroupName = "Soft Tissue Mesenchymal";
            
            this.m_List.Add(new Vimentin());            
            this.m_List.Add(new SmoothMuscleActin());
            this.m_List.Add(new Desmin());
            this.m_List.Add(new FactorVIII());
            this.m_List.Add(new FactorXIIIa());
            this.m_List.Add(new CD117());
            this.m_List.Add(new DOG1());
            this.m_List.Add(new BetaCatenin());            
        }
    }
}
