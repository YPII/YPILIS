using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.WPH
{
    public class WPHOBXCCView
    {
        Business.ClientOrder.Model.ClientOrder m_ClientOrder;

        public WPHOBXCCView(Business.ClientOrder.Model.ClientOrder clientOrder) 
        {
            this.m_ClientOrder = clientOrder;
        }

        private string GetCCNames()
        {
            List<string> ccNames = new List<string>();            
            var rows = System.Text.RegularExpressions.Regex.Split(this.m_ClientOrder.SpecialInstructions, "\r\n|\r|\n");
            if (rows.Length > 0)
            {
                foreach (string s in rows)
                {
                    if(s.Length >= 7)
                    {
                        if (s.Substring(0, 7) == "PATH.CC")
                        {
                            string[] colonSplit = s.Split(':');
                            if (colonSplit.Length > 0)
                            {
                                ccNames.Add(colonSplit[colonSplit.Length - 1].Trim());
                            }
                        }
                    }                    
                }
            }

            string result = null;
            foreach(string ccName in ccNames)
            {
                result += ccName + ";";
            }

            if(string.IsNullOrEmpty(result) == false)
            {
                result = result.Substring(0, result.Length - 1);
            }

            return result;
        }

        public void ToXml(XElement document)
        {            
            int obxCount = 0;
            foreach(var item in document.Elements("OBX"))
            {
                obxCount += 1;
            }
            obxCount += 1;

            string ccNames = this.GetCCNames();
            
            XElement obxElement = new XElement("OBX");
            document.Add(obxElement);

            XElement obx01Element = new XElement("OBX.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.1.1", obxCount.ToString(), obx01Element);
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
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.5.1", "CC:" + ccNames, obx05Element);
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
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.11.1", "F", obx11Element);
            obxElement.Add(obx11Element);                                                                    
        }
    }
}
