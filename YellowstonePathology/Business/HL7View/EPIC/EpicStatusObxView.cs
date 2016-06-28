using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EPIC
{
	public class EPICStatusObxView
	{		
		int m_ObxCount;

        public EPICStatusObxView(int obxCount)
		{			
			this.m_ObxCount = obxCount;
		}

		public int ObxCount
		{
			get { return this.m_ObxCount; }
		}

		public void ToXml(XElement document)
		{			
            this.AddNextObxElement("Yellowstone Pathology Institute: Order Is In Process.", document, "F");         
		}                

        public void AddNextObxElement(string value, XElement document, string observationResultStatus)
        {
            XElement obxElement = new XElement("OBX");
            XElement obx01Element = new XElement("OBX.1");
            XElement obx0101Element = new XElement("OBX.1.1", this.m_ObxCount.ToString());
            obx01Element.Add(obx0101Element);
            obxElement.Add(obx01Element);            

            XElement obx02Element = new XElement("OBX.2");
            XElement obx0201Element = new XElement("OBX.2.1", "TX");
            obx02Element.Add(obx0201Element);
            obxElement.Add(obx02Element);

            XElement obx03Element = new XElement("OBX.3");
            XElement obx0301Element = new XElement("OBX.3.1", "&GDT");
            obx03Element.Add(obx0301Element);
            obxElement.Add(obx03Element);

            XElement obx05Element = new XElement("OBX.5");
            XElement obx0501Element = new XElement("OBX.5.1", value);
            obx05Element.Add(obx0501Element);
            obxElement.Add(obx05Element);

            XElement obx11Element = new XElement("OBX.11");
            XElement obx1101Element = new XElement("OBX.11.1", observationResultStatus);
            obx11Element.Add(obx1101Element);
            obxElement.Add(obx11Element);

            this.m_ObxCount++;
            document.Add(obxElement);
        }

        private void HandleLongString(string value, XElement document, string observationResultStatus)
        {
            string[] textSplit = value.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries); 
            foreach (string text in textSplit)
            {
                this.AddNextObxElement(text, document, observationResultStatus);
            }       
        }        
	}
}
