using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupProstate
    {
        protected string m_GroupName;
        protected List<Test> m_List;

        public DisplayGroupProstate()
        {
            this.m_GroupName = "Prostate";

            this.m_List = new List<Test>();
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("355")); // NKX31());                        
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("133")); // P504sRacemase());
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
