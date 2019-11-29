using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.CLLByFish
{
    public class CLLByFishCMMCNTEView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public CLLByFishCMMCNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;
        }

        public override void ToXml(XElement document)
        {
            CLLByFishTestOrder panelSetOrder = (CLLByFishTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetOrder.PanelSetId);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement(panelSet.PanelSetName, document);
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);

            string result = "Result: " + panelSetOrder.Result;
            if (string.IsNullOrEmpty(panelSetOrder.ResultDescription) == false) result = "Result: " + panelSetOrder.ResultDescription;
            this.AddNextNteElement(result, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("Pathologist: " + panelSetOrder.Signature, document);
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextNteElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
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
            this.AddNextNteElement("Probe Set Details:", document);
            this.HandleLongString(panelSetOrder.ProbeSetDetail, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("Nuclei Scored:", document);
            this.HandleLongString(panelSetOrder.NucleiScored, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("References:", document);
            this.HandleLongString(panelSetOrder.ReportReferences, document);

            this.AddBlankNteElement(document);
            this.HandleLongString(panelSetOrder.ASR, document);
            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document);
            this.AddBlankNteElement(document);
        }
    }
}
