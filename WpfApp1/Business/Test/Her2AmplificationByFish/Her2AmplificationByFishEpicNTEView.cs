using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Her2AmplificationByFish
{
	public class Her2AmplificationByFishEPICNTEView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
		public Her2AmplificationByFishEPICNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{			
		}

        public override void ToXml(XElement document)
        {
			PanelSetOrderHer2AmplificationByFish panelSetOrder = (PanelSetOrderHer2AmplificationByFish)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            //Add the first element as narrative for Nikki to see.
            Business.HL7View.EPIC.EPICBeakerNarrativeOBXView.AddElement(document);

            this.AddCompanyHeaderNTE(document);
            this.AddNextNTEElement(panelSetOrder.PanelSetName, document);
            this.AddNextNTEElement("", document);

            if (panelSetOrder.NonBreast == false)
            {
                this.AddNextNTEElement(panelSetOrder.PanelSetName, document);
            }
            else
            {
                this.AddNextNTEElement("HER2 Gene Amplification", document);                
            }

            this.AddNextNTEElement("Report No: " + panelSetOrder.ReportNo, document);
            this.AddNextNTEElement("", document);            
            this.AddNextNTEElement("HER2: " + panelSetOrder.Result, document);
            this.AddNextNTEElement("Ratio: " + panelSetOrder.HER2CEN17SignalRatio, document);
            this.AddNextNTEElement("Average HER2 Copy Number = " + panelSetOrder.AverageHER2SignalsPerNucleus, document);
            this.AddNextNTEElement(string.Empty, document);
			this.AddNextNTEElement("Reference Range: " + panelSetOrder.ReferenceRanges, document);

            if (string.IsNullOrEmpty(panelSetOrder.Comment) != true)
            {
                this.AddNextNTEElement("Comment: " + panelSetOrder.Comment, document);
            }
            this.AddNextNTEElement(string.Empty, document);

            this.AddNextNTEElement("Pathologist: " + panelSetOrder.Signature, document);
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextNTEElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }
            this.AddNextNTEElement(string.Empty, document);
            this.AddAmendments(document);

            this.AddNextNTEElement("Number of invasive tumor cells counted: " + panelSetOrder.NucleiScored, document);
            this.AddNextNTEElement("HER2 average copy number per nucleus: " + panelSetOrder.AverageHER2SignalsPerNucleus, document);
            this.AddNextNTEElement("Chr17 average copy number per nucleus: " + panelSetOrder.AverageCEN17SignalsPerNucleus, document);
            this.AddNextNTEElement("Ratio of average HER2/Chr17 signals: " + panelSetOrder.HER2CEN17SignalRatio, document);
            this.AddNextNTEElement(string.Empty, document);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(panelSetOrder.OrderedOnId);
            string specimenDescriptionString = specimenOrder.Description + " - " + aliquotOrder.Label;

            this.AddNextNTEElement("Specimen Description: " + specimenDescriptionString, document);            

            this.AddNextNTEElement("Fixation Type: " + specimenOrder.LabFixation, document);
            this.AddNextNTEElement("Time to fixation: " + specimenOrder.TimeToFixationHourString, document);
            this.AddNextNTEElement("Duration of fixation: " + specimenOrder.FixationDurationString, document);
            this.AddNextNTEElement(string.Empty, document);

            this.AddNextNTEElement("Interpretation: ", document);
            this.AddNextNTEElement(panelSetOrder.Interpretation, document);
            this.AddNextNTEElement(string.Empty, document);

            this.AddNextNTEElement("Method: ", document);
            string method = System.Security.SecurityElement.Escape("This test was performed using a molecular method, In Situ Hybridization (ISH) with the US FDA approved Ventana HER2 DNA probe kit, modified to report results according to ASCO/CAP guidelines. The test was performed on paraffin embedded tissue in compliance with ASCO/CAP guidelines.  Probes used include a locus specific probe for HER2 and an internal hybridization control probe for the centromeric region of chromosome 17 (Chr17).");
            this.AddNextNTEElement(method, document);
            this.AddNextNTEElement(string.Empty, document);            

            this.AddNextNTEElement("References: ", document);
            this.AddNextNTEElement(panelSetOrder.Reference, document);
            this.AddNextNTEElement(string.Empty, document);

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(panelSetOrder.ReportNo);
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
            {
                this.AddNextNTEElement(amendment.AmendmentType + ": " + amendment.AmendmentDate.Value.ToString("MM/dd/yyyy"), document);
                this.AddNextNTEElement(amendment.Text, document);
                if (amendment.RequirePathologistSignature == true)
                {
                    this.AddNextNTEElement("Signature: " + amendment.PathologistSignature, document);
                }
                this.AddNextNTEElement("", document);
            }

            string ldtComment = "This test was performed using a US FDA approved DNA probe kit, modified to report results according to ASCO/CAP guidelines, and the modified procedure was validated by Yellowstone Pathology Institute (YPI).  YPI assumes the responsibility for test performance.";
            this.AddNextNTEElement(ldtComment, document);

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextNTEElement(locationPerformed, document);
            this.AddNextNTEElement(string.Empty, document);
        }		
	}
}
