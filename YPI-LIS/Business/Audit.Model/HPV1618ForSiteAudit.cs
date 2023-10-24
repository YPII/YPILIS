using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class HPV1618ForSiteAudit : Audit
    {
        private Business.Surgical.KeyWordCollection m_SpecimenDescriptionKeyWords;
        private Business.Surgical.KeyWordCollection m_ExcludeWords;
        private Business.Surgical.KeyWordCollection m_DiagnosisKeyWords;
        private Business.Billing.Model.CptCodeCollection m_CptCodeCollection;
        private Business.Test.AccessionOrder m_AccessionOrder;

        public HPV1618ForSiteAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_SpecimenDescriptionKeyWords = new Surgical.KeyWordCollection { "head", "neck" };
            this.m_ExcludeWords = new Surgical.KeyWordCollection { "skin" };
            this.m_DiagnosisKeyWords = new Surgical.KeyWordCollection { "squamous cell carcinoma" };
            this.m_CptCodeCollection = new Billing.Model.CptCodeCollection { Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88304", null),
                Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88305", null),
                Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88307", null),
                Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88309", null),
                Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88173", null) };
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            Business.Test.HPV1618.HPV1618Test hpv1618Test = new Test.HPV1618.HPV1618Test();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(hpv1618Test.PanelSetId) == false)
            {
                Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
                {
                    Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollectionForThisSpecimen = surgicalTestOrder.PanelSetOrderCPTCodeCollection.GetSpecimenOrderCollection(surgicalSpecimen.SpecimenOrder.SpecimenOrderId);
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
