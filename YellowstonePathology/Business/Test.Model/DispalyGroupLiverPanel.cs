﻿using System;
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
            TestCollection allTests = TestCollection.GetAllTests(false);

            this.m_List = new List<Test>();
            this.m_List.Add(allTests.GetTest("160")); // Trichrome());
            this.m_List.Add(allTests.GetTest("115")); // Iron());
            this.m_List.Add(allTests.GetTest("140")); // PASWithDiastase());
            this.m_List.Add(allTests.GetTest("151")); // Reticulin());
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
