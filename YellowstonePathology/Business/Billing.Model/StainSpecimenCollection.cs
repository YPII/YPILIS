using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class StainSpecimenCollection : List<StainSpecimen>
    {
        public StainSpecimenCollection()
        {

        }

        public bool Requires3395F()
        {
            bool result = false;
            foreach (StainSpecimen stainSpecimen in this)
            {
                if (stainSpecimen.TestOrderCollection != null)
                {
                    if (stainSpecimen.TestOrderCollection.Exists(278) == true || stainSpecimen.TestOrderCollection.Exists(145) == true)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public bool HasLSEIHCSpecimen()
        {
            bool result = false;
            foreach (StainSpecimen stainSpecimen in this)
            {
                if (stainSpecimen is LSEIHCStainSpecimen)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public int GetBillable88360Count()
        {
            int result = 0;
            foreach (StainSpecimen stainSpecimen in this)
            {
                result = result + stainSpecimen.GetBillableGradeStainCount(true);
            }
            return result;
        }

        public int GetBillable88305Count()
        {
            int result = 0;
            foreach (StainSpecimen stainSpecimen in this)
            {
                result = result + stainSpecimen.GetBillableHANDECount();
            }
            return result;
        }

        public int GetBillable88313Count()
        {
            int result = 0;
            foreach (StainSpecimen stainSpecimen in this)
            {
                result = result + stainSpecimen.GetBillableCytochemicalStainCount();
            }
            return result;
        }

        public int GetBillable88312Count()
        {
            int result = 0;
            foreach (StainSpecimen stainSpecimen in this)
            {
                result = result + stainSpecimen.GetBillableCytochemicalForMicroorganismsStainCount();
            }
            return result;
        }

        public int GetBillable88342Count()
        {
            int result = 0;
            foreach (StainSpecimen stainSpecimen in this)
            {
                int perSpecimenCount = 0;
                perSpecimenCount = stainSpecimen.GetBillableSinglePlexIHCTestOrderCount();
                perSpecimenCount = perSpecimenCount + stainSpecimen.GetBillableDualStainCount(true);
                if (perSpecimenCount > 0)
                {
                    result += 1;
                }
            }
            return result;
        }

        public int GetBillable88341Count()
        {
            int result = 0;
            foreach (StainSpecimen stainSpecimen in this)
            {
                int perSpecimenCount = 0;
                perSpecimenCount = stainSpecimen.GetBillableSinglePlexIHCTestOrderCount();
                perSpecimenCount = perSpecimenCount + stainSpecimen.GetBillableDualStainCount(true);
                if (perSpecimenCount > 1)
                {
                    result += perSpecimenCount - 1;
                }
            }
            return result;
        }
       
        public int GetBillable88343Count()
        {
            int result = 0;
            foreach (StainSpecimen stainSpecimen in this)
            {                
                int ihcDualCount = stainSpecimen.GetOrderedAsDualCount();
                result = result + ihcDualCount / 2;
            }
            return result;
        }

        public int GetG0461Count()
        {
            int result = 0;
            foreach (StainSpecimen stainSpecimen in this)
            {
                int perSpecimenCount = 0;
                perSpecimenCount = stainSpecimen.GetBillableSinglePlexIHCTestOrderCount();
                perSpecimenCount = perSpecimenCount + stainSpecimen.GetBillableDualStainCount(true);
                if (perSpecimenCount > 0)
                {
                    result += 1;
                }
            }
            return result;
        }

        public int GetG0462Count()
        {
            int result = 0;
            foreach (StainSpecimen stainSpecimen in this)
            {
                int perSpecimenCount = 0;
                perSpecimenCount = stainSpecimen.GetBillableSinglePlexIHCTestOrderCount();
                perSpecimenCount = perSpecimenCount + stainSpecimen.GetBillableDualStainCount(true);
                if (perSpecimenCount > 1)
                {
                    result += perSpecimenCount - 1;
                }
            }
            return result;
        }        

		public int GetNumberOfSpecimenWithBillable88342()
		{
			int result = 0;
			foreach (StainSpecimen stainSpecimen in this)
			{
                int testCount = stainSpecimen.GetBillableIHCTestOrderCount();				
				if (testCount >= 1)
				{
					result += 1;
				}
			}
			return result;
		}

        public static StainSpecimenCollection GetCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            StainSpecimenCollection result = new StainSpecimenCollection();
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in accessionOrder.SpecimenOrderCollection)
            {
                YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection = panelSetOrder.GetTestOrderCollection(specimenOrder.AliquotOrderCollection);

                if (panelSetOrder.PanelSetId == 13)
                {
					YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical = (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)panelSetOrder;
					YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen = panelSetOrderSurgical.SurgicalSpecimenCollection.GetBySpecimenOrderId(specimenOrder.SpecimenOrderId);
                    if (surgicalSpecimen != null)
                    {
                        YellowstonePathology.Business.SpecialStain.StainResultItemCollection stainResultCollection = surgicalSpecimen.StainResultItemCollection;
                        SurgicalStainSpecimen surgicalStainSpecimen = new SurgicalStainSpecimen(specimenOrder, testOrderCollection, stainResultCollection);
                        result.Add(surgicalStainSpecimen);
                    }
                }
                else
                {
					if (panelSetOrder is YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation)
                    {
                        if (result.HasLSEIHCSpecimen() == false)
                        {
                            LSEIHCStainSpecimen lseIHCStainSpecimen = new LSEIHCStainSpecimen();
                            result.Add(lseIHCStainSpecimen);
                        }
                    }
                    else
                    {
                        StainSpecimen stainSpecimen = new StainSpecimen(specimenOrder, testOrderCollection);
                        result.Add(stainSpecimen);
                    }                    
                }
            }
            return result;
        }
	}
}
