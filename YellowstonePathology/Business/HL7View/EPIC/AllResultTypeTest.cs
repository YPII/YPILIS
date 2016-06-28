using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class AllResultTypeTest
    {
        private string m_ResultTemplateFilePath = @"C:\EpicTesting\Template\ResultTemplate.xml";        
        private string m_ResultOutgoingPath = @"C:\EpicTesting\Outgoing\";
        private string m_ObxSegmentTemplateFileName = @"C:\EpicTesting\Template\ObxSegmentTemplate.xml";

        private string m_TestOrderFileName = @"C:\EpicTesting\AshtonTestOrder.xml";
        //private string m_TestOrderFileName = @"C:\EpicTesting\DemonTestOrder.xml";
        //private string m_TestOrderFileName = @"C:\EpicTesting\FirstRubyDemon.xml";
                
        private XElement m_TestOrderDocument;

        private EPICUniversalSerivceIdDictionary m_USI;
        private string m_MasterAccessionNo = "2013099123";        

        public AllResultTypeTest()
        {                                    
            this.m_TestOrderDocument = XElement.Load(this.m_TestOrderFileName);
            this.m_USI = new EPICUniversalSerivceIdDictionary();            
        }

        public void Run()
        {
            this.CreateIndividualResultFiles();
            this.CreateSummaryResultFile();
        }

        private void SetHl7Segments(XElement resultDocument, string usiKey, string usiDescription)
        {
            XElement pidElement = this.m_TestOrderDocument.Element("PID");
            resultDocument.Element("PID").ReplaceWith(pidElement);

            XElement orcElement = this.m_TestOrderDocument.Element("ORC");
            resultDocument.Element("ORC").ReplaceWith(orcElement);

            XElement obrElement = this.m_TestOrderDocument.Element("OBR");
            resultDocument.Element("OBR").ReplaceWith(obrElement);
            
            resultDocument.Element("MSH").Element("MSH.7").Element("MSH.7.1").SetValue(DateTime.Now.ToString("yyyyMMddhhmmss"));
            resultDocument.Element("MSH").Element("MSH.10").Element("MSH.10.1").SetValue(Guid.NewGuid().ToString().Replace("-", ""));

            XElement obr3Element = XElement.Parse("<OBR.3><OBR.3.1>" + this.m_MasterAccessionNo + "</OBR.3.1><OBR.3.2>YPILIS</OBR.3.2></OBR.3>");
            resultDocument.Element("OBR").Element("OBR.3").ReplaceWith(obr3Element);

            string escapedUsiDescription = usiDescription.Replace("&", "&amp;");
            XElement obr4Element = XElement.Parse("<OBR.4><OBR.4.1>" + usiKey + "</OBR.4.1><OBR.4.2>" + escapedUsiDescription + "</OBR.4.2></OBR.4>");
            resultDocument.Element("OBR").Element("OBR.4").ReplaceWith(obr4Element);
            
            XElement obr25Element = XElement.Parse("<OBR.25><OBR.25.1>" + ResultStatusEnum.Correction.Value + "</OBR.25.1></OBR.25>");
            resultDocument.Element("OBR").Element("OBR.25").ReplaceWith(obr25Element);

            XElement orc3Element = XElement.Parse("<ORC.3><ORC.3.1>" + this.m_MasterAccessionNo + "</ORC.3.1><ORC.3.2>YPILIS</ORC.3.2></ORC.3>");
            resultDocument.Element("ORC").Element("ORC.3").ReplaceWith(orc3Element);

            XElement orc1Element = XElement.Parse("<ORC.1><ORC.1.1>RE</ORC.1.1></ORC.1>");
            resultDocument.Element("ORC").Element("ORC.1").ReplaceWith(orc1Element);                       
        }

        private void CreateIndividualResultFiles()
        {            
            for (int i = 0; i < this.m_USI.Count; i++)
            {
                int obxCounter = 13;

                string usiKey = this.m_USI.ElementAt(i).Key;
                string usiDescription = this.m_USI.ElementAt(i).Value;

                if (usiKey != "YPI")
                {
                    XElement resultDocument = XElement.Load(this.m_ResultTemplateFilePath);
                    this.SetHl7Segments(resultDocument, usiKey, usiDescription);                    

                    XElement resultLine1 = XElement.Load(this.m_ObxSegmentTemplateFileName);
                    resultLine1.Element("OBX.1").Element("OBX.1.1").SetValue(obxCounter.ToString());
                    resultLine1.Element("OBX.5").Element("OBX.5.1").SetValue(usiDescription + " Result: ");
                    obxCounter += 1;

                    XElement resultLine2 = XElement.Load(this.m_ObxSegmentTemplateFileName);
                    resultLine2.Element("OBX.1").Element("OBX.1.1").SetValue(obxCounter.ToString());
                    resultLine2.Element("OBX.5").Element("OBX.5.1").SetValue("Negative");
                    obxCounter += 1;

                    XElement resultLine3 = XElement.Load(this.m_ObxSegmentTemplateFileName);
                    resultLine3.Element("OBX.1").Element("OBX.1.1").SetValue(obxCounter.ToString());
                    resultLine3.Element("OBX.5").Element("OBX.5.1").SetValue("");
                    obxCounter += 1;

                    resultDocument.Add(resultLine1);
                    resultDocument.Add(resultLine2);
                    resultDocument.Add(resultLine3);

                    string resultFileName = System.IO.Path.Combine(this.m_ResultOutgoingPath, usiKey + ".xml");

                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(resultFileName))
                    {
                        resultDocument.Save(sw);
                    }                    
                }
            }
        }

        private void CreateSummaryResultFile()
        {
            XElement resultDocument = XElement.Load(this.m_ResultTemplateFilePath);

            string usiKey = "YPI";
            string usiDescription = this.m_USI["YPI"];

            this.SetHl7Segments(resultDocument, usiKey, usiDescription);
            int obxCounter = 13;

            for (int i = 0; i < this.m_USI.Count; i++)
            {
                string usiKeyItem = this.m_USI.ElementAt(i).Key;
                string usiDescriptionItem = this.m_USI.ElementAt(i).Value;
                
                if (usiKeyItem != "YPI")
                {                                        
                    XElement resultLine1 = XElement.Load(this.m_ObxSegmentTemplateFileName);
                    resultLine1.Element("OBX.1").Element("OBX.1.1").SetValue(obxCounter.ToString());
                    resultLine1.Element("OBX.5").Element("OBX.5.1").SetValue(usiDescriptionItem + " Result: ");
                    obxCounter += 1;

                    XElement resultLine2 = XElement.Load(this.m_ObxSegmentTemplateFileName);
                    resultLine2.Element("OBX.1").Element("OBX.1.1").SetValue(obxCounter.ToString());
                    resultLine2.Element("OBX.5").Element("OBX.5.1").SetValue("Negative");
                    obxCounter += 1;

                    XElement resultLine3 = XElement.Load(this.m_ObxSegmentTemplateFileName);
                    resultLine3.Element("OBX.1").Element("OBX.1.1").SetValue(obxCounter.ToString());
                    resultLine3.Element("OBX.5").Element("OBX.5.1").SetValue("");
                    obxCounter += 1;

                    resultDocument.Add(resultLine1);
                    resultDocument.Add(resultLine2);
                    resultDocument.Add(resultLine3);
                }
            }

            string resultFileName = System.IO.Path.Combine(this.m_ResultOutgoingPath, "ResultSummary.xml");
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(resultFileName))
            {
                resultDocument.Save(sw);
            } 
        }
    }
}
