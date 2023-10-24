using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupLiverPanel
    {
        protected string m_GroupName;
        protected List<Test> m_List;

        public DisplayGroupLiverPanel()
        {
            this.m_GroupName = "Liver Panel";

            this.m_List = new List<Test>();
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("160")); // Trichrome());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("115")); // Iron());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("140")); // PASWithDiastase());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("151")); // Reticulin());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("213")); // CopperRhodanine());            
        }

        public string GroupName
        {
            get { return this.m_GroupName; }
        }

        public List<Test> List
        {
            get { return this.m_List; }
        }
    }
}
