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
            this.m_List.Add(new Trichrome());
            this.m_List.Add(new Iron());
            this.m_List.Add(new PASWithDiastase());
            this.m_List.Add(new Reticulin());
            this.m_List.Add(new CopperRhodanine());            
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
