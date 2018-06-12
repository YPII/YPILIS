﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupProliferationCellCycleMarkers : DisplayGroupIHC
    {
        public DisplayGroupProliferationCellCycleMarkers()
        {
            this.m_GroupName = "Proliferation Cell Cycle Markers";
            
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("349")); // Ki67());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("132")); // P16());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("134")); // P53());            
        }
    }
}
