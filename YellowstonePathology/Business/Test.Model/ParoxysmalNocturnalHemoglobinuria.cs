using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class ParoxysmalNocturnalHemoglobinuria : Test
	{
        public ParoxysmalNocturnalHemoglobinuria()
		{
			this.m_TestId = 198;
            this.m_TestName = "Paroxysmal Nocturnal Hemoglobinuria";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = false;

			this.m_ResultItemCollection.Add(new YellowstonePathology.Test.Model.ResultItem(27, "Negative (No evidence of paroxysmal nocturnal hemoglobinuria)"));
			this.m_ResultItemCollection.Add(new YellowstonePathology.Test.Model.ResultItem(28, "Positive (Small PNH clone identified)"));
			this.m_ResultItemCollection.Add(new YellowstonePathology.Test.Model.ResultItem(29, "Positive (Significant PNH clone identified)"));
			this.m_ResultItemCollection.Add(new YellowstonePathology.Test.Model.ResultItem(50, "GPI deficient cells identified"));
			this.m_ResultItemCollection.Add(new YellowstonePathology.Test.Model.ResultItem(51, "Persistent PNH clone identified"));
			this.m_ResultItemCollection.Add(new YellowstonePathology.Test.Model.ResultItem(56, "No PNH clone identified"));
		}
	}
}
