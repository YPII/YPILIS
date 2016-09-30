using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupIHC
    {
        protected string m_GroupName;
        protected List<ImmunoHistochemistryTest> m_List;

        public DisplayGroupIHC()
        {
            this.m_List = new List<ImmunoHistochemistryTest>();
        }

        public string GroupName
        {
            get { return this.m_GroupName; }
        }

        public List<ImmunoHistochemistryTest> List
        {
            get { return this.m_List; }
        }
    }
}
