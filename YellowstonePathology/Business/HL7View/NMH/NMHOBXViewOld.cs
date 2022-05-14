using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.NMH
{
    public class NMHOBXViewOld
    {
        protected int m_ObxCount;        
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;    

        public NMHOBXViewOld(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
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

        public void AddPDFSegments(string fileName, XElement document)
        {            
            byte[] bytes = System.IO.File.ReadAllBytes(fileName);
            string base64 = Convert.ToBase64String(bytes);
            IEnumerable<string> lines = Split(base64, 60);

            int obxCount = 1;
            AddNextPDFOBXElement(document, "Content - Type: text / plain; charset = US - ASCII;", obxCount);
            obxCount += 1;
            AddNextPDFOBXElement(document, "Content - transfer - encoding: base64", obxCount);
            obxCount += 1;
            AddNextPDFOBXElement(document, string.Empty, obxCount);
            obxCount += 1;

            AddNextPDFOBXElement(document, base64, obxCount);                       
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

        protected void AddNextOBXElement(string fieldAbbreviation, string fieldName, string fieldValue, XElement document, string observationResultStatus)
        {
            string sanitizedFielValue = this.ReplaceSpecialCharacters(fieldValue);
            string [] delimeter = new string[1] { @"\.br\" };
            string [] rawLines = sanitizedFielValue.Split(delimeter, StringSplitOptions.RemoveEmptyEntries);

            List<string> brokenLines = new List<string>();
            SplitLinesAt92Char(rawLines, brokenLines);

            foreach (string line in brokenLines)
            {
                XElement obxElement = new XElement("OBX");
                document.Add(obxElement);

                XElement obx01Element = new XElement("OBX.1");
                YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.1.1", this.m_ObxCount.ToString(), obx01Element);
                obxElement.Add(obx01Element);

                XElement obx02Element = new XElement("OBX.2");
                YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.2.1", "TX", obx02Element);
                obxElement.Add(obx02Element);

                XElement obx03Element = new XElement("OBX.3");
                YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.3.1", "S&" + fieldAbbreviation, obx03Element);
                YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.3.2", fieldName, obx03Element);                
                obxElement.Add(obx03Element);

                XElement obx04Element = new XElement("OBX.4");
                obxElement.Add(obx04Element);

                XElement obx05Element = new XElement("OBX.5");
                YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.5.1", line, obx05Element);
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
        }                    

        private void SplitLinesAt92Char(string [] rawLines, List<string> brokenlines)
        {
            int partLength = 92;
            foreach(string line in rawLines)
            {
                string[] words = line.Split(' ');
                var parts = new Dictionary<int, string>();
                string part = string.Empty;
                int partCounter = 0;
                foreach (var word in words)
                {
                    if (part.Length + word.Length < partLength)
                    {
                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                    }
                    else
                    {
                        parts.Add(partCounter, part);
                        part = word;
                        partCounter++;
                    }
                }

                parts.Add(partCounter, part);
                foreach (var item in parts)
                {
                    brokenlines.Add(item.Value);             
                }
            }            
        }

        private string ReplaceSpecialCharacters(string fieldValue)
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
    }
}
