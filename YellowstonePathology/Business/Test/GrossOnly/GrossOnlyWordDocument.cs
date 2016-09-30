using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.GrossOnly
{
    public class GrossOnlyWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        private YellowstonePathology.Business.Document.NativeDocumentFormatEnum m_NativeDocumentFormat;

        public GrossOnlyWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode)
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            YellowstonePathology.Business.Test.GrossOnly.GrossOnlyTestOrder grossOnlyTestOrder = (YellowstonePathology.Business.Test.GrossOnly.GrossOnlyTestOrder)this.m_PanelSetOrder;
            this.m_PanelSetOrder = grossOnlyTestOrder;

            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\GrossOnly.1.xml";
            base.OpenTemplate();

            base.SetDemographicsV2();

            
            this.ReplaceText("gross_description", grossOnlyTestOrder.GrossX);            

            string finalDate = YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate) + " - " + YellowstonePathology.Business.BaseData.GetMillitaryTimeString(this.m_PanelSetOrder.FinalTime);
            this.SetXmlNodeData("final_date", finalDate);

            this.SetReportDistribution();
            this.SetCaseHistory();            

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }

		public YellowstonePathology.Business.Rules.MethodResult DeleteCaseFiles(YellowstonePathology.Business.OrderIdParser orderIdParser)
		{
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
            methodResult.Success = true;
            return methodResult;
        }

        public YellowstonePathology.Business.Document.NativeDocumentFormatEnum NativeDocumentFormat
        {
            get { return this.m_NativeDocumentFormat; }
            set { this.m_NativeDocumentFormat = value; }
        }
    }
}
