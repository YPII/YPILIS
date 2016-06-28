using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ErPrSemiQuantitative
{
	public class ErPrSemiQuantitativeWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public ErPrSemiQuantitativeWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{            
			ErPrSemiQuantitativeTestOrder panelSetOrderErPrSemiQuantitative = (ErPrSemiQuantitativeTestOrder)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\ERPRSemiQuantitative.6.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			this.ReplaceText("er_result", panelSetOrderErPrSemiQuantitative.ErResult);
			this.ReplaceText("er_intensity", panelSetOrderErPrSemiQuantitative.ErIntensity);
			this.ReplaceText("er_percent_of_cells", panelSetOrderErPrSemiQuantitative.ErPercentageOfCells);
			this.ReplaceText("pr_result", panelSetOrderErPrSemiQuantitative.PrResult);
			this.ReplaceText("pr_intensity", panelSetOrderErPrSemiQuantitative.PrIntensity);
			this.ReplaceText("pr_percent_of_cells", panelSetOrderErPrSemiQuantitative.PrPercentageOfCells);
			this.ReplaceText("report_references", panelSetOrderErPrSemiQuantitative.ReportReferences);
			this.ReplaceText("report_method", panelSetOrderErPrSemiQuantitative.Method);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(panelSetOrderErPrSemiQuantitative.OrderedOnId);
            if(specimenOrder == null) specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection[0];
            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);            

            this.ReplaceText("specimen_description", specimenOrder.Description);
			this.ReplaceText("specimen_fixation_type", specimenOrder.LabFixation);
			this.ReplaceText("time_to_fixation", specimenOrder.TimeToFixationHourString);

            this.ReplaceText("duration_of_fixation", specimenOrder.FixationDurationString);
			this.ReplaceText("specimen_adequacy", panelSetOrderErPrSemiQuantitative.SpecimenAdequacy);
            this.ReplaceText("date_time_collected", collectionDateTimeString);

			this.ReplaceText("report_interpretation", panelSetOrderErPrSemiQuantitative.Interpretation);

			if (string.IsNullOrEmpty(panelSetOrderErPrSemiQuantitative.ResultComment) == true)
			{
				this.DeleteRow("result_comment");
			}
			else
			{
				this.ReplaceText("result_comment", panelSetOrderErPrSemiQuantitative.ResultComment);
			}

			this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));			
			this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);

			this.SaveReport();
		}

        public override void Publish()
        {
            base.Publish();
        }
	}
}
