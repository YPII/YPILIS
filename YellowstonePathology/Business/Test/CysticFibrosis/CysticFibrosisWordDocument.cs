using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	public class CysticFibrosisWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public CysticFibrosisWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{            
			CysticFibrosisTestOrder panelSetOrderCF = (CysticFibrosisTestOrder)this.m_PanelSetOrder;

			if (panelSetOrderCF.TemplateId == 1)
			{
				this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\CysticFibrosisCarrierScreening.1.xml";
			}
			else if (panelSetOrderCF.TemplateId == 2)
			{
				this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\CysticFibrosisCarrierScreeningUnknownEthnicGroup.1.xml";
			}
			else
			{
				return;
			}

			base.OpenTemplate();

            base.SetDemographicsV2();

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);
            this.SetXmlNodeData("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);	

            this.SetReportDistribution();
            this.SetCaseHistory();

			base.ReplaceText("report_result", panelSetOrderCF.Result);
			base.ReplaceText("mutations_detected", panelSetOrderCF.MutationsDetected);
			base.ReplaceText("report_comment", panelSetOrderCF.Comment);

            YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisEthnicGroupCollection cysticFibrosisEthnicGroupCollection = new CysticFibrosisEthnicGroupCollection();
            YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisEthnicGroup cysticFibrosisEthnicGroup = cysticFibrosisEthnicGroupCollection.GetCysticFibrosisEthnicGroup(panelSetOrderCF.EthnicGroupId);

            base.ReplaceText("report_ethnic_group", cysticFibrosisEthnicGroup.EthnicGroupName);
			base.ReplaceText("report_interpretation", panelSetOrderCF.Interpretation);
			base.ReplaceText("mutations_tested", panelSetOrderCF.MutationsTested);
			base.ReplaceText("report_method", panelSetOrderCF.Method);
			base.ReplaceText("report_references", panelSetOrderCF.ReportReferences);

			this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);

			YellowstonePathology.Business.Document.AmendmentSection amendment = new YellowstonePathology.Business.Document.AmendmentSection();
			amendment.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);            

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
