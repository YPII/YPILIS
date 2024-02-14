using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.JAK2Exon1214
{
	public class JAK2Exon1214EPICOBXView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
	{
		public JAK2Exon1214EPICOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
		}

		public override void ToXml(XElement document)
		{			
			Business.HL7View.EPIC.EPICBeakerNarrativeOBXView.AddElement(document);

			JAK2Exon1214TestOrder panelSetOrder = (JAK2Exon1214TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			
			this.AddNextObxElementBeaker("Report No", this.m_ReportNo, document, "F");			
			this.AddNextObxElementBeaker("JAK2 Exon", panelSetOrder.Result, document, "F", "Not Detected", false);

			this.AddNextObxElementBeaker("Pathologist", panelSetOrder.Signature, document, "F");
			if (panelSetOrder.FinalTime.HasValue == true)
			{
				this.AddNextObxElementBeaker("E-signed", panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
			}

			Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_ReportNo);
			if (amendmentCollection.Count != 0)
			{
				StringBuilder amendments = new StringBuilder();
				foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
				{
					if (amendment.Final == true)
					{
						amendments.AppendLine(amendment.AmendmentType + ": " + amendment.AmendmentDate.Value.ToString("MM/dd/yyyy"));
						amendments.AppendLine(amendment.Text);
						if (amendment.RequirePathologistSignature == true)
						{
							amendments.AppendLine("Signature" + amendment.PathologistSignature);
							amendments.AppendLine("E-signed" + amendment.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"));
						}
					}
				}
				amendments.AppendLine();
				this.AddNextObxElementBeaker("Amendments", amendments.ToString(), document, "C");
			}

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);			
			this.AddNextObxElementBeaker("Specimen Identification", specimenOrder.Description, document, "F");
			string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.AddNextObxElementBeaker("Collection Date/Time", collectionDateTimeString, document, "F");			
			this.AddNextObxElementBeaker("Interpretation", panelSetOrder.Interpretation, document, "F");			
			this.AddNextObxElementBeaker("Method", panelSetOrder.Method, document, "F");						
			this.AddNextObxElementBeaker("References", panelSetOrder.ReportReferences, document, "F");			
			
            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
			this.AddNextObxElementBeaker("Location Performed", locationPerformed, document, "F");			
		}
    }
}
