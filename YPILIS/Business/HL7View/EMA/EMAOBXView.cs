using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EMA
{
    public class EMAOBXView
    {
        protected int m_ObxCount;
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public EMAOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
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

        public static void AddPDFSegments(string fileName, XElement document, int obxCount)
        {
            byte[] bytes = System.IO.File.ReadAllBytes(fileName);
            string base64 = Convert.ToBase64String(bytes);            
            AddNextPDFOBXElement(document, "Content - Type: text / plain; charset = US - ASCII;", obxCount);
            obxCount += 1;
            AddNextPDFOBXElement(document, "Content - transfer - encoding: base64", obxCount);
            obxCount += 1;
            AddNextPDFOBXElement(document, string.Empty, obxCount);
            obxCount += 1;
            AddNextPDFOBXElement(document, base64, obxCount);
        }

        protected static void AddNextPDFOBXElement(XElement document, string line, int obxCount)
        {
            XElement obxElement = new XElement("OBX");
            document.Add(obxElement);

            XElement obx01Element = new XElement("OBX.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.1.1", obxCount.ToString(), obx01Element);
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
        }

        public static void AddNextObxElement(string observationCode, string observationValue, XElement document, string observationStatus, int obxCount)
        {                    
            XElement obxElement = new XElement("OBX");
            document.Add(obxElement);

            XElement obx01Element = new XElement("OBX.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.1.1", obxCount.ToString(), obx01Element);
            obxElement.Add(obx01Element);

            XElement obx02Element = new XElement("OBX.2");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.2.1", "TX", obx02Element);
            obxElement.Add(obx02Element);

            XElement obx03Element = new XElement("OBX.3");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.3.1", observationCode, obx03Element);
            obxElement.Add(obx03Element);

            XElement obx04Element = new XElement("OBX.4");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.4.1", "1", obx04Element);
            obxElement.Add(obx04Element);

            XElement obx05Element = new XElement("OBX.5");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.5.1", ReplaceSpecialCharacters(observationValue), obx05Element);
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
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.11.1", observationStatus, obx11Element);
            obxElement.Add(obx11Element);

            XElement obx12Element = new XElement("OBX.12");
            obxElement.Add(obx12Element);

            XElement obx13Element = new XElement("OBX.13");
            obxElement.Add(obx13Element);

            XElement obx14Element = new XElement("OBX.14");
            obxElement.Add(obx14Element);
        }

        public static void AddNextNteElement(string value, XElement document, int nteCount)
        {
            XElement nteElement = new XElement("NTE");
            document.Add(nteElement);

            XElement nte01Element = new XElement("NTE.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("NTE.1.1", nteCount.ToString(), nte01Element);
            nteElement.Add(nte01Element);

            XElement nte02Element = new XElement("NTE.2");            
            nteElement.Add(nte02Element);

            XElement nte03Element = new XElement("NTE.3");            
            XElement nte0301Element = new XElement("NTE.3.1", ReplaceSpecialCharacters(value));
            nte03Element.Add(nte0301Element);

            nteElement.Add(nte03Element);
        }

        public static string ReplaceSpecialCharacters(string fieldValue)
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

        private List<string> GetLines(string value)
        {
            List<string> lines = new List<string>();
            int startIndex = 0;
            int length = 0;
            int maxLineLength = 99;

            if (value.Length > maxLineLength)
            {
                while (true)
                {
                    length = value.LastIndexOf(" ", startIndex + maxLineLength, maxLineLength) - startIndex;
                    if (length != -1)
                    {
                        lines.Add(value.Substring(startIndex, length));
                        startIndex = startIndex + length + 1;
                        if (startIndex + maxLineLength > value.Length)
                        {
                            length = value.Length - startIndex;
                            lines.Add(value.Substring(startIndex, length));
                            break;
                        }
                    }
                    else
                    {

                        length = value.Length - startIndex;
                        lines.Add(value.Substring(startIndex, length));
                        break;
                    }
                }
            }
            else
            {
                lines.Add(value);
            }

            return lines;
        }
    }
}
