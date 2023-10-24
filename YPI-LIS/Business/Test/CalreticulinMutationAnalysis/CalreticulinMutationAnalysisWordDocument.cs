﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CalreticulinMutationAnalysis
{
	public class CalreticulinMutationAnalysisWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public CalreticulinMutationAnalysisWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			CalreticulinMutationAnalysisTestOrder reportOrderCalreticulinMutationAnalysis = (CalreticulinMutationAnalysisTestOrder)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\fileserver\Documents\ReportTemplates\XmlTemplates\CalreticulinMutationAnalysis.3.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			string reportResult = reportOrderCalreticulinMutationAnalysis.Result;
			if (string.IsNullOrEmpty(reportResult))
			{
				reportResult = string.Empty;
			}

            if (reportOrderCalreticulinMutationAnalysis.Result == "Detected")
            {
                this.ReplaceText("report_mutations", reportOrderCalreticulinMutationAnalysis.Mutations);
            }
            else
            {
                this.DeleteRow("report_mutations");
            }

			this.ReplaceText("report_result", reportResult);            

            this.ReplaceText("report_interpretation", reportOrderCalreticulinMutationAnalysis.Interpretation);
            this.ReplaceText("report_method", reportOrderCalreticulinMutationAnalysis.Method);
            this.ReplaceText("report_references", reportOrderCalreticulinMutationAnalysis.ReportReferences);
            this.ReplaceText("test_asr", reportOrderCalreticulinMutationAnalysis.ASR);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
			base.ReplaceText("specimen_description", specimenOrder.Description);

			string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
			this.ReplaceText("pathologist_signature", reportOrderCalreticulinMutationAnalysis.ReferenceLabSignature);

			this.SaveReport();
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
