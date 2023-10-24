using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PNH
{
	public class PNHSmallPositiveResult : PNHResult
	{
		public PNHSmallPositiveResult()
		{
			this.m_Result = "Minor PNH clone identified.";
			this.m_ResultCode = "PNHSMLLPSTV";
			this.m_Comment = "Flow cytometric analysis identified a minor PNH clonal population in at least two separate cell populations, comprised " +
				"of less than 1% of cells.  The clinical significance of a minor PNH clone is uncertain at this time.  Recommend follow-up testing in " +
				"3-6 months, as well as correlation with other laboratory and clinical information.";
		}
	}
}
