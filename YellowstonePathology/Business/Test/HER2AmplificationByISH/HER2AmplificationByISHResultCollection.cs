using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
	public class HER2AmplificationByISHResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public HER2AmplificationByISHResultCollection()
		{
			this.Add(new HER2AmplificationByISHBreastResult());
			this.Add(new HER2AmplificationByISHGastricAdenocarcinomaResult());			
		}

		public HER2AmplificationByISHResult GetResultFromIndication(HER2AmplificationByISHTestOrder testOrder)
		{
			HER2AmplificationByISHResult result = null;			
			if (testOrder.Indicator == HER2AmplificationByISHIndicatorCollection.BreastIndication) result = new HER2AmplificationByISHBreastResult();
			else if (testOrder.Indicator == HER2AmplificationByISHIndicatorCollection.GastricIndication) result = new HER2AmplificationByISHGastricAdenocarcinomaResult();
			return result;
		}
	}
}
