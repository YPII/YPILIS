using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.MPNExtendedReflex
{
	public class MPNExtendedReflexWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public MPNExtendedReflexWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{
            PanelSetOrderMPNExtendedReflex panelSetOrderMPNExtendedReflex = (PanelSetOrderMPNExtendedReflex)this.m_PanelSetOrder;
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\MPNExtendedReflex.1.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            this.ReplaceText("panelset_name", panelSetOrderMPNExtendedReflex.PanelSetName);

            if(string.IsNullOrEmpty(panelSetOrderMPNExtendedReflex.JAK2V617FResult) == false)
            {
                this.ReplaceText("jak2v617_result", panelSetOrderMPNExtendedReflex.JAK2V617FResult);
            }
            else
            {
                this.DeleteRow("jak2v617_result");
            }

            if (string.IsNullOrEmpty(panelSetOrderMPNExtendedReflex.CalreticulinMutationAnalysisResult) == false)
            {
                this.ReplaceText("calr_result", panelSetOrderMPNExtendedReflex.CalreticulinMutationAnalysisResult);
            }
            else
            {
                this.DeleteRow("calr_result");
            }

            if (string.IsNullOrEmpty(panelSetOrderMPNExtendedReflex.MPLResult) == false)
            {
                this.ReplaceText("mpl_result", panelSetOrderMPNExtendedReflex.MPLResult);
            }
            else
            {
                this.DeleteRow("mpl_result");
            }

            this.ReplaceText("report_comment", panelSetOrderMPNExtendedReflex.Comment);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            base.ReplaceText("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.ReplaceText("report_interpretation", panelSetOrderMPNExtendedReflex.Interpretation);
            this.ReplaceText("report_method", panelSetOrderMPNExtendedReflex.Method);
            this.ReplaceText("report_reference", panelSetOrderMPNExtendedReflex.ReportReferences);

			this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
            this.ReplaceText("pathologist_signature", panelSetOrderMPNExtendedReflex.Signature);

			this.SaveReport();
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
