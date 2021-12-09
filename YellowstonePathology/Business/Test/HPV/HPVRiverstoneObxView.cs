using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HPV
{
    public class HPVRiverstoneObxView : Business.HL7View.Riverstone.RiverstoneOBXView
    {

        public HPVRiverstoneObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document, string observationResultStatus)
        {         
            HPVTestOrder panelSetOrder = (HPVTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_ReportNo);

            this.AddNextObxWithAttributeElement("HPVRESULT^HPV Result", panelSetOrder.Result, document, observationResultStatus);

            this.AddNextNteElement($"Report No: {this.m_ReportNo}", document);            
            this.AddNextNteElement($"HPV Result: {panelSetOrder.Result}", document);

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
                this.AddNextNteElement($"Amendments: {amendments.ToString()}", document);
            }

            this.AddNextNteElement("Specimen: ThinPrep fluid", document);

            this.AddNextNteElement($"Test Information: {panelSetOrder.TestInformation}", document);

            this.AddNextNteElement($"References {panelSetOrder.ReportReferences}", document);

            this.AddNextNteElement($"ASR {panelSetOrder.ASRComment}", document);

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextNteElement($"Location Performed {locationPerformed}", document);
        }
    }
}
