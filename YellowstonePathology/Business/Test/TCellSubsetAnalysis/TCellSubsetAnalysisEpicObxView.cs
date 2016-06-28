/*
 * Created by SharpDevelop.
 * User: william.copland
 * Date: 1/5/2016
 * Time: 10:43 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Xml.Linq;
using YellowstonePathology.Business.Helper;

namespace YellowstonePathology.Business.Test.TCellSubsetAnalysis
{
	/// <summary>
	/// Description of TCellSubsetAnalysisEpicObxView.
	/// </summary>
	public class TCellSubsetAnalysisEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
	{
		public TCellSubsetAnalysisEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
		}

		public override void ToXml(XElement document)
		{
			TCellSubsetAnalysisTestOrder panelSetOrder = (TCellSubsetAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			this.AddHeader(document, panelSetOrder, "B-Cell Enumeration");

			this.AddNextObxElement("", document, "F");
			string result = "CD3 Percent: " + panelSetOrder.CD3Percent.ToString().StringAsPercent();
			this.AddNextObxElement(result, document, "F");
			result = "CD4 Percent: " + panelSetOrder.CD4Percent.ToString().StringAsPercent();
			this.AddNextObxElement(result, document, "F");
			result = "CD8 Percent: " + panelSetOrder.CD8Percent.ToString().StringAsPercent();
			this.AddNextObxElement(result, document, "F");
			string value = string.Empty;
			if(panelSetOrder.CD4CD8Ratio.HasValue) value = Math.Round(panelSetOrder.CD4CD8Ratio.Value, 2).ToString();
			result = "CD4/CD8 Ratio: " + value;
			this.AddNextObxElement(result, document, "F");
            this.AddNextObxElement("Reference Range: " + panelSetOrder.ReferenceRange, document, "F");
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
            this.AddNextObxElement("Interpretation: " + panelSetOrder.Interpretation, document, "F");

            this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Method:", document, "F");
			this.HandleLongString(panelSetOrder.Method, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("References:", document, "F");
			this.HandleLongString(panelSetOrder.References, document, "F");
			this.AddNextObxElement("", document, "F");

            this.AddNextObxElement(panelSetOrder.Disclosure, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement(panelSetOrder.ASRComment, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");

			this.AddNextObxElement(panelSetOrder.GetLocationPerformedComment(), document, "F");
			this.AddNextObxElement(string.Empty, document, "F");
		}
	}
}
