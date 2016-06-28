using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupHematopoietic : DisplayGroupIHC
    {
        public DisplayGroupHematopoietic()
        {
            this.m_GroupName = "Hematopoietic";            

            this.m_List.Add(new CD3());
            this.m_List.Add(new CD4());
            this.m_List.Add(new CD5());            
            this.m_List.Add(new CD8());
            this.m_List.Add(new CD10());
            this.m_List.Add(new CD15());
            this.m_List.Add(new CD19());
            this.m_List.Add(new CD20());
            this.m_List.Add(new CD23());
            this.m_List.Add(new CD30());
            this.m_List.Add(new CD31());
            this.m_List.Add(new CD34());
            this.m_List.Add(new CD45());
            this.m_List.Add(new CD56());
            this.m_List.Add(new CD68());
            this.m_List.Add(new CD79a());
            this.m_List.Add(new CD99());
            this.m_List.Add(new CD117());
            this.m_List.Add(new CD138());
            this.m_List.Add(new Bcl2());
            this.m_List.Add(new Bcl6());
            this.m_List.Add(new CyclinD1());            
            this.m_List.Add(new IgKappa());            
            this.m_List.Add(new IgLambda());
            this.m_List.Add(new Myeloperoxidase());
            this.m_List.Add(new TdT());            
            this.m_List.Add(new PAX5());            
        }
    }
}
