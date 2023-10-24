using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Audit.Model
{
    public class BoneMarrowSummaryAudit : Audit
    {
        private YellowstonePathology.Business.Surgical.KeyWordCollection m_KeyWordsOnly;
        private YellowstonePathology.Business.Surgical.KeyWordCollection m_KeyWordsAndOrder;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public BoneMarrowSummaryAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_KeyWordsOnly = new Surgical.KeyWordCollection { "bone marrow" };
            this.m_KeyWordsAndOrder = new Surgical.KeyWordCollection { "peripheral blood", "peripheral smear" };

        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            Test.BoneMarrowSummary.BoneMarrowSummaryTest boneMarrowSummaryTest = new Test.BoneMarrowSummary.BoneMarrowSummaryTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(boneMarrowSummaryTest.PanelSetId) == false)
            {
                foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
                {
                    if (this.IndicatorExists(specimenOrder.Description) == true || this.IndicatorAndOrderExists(specimenOrder.Description) == true)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        this.m_Message.Append(boneMarrowSummaryTest.PanelSetName);
                        break;
                    }
                }
            }
        }

        private bool IndicatorExists(string description)
        {
            bool result = false;

            if (this.m_KeyWordsOnly.WordsExistIn(description) == true)
            {
                result = true;
            }
            return result;
        }

        private bool IndicatorAndOrderExists(string description)
        {
            bool result = false;

            if (this.m_KeyWordsAndOrder.WordsExistIn(description) == true)
            {
                Test.LLP.LeukemiaLymphomaTest llpTest = new Test.LLP.LeukemiaLymphomaTest();
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(llpTest.PanelSetId) == true)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
