/*
 * Created by SharpDevelop.
 * User: william.copland
 * Date: 1/5/2016
 * Time: 9:49 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using YellowstonePathology.Business.Helper;

namespace YellowstonePathology.Business.Test.BCellEnumeration
{
	/// <summary>
	/// Description of BCellEnumerationEpic.
	/// </summary>
	public class BCellEnumerationEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
	{
		public BCellEnumerationEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
		}

		public override void ToXml(XElement document)
		{
			BCellEnumerationTestOrder panelSetOrder = (BCellEnumerationTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			this.AddHeader(document, panelSetOrder, "B-Cell Enumeration");

			this.AddNextObxElement("", document, "F");
			StringBuilder result = new StringBuilder("WBC: " + panelSetOrder.WBC.ToString() + "/uL (from client)");
			this.AddNextObxElement(result.ToString(), document, "F");
			result = new StringBuilder("Lymphocyte Percentage: " + panelSetOrder.LymphocytePercentage.ToString().StringAsPercent());
			this.AddNextObxElement(result.ToString(), document, "F");			
			result = new StringBuilder("CD19 B-Cell Positive Percent: " + panelSetOrder.CD19BCellPositivePercent.ToString().StringAsPercent());
			this.AddNextObxElement(result.ToString(), document, "F");			
			result = new StringBuilder("CD20 B-Cell Positive Percent: " + panelSetOrder.CD20BCellPositivePercent.ToString().StringAsPercent());
			this.AddNextObxElement(result.ToString(), document, "F");
			result = new StringBuilder("CD19 Absolute Count: " + panelSetOrder.CD19AbsoluteCount.ToString() + "/uL");
			this.AddNextObxElement(result.ToString(), document, "F");
			result = new StringBuilder("CD20 Absolute Count: " + panelSetOrder.CD20AbsoluteCount.ToString() + "/uL");
			this.AddNextObxElement(result.ToString(), document, "F");			

			this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

			this.AddNextObxElement("CD 19+ Lymphocyte  Reference Ranges : Percentage (Absolute Count/uL)", document, "F");
			this.AddNextObxElement("0 – 36 Months, minimum: 11 (430), maximum: 45 (3300)", document, "F");
			this.AddNextObxElement("3 – 17 Years, minimum: 9 (200), maximum: 29 (1300)", document, "F");
			this.AddNextObxElement("Adult, minimum:  4 (32), maximum: 17 (341)", document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("CD 20+ Lymphocyte  Reference Ranges : Percentage (Absolute Count/uL)", document, "F");
			this.AddNextObxElement("0 – 1 Year, minimum: 17.5(773), maximum: 39(1990)", document, "F");
			this.AddNextObxElement("2 - 3 Years, minimum: 15.7(529), maximum: 35.6(1930)", document, "F");
			this.AddNextObxElement("4 - 5 Years, minimum: 11(323), maximum: 26(1000)", document, "F");
			this.AddNextObxElement("6 - 10 Years, minimum: 7.7(212), maximum: 24.3(796)", document, "F");
			this.AddNextObxElement("11 - 18 Years, minimum: 7.1(163), maximum: 27(559)", document, "F");
			this.AddNextObxElement("Adult, minimum: 3(0), maximum: 27(559)", document, "F");
			this.AddNextObxElement("", document, "F");

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
			this.AddNextObxElement("Specimen Description: " + specimenOrder.Description, document, "F");
			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Method:", document, "F");
			this.HandleLongString(panelSetOrder.Method, document, "F");
			
			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("References:", document, "F");
			this.HandleLongString(panelSetOrder.ReportReferences, document, "F");
			this.AddNextObxElement("", document, "F");

			this.HandleLongString(panelSetOrder.ASRComment, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");

			this.HandleLongString(panelSetOrder.GetLocationPerformedComment(), document, "F");
			this.AddNextObxElement(string.Empty, document, "F");
		}
	}
}
