﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CCNDIBCLIGHByFISH
{
	public class CCNDIBCLIGHByFISHWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public CCNDIBCLIGHByFISHWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			CCNDIBCLIGHByFISHTestOrder panelSetOrderCCNDIBCLIGH = (CCNDIBCLIGHByFISHTestOrder)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\fileserver\Documents\ReportTemplates\XmlTemplates\CCNDIBCLIGHByFISH.3.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

            this.ReplaceText("report_title", this.m_PanelSetOrder.PanelSetName);

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			this.ReplaceText("report_result", panelSetOrderCCNDIBCLIGH.Result);
            this.SetXmlNodeData("report_interpretation", panelSetOrderCCNDIBCLIGH.Interpretation);
            this.SetXmlNodeData("probe_set_detail", panelSetOrderCCNDIBCLIGH.ProbeSetDetail);
			this.ReplaceText("nuclei_scored", panelSetOrderCCNDIBCLIGH.NucleiScored);
            this.SetXmlNodeData("report_references", panelSetOrderCCNDIBCLIGH.ReportReferences);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
			base.ReplaceText("specimen_description", specimenOrder.Description);

			string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
			this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.ReferenceLabSignature);

			this.SaveReport();
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
