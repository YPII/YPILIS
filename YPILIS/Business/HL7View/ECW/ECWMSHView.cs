using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.ECW
{
	public class ECWMSHView
	{
        public static string CLIANUMBER = "27D0946844";

        Hl7Client m_Client;
        Hl7MessageType m_MessageType;
        string m_LocationCode;

        public ECWMSHView(Hl7Client client, Hl7MessageType messageType, string locationCode)
		{
            this.m_Client = client;
            this.m_MessageType = messageType;
            this.m_LocationCode = locationCode;
		}       

        public void ToXml(XElement document)
        {
            Guid guid = Guid.NewGuid();
            string messageControlId = guid.ToString("N");

            XElement mshElement = new XElement("MSH");
            document.Add(mshElement);

            XElement msh01Element = new XElement("MSH.1", "|");
            mshElement.Add(msh01Element);

            XElement msh02Element = new XElement("MSH.2", "^~\\&");
            mshElement.Add(msh02Element);

            XElement msh03Element = new XElement("MSH.3");
            XElement msh0301Element = new XElement("MSH.3.1", "YPIILIS");
            msh03Element.Add(msh0301Element);
            mshElement.Add(msh03Element);
            
            XElement msh04Element = new XElement("MSH.4");
            XElement msh0401Element = new XElement("MSH.4.1", m_LocationCode);                        
            msh04Element.Add(msh0401Element);            
            mshElement.Add(msh04Element);

            XElement msh05Element = new XElement("MSH.5");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("MSH.5.1", this.m_Client.ReceivingApplication, msh05Element);            
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(mshElement, msh05Element);

            XElement msh06Element = new XElement("MSH.6");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("MSH.6.1", this.m_Client.ReceivingFacility, msh06Element);            
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(mshElement, msh06Element);                       

            XElement msh07Element = new XElement("MSH.7");
            XElement msh0701Element = new XElement("MSH.7.1", DateTime.Now.ToString("yyyyMMddhhmmss"));
            msh07Element.Add(msh0701Element);
            mshElement.Add(msh07Element);

            XElement msh09Element = new XElement("MSH.9");
            XElement msh0901Element = new XElement("MSH.9.1", this.m_MessageType.MessageType);
            XElement msh0902Element = new XElement("MSH.9.2", this.m_MessageType.TriggerEvent);
            msh09Element.Add(msh0901Element);
            msh09Element.Add(msh0902Element);
            mshElement.Add(msh09Element);

            XElement msh10Element = new XElement("MSH.10");
            XElement msh1001Element = new XElement("MSH.10.1", messageControlId);
            msh10Element.Add(msh1001Element);
            mshElement.Add(msh10Element);

            XElement msh11Element = new XElement("MSH.11");
            XElement msh1101Element = new XElement("MSH.11.1", "P");
            msh11Element.Add(msh1101Element);
            mshElement.Add(msh11Element);

            XElement msh12Element = new XElement("MSH.12");
            XElement msh1201Element = new XElement("MSH.12.1", "2.3");
            msh12Element.Add(msh1201Element);
            mshElement.Add(msh12Element);
        }        
	}
}
