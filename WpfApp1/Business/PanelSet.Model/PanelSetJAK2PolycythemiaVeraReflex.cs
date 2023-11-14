using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetJAK2PolycythemiaVeraReflex : YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexTest
    {
        public PanelSetJAK2PolycythemiaVeraReflex()
        {
            this.m_PanelSetName = "JAK2 Polycythemia Vera Reflex";
            this.m_Abbreviation = "J2PV";
            this.m_Active = false;
        }
    }
}
