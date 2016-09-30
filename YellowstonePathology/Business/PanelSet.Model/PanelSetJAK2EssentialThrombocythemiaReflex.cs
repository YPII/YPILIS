using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetJAK2EssentialThrombocythemiaReflex : YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexTest
    {
        public PanelSetJAK2EssentialThrombocythemiaReflex()
        {
            this.m_PanelSetName = "JAK2 Essential Thrombocythemia Reflex";
            this.m_Abbreviation = "J2ET";
        }
    }
}
