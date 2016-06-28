using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class HPV1618ForSiteAudit : Audit
    {
        private YellowstonePathology.Business.Surgical.KeyWordCollection m_SpecimenDescriptionKeyWords;
        private YellowstonePathology.Business.Surgical.KeyWordCollection m_ExcludeWords;
        private YellowstonePathology.Business.Surgical.KeyWordCollection m_DiagnosisKeyWords;
        private YellowstonePathology.Business.Billing.Model.CptCodeCollection m_CptCodeCollection;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public HPV1618ForSiteAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_SpecimenDescriptionKeyWords = new Surgical.KeyWordCollection { "head", "neck" };
            this.m_ExcludeWords = new Surgical.KeyWordCollection { "skin" };
            this.m_DiagnosisKeyWords = new Surgical.KeyWordCollection { "squamous cell carcinoma" };
            this.m_CptCodeCollection = new Billing.Model.CptCodeCollection { new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88304(),
                new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88305(),
                new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88307(),
                new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88309(),
                new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88173() };
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            YellowstonePathology.Business.Test.HPV1618.HPV1618Test hpv1618Test = new Test.HPV1618.HPV1618Test();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(hpv1618Test.PanelSetId) == false)
            {
                YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
                {
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollectionForThisSpecimen = surgicalTestOrder.PanelSetOrderCPTCodeCollection.GetSpecimenOrderCollection(surgicalSpecimen.SpecimenOrder.SpecimenOrderId);
                    if (this.HPVIndicatorExists(surgicalSpecimen.SpecimenOrder.Description, surgicalSpecimen.Diagnosis, panelSetOrderCPTCodeCollectionForThisSpecimen) == true)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        this.m_Message.Append(hpv1618Test.PanelSetName);
                        break;
                    }
                }
            }
        }

        private bool HPVIndicatorExists(string description, string diagnosis, YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection)
        {
            bool result = false;

            if (this.m_SpecimenDescriptionKeyWords.WordsExistIn(description) == true)
            {
                if(this.m_ExcludeWords.WordsExistIn(description) == false)
                {
                    if (this.m_DiagnosisKeyWords.WordsExistIn(diagnosis) == true)
                    {                    
                        if (panelSetOrderCPTCodeCollection.DoesCollectionHaveCodes(this.m_CptCodeCollection) == true)
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }
    }
}
