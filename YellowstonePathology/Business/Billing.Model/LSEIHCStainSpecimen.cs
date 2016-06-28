using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class LSEIHCStainSpecimen : StainSpecimen
    {                                

        public override void BillSplit()
        {

        }

		public override int GetBillableGradeStainCount(bool includeOrderedAsDual)
		{
            return 0;
		}

		public override int GetBillableCytochemicalStainCount()
		{
            return 0;
		}

		public override int GetBillableCytochemicalForMicroorganismsStainCount()
		{
            return 0;
		}

        public override int GetBillableIHCTestOrderCount()
		{
            return 4;
		}

        public override int GetOrderedAsDualCount()
		{
            return 0;
		}

        public override int GetBillableDualStainCount(bool includeDualsWithGradedStains)
		{
            return 0;
		}

        public override int GetBillableSinglePlexIHCTestOrderCount()
		{
            return 4;
		}

        public override int GetTestOrderCount()
		{
            return 4;
		}
	}
}
