using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Audit.Model
{
    public class PDL1Audit : Audit
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Surgical.KeyWordCollection m_DescriptionKeyWords;
        private YellowstonePathology.Business.Surgical.KeyWordCollection m_DiagnosisKeyWords;
        private YellowstonePathology.Business.Billing.Model.CptCodeCollection m_CptCodeCollection;

        public PDL1Audit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_DescriptionKeyWords = new YellowstonePathology.Business.Surgical.KeyWordCollection { "lung" };
            this.m_DiagnosisKeyWords = new YellowstonePathology.Business.Surgical.KeyWordCollection { "carcinoma" };
            this.m_CptCodeCollection = new Billing.Model.CptCodeCollection();
            //this.m_CptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88309());
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            YellowstonePathology.Business.Test.PDL1.PDL1Test pdl1Test = new Test.PDL1.PDL1Test();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(pdl1Test.PanelSetId) == false)
            {
                YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
                {
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollectionForThisSpecimen = surgicalTestOrder.PanelSetOrderCPTCodeCollection.GetSpecimenOrderCollection(surgicalSpecimen.SpecimenOrder.SpecimenOrderId);
                    if (this.IndicatorExists(surgicalSpecimen.SpecimenOrder.Description, surgicalSpecimen.Diagnosis, panelSetOrderCPTCodeCollectionForThisSpecimen) == true)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        this.m_Message.Append(pdl1Test.PanelSetName);
                        break;
                    }
                }
            }
        }

        private bool IndicatorExists(string description, string diagnosis, YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection)
        {
            bool result = false;

            if (this.m_DescriptionKeyWords.WordsExistIn(description) == true)
            {
                if (this.m_DiagnosisKeyWords.WordsExistIn(diagnosis) == true)
                {
                    //if (panelSetOrderCPTCodeCollection.DoesCollectionHaveCodes(this.m_CptCodeCollection) == true)
                    //{
                        result = true;
                    //}
                }
            }
            return result;
        }
    }
}
