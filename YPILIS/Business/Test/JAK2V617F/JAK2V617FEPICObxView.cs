using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.JAK2V617F
{
	public class JAK2V617FEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {        
        public JAK2V617FEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int nteCount) 
            : base(accessionOrder, reportNo, nteCount)
		{
            
		}

        public override void ToXml(XElement document)
        {
			JAK2V617FTestOrder panelSetOrder = (JAK2V617FTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);            
            this.AddNextObxElementBeaker("Result", panelSetOrder.Result, document, "F");            
            this.AddNextObxElementBeaker("Pathologist", panelSetOrder.ReferenceLabSignature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElementBeaker("E-signed ", panelSetOrder.ReferenceLabFinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
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
                            amendments.AppendLine("Signature: " + amendment.PathologistSignature);
                            amendments.AppendLine("E-signed " + amendment.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"));
                        }
                    }
                }
                amendments.AppendLine();
                this.AddNextObxElementBeaker("Amendments", amendments.ToString(), document, "C");
            }

            this.AddNextObxElementBeaker("Interpretation", panelSetOrder.Interpretation, document, "F");                       			
            this.AddNextObxElementBeaker("Method", panelSetOrder.Method, document, "F");            
            this.AddNextObxElementBeaker("References", panelSetOrder.Reference, document, "F");                        
            this.AddNextObxElementBeaker("Disclosure", panelSetOrder.Disclosure, document, "F");
            
            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElementBeaker("Location Performed", locationPerformed, document, "F");            
        }
    }
}
