using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.NeoARRAYSNPCytogeneticProfile
{
	public class NeoARRAYSNPCytogeneticProfileEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
	{
		public NeoARRAYSNPCytogeneticProfileEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
		}

		public override void ToXml(XElement document)
		{
			NeoARRAYSNPCytogeneticProfileTestOrder panelSetOrder = (NeoARRAYSNPCytogeneticProfileTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			this.AddHeader(document, panelSetOrder, "NeoARRAY SNP/Cytogenetic Profile");

			this.AddNextObxElement("", document, "F");
			string result = "Result: " + panelSetOrder.Result;
			this.AddNextObxElement(result, document, "F");
			result = "Copy Number Variant(s): " + panelSetOrder.CopyNumberVariant;
			this.AddNextObxElement(result, document, "F");
			result = "Uniparental Disomy: " + panelSetOrder.UniparentalDisomy;
			this.AddNextObxElement(result, document, "F");
			result = "Microarray Results: " + panelSetOrder.MicroarrayResults;
			this.AddNextObxElement(result, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Pathologist: " + panelSetOrder.ReferenceLabSignature, document, "F");
			if (panelSetOrder.FinalTime.HasValue == true)
			{
				this.AddNextObxElement("E-signed " + panelSetOrder.ReferenceLabFinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
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
			this.AddNextObxElement("Method:", document, "F");
			this.HandleLongString(panelSetOrder.Method, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("References:", document, "F");
			this.HandleLongString(panelSetOrder.ReportReferences, document, "F");			
		}
	}
}
