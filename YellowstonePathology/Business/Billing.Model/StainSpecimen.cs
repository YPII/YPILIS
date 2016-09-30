using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class StainSpecimen
    {
		protected YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
        protected YellowstonePathology.Business.Test.Model.TestOrderCollection m_TestOrderCollection;        

        public StainSpecimen()
        {

        }

		public StainSpecimen(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection)
        {
            this.m_SpecimenOrder = specimenOrder;
            this.m_TestOrderCollection = testOrderCollection;            
        }

        public YellowstonePathology.Business.Test.Model.TestOrderCollection TestOrderCollection
        {
            get { return this.m_TestOrderCollection; }
        }

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
        {
            get { return this.m_SpecimenOrder; }
        }        

        public virtual void BillSplit()
        {

        }

		public virtual int GetBillableGradeStainCount(bool includeOrderedAsDual)
		{
			int result = this.TestOrderCollection.GetBillableGradeStainCount(includeOrderedAsDual);
			return result;
		}

        public virtual int GetBillableHANDECount()
        {
            int result = this.TestOrderCollection.GetBillableHANDECount();
            return result;
        }

        public virtual int GetBillableCytochemicalStainCount()
		{
			int result = this.TestOrderCollection.GetBillableCytochemicalStainCount();
			return result;
		}

		public virtual int GetBillableCytochemicalForMicroorganismsStainCount()
		{
			int result = this.TestOrderCollection.GetCytochemicalForMicroorganismsStainCount();
			return result;
		}

		public virtual int GetBillableIHCTestOrderCount()
		{
			int result = this.TestOrderCollection.GetChargeableIHCTestOrderCount();
			return result;
		}

		public virtual int GetOrderedAsDualCount()
		{
			int result = this.TestOrderCollection.GetOrderedAsDualCount();
			return result;
		}

		public virtual int GetBillableDualStainCount(bool includeDualsWithGradedStains)
		{
			int result = this.TestOrderCollection.GetBillableDualStainCount(includeDualsWithGradedStains);
			return result;
		}

		public virtual int GetBillableSinglePlexIHCTestOrderCount()
		{            
            YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection = this.TestOrderCollection.GetBillableSinglePlexIHCTestOrders();
            int result = testOrderCollection.GetUniqueTestCount();
			return result;
		}

		public virtual int GetTestOrderCount()
		{
			int result = this.TestOrderCollection.Count;
			return result;
		}
	}
}
