using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LynchSyndromeIHCPanel : YellowstonePathology.Business.Panel.Model.Panel
    {
        public LynchSyndromeIHCPanel()
        {
            this.m_PanelId = 66;
            this.m_PanelName = "Immunohistochemistry";
            this.m_AcknowledgeOnOrder = false;

            Business.Test.Model.Test mlh1 = Business.Test.Model.TestCollectionInstance.GetClone("121");            
            this.m_TestCollection.Add(mlh1);

            Business.Test.Model.Test pms2 = Business.Test.Model.TestCollectionInstance.GetClone("217");
            this.m_TestCollection.Add(pms2);

            Business.Test.Model.Test msh2 = Business.Test.Model.TestCollectionInstance.GetClone("122");
            this.m_TestCollection.Add(msh2);

            Business.Test.Model.Test msh6 = Business.Test.Model.TestCollectionInstance.GetClone("218");
            this.m_TestCollection.Add(msh6);            
        }
    }
}
