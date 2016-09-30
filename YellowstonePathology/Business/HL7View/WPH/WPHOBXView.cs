using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.WPH
{
    public class WPHOBXView
    {
        protected int m_ObxCount;
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;    

        public WPHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_ReportNo = reportNo;
			this.m_ObxCount = obxCount;
        }

        public int ObxCount
        {
            get { return this.m_ObxCount; }
        }

        public virtual void ToXml(XElement document)
        {
            throw new Exception("Not implemented in the base.");
        }

        public virtual void AddAmendments(XElement document)
        {
            Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in panelSetOrder.AmendmentCollection)
            {
                if (amendment.Final == true)
                {
                    this.AddNextObxElement(amendment.AmendmentType + ": " + amendment.AmendmentDate.Value.ToString("MM/dd/yyyy"), document, "C");
                    this.HandleLongString(amendment.Text, document, "C");
                    if (amendment.RequirePathologistSignature == true)
                    {
                        this.AddNextObxElement("Signature: " + amendment.PathologistSignature, document, "C");
                    }
                    this.AddNextObxElement("", document, "C");
                }
            }
        }

        protected void AddHeader(XElement document, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, string title)
        {
            this.AddNextObxElement(string.Empty, document, "F");
            this.AddNextObxElement("Yellowstone Pathology Institute, Inc", document, "F");
            this.AddNextObxElement("2900 12th Ave. North, Ste. 295W", document, "F");
            this.AddNextObxElement("Billings, Mt 59101", document, "F");
            this.AddNextObxElement("(406)238-6360", document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
            this.AddNextObxElement(title, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
            this.AddNextObxElement("Patient Name: " + this.m_AccessionOrder.PatientDisplayName, document, "F");
            this.AddNextObxElement("Master Accession: " + this.m_AccessionOrder.MasterAccessionNo.ToString(), document, "F");
            this.AddNextObxElement("Report No: " + this.m_ReportNo, document, "F");
            this.AddNextObxElement("Encounter: " + this.m_AccessionOrder.SvhAccount, document, "F");
            this.AddNextObxElement("DOB: " + this.m_AccessionOrder.PBirthdate.Value.ToString("MM/dd/yyyy"), document, "F");
            this.AddNextObxElement("Provider: " + this.m_AccessionOrder.PhysicianName, document, "F");            
        }        

        protected void AddNextObxElement(string value, XElement document, string observationResultStatus)
        {            
            string escapedString = value;

            XElement obxElement = new XElement("OBX");
            document.Add(obxElement);

            XElement obx01Element = new XElement("OBX.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.1.1", this.m_ObxCount.ToString(), obx01Element);
            obxElement.Add(obx01Element);

            XElement obx02Element = new XElement("OBX.2");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.2.1", "TX", obx02Element);
            obxElement.Add(obx02Element);

            XElement obx03Element = new XElement("OBX.3");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.3.1", "&GDT", obx03Element);
            obxElement.Add(obx03Element);

            XElement obx04Element = new XElement("OBX.4");
            obxElement.Add(obx04Element);

            XElement obx05Element = new XElement("OBX.5");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.5.1", escapedString, obx05Element);
            obxElement.Add(obx05Element);

            XElement obx06Element = new XElement("OBX.6");
            obxElement.Add(obx06Element);

            XElement obx07Element = new XElement("OBX.7");
            obxElement.Add(obx07Element);

            XElement obx08Element = new XElement("OBX.8");
            obxElement.Add(obx08Element);

            XElement obx09Element = new XElement("OBX.9");
            obxElement.Add(obx09Element);

            XElement obx10Element = new XElement("OBX.10");
            obxElement.Add(obx10Element);

            XElement obx11Element = new XElement("OBX.11");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.11.1", observationResultStatus, obx11Element);
            obxElement.Add(obx11Element);

            this.m_ObxCount += 1;
        }

        protected void HandleLongString(string value, XElement document, string observationResultStatus)
        {
            if (string.IsNullOrEmpty(value) == false)
            {
                string[] textSplit = value.Split(new string[] { "\n", "\r\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string text in textSplit)
                {
                    this.AddNextObxElement(text.Trim(), document, observationResultStatus);
                }
            }
        }		
    }
}
