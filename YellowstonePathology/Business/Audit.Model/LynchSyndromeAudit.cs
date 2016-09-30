using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class LynchSyndromeAudit : Audit
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Surgical.KeyWordCollection m_ColorectalDescriptionKeyWords;
        private YellowstonePathology.Business.Surgical.KeyWordCollection m_ColorectalDiagnosisKeyWords;
        private YellowstonePathology.Business.Billing.Model.CptCodeCollection m_ColorectalCptCodeCollection;

        private YellowstonePathology.Business.Surgical.KeyWordCollection m_EndometrialDescriptionKeyWords;
        private YellowstonePathology.Business.Surgical.KeyWordCollection m_EndometrialDiagnosisKeyWords;
        private YellowstonePathology.Business.Billing.Model.CptCodeCollection m_EndometrialCptCodeCollection;

        private YellowstonePathology.Business.Surgical.KeyWordCollection m_UterineDescriptionKeyWords;
        private YellowstonePathology.Business.Surgical.KeyWordCollection m_UterineDiagnosisKeyWords;
        private YellowstonePathology.Business.Billing.Model.CptCodeCollection m_UterineCptCodeCollection;

        public LynchSyndromeAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ColorectalDescriptionKeyWords = new YellowstonePathology.Business.Surgical.KeyWordCollection { "colon", "cecum", "appendix", "rectum" };
            this.m_ColorectalDiagnosisKeyWords = new YellowstonePathology.Business.Surgical.KeyWordCollection { "carcinoma", "adenocarcinoma" };
            this.m_ColorectalCptCodeCollection = new Billing.Model.CptCodeCollection();
            this.m_ColorectalCptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88309());

            this.m_EndometrialDescriptionKeyWords = new YellowstonePathology.Business.Surgical.KeyWordCollection { "endometrium" };
            this.m_EndometrialDiagnosisKeyWords = new YellowstonePathology.Business.Surgical.KeyWordCollection { "endometrioid carcinoma", "endometrioid adenocarcinoma" };
            this.m_EndometrialCptCodeCollection = new Billing.Model.CptCodeCollection();
            this.m_EndometrialCptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88305());

            this.m_UterineDescriptionKeyWords = new YellowstonePathology.Business.Surgical.KeyWordCollection { "uterus" };
            this.m_UterineDiagnosisKeyWords = new YellowstonePathology.Business.Surgical.KeyWordCollection { "endometrioid carcinoma", "endometrioid adenocarcinoma" };
            this.m_UterineCptCodeCollection = new Billing.Model.CptCodeCollection();
            this.m_UterineCptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88309());
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest lynchSyndromeEvaluationTest = new Test.LynchSyndrome.LynchSyndromeEvaluationTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(lynchSyndromeEvaluationTest.PanelSetId) == false)
            {
                YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
                {
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollectionForThisSpecimen = surgicalTestOrder.PanelSetOrderCPTCodeCollection.GetSpecimenOrderCollection(surgicalSpecimen.SpecimenOrder.SpecimenOrderId);
                    if (this.ColorectalIndicatorExists(surgicalSpecimen.SpecimenOrder.Description, surgicalSpecimen.Diagnosis, panelSetOrderCPTCodeCollectionForThisSpecimen) == true)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        this.m_Message.Append(lynchSyndromeEvaluationTest.PanelSetName);
                        break;
                    }
                    else if (this.EndometrialIndicatorExists(surgicalSpecimen.SpecimenOrder.Description, surgicalSpecimen.Diagnosis, panelSetOrderCPTCodeCollectionForThisSpecimen) == true)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        this.m_Message.Append(lynchSyndromeEvaluationTest.PanelSetName);
                        break;
                    }
                    else if ( this.UterineIndicatorExists(surgicalSpecimen.SpecimenOrder.Description, surgicalSpecimen.Diagnosis, panelSetOrderCPTCodeCollectionForThisSpecimen) == true)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        this.m_Message.Append(lynchSyndromeEvaluationTest.PanelSetName);
                        break;
                    }
                }
            }
        }

        private bool ColorectalIndicatorExists(string description, string diagnosis, YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection)
        {
            bool result = false;

            if (this.m_ColorectalDescriptionKeyWords.WordsExistIn(description) == true)
            {
                if (this.m_ColorectalDiagnosisKeyWords.WordsExistIn(diagnosis) == true)
                {
                    if (panelSetOrderCPTCodeCollection.DoesCollectionHaveCodes(this.m_ColorectalCptCodeCollection) == true)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        private bool EndometrialIndicatorExists(string description, string diagnosis, YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection)
        {
            bool result = false;

            if (this.m_EndometrialDescriptionKeyWords.WordsExistIn(description) == true)
            {
                if (this.m_EndometrialDiagnosisKeyWords.WordsExistIn(diagnosis) == true)
                {
                    if (panelSetOrderCPTCodeCollection.DoesCollectionHaveCodes(this.m_EndometrialCptCodeCollection) == true)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        private bool UterineIndicatorExists(string description, string diagnosis, YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection)
        {
            bool result = false;

            if (this.m_UterineDescriptionKeyWords.WordsExistIn(description) == true)
            {
                if (this.m_UterineDiagnosisKeyWords.WordsExistIn(diagnosis) == true)
                {
                    if (panelSetOrderCPTCodeCollection.DoesCollectionHaveCodes(this.m_UterineCptCodeCollection) == true)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}
