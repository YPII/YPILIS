using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Trichomonas
{
	public class TrichomonasCMMCNteView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
	{
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public TrichomonasCMMCNteView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;            
		}

        public override void ToXml(XElement document)
        {
			TrichomonasTestOrder panelSetOrder = (TrichomonasTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);            

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Trichomonas Vaginalis", document);
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Result: " + panelSetOrder.Result, document);
            this.AddNextNteElement("(Reference: Negative)", document);            
            this.AddBlankNteElement(document);
            
            this.AddNextNteElement("Specimen: Thin Prep Fluid", document);
            this.AddBlankNteElement(document);

            string method = "DNA was extracted from the patient's specimen using an automated method.  Real time PCR amplification was performed for organism detection and identification.";
            this.AddNextNteElement("Method:", document);
            this.AddNextNteElement(method, document);
            this.AddBlankNteElement(document);
        }
	}
}
