using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ChromosomeAnalysis
{
	public class ChromosomeAnalysisEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
	{
		public ChromosomeAnalysisEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
		}

		public override void ToXml(XElement document)
		{
			ChromosomeAnalysisTestOrder panelSetOrder = (ChromosomeAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			this.AddHeader(document, panelSetOrder, "Cytogenetic Chromosome Analysis");

			this.AddNextObxElement("", document, "F");
			string result = "Result: " + panelSetOrder.Result;
			this.AddNextObxElement(result, document, "F");
			result = "  Karyotype : " + panelSetOrder.Karyotype;
			this.AddNextObxElement(result, document, "F");

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
			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Interpretation:", document, "F");
			this.HandleLongString(panelSetOrder.Interpretation, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Comment:", document, "F");
			this.HandleLongString(panelSetOrder.Comment, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Test Details:", document, "F");
			this.AddNextObxElement("  Metaphases Counted   : " + panelSetOrder.MetaphasesCounted, document, "F");
			this.AddNextObxElement("  Metaphases Analyzed   : " + panelSetOrder.MetaphasesAnalyzed, document, "F");
			this.AddNextObxElement("  Metaphases Karyotyped   : " + panelSetOrder.MetaphasesKaryotyped, document, "F");
			this.AddNextObxElement("  Culture Type   : " + panelSetOrder.CultureType, document, "F");
			this.AddNextObxElement("  Banding Technique   : " + panelSetOrder.BandingTechnique, document, "F");
			this.AddNextObxElement("  Banding Resolution   : " + panelSetOrder.BandingResolution, document, "F");

			this.AddNextObxElement("", document, "F");
            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
			this.AddNextObxElement(locationPerformed, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");
		}
	}
}
