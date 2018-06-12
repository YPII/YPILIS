﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupMiscellaneous : DisplayGroupIHC
    {
        public DisplayGroupMiscellaneous()
        {
            this.m_GroupName = "Miscellaneous";
            
            this.m_List.Add((Business.Test.Model.ImmunoHistochemistryTest)Business.Test.Model.TestCollection.Instance.GetTest("173"));
            this.m_List.Add((Business.Test.Model.ImmunoHistochemistryTest)Business.Test.Model.TestCollection.Instance.GetTest("224"));            
        }
    }
}
