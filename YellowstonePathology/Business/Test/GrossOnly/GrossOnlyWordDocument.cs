using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.GrossOnly
{
    public class GrossOnlyWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {     
        public GrossOnlyWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode)
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            YellowstonePathology.Business.Test.GrossOnly.GrossOnlyTestOrder grossOnlyTestOrder = (YellowstonePathology.Business.Test.GrossOnly.GrossOnlyTestOrder)this.m_PanelSetOrder;
            this.m_PanelSetOrder = grossOnlyTestOrder;

            this.m_TemplateName = @"\\fileserver\Documents\ReportTemplates\XmlTemplates\GrossOnly.2.xml";
            base.OpenTemplate();

            base.SetDemographicsV2();

            
            this.ReplaceText("gross_description", grossOnlyTestOrder.GrossX);            

            string finalDate = Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate) + " - " + YellowstonePathology.Business.BaseData.GetMillitaryTimeString(this.m_PanelSetOrder.FinalTime);
            this.SetXmlNodeData("final_date", finalDate);

            this.SetReportDistribution();
            this.SetCaseHistory();            

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }

		public override YellowstonePathology.Business.Rules.MethodResult DeleteCaseFiles(YellowstonePathology.Business.OrderIdParser orderIdParser)
		{
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
            methodResult.Success = true;
            return methodResult;
        }        
    }
}
