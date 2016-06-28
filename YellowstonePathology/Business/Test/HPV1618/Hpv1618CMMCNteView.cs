using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HPV1618
{
	public class HPV1618CMMCNteView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

		public HPV1618CMMCNteView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;            
		}

        public override void ToXml(XElement document)
        {
			YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618 panelSetOrder = (YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

			this.AddNextNteElement("HPV-16/18 Genotyping", document);
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);

			string hpv16ResultText = "HPV-16 Result: " + panelSetOrder.HPV16Result;
			this.AddNextNteElement(hpv16ResultText, document);
			this.AddNextNteElement("HPV-16 Reference: Negative", document);
			this.AddBlankNteElement(document);

			string hpv18ResultText = "HPV-18/45 Result: " + panelSetOrder.HPV18Result;
			this.AddNextNteElement(hpv18ResultText, document);
			this.AddNextNteElement("HPV-18/45 Reference: Negative", document);
			this.AddBlankNteElement(document);

			YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(panelSetOrder.OrderedOnId);
			this.AddNextNteElement("Specimen: " + orderTarget.GetDescription(), document);
			this.AddBlankNteElement(document);

			this.AddNextNteElement("Method:", document);
			this.HandleLongString(panelSetOrder.Method, document);
            this.AddBlankNteElement(document);

			this.AddNextNteElement("References:", document);
			this.HandleLongString(panelSetOrder.References, document);
			this.AddBlankNteElement(document);

			this.AddNextNteElement("This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  It has not been cleared or approved by the U.S. Food and Drug Administration. The FDA has determined that such clearance or approval is not necessary.  This test is used for clinical purposes.  It should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.", document);

			string locationPerformed = panelSetOrder.GetLocationPerformedComment();
			this.AddNextNteElement(locationPerformed, document);
			this.AddBlankNteElement(document);
		}
	}
}
