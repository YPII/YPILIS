using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain.Cytology
{
	public class HpvOrderInstructionsConverter
	{
		public static string GetHpvOrderInstructions(YellowstonePathology.Business.ClientOrder.Model.CytologyClientOrder cytologyClientOrder)
		{
			string result = "Not Specified";

			if (cytologyClientOrder.ReflexHPV == true)
			{
				result = "Reflex If ASCUS";
			}
			if (cytologyClientOrder.RoutineHPVTesting == true)
			{
				result = "Routine";
			}

			return result;
		}
	}
}
