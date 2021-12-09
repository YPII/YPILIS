using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.SARSCoV2
{
	public class SARSCoV2EPICOBXView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
		public SARSCoV2EPICOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {            
            SARSCoV2TestOrder panelSetOrder = (SARSCoV2TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);            

            bool isCritical = false;
            if (panelSetOrder.Result == "DETECTED") isCritical = true;
            this.AddNextObxElementBeaker("SARS-Co-V2", panelSetOrder.Result, document, "F", "NOT DETECTED", isCritical);

            this.AddCompanyHeaderNTE(document);
            this.AddNextNTEElement($"Test Name: {panelSetOrder.PanelSetName}", document);
            this.AddNextNTEElement($"Report No: {panelSetOrder.ReportNo}", document);
            this.AddNextNTEElement($"Result: {panelSetOrder.Result}", document);            

            this.AddAmendmentsNTE(document, panelSetOrder, this.m_AccessionOrder);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = specimenOrder.AliquotOrderCollection.GetByAliquotOrderId(panelSetOrder.OrderedOnId);
            string description = specimenOrder.Description;
            this.AddNextNTEElement($"Specimen: {description}", document);

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextNTEElement($"Collection Date/Time: {collectionDateTimeString}", document);

            this.AddNextNTEElement($"Method: {panelSetOrder.Method}", document);
            this.AddNextNTEElement($"References: {panelSetOrder.ReportReferences}", document);            
        }
    }
}
