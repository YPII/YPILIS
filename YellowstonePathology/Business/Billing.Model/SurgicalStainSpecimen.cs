using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class SurgicalStainSpecimen : StainSpecimen
    {
        private YellowstonePathology.Business.SpecialStain.StainResultItemCollection m_StainResultCollection;

        public SurgicalStainSpecimen(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, 
            YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection,
            YellowstonePathology.Business.SpecialStain.StainResultItemCollection stainResultCollection) 
            : base(specimenOrder, testOrderCollection)
        {
            this.m_StainResultCollection = stainResultCollection;
            this.UpdateNoChargeStatus();
        }

        public override int GetBillableIHCTestOrderCount()
        {
            int result = 0;
            YellowstonePathology.Business.Test.Model.TestCollection ihcTestCollection = YellowstonePathology.Business.Test.Model.TestCollection.GetIHCTests();

            foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in this.m_TestOrderCollection)
            {
                if (testOrder.NoCharge == false)
                {
                    if (ihcTestCollection.Exists(testOrder.TestId) == true)
                    {
                        if (this.m_StainResultCollection.TestOrderExists(testOrder.TestOrderId) == true)
                        {
                            YellowstonePathology.Business.SpecialStain.StainResultItem stainResult = this.m_StainResultCollection.GetStainResult(testOrder.TestOrderId);
                            if (stainResult.IsGraded == false)
                            {
                                result += 1;
                            }
                        }                        
                    }
                }
            }
            return result;
        }

        public override int GetBillableGradeStainCount(bool includeOrderedAsDual)
        {
            int result = 0;
            YellowstonePathology.Business.Test.Model.TestCollection gradedTestCollection = YellowstonePathology.Business.Test.Model.TestCollection.GetGradedTests();

            foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in this.m_TestOrderCollection)
            {
                if (testOrder.NoCharge == false)
                {
                    if (gradedTestCollection.Exists(testOrder.TestId) == true)
                    {
                        if (includeOrderedAsDual == true)
                        {
                            result += 1;
                        }
                        else if (includeOrderedAsDual == false)
                        {
                            if (testOrder.OrderedAsDual == false)
                            {
                                result += 1;
                            }
                        }
                    }
                    else if (this.m_StainResultCollection.TestOrderExists(testOrder.TestOrderId) == true)
                    {
                        YellowstonePathology.Business.SpecialStain.StainResultItem stainResult = this.m_StainResultCollection.GetStainResult(testOrder.TestOrderId);
                        if (stainResult.IsGraded == true)
                        {
                            if (includeOrderedAsDual == true)
                            {
                                result += 1;
                            }
                            else if (includeOrderedAsDual == false)
                            {
                                if (testOrder.OrderedAsDual == false)
                                {
                                    result += 1;
                                }
                            }
                        }
                    }  
                }
            }

            return result;
        }

        private void UpdateNoChargeStatus()
        {
            foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResult in this.m_StainResultCollection)
            {
                YellowstonePathology.Business.Test.Model.TestOrder testOrder = this.m_TestOrderCollection.Get(stainResult.TestOrderId);
                testOrder.NoCharge = stainResult.NoCharge;
            }
        }

        public override int GetBillableSinglePlexIHCTestOrderCount()
        {
            YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection = this.TestOrderCollection.GetBillableSinglePlexIHCTestOrders();
            this.m_StainResultCollection.RemoveGradedStains(testOrderCollection);
            int uniqueTestCount = testOrderCollection.GetUniqueTestCount();
            return uniqueTestCount;            
        }
    }
}
