using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Trichomonas
{
	public class TrichomonasEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public TrichomonasEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			TrichomonasTestOrder panelSetOrder = (TrichomonasTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "Trichomonas Vaginalis");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Result: " +  panelSetOrder.Result, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Reference Range: Negative", document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddAmendments(document);

            this.AddNextObxElement("Specimen Description:", document, "F");
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElement(specimenOrder.Description, document, "F");
            this.AddNextObxElement("", document, "F");

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Method:", document, "F");
            this.HandleLongString(panelSetOrder.Method, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("References:", document, "F");
            this.HandleLongString("Jordan JA, Lowery D, Trucco M. Taqman-Based Detection of Trichomonas vaginalis DNA from female genitals specimens. J Clin Micro 2001; 3819-22", document, "F");
            this.AddNextObxElement("", document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
