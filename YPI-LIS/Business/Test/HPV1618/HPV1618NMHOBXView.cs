﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HPV1618
{
    public class HPV1618NMHOBXView : YellowstonePathology.Business.HL7View.NMH.NMHOBXViewOld
    {
		public HPV1618NMHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        //REPN
        //HPV16
        //HPV18
        //COM
        //AMEND
        //SPECIMEN
        //METHOD
        //REF
        //LOC

        public override void ToXml(XElement document)
        {
            PanelSetOrderHPV1618 panelSetOrder = (PanelSetOrderHPV1618)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_ReportNo);

            this.AddNextOBXElement("REPN", "Report No", this.m_ReportNo, document, "F");
            this.AddNextOBXElement("HPV16", "HPV-16 Result", panelSetOrder.HPV16Result, document, "F");            
            this.AddNextOBXElement("HPV18", "HPV-18/45 Result", panelSetOrder.HPV18Result, document, "F");            

            if (string.IsNullOrEmpty(panelSetOrder.Comment) == false)
            {
                this.AddNextOBXElement("COM", "Comment", "Comment" + panelSetOrder.Comment, document, "F");
            }

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
                this.AddNextOBXElement("AMEND", "Amendments", amendments.ToString(), document, "F");
            }

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(panelSetOrder.OrderedOnId);
            this.AddNextOBXElement("SPECIMEN", "Specimen", specimenOrder.GetDescription(), document, "F");

            this.AddNextOBXElement("METHOD", "Method", panelSetOrder.Method, document, "F");

            this.AddNextOBXElement("REF", "References", panelSetOrder.ReportReferences, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextOBXElement("LOC", "Location Performed", locationPerformed, document, "F");
        }
    }
}
