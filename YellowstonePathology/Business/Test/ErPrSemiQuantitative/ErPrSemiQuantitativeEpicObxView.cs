using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ErPrSemiQuantitative
{
	public class ErPrSemiQuantitativeEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
	{
		public ErPrSemiQuantitativeEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
		}

        public override void ToXml(XElement document)
        {
			ErPrSemiQuantitativeTestOrder panelSetOrder = (ErPrSemiQuantitativeTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			this.AddHeader(document, panelSetOrder, "Estrogen/Progesterone Receptor, Semi-Quantitative");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Result:", document, "F");
			
			this.AddNextObxElement("Estrogen Receptor: " + panelSetOrder.ErResult, document, "F");
			this.AddNextObxElement("   Intensity: " + panelSetOrder.ErIntensity, document, "F");
			this.AddNextObxElement("   Percentage Of Cells: " + panelSetOrder.ErPercentageOfCells, document, "F");

			this.AddNextObxElement("Progesterone Receptor:" + panelSetOrder.PrResult, document, "F");
			this.AddNextObxElement("   Intensity: " + panelSetOrder.PrIntensity, document, "F");
			this.AddNextObxElement("   Percentage Of Cells: " + panelSetOrder.PrPercentageOfCells, document, "F");

			if (string.IsNullOrEmpty(panelSetOrder.ResultComment) == false)
			{
				this.AddNextObxElement("", document, "F");
				this.AddNextObxElement("Comment:", document, "F");
				this.HandleLongString(panelSetOrder.ResultComment, document, "F");
			}

            this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
			if (panelSetOrder.FinalTime.HasValue == true)
            {
				this.AddNextObxElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }

            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen Information:", document, "F");
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElement("Specimen Identification: " + specimenOrder.Description, document, "F");
			this.AddNextObxElement("Specimen fixation type: " + specimenOrder.LabFixation, document, "F");
			this.AddNextObxElement("Time to fixation: " + specimenOrder.TimeToFixationHourString, document, "F");
			this.AddNextObxElement("Duration of fixation: " + specimenOrder.FixationDuration, document, "F");
			this.AddNextObxElement("Sample adequacy: " + panelSetOrder.SpecimenAdequacy, document, "F");
			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");            

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Interpretation:", document, "F");
			this.HandleLongString(panelSetOrder.Interpretation, document, "F"); 

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Method:", document, "F");
			this.HandleLongString(panelSetOrder.Method, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("References:", document, "F");
			this.HandleLongString(panelSetOrder.ReportReferences, document, "F");

			this.AddNextObxElement("", document, "F");
            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
	}
}
