using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class WrightsStain : Test
	{
		public WrightsStain()
		{
			this.m_TestId = "205";
			this.m_TestName = "Wrights Stain";
            this.m_TestAbbreviation = "Wrights Stain";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = false;
			this.m_IsBillable = true;
		}

		public override YellowstonePathology.Business.Billing.Model.CptCode GetCptCode(bool isTechnicalOnly)
		{
			YellowstonePathology.Business.Billing.Model.CptCode result = null;
			result = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88312", null);
			return result;
		}
	}
}
