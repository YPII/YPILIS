using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.AuthorizationForVerbalTestRequest
{
    public class AuthorizationForVerbalTestRequestWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public AuthorizationForVerbalTestRequestWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\AuthorizationForVerbalTestRequest.xml";
            base.OpenTemplate();

            PanelSetOrder panelSetOrder = this.m_PanelSetOrder;
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(363);
            AuthorizationForVerbalTestRequestTestOrder testOrder = (AuthorizationForVerbalTestRequestTestOrder)this.m_PanelSetOrder;

            base.ReplaceText("pso_name", panelSetOrder.PanelSetName);
            base.ReplaceText("order_date", panelSetOrder.OrderDate.Value.ToShortDateString());
            base.ReplaceText("pat_name", this.m_AccessionOrder.PatientDisplayName);
            base.ReplaceText("pso_report_no", panelSetOrder.ReportNo);
            if (string.IsNullOrEmpty(this.m_AccessionOrder.PAddress1) == false)
            {
                string address = this.m_AccessionOrder.PAddress1;
                if (string.IsNullOrEmpty(this.m_AccessionOrder.PAddress2) == false) address += ", " + this.m_AccessionOrder.PAddress2;
                base.ReplaceText("pat_address", address);
            }
            else
            {
                base.ReplaceText("pat_address", "_____________________________");
            }

            if (string.IsNullOrEmpty(this.m_AccessionOrder.PCity) == false) base.ReplaceText("pat_city", this.m_AccessionOrder.PCity);
            else base.ReplaceText("pat_city", "______________");

            if (string.IsNullOrEmpty(this.m_AccessionOrder.PState) == false) base.ReplaceText("pat_state", this.m_AccessionOrder.PState);
            else base.ReplaceText("pat_state", "______");

            if (string.IsNullOrEmpty(this.m_AccessionOrder.PZipCode) == false) base.ReplaceText("pat_zip", this.m_AccessionOrder.PZipCode);
            else base.ReplaceText("pat_zip", "________");

            if (string.IsNullOrEmpty(this.m_AccessionOrder.PSSN) == false) base.ReplaceText("pat_ssn", this.m_AccessionOrder.PSSN);
            else base.ReplaceText("pat_ssn", "________");

            if (string.IsNullOrEmpty(this.m_AccessionOrder.PSex) == false) base.ReplaceText("p_g", this.m_AccessionOrder.PSex);
            else base.ReplaceText("p_g", "(M) (F)");

            if (this.m_AccessionOrder.PBirthdate.HasValue == true) base.ReplaceText("pat_dob", this.m_AccessionOrder.PBirthdate.Value.ToShortDateString());
            else base.ReplaceText("pat_dob", "____________");

            if (string.IsNullOrEmpty(this.m_AccessionOrder.PrimaryInsurance) == false && this.m_AccessionOrder.PrimaryInsurance != "Not Selected")
                base.ReplaceText("primary_insurance", this.m_AccessionOrder.PrimaryInsurance);
            else base.ReplaceText("primary_insurance", "______________________");

            //if (string.IsNullOrEmpty(this.m_AccessionOrder.) == false) base.ReplaceText("prim_insu_address", this.m_AccessionOrder.);
            //else
             base.ReplaceText("prim_insu_address", "____________________________");

            //if (string.IsNullOrEmpty(this.m_AccessionOrder.) == false) base.ReplaceText("subscriber_name", this.m_AccessionOrder.);
            //else 
            base.ReplaceText("subscriber_name", "_____________________________");

            //if (string.IsNullOrEmpty(this.m_AccessionOrder.) == false) base.ReplaceText("subscriber_dob", this.m_AccessionOrder.);
            //else 
            base.ReplaceText("subscriber_dob", "_______________________________");

            if (string.IsNullOrEmpty(this.m_AccessionOrder.InsurancePlan1) == false) base.ReplaceText("ins_id", this.m_AccessionOrder.InsurancePlan1);
            else base.ReplaceText("ins_id", "____________________________________");

            //if (string.IsNullOrEmpty(this.m_AccessionOrder.I) == false) base.ReplaceText("ins_group", this.m_AccessionOrder.PSex);
            //else 
            base.ReplaceText("ins_group", "__________________________________");

            this.SaveReport();
        }
    }
}
