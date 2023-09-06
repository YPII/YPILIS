using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.InvasiveBreastPanel
{
	public class InvasiveBreastPanelWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public InvasiveBreastPanelWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {            
            this.m_TemplateName = @"\\fileserver\Documents\ReportTemplates\XmlTemplates\InvasiveBreastPanel.3.xml";
            this.OpenTemplate();
            this.SetDemographicsV2();
            this.SetReportDistribution();

            YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanel invasiveBreastPanel = (YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanel)this.m_PanelSetOrder;
			YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelResult invasiveBreastPanelResult = new Test.InvasiveBreastPanel.InvasiveBreastPanelResult(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder);

            this.ReplaceText("specimen_description", invasiveBreastPanelResult.SpecimenOrder.GetSpecimenDescriptionString());
			if (invasiveBreastPanelResult.HasSurgical == true)
			{
				this.SetXMLNodeParagraphData("surgical_diagnosis", invasiveBreastPanelResult.SurgicalSpecimen.Diagnosis);
			}
			else this.DeleteRow("surgical_diagnosis");

            this.ReplaceText("her2_result", invasiveBreastPanelResult.HER2ResultString);
            this.ReplaceText("er_result", invasiveBreastPanelResult.ERResultString);
            this.ReplaceText("pr_result", invasiveBreastPanelResult.PRResultString);            
            this.ReplaceText("ki67_result", invasiveBreastPanelResult.Ki67Result);

            this.ReplaceText("report_date", BaseData.GetShortDateString(invasiveBreastPanel.FinalDate));
			this.ReplaceText("pathologist_signature", invasiveBreastPanel.Signature);

            this.SaveReport();
        }
        
        public override void Publish()
        {
            base.Publish();
        }        
	}
}
