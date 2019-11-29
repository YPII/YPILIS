using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.BCRABLByPCR
{
    class BCRABLByPCRCMMCNTEView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public BCRABLByPCRCMMCNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;
        }

        public override void ToXml(XElement document)
        {
            BCRABLByPCRTestOrder panelSetOrder = (BCRABLByPCRTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetOrder.PanelSetId);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement(panelSet.PanelSetName, document);
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);

            string result = "Result: " + panelSetOrder.Result;
            this.AddNextNteElement(result, document);
            if (string.IsNullOrEmpty(panelSetOrder.DetectedLogReduction) == false)
            {
                result = "Detected Log Reduction: " + panelSetOrder.DetectedLogReduction;
                this.AddNextNteElement(result, document);
            }

            result = "Fusion Transcript Type: " + panelSetOrder.FusionTranscriptType;
            this.AddNextNteElement(result, document);

            result = "% BCR - ABL1 / ABL1(IS): " + panelSetOrder.PercentBCRABL;
            this.AddNextNteElement(result, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("Pathologist: " + panelSetOrder.ReferenceLabSignature, document);
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextNteElement("E-signed " + panelSetOrder.ReferenceLabFinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }
            this.AddBlankNteElement(document);

            this.AddAmendments(document, panelSetOrder, this.m_AccessionOrder);

            this.AddNextNteElement("Specimen Information:", document);
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextNteElement("Specimen Identification: " + specimenOrder.Description, document);
            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextNteElement("Collection Date/Time: " + collectionDateTimeString, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("Interpretation:", document);
            this.HandleLongString(panelSetOrder.Interpretation, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("Method:", document);
            this.HandleLongString(panelSetOrder.Method, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("References:", document);
            this.HandleLongString(panelSetOrder.ReportReferences, document);

            this.AddBlankNteElement(document);
            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document);
            this.AddBlankNteElement(document);
        }
    }
}
