using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class PNHOnBoneMarrowSpecimenAudit : Audit
    {
        private YellowstonePathology.Business.Surgical.KeyWordCollection m_KeyWords;
        private YellowstonePathology.Business.Billing.Model.CptCodeCollection m_CptCodeCollection;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public PNHOnBoneMarrowSpecimenAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_KeyWords = new Surgical.KeyWordCollection { "anemia", "pancytopenia" };
            this.m_CptCodeCollection = new Billing.Model.CptCodeCollection{ new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT85060(),
                new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT85097() };
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            YellowstonePathology.Business.Test.PNH.PNHTest pnhTest = new Test.PNH.PNHTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(pnhTest.PanelSetId) == false)
            {
                YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                if (this.AllCPTCodesArePresent(surgicalTestOrder.PanelSetOrderCPTCodeCollection) == true)
                {
                    foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
                    {
                        if (this.IndicatorExists(surgicalSpecimen.Diagnosis) == true)
                        {
                            this.m_Status = AuditStatusEnum.Failure;
                            this.m_Message.Append(pnhTest.PanelSetName);
                            break;
                        }
                    }
                }
            }
        }

        private bool AllCPTCodesArePresent(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection)
        {
            bool result = true;
            foreach(YellowstonePathology.Business.Billing.Model.CptCode cptCode in this.m_CptCodeCollection)
            {
                if(panelSetOrderCPTCodeCollection.GetPanelSetOrderCPTCodeByCPTCode(cptCode.Code) == null)
                {
                    result = false;
                    break;
                }
            }
            
            return result;
        }

        private bool IndicatorExists(string diagnosis)
        {
            bool result = false;

            if (this.m_KeyWords.WordsExistIn(diagnosis) == true)
            {
                result = true;
            }
            return result;
        }
    }
}
