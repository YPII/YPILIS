using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace YellowstonePathology.Business.Test.PNH
{
	public class PNHWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public PNHWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }
        public override void Render()
		{            
			PNHTestOrder testOrder = (PNHTestOrder)this.m_PanelSetOrder;

			if (string.IsNullOrEmpty(testOrder.ResultCode) == true)
			{
				System.Windows.MessageBox.Show("Unable to create report. Template is not selected");
				return;
			}

            PNHNegativeResult pnhNegativeResult = new PNHNegativeResult();
			if (testOrder.ResultCode == pnhNegativeResult.ResultCode)
			{
				this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\PNHAnalysisNegative.8.xml";
			}
			else
			{
				this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\PNHAnalysisPositive.8.xml";
			}


			this.OpenTemplate();

			//setting date of service to date only
			if (this.m_AccessionOrder.CollectionDate.HasValue)
			{
				this.ReplaceText("collection_date", this.m_AccessionOrder.CollectionDate.Value.ToShortDateString());
			}

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			if (this.m_PanelSetOrder.Final)
			{
				this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);
			}

			this.SetXmlNodeData("report_result", testOrder.Result);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            this.SetXmlNodeData("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);						

			//Handle result type            
			if (testOrder.ResultCode != pnhNegativeResult.ResultCode)
			{
				this.HandleResultMonitor(testOrder.ResultCode);
			}

            this.ReplaceText("report_result", testOrder.Result);
            this.ReplaceText("report_comment", testOrder.Comment);
			this.ReplaceText("report_method", testOrder.Method);
			this.ReplaceText("report_references", testOrder.References);
			this.ReplaceText("asr_comment", testOrder.ASRComment);


			YellowstonePathology.Business.Document.AmendmentSection amendment = new YellowstonePathology.Business.Document.AmendmentSection();
			amendment.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			this.SaveReport();
		}

        public override void Publish()
        {
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			YellowstonePathology.Business.Document.CaseDocument.SaveXMLAsPDF(orderIdParser);
            YellowstonePathology.Business.Helper.FileConversionHelper.SaveXpsReportToTiff(this.m_PanelSetOrder.ReportNo);
        }

		private void HandleResultMonitor(string resultCode)
		{
			if (resultCode == PNHPersistentPositiveResult.PNHPersistentPositiveResultResultCode || resultCode == PNHNegativeWithPreviousPositiveResult.PNHNegativeWithPreviousPositiveResultResultCode)
			{
				this.ReplaceText("delete_row1", string.Empty);
				this.ReplaceText("delete_row2", string.Empty);
				this.ReplaceText("delete_row3", string.Empty);
				this.ReplaceText("delete_row4", string.Empty);
				this.ReplaceText("delete_row5", string.Empty);
				this.ReplaceText("delete_row6", string.Empty);
				this.SetPositiveResults(true);
			}
			else
			{
				this.DeleteRow("delete_row1");
				this.DeleteRow("delete_row2");
				this.DeleteRow("delete_row3");
				this.DeleteRow("delete_row4");
				this.DeleteRow("delete_row5");
				this.DeleteRow("delete_row6");
				this.SetPositiveResults(false);
			}
		}

		private void SetPositiveResults(bool needsPreviousValues)
		{
			PNHResult pnhResult = new PNHResult();
			pnhResult.SetTotals((PNHTestOrder)this.m_PanelSetOrder);
			this.ReplaceText("rbc2_rbc3", pnhResult.RedBloodTotal.ToString("F") + "%");
			this.ReplaceText("gran_t2_t3", pnhResult.GranulocytesTotal.ToString("F") + "%");
			this.ReplaceText("mono_t2_t3", pnhResult.MonocytesTotal.ToString("F") + "%");

			this.ReplaceText("rbc_3", "Type III (complete CD59 deficiency) = " + pnhResult.RedBloodCellsTypeIIIResult.ToString("F") + "%");
			this.ReplaceText("rbc_2", "Type II (partial CD59 deficiency) = " + pnhResult.RedBloodCellsTypeIIResult.ToString("F") + "%");

			this.ReplaceText("gran_3", "TypeIII (complete FLAER/CD24 deficiency) = " + pnhResult.GranulocytesTypeIIIResult.ToString("F") + "%");
			if (pnhResult.GranulocytesTypeIIResult > 0.0m && pnhResult.GranulocytesTypeIIIResult > 0.0m)
			{
				this.ReplaceText("gran_2", "TypeII (partial FLAER/CD24 deficiency) = " + pnhResult.GranulocytesTypeIIResult.ToString("F") + "%");
			}
			else
			{
				this.DeleteRow("gran_2");
			}

			this.ReplaceText("mono_3", "TypeIII (complete FLAER/CD14 deficiency) = " + pnhResult.MonocytesTypeIIIResult.ToString("F") + "%");
			if (pnhResult.MonocytesTypeIIResult > 0.0m)
			{
				this.ReplaceText("mono_2", "TypeII (partial FLAER/CD14 deficiency) = " + pnhResult.MonocytesTypeIIResult.ToString("F") + "%");
			}
			else
			{
				this.DeleteRow("mono_2");
			}

			if (needsPreviousValues)
			{
				string dateString = string.Empty;
				if(this.m_PanelSetOrder.FinalDate.HasValue)
				{
					dateString = m_PanelSetOrder.FinalDate.Value.ToShortDateString();
				}
				this.ReplaceText("current_date", dateString);
				this.ReplaceText("rbc1_rbc2_c", pnhResult.RedBloodTotal.ToString("F") + "%");
				this.ReplaceText("g2_g3_c", pnhResult.GranulocytesTotal.ToString("F") + "%");
				this.ReplaceText("m2_m3_c", pnhResult.MonocytesTotal.ToString("F") + "%");
				this.SetPreviousResults();
			}
		}

		private void SetPreviousResults()
		{
			PNHResult pnhResult = new PNHResult();
			List<YellowstonePathology.Business.Test.AccessionOrder> accessionOrders = pnhResult.GetPreviousAccessions(this.m_AccessionOrder.PatientId);
			List<PNHTestOrder> pnhTestOrders = pnhResult.GetPreviousPanelSetOrders(accessionOrders, this.m_PanelSetOrder.MasterAccessionNo, this.m_PanelSetOrder.OrderDate.Value);
			string dateString = string.Empty;
			string rbcString = string.Empty;
			string monoString = string.Empty;
			string granString = string.Empty;
			if (pnhTestOrders.Count > 0)
			{
				pnhResult.SetTotals(pnhTestOrders[0]);
				dateString = pnhTestOrders[0].FinalDate.Value.ToShortDateString();
				rbcString = pnhResult.RedBloodTotal.ToString("F") + "%";
				granString = pnhResult.GranulocytesTotal.ToString("F") + "%";
				monoString = pnhResult.MonocytesTotal.ToString("F") + "%";
			}
			this.ReplaceText("p_date_1", dateString);
			this.ReplaceText("rbc1_rbc2_p1", rbcString);
			this.ReplaceText("g2_g3_p1", granString);
			this.ReplaceText("m2_m3_p1", monoString);

			dateString = string.Empty;
			rbcString = string.Empty;
			monoString = string.Empty;
			granString = string.Empty;
			if (pnhTestOrders.Count > 1)
			{
				pnhResult.SetTotals(pnhTestOrders[1]);
				dateString = pnhTestOrders[1].FinalDate.Value.ToShortDateString();
				rbcString = pnhResult.RedBloodTotal.ToString("F") + "%";
				granString = pnhResult.GranulocytesTotal.ToString("F") + "%";
				monoString = pnhResult.MonocytesTotal.ToString("F") + "%";
			}
			this.ReplaceText("p_date_2", dateString);
			this.ReplaceText("rbc1_rbc2_p2", rbcString);
			this.ReplaceText("g2_g3_p2", granString);
			this.ReplaceText("m2_m3_p2", monoString);

			dateString = string.Empty;
			rbcString = string.Empty;
			monoString = string.Empty;
			granString = string.Empty;
			if (pnhTestOrders.Count > 2)
			{
				pnhResult.SetTotals(pnhTestOrders[2]);
				dateString = pnhTestOrders[2].FinalDate.Value.ToShortDateString();
				rbcString = pnhResult.RedBloodTotal.ToString("F") + "%";
				granString = pnhResult.GranulocytesTotal.ToString("F") + "%";
				monoString = pnhResult.MonocytesTotal.ToString("F") + "%";
			}
			this.ReplaceText("p_date_3", dateString);
			this.ReplaceText("rbc1_rbc2_p3", rbcString);
			this.ReplaceText("g2_g3_p3", granString);
			this.ReplaceText("m2_m3_p3", monoString);
		}
	}
}
