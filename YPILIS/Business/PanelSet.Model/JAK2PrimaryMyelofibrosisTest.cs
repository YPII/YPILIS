using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class JAK2PrimaryMyelofibrosisTest : YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexTest
    {
        public JAK2PrimaryMyelofibrosisTest()
        {
            this.m_PanelSetName = "JAK2 Primary Myelofibrosis";
            this.m_Abbreviation = "J2PM";
            this.m_Active = false;
        }
    }
}
