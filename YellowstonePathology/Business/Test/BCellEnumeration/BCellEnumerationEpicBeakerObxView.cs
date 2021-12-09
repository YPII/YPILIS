using System;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using YellowstonePathology.Business.Helper;

namespace YellowstonePathology.Business.Test.BCellEnumeration
{
	public class BCellEnumerationEPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
	{
		public BCellEnumerationEPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
		}

		public override void ToXml(XElement document)
		{
			BCellEnumerationTestOrder panelSetOrder = (BCellEnumerationTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);			

			this.AddNextObxElementBeaker("Test Name", "B-Cell Enumeration", document, "F");			
			this.AddNextObxElementBeaker("WBC", panelSetOrder.WBC.ToString() + "/uL (from client)", document, "F");			
			this.AddNextObxElementBeaker("Lymphocyte Percentage", panelSetOrder.LymphocytePercentage.ToString().StringAsPercent(), document, "F");						
			this.AddNextObxElementBeaker("CD19 B-Cell Positive Percent", panelSetOrder.CD19BCellPositivePercent.ToString().StringAsPercent(), document, "F");						
			this.AddNextObxElementBeaker("CD20 B-Cell Positive Percent", panelSetOrder.CD20BCellPositivePercent.ToString().StringAsPercent(), document, "F");			
			this.AddNextObxElementBeaker("CD19 Absolute Count", panelSetOrder.CD19AbsoluteCount.ToString() + "/uL", document, "F");			
			this.AddNextObxElementBeaker("CD20 Absolute Count", panelSetOrder.CD20AbsoluteCount.ToString() + "/uL", document, "F");			
			
            this.AddAmendments(document);

			StringBuilder referenceRange = new StringBuilder();
			referenceRange.AppendLine("CD 19+ Lymphocyte  Reference Ranges : Percentage (Absolute Count/uL)");
			referenceRange.AppendLine("0 - 36 Months, minimum: 11 (430), maximum: 45 (3300)");
			referenceRange.AppendLine("3 - 17 Years, minimum: 9 (200), maximum: 29 (1300)");
			referenceRange.AppendLine("Adult, minimum:  4 (32), maximum: 17 (341)");
			referenceRange.AppendLine("CD 20+ Lymphocyte  Reference Ranges : Percentage (Absolute Count/uL)");
			referenceRange.AppendLine("0 - 1 Year, minimum: 17.5(773), maximum: 39(1990)");
			referenceRange.AppendLine("2 - 3 Years, minimum: 15.7(529), maximum: 35.6(1930)");
			referenceRange.AppendLine("4 - 5 Years, minimum: 11(323), maximum: 26(1000)");
			referenceRange.AppendLine("6 - 10 Years, minimum: 7.7(212), maximum: 24.3(796)");
			referenceRange.AppendLine("11 - 18 Years, minimum: 7.1(163), maximum: 23.8(600)");
			referenceRange.AppendLine("Adult, minimum: 3(0), maximum: 27(559)");
			this.AddNextObxElementBeaker("Reference Range", referenceRange.ToString(), document, "F");

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
			this.AddNextObxElementBeaker("Specimen Description", specimenOrder.Description, document, "F");
			string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.AddNextObxElementBeaker("Collection Date/Time", collectionDateTimeString, document, "F");			
			this.AddNextObxElementBeaker("Method", panelSetOrder.Method, document, "F");						
			this.AddNextObxElementBeaker("References", panelSetOrder.ReportReferences, document, "F");
			this.AddNextObxElementBeaker("Test Development", panelSetOrder.ASRComment, document, "F");			
			this.AddNextObxElementBeaker("Location Performed", panelSetOrder.GetLocationPerformedComment(), document, "F");
		}
	}
}
