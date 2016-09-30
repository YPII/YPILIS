using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.NGCT
{
	public class NGCTCMMCNteView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
	{
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public NGCTCMMCNteView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;            
		}

        public override void ToXml(XElement document)
        {
            NGCTTestOrder testOrder = (NGCTTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Chlamydia Gonorrhea Screening", document);
            this.AddNextNteElement("Master Accession #: " + testOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + testOrder.ReportNo, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Chlamydia trachomatis result: " + testOrder.ChlamydiaTrachomatisResult, document);
            this.AddNextNteElement("Neisseria gonorrhoeae result: " + testOrder.NeisseriaGonorrhoeaeResult, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Specimen: Thin Prep Fluid", document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Method:", document);
            this.AddNextNteElement(testOrder.Method, document);
            this.AddBlankNteElement(document);
        }
	}
}

