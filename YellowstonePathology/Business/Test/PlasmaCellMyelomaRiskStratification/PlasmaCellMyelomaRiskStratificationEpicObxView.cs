using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.PlasmaCellMyelomaRiskStratification
{
	public class PlasmaCellMyelomaRiskStratificationEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
	{
		public PlasmaCellMyelomaRiskStratificationEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
		}

		public override void ToXml(XElement document)
		{
			PlasmaCellMyelomaRiskStratificationTestOrder testOrder = (PlasmaCellMyelomaRiskStratificationTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			this.AddHeader(document, testOrder, "Plasma Cell Myeloma Risk Stratification");

			this.AddNextObxElement("", document, "F");
			string result = "Result: " + testOrder.Result;
			this.AddNextObxElement(result, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Pathologist: " + testOrder.Signature, document, "F");
			if (testOrder.FinalTime.HasValue == true)
			{
				this.AddNextObxElement("E-signed " + testOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
			}

			this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen Information:", document, "F");
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
			this.AddNextObxElement("Specimen Identification: " + specimenOrder.Description, document, "F");
			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Interpretation:", document, "F");
			this.HandleLongString(testOrder.Interpretation, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Probe Set Details:", document, "F");
			this.HandleLongString(testOrder.ProbeSetDetail, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Nuclei Scored:", document, "F");
			this.HandleLongString(testOrder.NucleiScored, document, "F");

			this.AddNextObxElement("", document, "F");
			this.HandleLongString(testOrder.ReportDisclaimer, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");
		}
	}
}
