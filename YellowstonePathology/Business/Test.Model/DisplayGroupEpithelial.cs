using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupEpithelial : DisplayGroupIHC
    {
        public DisplayGroupEpithelial()
        {
            this.m_GroupName = "Epithelial";            

            this.m_List.Add(new Pancytokeratin());
            this.m_List.Add(new OSCAR());
            this.m_List.Add(new Cytokeratin56());
            this.m_List.Add(new Cytokeratin7());
            this.m_List.Add(new Cytokeratin17());
            this.m_List.Add(new Cytokeratin20());
            this.m_List.Add(new Cytokeratin34());
            this.m_List.Add(new EMA());
            this.m_List.Add(new Ecadherin());
            this.m_List.Add(new P63());
            this.m_List.Add(new MOC31());
        }
    }
}
