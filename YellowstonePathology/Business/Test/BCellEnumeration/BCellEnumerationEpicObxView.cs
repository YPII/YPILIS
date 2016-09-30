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
			StringBuilder result = new StringBuilder("WBC: " + panelSetOrder.WBC.ToString());
			this.AddNextObxElement(result.ToString(), document, "F");
			result = new StringBuilder("Lymphocyte Percentage: " + panelSetOrder.LymphocytePercentage.ToString().StringAsPercent());
			this.AddNextObxElement(result.ToString(), document, "F");			
			result = new StringBuilder("CD19 B-Cell Positive Percent: " + panelSetOrder.CD19BCellPositivePercent.ToString().StringAsPercent());
			this.AddNextObxElement(result.ToString(), document, "F");			
			result = new StringBuilder("CD20 B-Cell Positive Percent: " + panelSetOrder.CD20BCellPositivePercent.ToString().StringAsPercent());
			this.AddNextObxElement(result.ToString(), document, "F");
			result = new StringBuilder("CD19 Absolute Count: " + panelSetOrder.CD19AbsoluteCount.ToString());
			this.AddNextObxElement(result.ToString(), document, "F");
			result = new StringBuilder("CD20 Absolute Count: " + panelSetOrder.CD20AbsoluteCount.ToString());
			this.AddNextObxElement(result.ToString(), document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
			if (panelSetOrder.FinalTime.HasValue == true)
			{
				this.AddNextObxElement("E-signed " + panelSetOrder.FinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
			}

			this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen Information:", document, "F");
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
			this.AddNextObxElement("Specimen Identification: " + specimenOrder.Description, document, "F");
			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Method:", document, "F");
			this.HandleLongString(panelSetOrder.Method, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("References:", document, "F");
			this.HandleLongString(panelSetOrder.ReportReferences, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement(panelSetOrder.ASRComment, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");

			this.AddNextObxElement(panelSetOrder.GetLocationPerformedComment(), document, "F");
			this.AddNextObxElement(string.Empty, document, "F");
		}
	}
}
