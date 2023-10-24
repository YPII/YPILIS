using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test
{
    public class TestOrderAssemblyQualifiedTypeName
    {
        List<Type> m_TypeList;

        public TestOrderAssemblyQualifiedTypeName()
        {
            this.m_TypeList = new List<Type>();
			this.m_TypeList.Add(typeof(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder));
            this.m_TypeList.Add(typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology));            
            //this.m_TypeList.Add(typeof(YellowstonePathology.Business.Test.PanelOrderErPr));
            this.m_TypeList.Add(typeof(YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder));
            this.m_TypeList.Add(typeof(YellowstonePathology.Business.Test.HPV.HPVTestOrder));
        }
    }
}
