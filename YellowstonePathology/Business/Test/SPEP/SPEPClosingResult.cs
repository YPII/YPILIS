using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.SPEP
{
    public class SPEPClosingResult : Business.HL7View.EPIC.EPICBeakerObxView
    {

        public SPEPClosingResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            string observationResultStatus = "F";            
            this.AddNextObxElementBeaker("Report No", this.m_ReportNo, document, observationResultStatus);            
            this.AddNextObxElementBeaker("Serum Protein Electrophoresis", "No result required.", document, observationResultStatus, "No Result Required", false);            
        }
    }
}
