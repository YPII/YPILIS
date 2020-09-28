using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ErPrSemiQuantitative
{
	public class ErPrSemiQuantitativeEPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
	{
		public ErPrSemiQuantitativeEPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
		}

        public override void ToXml(XElement document)
        {
			ErPrSemiQuantitativeTestOrder panelSetOrder = (ErPrSemiQuantitativeTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			
			this.AddNextObxElementBeaker("Report No", panelSetOrder.ReportNo, document, "F");
			this.AddNextObxElementBeaker("Estrogen Receptor", panelSetOrder.ErResult, document, "F");
			this.AddNextObxElementBeaker("ER Intensity", panelSetOrder.ErIntensity, document, "F");
			this.AddNextObxElementBeaker("ER Percentage Of Cells", panelSetOrder.ErPercentageOfCells, document, "F");

			this.AddNextObxElementBeaker("Progesterone Receptor", panelSetOrder.PrResult, document, "F");
			this.AddNextObxElementBeaker("PR Intensity", panelSetOrder.PrIntensity, document, "F");
			this.AddNextObxElementBeaker("PR Percentage Of Cells", panelSetOrder.PrPercentageOfCells, document, "F");

			if (string.IsNullOrEmpty(panelSetOrder.ResultComment) == false)
			{
				this.AddNextObxElementBeaker("Comment", panelSetOrder.ResultComment, document, "F");
			}

			this.AddNextObxElementBeaker("Pathologist", panelSetOrder.Signature, document, "F");
			if (panelSetOrder.FinalTime.HasValue == true)
            {
				this.AddNextObxElementBeaker("E-signed", panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElementBeaker("Specimen Identification", specimenOrder.Description, document, "F");
			this.AddNextObxElementBeaker("Specimen fixation type", specimenOrder.LabFixation, document, "F");
			this.AddNextObxElementBeaker("Time to fixation", specimenOrder.TimeToFixationHourString, document, "F");
			this.AddNextObxElementBeaker("Duration of fixation", specimenOrder.FixationDuration.ToString(), document, "F");
			this.AddNextObxElementBeaker("Sample adequacy", panelSetOrder.SpecimenAdequacy, document, "F");
			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.AddNextObxElementBeaker("Collection Date/Time", collectionDateTimeString, document, "F");

            if (string.IsNullOrEmpty(specimenOrder.FixationComment) == false)
            {
                this.AddNextObxElementBeaker("Fixation Comment", specimenOrder.FixationComment, document, "F");             
            }            

			this.AddNextObxElementBeaker("Interpretation", panelSetOrder.Interpretation, document, "F"); 

			this.AddNextObxElementBeaker("Method", panelSetOrder.Method, document, "F");
			this.AddNextObxElementBeaker("References", panelSetOrder.ReportReferences, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElementBeaker("Location Performed", locationPerformed, document, "F");
        }
	}
}
