using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace YellowstonePathology.Business.Test.Her2AmplificationByIHC
{
	public class Her2AmplificationByIHCWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public Her2AmplificationByIHCWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			PanelSetOrderHer2AmplificationByIHC panelSetOrder = (PanelSetOrderHer2AmplificationByIHC)this.m_PanelSetOrder;

            //If the ish is equivocal and the ihc is not equivocal show the summary
            if(this.m_AccessionOrder.PanelSetOrderCollection.DoesPanelSetExist(46) == true)
            {
                HER2AmplificationByISH.HER2AmplificationByISHTestOrder ish = (HER2AmplificationByISH.HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(46);
                if (string.IsNullOrEmpty(ish.Result) == false && ish.Result.ToUpper().Contains("EQUIVOCAL") == true)
                {
                    if(panelSetOrder.Result.ToUpper().Contains("EQUIVOCAL") == false)
                    {
                        this.RenderSummary();
                        return;
                    }
                }
            }            

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\Her2AmplificationByIHC.2.xml";
			base.OpenTemplate();

			base.SetDemographicsV2();

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
			base.ReplaceText("specimen_description", specimenOrder.Description);

			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			base.ReplaceText("report_result", panelSetOrder.Result);
			base.ReplaceText("report_score", panelSetOrder.Score);
			base.ReplaceText("report_percent", panelSetOrder.IntenseCompleteMembraneStainingPercent);
			base.ReplaceText("report_fixative", panelSetOrder.BreastTestingFixative);
			base.ReplaceText("report_method", panelSetOrder.Method);
			base.ReplaceText("report_interpretation", panelSetOrder.Interpretation);
			base.ReplaceText("report_reference", panelSetOrder.Reference);
			base.ReplaceText("report_disclaimer", panelSetOrder.ReportDisclaimer);

			this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
			this.SetXmlNodeData("pathologist_signature", this.m_PanelSetOrder.Signature);

			this.SetReportDistribution();
			this.SetCaseHistory();

			this.SaveReport();
		}

        private void RenderSummary()
        {
            HER2AmplificationByISH.HER2AmplificationByISHTestOrder ish = (HER2AmplificationByISH.HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(46);
            PanelSetOrderHer2AmplificationByIHC ihc = (PanelSetOrderHer2AmplificationByIHC)this.m_PanelSetOrder;
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\HER2AmplificationSummaryShort.Breast.xml";
            base.OpenTemplate();

            base.SetDemographicsV2();

            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_PanelSetOrder.OrderedOnId);
            string blockDescription = string.Empty;
            if (aliquotOrder != null)
            {
                blockDescription = " - Block " + aliquotOrder.Label;
            }

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            SetXmlNodeData("specimen_type", specimenOrder.Description + blockDescription);
            SetXmlNodeData("specimen_fixation", specimenOrder.LabFixation);
            SetXmlNodeData("time_to_fixation", specimenOrder.TimeToFixationHourString);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            base.ReplaceText("test_result", ihc.Result);
            base.ReplaceText("ihc_score", ihc.Score + " (" + ihc.Result + ")");
            this.SetXmlNodeData("ish_result", ish.Result);

            if (ish.Her2Chr17Ratio.HasValue == true)
            {
                this.SetXmlNodeData("test_ratio", "HER2/Chr17 Ratio = " + ish.AverageHer2Chr17Signal);
            }
            else
            {
                this.DeleteRow("test_ratio");
            }

            if (ish.AverageHer2NeuSignal.HasValue == true)
            {
                this.SetXmlNodeData("copy_number", "Average HER2 Copy Number = " + ish.AverageHer2NeuSignal.Value.ToString());
            }
            else
            {
                this.DeleteRow("copy_number");
            }

            this.SetXmlNodeData("report_reference", ish.ReportReference + Environment.NewLine + Environment.NewLine + ihc.ReportReferences);
            SetXmlNodeData("duration_of_fixation", specimenOrder.FixationDurationString);

            if (string.IsNullOrEmpty(specimenOrder.FixationComment) == false)
            {
                this.SetXmlNodeData("fixation_comment", specimenOrder.FixationComment);
            }
            else
            {
                this.SetXmlNodeData("fixation_comment", string.Empty);
            }

            this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
            this.SetXmlNodeData("pathologist_signature", this.m_PanelSetOrder.Signature);

            if (string.IsNullOrEmpty(ish.ResultComment) == false)
            {
                this.SetXmlNodeData("result_comment", ish.ResultComment);
            }
            else
            {
                this.DeleteRow("result_comment");
            }

            this.SetXmlNodeData("cell_cnt", ish.CellsCounted.ToString());
            this.SetXmlNodeData("obs_cnt", ish.NumberOfObservers.ToString());

            if (ish.AverageHer2NeuSignal.HasValue == true)
            {
                this.SetXmlNodeData("avg_her", ish.AverageHer2NeuSignal.Value.ToString());
            }
            else
            {
                this.SetXmlNodeData("avg_her", "Unable to calculate");
            }

            this.SetXmlNodeData("avg_chr", ish.AverageChr17Signal);

            if (ish.Her2Chr17Ratio.HasValue == true)
            {
                this.SetXmlNodeData("tst_ratio", ish.Her2Chr17Ratio.Value.ToString());
            }
            else
            {
                this.SetXmlNodeData("tst_ratio", "Unable to calculate");
            }

            SetXmlNodeData("report_method", ish.Method + Environment.NewLine + Environment.NewLine + ihc.Method);
            SetXmlNodeData("asr_comment", ish.ASRComment);
            SetXmlNodeData("sample_adequacy", ish.SampleAdequacy);
            SetXmlNodeData("date_time_collected", collectionDateTimeString);            

            this.SetReportDistribution();
            this.SetCaseHistory();

            this.SaveReport();
        }

		public override void Publish()
		{
			base.Publish();
		}
	}
}
