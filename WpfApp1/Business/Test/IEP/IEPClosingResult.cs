using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.IEP
{
    public class IEPClosingResult : Business.HL7View.EPIC.EPICBeakerObxView
    {

        public IEPClosingResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            string observationResultStatus = "F";            
            this.AddNextObxElementBeaker("Report No", this.m_ReportNo, document, observationResultStatus);            
            this.AddNextObxElementBeaker("Immunoelectrophoresis", "No result required.", document, observationResultStatus, "No Result Required", false);            
        }
    }
}
