using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MultipleMyelomaMGUSByFish
{
	public class MultipleMyelomaMGUSByFishWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public MultipleMyelomaMGUSByFishWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			MultipleMyelomaMGUSByFishTestOrder panelSetOrderMultipleMyelomaMGUSByFish = (MultipleMyelomaMGUSByFishTestOrder)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\MultipleMyelomaMGUSByFish.1.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			this.ReplaceText("report_result", panelSetOrderMultipleMyelomaMGUSByFish.Result);

            if (string.IsNullOrEmpty(panelSetOrderMultipleMyelomaMGUSByFish.ReportComment) == false)
            {
                this.ReplaceText("report_comment", panelSetOrderMultipleMyelomaMGUSByFish.ReportComment);
            }
            else
            {
                this.DeleteRow("report_comment");
            }

			this.ReplaceText("report_interpretation", panelSetOrderMultipleMyelomaMGUSByFish.Interpretation);
			this.ReplaceText("probe_set_detail", panelSetOrderMultipleMyelomaMGUSByFish.ProbeSetDetail);
			this.ReplaceText("nuclei_scored", panelSetOrderMultipleMyelomaMGUSByFish.NucleiScored);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
			base.ReplaceText("specimen_description", specimenOrder.Description);

			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
			this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.ReferenceLabSignature);

			this.SaveReport();
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
