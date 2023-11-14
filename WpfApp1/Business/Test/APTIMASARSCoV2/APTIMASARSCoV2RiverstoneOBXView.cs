using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.APTIMASARSCoV2
{
	public class APTIMASARSCoV2RiverstoneOBXView : Business.HL7View.Riverstone.RiverstoneOBXView
    {
		public APTIMASARSCoV2RiverstoneOBXView(Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document, string resultStatus)
        {            
            APTIMASARSCoV2TestOrder panelSetOrder = (APTIMASARSCoV2TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddNextObxWithAttributeElement("SARSCOV2RESULT^SARS-CoV-2", panelSetOrder.Result, document, resultStatus);
            
            this.AddNextNteElement($"Test Name: {panelSetOrder.PanelSetName}", document);
            this.AddNextNteElement($"Report No: {panelSetOrder.ReportNo}", document);
            this.AddNextNteElement($"Result: {panelSetOrder.Result}", document);            

            //this.AddAmendmentsNTE(document, panelSetOrder, this.m_AccessionOrder);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = specimenOrder.AliquotOrderCollection.GetByAliquotOrderId(panelSetOrder.OrderedOnId);
            string description = specimenOrder.Description;
            this.AddNextNteElement($"Specimen: {description}", document);

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextNteElement($"Collection Date/Time: {collectionDateTimeString}", document);

            this.AddNextNteElement($"Method: {panelSetOrder.Method}", document);
            this.AddNextNteElement($"References: {panelSetOrder.ReportReferences}", document);            
        }
    }
}
