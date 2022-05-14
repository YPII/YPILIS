using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetERPRForDCIS : YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTest
    {
        public PanelSetERPRForDCIS()
        {
            this.m_PanelSetName = "ER/PR for DCIS";            
        }

        public override void AddPanel()
        {
            Business.Test.ErPrSemiQuantitative.ERPRSemiQuantitativePanel erprSemiQuantitativePanel = new Business.Test.ErPrSemiQuantitative.ERPRSemiQuantitativePanel(false);
            this.m_PanelCollection.Add(erprSemiQuantitativePanel);
        }
    }
}
