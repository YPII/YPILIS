using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Her2AmplificationByIHC
{
	public class Her2AmplificationByIHCEPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
	{
		public Her2AmplificationByIHCEPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
		}

		public override void ToXml(XElement document)
		{
			PanelSetOrderHer2AmplificationByIHC panelSetOrder = (PanelSetOrderHer2AmplificationByIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, panelSetOrder, panelSetOrder.PanelSetName);

            this.AddNextObxElementBeaker("Report No", panelSetOrder.ReportNo, document, "F");
			this.AddNextObxElementBeaker("Result", panelSetOrder.Result, document, "F");
			this.AddNextObxElementBeaker("Score", panelSetOrder.Score, document, "F");
			this.AddNextObxElementBeaker("Percentage of Cells with Uniform Intense Complete Membrane Staining", panelSetOrder.IntenseCompleteMembraneStainingPercent, document, "F");

			this.AddNextObxElementBeaker("Pathologist", panelSetOrder.ReferenceLabSignature, document, "F");
			if (panelSetOrder.ReferenceLabFinalDate.HasValue == true)
			{
				this.AddNextObxElementBeaker("E-signed", panelSetOrder.ReferenceLabFinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
			}

            this.AddAmendments(document);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
			this.AddNextObxElementBeaker("Specimen Identification", specimenOrder.Description, document, "F");
			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.AddNextObxElementBeaker("Collection Date/Time", collectionDateTimeString, document, "F");

			this.AddNextObxElementBeaker("Breast Testing Fixative", panelSetOrder.BreastTestingFixative, document, "F");
			this.AddNextObxElementBeaker("Interpretation", panelSetOrder.Interpretation, document, "F");
			this.AddNextObxElementBeaker("Method", panelSetOrder.Method, document, "F");
			this.AddNextObxElementBeaker("References", panelSetOrder.Reference, document, "F");
			this.AddNextObxElementBeaker("Test Development", panelSetOrder.ReportDisclaimer, document, "F");
		}
    }
}
