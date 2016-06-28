using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.Document
{
	public class FishAnalysisReport : CaseReportV2
    {
        public FishAnalysisReport(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{
            
        }

        public override void Publish()
        {            
            YellowstonePathology.Business.Helper.FileConversionHelper.SaveXpsReportToTiff(this.m_PanelSetOrder.ReportNo);
        }
    }
}
