using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ReticulatedPlateletAnalysisV2
{
    public class ReticulatedPlateletAnalysisV2EPICBeakerOBXView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
        public ReticulatedPlateletAnalysisV2EPICBeakerOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            ReticulatedPlateletAnalysisV2TestOrder panelSetOrder = (ReticulatedPlateletAnalysisV2TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            
            this.AddNextObxElementBeaker("Test", panelSetOrder.PanelSetName, document, "F");

            this.AddNextObxElementBeaker("Result", panelSetOrder.Result, document, "F");
            this.AddNextObxElementBeaker("Reference", panelSetOrder.ResultReference, document, "F");            

            this.AddAmendments(document);
           
            this.AddNextObxElementBeaker("Antibodies Used",  "CD41, Thiozole Orange", document, "F");

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElementBeaker("Specimen Description", specimenOrder.Description, document, "F");            

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElementBeaker("Collection Date/Time", collectionDateTimeString, document, "F");            

            this.AddNextObxElementBeaker("Method", "Quantitative Flow Cytometry", document, "F");            

            this.AddNextObxElementBeaker("Test Development", panelSetOrder.ASRComment, document, "F");            

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElementBeaker("Location Performed", locationPerformed, document, "F");            
        }
    }
}
