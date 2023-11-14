using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LynchSyndromeIHCPanelCMMCNTEView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public LynchSyndromeIHCPanelCMMCNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;
        }

        public override void ToXml(XElement document)
        {
            PanelSetOrderLynchSyndromeIHC panelSetOrder = (PanelSetOrderLynchSyndromeIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Lynch Syndrome IHC Panel", document);
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("MLH1 Result: " + panelSetOrder.MLH1Result, document);
            this.AddNextNteElement("PMS2 Result: " + panelSetOrder.PMS2Result, document);
            this.AddNextNteElement("MSH2 Result: " + panelSetOrder.MSH2Result, document);
            this.AddNextNteElement("MSH6 Result: " + panelSetOrder.MSH6Result, document);
            this.AddBlankNteElement(document);
            this.HandleLongString("Comment: " + panelSetOrder.Comment, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("Pathologist: " + panelSetOrder.Signature, document);
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextNteElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }

            this.AddBlankNteElement(document);
            this.AddAmendments(document, panelSetOrder, this.m_AccessionOrder);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("Specimen Description:", document);
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextNteElement(specimenOrder.Description, document);

            this.AddBlankNteElement(document);
            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document);
            this.AddBlankNteElement(document);
        }
    }
}
