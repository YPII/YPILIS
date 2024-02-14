using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EPICClosingOrderView
    {        
        private int m_ObxCount;        
        private bool m_SendUnsolicited;
        private bool m_Testing;        

        private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;        
        private YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;

        public EPICClosingOrderView(Business.ClientOrder.Model.ClientOrder clientOrder)
        {            
            this.m_ClientOrder = clientOrder;         
            this.m_OrderingPhysician = Business.Gateway.PhysicianClientGateway.GetPhysicianByNpi(clientOrder.ProviderId);           
		}        

        public XElement GetDocument()
        {
            return CreateDocument();
        }

        public void Send(YellowstonePathology.Business.Rules.MethodResult result)
        {                        
            XElement detailDocument = CreateDocument();
            this.WriteDocumentToServer(detailDocument);            

            result.Success = true;
            result.Message = "An HL7 message was created and sent to the interface.";         
        }        

        public XElement CreateDocument()
        {
            XElement document = new XElement("HL7Message");
            this.m_ObxCount = 1;

            EPICHl7Client client = new EPICHl7Client();
            OruR01 messageType = new OruR01();

            string locationCode = "YPIIBILLINGS";
            if (this.m_ClientOrder.SvhMedicalRecord.StartsWith("A") == true)
            {
                locationCode = "SVHNPATH";
            }

            EPICBeakerMshView msh = new EPICBeakerMshView(client, messageType, locationCode);
            msh.ToXml(document);

            EpicPidView pid = new EpicPidView(this.m_ClientOrder.SvhMedicalRecord, this.m_ClientOrder.PLastName, this.m_ClientOrder.PFirstName, this.m_ClientOrder.PBirthdate,
                this.m_ClientOrder.PSex, this.m_ClientOrder.SvhAccountNo, this.m_ClientOrder.PSSN);
            pid.ToXml(document);

            EPICBeakerOrcView orc = new EPICBeakerOrcView(this.m_ClientOrder.ExternalOrderId, this.m_OrderingPhysician, this.m_ClientOrder.MasterAccessionNo, OrderStatusEnum.Complete, this.m_ClientOrder.SystemInitiatingOrder, this.m_SendUnsolicited);
            orc.ToXml(document);           

            string secondaryExternalOrderId = this.m_ClientOrder.SecondaryExternalOrderId;
            if (string.IsNullOrEmpty(this.m_ClientOrder.SecondaryExternalOrderId) == false) secondaryExternalOrderId = this.m_ClientOrder.SecondaryExternalOrderId;

            string externalOrderId = this.m_ClientOrder.ExternalOrderId;
            if(this.m_ClientOrder.SystemInitiatingOrder == "Optime")
            {
                externalOrderId = this.m_ClientOrder.ExternalOrderId;
            }

            Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceSPEP spep = new ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceSPEP();
            EPICBeakerObrViewPDF obr = new EPICBeakerObrViewPDF(externalOrderId, secondaryExternalOrderId, "99-9999", "99-9999", this.m_ClientOrder.OrderDate, this.m_ClientOrder.OrderTime, this.m_ClientOrder.OrderTime,
                            this.m_ClientOrder.OrderTime, this.m_OrderingPhysician, "F", spep, this.m_SendUnsolicited, this.m_ClientOrder.SystemInitiatingOrder, 99, this.m_ClientOrder.ClientId.ToString());
            obr.ToXml(document);

            string observationResultStatus = "F";
            this.AddNextObxElementBeaker("Report No", "99-9999", document, observationResultStatus);
            this.AddNextObxElementBeaker("Serum Protein Electrophoresis", "No result required.", document, observationResultStatus, "No Result Required", false);
            

            return document;
        }

        public void WriteDocumentToServer(XElement document)
        {            
            string interfaceFileName = $@"\\ypiiinterface2\ChannelData\Outgoing\SCLHealth\{this.m_ClientOrder.ClientOrderId}.HL7.xml";            
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(interfaceFileName))
            {
                document.Save(sw);
            }            
        }

        protected void AddNextObxElementBeaker(string fieldName, string fieldValue, XElement document, string observationResultStatus, string referenceRange, bool isCritical)
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
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.3.1", fieldName, obx03Element);
            obxElement.Add(obx03Element);

            XElement obx04Element = new XElement("OBX.4");
            obxElement.Add(obx04Element);

            XElement obx05Element = new XElement("OBX.5");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.5.1", fieldValue, obx05Element);
            obxElement.Add(obx05Element);

            XElement obx06Element = new XElement("OBX.6");
            obxElement.Add(obx06Element);

            XElement obx07Element = new XElement("OBX.7");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.7.1", referenceRange, obx07Element);
            obxElement.Add(obx07Element);

            XElement obx08Element = new XElement("OBX.8");
            if (isCritical == true)
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

        protected void AddNextObxElementBeaker(string fieldName, string fieldValue, XElement document, string observationResultStatus)
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
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.3.1", fieldName, obx03Element);
            obxElement.Add(obx03Element);

            XElement obx04Element = new XElement("OBX.4");
            obxElement.Add(obx04Element);

            XElement obx05Element = new XElement("OBX.5");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.5.1", fieldValue, obx05Element);
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
}
