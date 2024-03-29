﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EPICBeakerObxView
    {
        protected int m_ObxCount;
        protected int m_NTECount;        

        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;    

        public EPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_ReportNo = reportNo;
			this.m_ObxCount = obxCount;
            this.m_NTECount = 1;
        }        

        public int ObxCount
        {
            get { return this.m_ObxCount; }
        }

        public int NTECount
        {
            get { return this.m_NTECount; }
        }

        public virtual void ToXml(XElement document)
        {
            throw new Exception("Not implemented in the base.");
        }

        public virtual void AddAmendments(XElement document)
        {
            Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(panelSetOrder.ReportNo);
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
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
            this.AddNextObxElement("Billings, MT 59101", document, "F");
            this.AddNextObxElement("(406)238-6360", document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
            this.AddNextObxElement(title, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
            this.AddNextObxElement("Patient Name: " + this.m_AccessionOrder.PatientDisplayName, document, "F");
            this.AddNextObxElement("Master Accession: " + this.m_AccessionOrder.MasterAccessionNo.ToString(), document, "F");
            this.AddNextObxElement("Report No: " + this.m_ReportNo, document, "F");
            this.AddNextObxElement("MRN: " + this.m_AccessionOrder.SvhMedicalRecord, document, "F");
            this.AddNextObxElement("Encounter: " + this.m_AccessionOrder.SvhAccount, document, "F");
            this.AddNextObxElement("DOB: " + this.m_AccessionOrder.PBirthdate.Value.ToString("MM/dd/yyyy"), document, "F");
            this.AddNextObxElement("Provider: " + this.m_AccessionOrder.PhysicianName, document, "F");
            this.AddNextObxElement("Location: " + this.m_AccessionOrder.ClientName, document, "F");
        }

        protected void AddCompanyHeaderNTE(XElement document)
        {
            this.AddNextNTEElement("Yellowstone Pathology Institute, Inc", document);
            this.AddNextNTEElement("2900 12th Ave. North, Ste. 295W", document);
            this.AddNextNTEElement("Billings, Mt 59101", document);
            this.AddNextNTEElement("(406)238-6360", document);
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

        protected void AddNextObxElementBeaker(string fieldName, string fieldValue, XElement document, string observationResultStatus)
        {
            string escapedString = this.ReplaceSpecialCharacters(fieldValue);

            XElement obxElement = new XElement("OBX");
            document.Add(obxElement);

            XElement obx01Element = new XElement("OBX.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.1.1", this.m_ObxCount.ToString(), obx01Element);
            obxElement.Add(obx01Element);

            XElement obx02Element = new XElement("OBX.2");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.2.1", "TX", obx02Element);
            obxElement.Add(obx02Element);

            XElement obx03Element = new XElement("OBX.3");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.3.1", fieldName, obx03Element);
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

        protected void AddNextObxElementBeaker(string fieldName, string fieldValue, XElement document, string observationResultStatus, string referenceRange, bool isCritical)
        {
            string escapedString = this.ReplaceSpecialCharacters(fieldValue);

            XElement obxElement = new XElement("OBX");
            document.Add(obxElement);

            XElement obx01Element = new XElement("OBX.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.1.1", this.m_ObxCount.ToString(), obx01Element);
            obxElement.Add(obx01Element);

            XElement obx02Element = new XElement("OBX.2");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.2.1", "TX", obx02Element);
            obxElement.Add(obx02Element);

            XElement obx03Element = new XElement("OBX.3");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.3.1", fieldName, obx03Element);
            obxElement.Add(obx03Element);

            XElement obx04Element = new XElement("OBX.4");
            obxElement.Add(obx04Element);

            XElement obx05Element = new XElement("OBX.5");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.5.1", escapedString, obx05Element);
            obxElement.Add(obx05Element);

            XElement obx06Element = new XElement("OBX.6");
            obxElement.Add(obx06Element);

            XElement obx07Element = new XElement("OBX.7");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.7.1", referenceRange, obx07Element);
            obxElement.Add(obx07Element);

            XElement obx08Element = new XElement("OBX.8");
            if(isCritical == true)
            {
                YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.8.1", "A", obx08Element);
            }            
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

        protected string ReplaceSpecialCharacters(string fieldValue)
        {
            string result = fieldValue;
            if (string.IsNullOrEmpty(fieldValue) == false)
            {
                result = result.Trim();
                result = result.Replace("\r\n", @"\.br\").Replace("\n", @"\.br\").Replace("\r", @"\.br\");
                result = result.Replace("&", @"\T\");
                result = result.Replace("~", @"\R\");
                result = result.Replace("^", @"\S\");
            }
            return result;
        }

        public virtual void AddAmendmentsNTE(XElement document, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
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

        public void AddPDFSegments(XElement document)
        {
            string fileName = Business.Document.CaseDocument.GetCaseFileNamePDF(new OrderIdParser(this.m_ReportNo));

            if (System.IO.File.Exists(fileName) == true)
            {
                byte[] bytes = System.IO.File.ReadAllBytes(fileName);
                string base64 = Convert.ToBase64String(bytes);

                XElement obxElement = new XElement("OBX");
                document.Add(obxElement);

                XElement obx01Element = new XElement("OBX.1");
                Business.Helper.XmlDocumentHelper.AddElement("OBX.1.1", this.m_ObxCount.ToString(), obx01Element);
                obxElement.Add(obx01Element);

                XElement obx05Element = new XElement("OBX.5");
                Business.Helper.XmlDocumentHelper.AddElement("OBX.5.2", "PDF", obx05Element);
                Business.Helper.XmlDocumentHelper.AddElement("OBX.5.4", "Base64", obx05Element);
                Business.Helper.XmlDocumentHelper.AddElement("OBX.5.5", base64, obx05Element);
                obxElement.Add(obx05Element);
            }
            else
            {
                throw new Exception($"PDF File not found: {fileName}");
            }
        }

        static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        protected void AddNextPDFOBXElement(XElement document, string line, int obxCount)
        {
            XElement obxElement = new XElement("OBX");
            document.Add(obxElement);

            XElement obx01Element = new XElement("OBX.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.1.1", this.m_ObxCount.ToString(), obx01Element);
            obxElement.Add(obx01Element);

            XElement obx02Element = new XElement("OBX.2");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.2.1", "ED", obx02Element);
            obxElement.Add(obx02Element);

            XElement obx03Element = new XElement("OBX.3");
            obxElement.Add(obx03Element);

            XElement obx04Element = new XElement("OBX.4");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.4.1", "1", obx04Element);
            obxElement.Add(obx04Element);

            XElement obx05Element = new XElement("OBX.5");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.5.1", line, obx05Element);
            obxElement.Add(obx05Element);

            this.m_ObxCount += 1;
        }
    }
}
