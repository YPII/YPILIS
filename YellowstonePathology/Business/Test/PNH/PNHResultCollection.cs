using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PNH
{
	public class PNHResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public PNHResultCollection()
		{
			this.Add(new PNHNegativeResult());
			this.Add(new PNHNegativeWithPreviousPositiveResult());
			this.Add(new PNHSmallPositiveResult());
			this.Add(new PNHSignificantPositiveResult());
			this.Add(new PNHPersistentPositiveResult());
			this.Add(new PNHGpiDeficientResult());
            this.Add(new PNHRareResult());
            this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}

		public static PNHResult GetResult(PNHTestOrder pnhTestOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			PNHResult result = new PNHResult();
			List<YellowstonePathology.Business.Test.AccessionOrder> accessionOrders = result.GetPreviousAccessions(accessionOrder.PatientId);

			result.SetTotals(pnhTestOrder);

			if (result.IsNegativeWithPreviousPositiveResult(accessionOrders, pnhTestOrder.MasterAccessionNo, pnhTestOrder.OrderDate.Value) == true)
			{
				result = new PNHNegativeWithPreviousPositiveResult();
			}
			else if (result.IsPersistentResult(accessionOrders, pnhTestOrder.MasterAccessionNo, pnhTestOrder.OrderDate.Value) == true)
			{
				result = new PNHPersistentPositiveResult();
			}
			else if (result.IsNegativeResult == true)
			{
				result = new PNHNegativeResult();
			}
            else if (result.IsRareResult == true)
            {
                result = new PNHRareResult();
            }
            else if (result.IsSmallPositiveResult == true)
			{
				result = new PNHSmallPositiveResult();
			}            
            else if (result.IsSignificantPositiveResult == true)
			{
				result = new PNHSignificantPositiveResult();
			}
			else if (result.IsGpiDeficientResult == true)
			{
				result = new PNHGpiDeficientResult();
			}            

            result.SetTotals(pnhTestOrder);
			return result;
		}
	}
}
