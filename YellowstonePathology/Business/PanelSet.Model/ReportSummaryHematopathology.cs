using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class ReportSummaryHematopathology : ReportSummary
	{        
        public ReportSummaryHematopathology()
		{            
            this.m_ReportTemplatePath = @"\\cfileserver\Documents\ReportTemplates\XmlTemplates\HematopathologySummary.xml";
		}     
	}
}
