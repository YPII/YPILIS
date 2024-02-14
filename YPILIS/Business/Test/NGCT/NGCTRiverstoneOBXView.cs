using System;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.NGCT
{
    public class NGCTRiverstoneOBXView : Business.HL7View.Riverstone.RiverstoneOBXView
    {
        public NGCTRiverstoneOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document, string observationResultStatus)
        {            
            NGCTTestOrder testOrder = (NGCTTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_ReportNo);

            this.AddNextObxWithAttributeElement("NGRESULT^Neisseria gonorrhoeae", testOrder.NeisseriaGonorrhoeaeResult, document, observationResultStatus);
            this.AddNextObxWithAttributeElement("CTRESULT^Chlamydia trachomatis", testOrder.ChlamydiaTrachomatisResult, document,observationResultStatus);

            this.AddNextNteElement($"Report No: {this.m_ReportNo}", document);
            this.AddNextNteElement($"Chlamydia trachomatis result: {testOrder.ChlamydiaTrachomatisResult}", document);
            this.AddNextNteElement($"Neisseria gonorrhoeae result: {testOrder.NeisseriaGonorrhoeaeResult}", document);

            if (amendmentCollection.Count != 0)
            {                
                foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
                {
                    if (amendment.Final == true)
                    {
                        this.AddNextNteElement($"{amendment.AmendmentType}: {amendment.AmendmentDate.Value.ToString("MM/dd/yyyy")}", document);
                        this.AddNextNteElement(amendment.Text, document);
                        if (amendment.RequirePathologistSignature == true)
                        {
                            this.AddNextNteElement($"Signature: {amendment.PathologistSignature}", document);
                            this.AddNextNteElement($"E-signed {amendment.FinalTime.Value.ToString("MM/dd/yyyy HH:mm")}", document);
                        }
                    }
                }
                this.AddNextNteElement(string.Empty, document);                
            }

            this.AddNextNteElement("Specimen: Thin Prep Fluid", document);
            this.AddNextNteElement($"Method: {testOrder.Method}", document);
            this.AddNextNteElement($"References: {testOrder.ReportReferences}", document);
            this.AddNextNteElement($"Test Information: {testOrder.TestInformation}", document);

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.AddNextNteElement($"Location Performed: {locationPerformed}", document);            
        }        
    }
}
