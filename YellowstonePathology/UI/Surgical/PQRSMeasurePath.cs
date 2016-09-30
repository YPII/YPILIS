using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Surgical
{
    public class PQRSMeasurePath
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_PanelSetOrder;
        private PQRSMeasureDialog m_PQRSMeasureDialog;

		public PQRSMeasurePath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = panelSetOrder;
        }

        public void HandlePQRS()
        {
            YellowstonePathology.Business.Surgical.PQRSMeasureCollection pqrsCollection = YellowstonePathology.Business.Surgical.PQRSMeasureCollection.GetAll();
			foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in this.m_PanelSetOrder.SurgicalSpecimenCollection)
			{
				bool pqrsFound = false;
                foreach (YellowstonePathology.Business.Surgical.PQRSMeasure pqrsMeasure in pqrsCollection)
                {
					int patientAge = YellowstonePathology.Business.Helper.PatientHelper.GetAge(this.m_AccessionOrder.PBirthdate.Value);
                    if (pqrsMeasure.DoesMeasureApply(this.m_PanelSetOrder, surgicalSpecimen, patientAge) == true)
                    {
                        this.m_PQRSMeasureDialog = new PQRSMeasureDialog();
						PQRSMeasurePage pqrsMeasurePage = new PQRSMeasurePage(pqrsMeasure, surgicalSpecimen);
                        pqrsMeasurePage.Cancel += new PQRSMeasurePage.CancelEventHandler(PQRSMeasurePage_Cancel);
                        pqrsMeasurePage.AddPQRSCode += new PQRSMeasurePage.AddPQRSCodeEventHandler(PQRSMeasurePage_AddPQRSCode);
						pqrsMeasurePage.PQRSCodeNotApplicable += new PQRSMeasurePage.PQRSCodeNotApplicableEventHandler(PQRSMeasurePage_PQRSCodeNotApplicable);
						this.m_PQRSMeasureDialog.PageNavigator.Navigate(pqrsMeasurePage);
						this.m_PQRSMeasureDialog.ShowDialog();
						pqrsFound = true;
						break;
                    }
                }
				if (pqrsFound) break;
            }
        }

		private void PQRSMeasurePage_PQRSCodeNotApplicable(object sender, EventArgs e)
		{
			this.m_PQRSMeasureDialog.Close();
		}

        private void PQRSMeasurePage_AddPQRSCode(object sender, CustomEventArgs.AddPQRSReturnEventArgs e)
        {
            this.AddPQRSCode(e.PQRSCode, e.SurgicalSpecimen);
			this.m_PQRSMeasureDialog.Close();
        }

        private void PQRSMeasurePage_Cancel(object sender, EventArgs e)
        {
            this.m_PQRSMeasureDialog.Close();
        }

		protected void AddPQRSCode(YellowstonePathology.Business.Billing.Model.PQRSCode pqrsCode, YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen)
        {            
            YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
            panelSetOrderCPTCode.Quantity = 1;
            panelSetOrderCPTCode.CPTCode = pqrsCode.Code;
            panelSetOrderCPTCode.Modifier = pqrsCode.Modifier;
            panelSetOrderCPTCode.CodeableDescription = "PQRS Code";
            panelSetOrderCPTCode.CodeableType = "PQRS";
            panelSetOrderCPTCode.EntryType = YellowstonePathology.Business.Billing.Model.PanelSetOrderCPTCodeEntryType.ManualEntry;
            panelSetOrderCPTCode.SpecimenOrderId = surgicalSpecimen.SpecimenOrderId;
            panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
            this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
		}
    }
}
