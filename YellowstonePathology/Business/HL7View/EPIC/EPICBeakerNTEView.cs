using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EPICBeakerNTEView
    {
        protected int m_NTECount;

        public EPICBeakerNTEView()
        {            
			this.m_NTECount = 1;
        }

        protected void AddCompanyHeader(XElement document)
        {
            this.AddNextNTEElement("Yellowstone Pathology Institute, Inc", document);
            this.AddNextNTEElement("2900 12th Ave. North, Ste. 295W", document);
            this.AddNextNTEElement("Billings, Mt 59101", document);
            this.AddNextNTEElement("(406)238-6360", document);
        }

        public virtual void ToXml(XElement document)
        {
            throw new Exception("Not implemented in the base.");
        }

        public void AddNextNTEElement(string text, XElement document)
        {
            string normalizedText = StringHelper.ReplaceSpecialCharacters(text) + @"\.br\";

            XElement nteElement = new XElement("NTE");
            document.Add(nteElement);

            XElement nte01Element = new XElement("NTE.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("NTE.1.1", this.m_NTECount.ToString(), nte01Element);
            nteElement.Add(nte01Element);

            XElement nte02Element = new XElement("NTE.2");            
            nteElement.Add(nte02Element);

            XElement nte03Element = new XElement("NTE.3");
            XElement nte0301Element = new XElement("NTE.3.1", normalizedText);
            nte03Element.Add(nte0301Element);
            nteElement.Add(nte03Element);
            
            this.m_NTECount += 1;
        }

        public void AddBlankNteElement(XElement document)
        {
            XElement nteElement = new XElement("NTE");
            document.Add(nteElement);

            XElement nte01Element = new XElement("NTE.1");
            nteElement.Add(nte01Element);            

            XElement nte0101Element = new XElement("NTE.1.1", this.m_NTECount.ToString());
            nte01Element.Add(nte0101Element);

            this.m_NTECount += 1;
        }        

        public virtual void AddAmendments(XElement document, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = accessionOrder.AmendmentCollection.GetAmendmentsForReport(panelSetOrder.ReportNo);
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
            {
                if (amendment.Final == true)
                {
                    this.AddNextNTEElement(amendment.AmendmentType + ": " + amendment.AmendmentDate.Value.ToString("MM/dd/yyyy"), document);
                    this.AddNextNTEElement(amendment.Text, document);
                    if (amendment.RequirePathologistSignature == true)
                    {
                        this.AddNextNTEElement("Signature: " + amendment.PathologistSignature, document);
                    }
                    this.AddBlankNteElement(document);
                }
            }
        }
    }
}
