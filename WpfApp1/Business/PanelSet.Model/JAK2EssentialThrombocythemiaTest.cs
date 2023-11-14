using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class JAK2EssentialThrombocythemiaTest : YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexTest
    {
        public JAK2EssentialThrombocythemiaTest()
        {
            this.m_PanelSetName = "JAK2 Essential Thrombocythemia";
            this.m_Abbreviation = "J2ET";
            this.m_Active = false;
        }
    }
}
