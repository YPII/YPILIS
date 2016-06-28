using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace YellowstonePathology.Business.Test.KRASStandardReflex
{
	public class KRASStandardReflexWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public KRASStandardReflexWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
            KRASStandardReflexResult krasStandardReflexResult = KRASStandardReflexResultFactory.GetResult(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder);            
			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\KRASStandardReflex.1.xml";
			
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();
            

            this.SetXmlNodeData("kras_result", krasStandardReflexResult.KRASStandardResult);
            this.SetXmlNodeData("braf_result", krasStandardReflexResult.BRAFV600EKResult);
			this.SetXmlNodeData("final_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			if (m_PanelSetOrder.AmendmentCollection.Count == 0)
			{
				this.SetXmlNodeData("test_result_header", "Test Result");
			}
			else // If an amendment exists show as corrected
			{
				this.SetXmlNodeData("test_result_header", "Corrected Test Result");
			}

			//delete the kras_result_detail line if not used
            YellowstonePathology.Business.Test.KRASStandard.KRASStandardNotDetectedResult krasStandardNotDetectedResult = new YellowstonePathology.Business.Test.KRASStandard.KRASStandardNotDetectedResult();
            if (krasStandardReflexResult.KRASStandardResult == krasStandardNotDetectedResult.ResultCode)
			{			
		        this.DeleteRow("kras_result_detail");			
			}

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
			this.ReplaceText("specimen_description", specimenOrder.Description);

			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            if (string.IsNullOrEmpty(krasStandardReflexResult.KRASStandardReflexTestOrder.Comment) == false)
            {
                this.SetXMLNodeParagraphData("result_comment", krasStandardReflexResult.KRASStandardReflexTestOrder.Comment);
            }
            else
            {
                this.DeleteRow("result_comment");
            }

            this.SetXMLNodeParagraphData("tumor_nuclei_percent", krasStandardReflexResult.KRASStandardReflexTestOrder.TumorNucleiPercentage);
            this.SetXMLNodeParagraphData("report_indication_comment", krasStandardReflexResult.KRASStandardReflexTestOrder.IndicationComment);
            this.SetXMLNodeParagraphData("report_interpretation", krasStandardReflexResult.KRASStandardReflexTestOrder.Interpretation);
            this.SetXMLNodeParagraphData("report_references", krasStandardReflexResult.KRASStandardReflexTestOrder.References);
            this.SetXMLNodeParagraphData("report_method", krasStandardReflexResult.KRASStandardReflexTestOrder.Method);
            this.SetXMLNodeParagraphData("report_disclaimer", krasStandardReflexResult.KRASStandardReflexTestOrder.GetLocationPerformedComment());

			this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
			this.SetXmlNodeData("pathologist_signature", m_PanelSetOrder.Signature);
			this.SaveReport();
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
