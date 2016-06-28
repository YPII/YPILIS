using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class PanMelanomaCocktail : ImmunoHistochemistryTest
	{
		public PanMelanomaCocktail()
		{
			this.m_TestId = 181;
			this.m_TestName = "Pan-melanoma cocktail";
            this.m_TestAbbreviation = "Pan-melanoma cocktail";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
