using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class SurgicalCaseHasQuestionMarksAudit : Audit
    {
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;

        public SurgicalCaseHasQuestionMarksAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_SurgicalTestOrder = surgicalTestOrder;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();
            string resultMsg = "There are ??? or no entry in ";
            this.m_Message.Append(resultMsg);
            if (string.IsNullOrEmpty(this.m_SurgicalTestOrder.GrossX) == true || this.m_SurgicalTestOrder.GrossX.Contains("???") == true) this.m_Message.Append("Gross Description, ");
            if (string.IsNullOrEmpty(this.m_SurgicalTestOrder.MicroscopicX) == true || this.m_SurgicalTestOrder.MicroscopicX.Contains("???") == true) this.m_Message.Append("Microscopic Description, ");
            if (!string.IsNullOrEmpty(this.m_SurgicalTestOrder.Comment) && this.m_SurgicalTestOrder.Comment.Contains("???") == true) this.m_Message.Append("Comment, ");
            if (!string.IsNullOrEmpty(this.m_SurgicalTestOrder.CancerSummary) && this.m_SurgicalTestOrder.CancerSummary.Contains("???") == true) this.m_Message.Append("Cancer Summary, ");
            if (string.IsNullOrEmpty(this.m_AccessionOrder.ClinicalHistory) == true || this.m_AccessionOrder.ClinicalHistory.Contains("???") == true) this.m_Message.Append("Clinical History, ");

            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen specimen in this.m_SurgicalTestOrder.SurgicalSpecimenCollection)
            {
                if (string.IsNullOrEmpty(specimen.Diagnosis) == true || specimen.Diagnosis.Contains("???") == true) this.m_Message.Append("Diagnosis for " + specimen.DiagnosisId + ", ");
                if (string.IsNullOrEmpty(specimen.SpecimenOrder.Description) == true || specimen.SpecimenOrder.Description.Contains("???") == true) this.m_Message.Append("Description for " + specimen.DiagnosisId + ", ");

                foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResultItem in specimen.StainResultItemCollection)
                {
                    if(stainResultItem.ClientAccessioned == false)
                    {
                        if (string.IsNullOrEmpty(stainResultItem.ControlComment) == false && stainResultItem.ControlComment.Contains("???") == true) this.m_Message.Append(stainResultItem.ProcedureName + " control comment, ");
                        if (string.IsNullOrEmpty(stainResultItem.ReportComment) == false && stainResultItem.ReportComment.Contains("???") == true) this.m_Message.Append(stainResultItem.ProcedureName + " report comment, ");
                        if (string.IsNullOrEmpty(stainResultItem.Result) == true || stainResultItem.Result.Contains("???") == true) this.m_Message.Append(stainResultItem.ProcedureName + " result, ");
                    }
                }
                foreach (YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult intraoperativeConsultation in specimen.IntraoperativeConsultationResultCollection)
                {
                    if (string.IsNullOrEmpty(intraoperativeConsultation.Result) == true || intraoperativeConsultation.Result.Contains("???") == true) this.m_Message.Append("intraoperative consultation result, ");
                }

                YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_SurgicalTestOrder.ReportNo);
                foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
                {
                    if (string.IsNullOrEmpty(amendment.Text) == true || amendment.Text.Contains("???") == true) this.m_Message.Append("amendment, ");
                }
            }

            if (this.m_Message.Length > resultMsg.Length)
            {
                this.m_Status = AuditStatusEnum.Failure;
                this.m_Message.Remove(this.m_Message.Length - 2, 2);
            }
            else
            {
                this.m_Message.Clear();
            }
        }
    }
}
