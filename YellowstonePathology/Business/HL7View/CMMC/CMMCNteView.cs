using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.CMMC
{
    public class CMMCNteView
    {
        protected int m_NteCount;

        public CMMCNteView()
        {            
			this.m_NteCount = 1;
        }

        protected void AddCompanyHeader(XElement document)
        {
            this.AddNextNteElement("Yellowstone Pathology Institute, Inc", document);
            this.AddNextNteElement("2900 12th Ave. North, Ste. 295W", document);
            this.AddNextNteElement("Billings, Mt 59101", document);
            this.AddNextNteElement("(406)238-6360", document);
        }

        public virtual void ToXml(XElement document)
        {
            throw new Exception("Not implemented in the base.");
        }

        public void AddNextNteElement(string value, XElement document)
        {
            XElement nteElement = new XElement("NTE");
            document.Add(nteElement);

            XElement nte01Element = new XElement("NTE.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("NTE.1.1", this.m_NteCount.ToString(), nte01Element);
            nteElement.Add(nte01Element);

            XElement nte02Element = new XElement("NTE.2");            
            nteElement.Add(nte02Element);

            XElement nte03Element = new XElement("NTE.3");
            XElement nte0301Element = new XElement("NTE.3.1", value);
            nte03Element.Add(nte0301Element);
            nteElement.Add(nte03Element);
            
            this.m_NteCount += 1;
        }

        public void AddBlankNteElement(XElement document)
        {
            XElement nteElement = new XElement("NTE");
            document.Add(nteElement);

            XElement nte01Element = new XElement("NTE.1");
            nteElement.Add(nte01Element);            

            XElement nte0101Element = new XElement("NTE.1.1", this.m_NteCount.ToString());
            nte01Element.Add(nte0101Element);

            this.m_NteCount += 1;
        }

        public void HandleLongString(string value, XElement document)
        {
            if (string.IsNullOrEmpty(value) == false)
            {
                string[] textSplit = value.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string text in textSplit)
                {
                    this.AddNextNteElement(text.Trim(), document);
                }
            }
        }

        public virtual void AddAmendments(XElement document, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in panelSetOrder.AmendmentCollection)
            {
                if (amendment.Final == true)
                {
                    this.AddNextNteElement(amendment.AmendmentType + ": " + amendment.AmendmentDate.Value.ToString("MM/dd/yyyy"), document);
                    this.HandleLongString(amendment.Text, document);
                    if (amendment.RequirePathologistSignature == true)
                    {
                        this.AddNextNteElement("Signature: " + amendment.PathologistSignature, document);
                    }
                    this.AddBlankNteElement(document);
                }
            }
        }
    }
}
