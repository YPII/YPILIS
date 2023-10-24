using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.NMH
{
    public class NMHNTEView
    {
        protected int m_NteCount;

        public NMHNTEView()
        {            
			this.m_NteCount = 1;
        }

        public void AddCompanyHeader(XElement document)
        {
            this.AddNextNteElement("Yellowstone Pathology Institute, Inc", document);
            this.AddNextNteElement("2900 12th Ave. North, Ste. 295W", document);
            this.AddNextNteElement("Billings, Mt 59101", document);
            this.AddNextNteElement("(406)238-6360", document);
        }        

        public void AddNextNteElement(string value, XElement document)
        {
            XElement nteElement = new XElement("NTE");
            document.Add(nteElement);

            XElement nte01Element = new XElement("NTE.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("NTE.1.1", this.m_NteCount.ToString(), nte01Element);
            nteElement.Add(nte01Element);

            XElement nte02Element = new XElement("NTE.2");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("NTE.2.1", "P", nte02Element);
            nteElement.Add(nte02Element);

            XElement nte03Element = new XElement("NTE.3");
            XElement nte0301Element = new XElement("NTE.3.1", value);
            nte03Element.Add(nte0301Element);
            nteElement.Add(nte03Element);
            
            this.m_NteCount += 1;
        }        
    }
}
