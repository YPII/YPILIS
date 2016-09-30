using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear
{
    public class TechInitiatedPeripheralSmearWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public TechInitiatedPeripheralSmearWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {            
            YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear.TechInitiatedPeripheralSmearTestOrder techInitiatedPeripheralSmearTestOrder = (YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear.TechInitiatedPeripheralSmearTestOrder)this.m_PanelSetOrder;

            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\TechInitiatedPeripheralSmear.1.xml";
            base.OpenTemplate();

            base.SetDemographicsV2();                        

            base.ReplaceText("technologist_question", techInitiatedPeripheralSmearTestOrder.TechnologistsQuestion);
            base.ReplaceText("pathologist_feedback", techInitiatedPeripheralSmearTestOrder.PathologistFeedback);
            base.ReplaceText("cbc_comment", techInitiatedPeripheralSmearTestOrder.CBCComment);

            this.SetReportDistribution();            

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
