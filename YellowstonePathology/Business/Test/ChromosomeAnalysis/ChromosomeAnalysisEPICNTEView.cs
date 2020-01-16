using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ChromosomeAnalysis
{
	public class ChromosomeAnalysisEPICNTEView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerNTEView
	{
        private Business.Test.AccessionOrder m_AccessionOrder;
        private string m_ReportNo;
        private int m_NTECount;

		public ChromosomeAnalysisEPICNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int nteCount)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;
            this.m_NTECount = nteCount;
		}

		public void ToXml(XElement document)
		{
			ChromosomeAnalysisTestOrder panelSetOrder = (ChromosomeAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			this.AddCompanyHeader(document);
            this.AddNextNTEElement("Cytogenetic Chromosome Analysis", document);

            this.AddNextNTEElement("", document);
			string result = "Result: " + panelSetOrder.Result;
			this.AddNextNTEElement(result, document);
			result = "  Karyotype : " + panelSetOrder.Karyotype;
			this.AddNextNTEElement(result, document);

			this.AddNextNTEElement("", document);
			this.AddNextNTEElement("Pathologist: " + panelSetOrder.Signature, document);
			if (panelSetOrder.FinalTime.HasValue == true)
			{
				this.AddNextNTEElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
			}

			this.AddNextNTEElement("", document);
            this.AddAmendments(document, panelSetOrder, this.m_AccessionOrder);

            this.AddNextNTEElement("Specimen Information:", document);
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
			this.AddNextNTEElement("Specimen Identification: " + specimenOrder.Description, document);
			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.AddNextNTEElement("Collection Date/Time: " + collectionDateTimeString, document);

			this.AddNextNTEElement("", document);
			this.AddNextNTEElement("Interpretation:", document);
			this.AddNextNTEElement(panelSetOrder.Interpretation, document);

			this.AddNextNTEElement("", document);
			this.AddNextNTEElement("Comment:", document);
			this.AddNextNTEElement(panelSetOrder.Comment, document);

			this.AddNextNTEElement("", document);
			this.AddNextNTEElement("Test Details:", document);
			this.AddNextNTEElement("Metaphases Counted: " + panelSetOrder.MetaphasesCounted, document);
			this.AddNextNTEElement("Metaphases Analyzed: " + panelSetOrder.MetaphasesAnalyzed, document);
			this.AddNextNTEElement("Metaphases Karyotyped: " + panelSetOrder.MetaphasesKaryotyped, document);
			this.AddNextNTEElement("Culture Type: " + panelSetOrder.CultureType, document);
			this.AddNextNTEElement("Banding Technique: " + panelSetOrder.BandingTechnique, document);
			this.AddNextNTEElement("Banding Resolution: " + panelSetOrder.BandingResolution, document);

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement(panelSetOrder.ASR, document);

            this.AddNextNTEElement("", document);
            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
			this.AddNextNTEElement(locationPerformed, document);
			this.AddNextNTEElement(string.Empty, document);
		}
	}
}
