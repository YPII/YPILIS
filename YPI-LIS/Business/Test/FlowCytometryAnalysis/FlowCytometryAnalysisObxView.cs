using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.FlowCytometryAnalysis
{
    public class FlowCytometryAnalysisObxView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
        public FlowCytometryAnalysisObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{ }

        public override void ToXml(XElement document)
        {            
            this.AddNextObxElementBeaker("Report No", this.m_ReportNo, document, "F");            
            this.AddNextObxElementBeaker("Interpretive Comment", "Please see PDF Document.", document, "F");
        }        
    }
}
