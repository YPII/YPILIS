using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Her2AmplificationByFish
{
	public class Her2AmplificationByFishEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public Her2AmplificationByFishEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{			
		}

        public override void ToXml(XElement document)
        {
			PanelSetOrderHer2AmplificationByFish panelSetOrder = (PanelSetOrderHer2AmplificationByFish)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);                        

            this.AddHeader(document, panelSetOrder, "HER2 Gene Amplification");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("HER2: " + panelSetOrder.Result, document, "F");
            this.AddNextObxElement("Ratio: " + panelSetOrder.HER2CEN17SignalRatio, document, "F");
            this.AddNextObxElement("Average HER2 Copy Number = " + panelSetOrder.AverageHER2SignalsPerNucleus, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
			this.AddNextObxElement("Reference Range: " + panelSetOrder.ReferenceRanges, document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.Comment) != true)
            {
                this.HandleLongString("Comment: " + panelSetOrder.Comment, document, "F");
            }
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement(string.Empty, document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Number of invasive tumor cells counted: " + panelSetOrder.NucleiScored, document, "F");
            this.AddNextObxElement("HER2 average copy number per nucleus: " + panelSetOrder.AverageHER2SignalsPerNucleus, document, "F");
            this.AddNextObxElement("Chr17 average copy number per nucleus: " + panelSetOrder.AverageCEN17SignalsPerNucleus, document, "F");
            this.AddNextObxElement("Ratio of average HER2/Chr17 signals: " + panelSetOrder.HER2CEN17SignalRatio, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(panelSetOrder.OrderedOnId);
            string specimenDescriptionString = specimenOrder.Description + " - " + aliquotOrder.Label;

            this.AddNextObxElement("Specimen Description: " + specimenDescriptionString, document, "F");            

            this.AddNextObxElement("Fixation Type: " + specimenOrder.LabFixation, document, "F");
            this.AddNextObxElement("Time to fixation: " + specimenOrder.TimeToFixationHourString, document, "F");
            this.AddNextObxElement("Duration of fixation: " + specimenOrder.FixationDurationString, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Interpretation: ", document, "F");
            this.HandleLongString(panelSetOrder.Interpretation, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Method: ", document, "F");
            string method = System.Security.SecurityElement.Escape("This test was performed using a molecular method, In Situ Hybridization (ISH) with the US FDA approved Inform HER2 DNA probe kit, modified to report results according to ASCO/CAP guidelines. The test was performed on paraffin embedded tissue in compliance with ASCO/CAP guidelines.  Probes used include a locus specific probe for HER2 and an internal hybridization control probe for the centromeric region of chromosome 17 (Chr17).");
            this.HandleLongString(method, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");            

            this.AddNextObxElement("References: ", document, "F");
            this.HandleLongString(panelSetOrder.Reference, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in panelSetOrder.AmendmentCollection)
            {
                this.AddNextObxElement(amendment.AmendmentType + ": " + amendment.AmendmentDate.Value.ToString("MM/dd/yyyy"), document, "C");
                this.HandleLongString(amendment.Text, document, "C");
                if (amendment.RequirePathologistSignature == true)
                {
                    this.AddNextObxElement("Signature: " + amendment.PathologistSignature, document, "C");
                }
                this.AddNextObxElement("", document, "C");
            }

            string ldtComment = "This test was performed using a US FDA approved DNA probe kit, modified to report results according to ASCO/CAP guidelines, and the modified procedure was validated by Yellowstone Pathology Institute (YPI).  YPI assumes the responsibility for test performance.";
            this.HandleLongString(ldtComment, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }		
	}
}
