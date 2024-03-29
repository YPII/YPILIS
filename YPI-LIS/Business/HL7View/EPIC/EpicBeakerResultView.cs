﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EPICBeakerResultView : IResultView
    {        
        private int m_ObxCount;        
        private bool m_SendUnsolicited;
        private bool m_Testing;        

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;

        public EPICBeakerResultView(string reportNo, Business.Test.AccessionOrder accessionOrder, bool sendUnsolicted, bool testing)
        {        
            this.m_SendUnsolicited = sendUnsolicted;
            this.m_Testing = testing;
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);                                  

            this.m_OrderingPhysician = Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);           
		}        

        public XElement GetDocument()
        {
            return CreateDocument();
        }

        public void Send(YellowstonePathology.Business.Rules.MethodResult result)
        {                        
            XElement detailDocument = CreateDocument();
            this.WriteDocumentToServer(detailDocument);

            //Dont send pdf if one of these.
            List<int> exclusionList = new List<int>();
            exclusionList.Add(378); //IEP
            exclusionList.Add(379); //SPEP

            if(exclusionList.Any(i => i == this.m_PanelSetOrder.PanelSetId) == false)
            {
                YellowstonePathology.Business.HL7View.EPIC.EPICBeakerResultViewPDF resultViewPdf = new Business.HL7View.EPIC.EPICBeakerResultViewPDF(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, this.m_SendUnsolicited, this.m_Testing);
                YellowstonePathology.Business.Rules.MethodResult methodResultPdf = new Business.Rules.MethodResult();
                resultViewPdf.Send(methodResultPdf);
            }            

            result.Success = true;
            result.Message = "An HL7 message was created and sent to the interface.";
            this.HandleSendToProvation(result);
        }

        public void HandleSendToProvation(Business.Rules.MethodResult result)
        {            
            if (this.m_PanelSetOrder.PanelSetId == 13)
            {
                List<int> providerList = new List<int>();
                providerList.Add(166);
                providerList.Add(688);
                providerList.Add(3887);
                providerList.Add(2190);
                providerList.Add(3069);
                providerList.Add(4062);
                providerList.Add(2458);
                providerList.Add(4793);
                providerList.Add(2101);
                providerList.Add(4929);
                providerList.Add(4748);
                providerList.Add(4942);
                providerList.Add(5197);

                if(providerList.Contains(this.m_AccessionOrder.PhysicianId) == true)
                {
                    XElement detailDocument = CreateDocument();
                    this.WriteDocumentToProvation(detailDocument);                
                }                
            }            
        }

        private XElement CreateDocument()
        {
            XElement document = new XElement("HL7Message");
            this.m_ObxCount = 1;

            EPICHl7Client client = new EPICHl7Client();
            OruR01 messageType = new OruR01();

            string locationCode = "YPIIBILLINGS";
            if (this.m_AccessionOrder.SvhMedicalRecord.StartsWith("A") == true)
            {
                locationCode = "SVHNPATH";
            }

            EPICBeakerMshView msh = new EPICBeakerMshView(client, messageType, locationCode);
            msh.ToXml(document);

            EpicPidView pid = new EpicPidView(this.m_AccessionOrder.SvhMedicalRecord, this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PBirthdate,
                this.m_AccessionOrder.PSex, this.m_AccessionOrder.SvhAccount, this.m_AccessionOrder.PSSN);
            pid.ToXml(document);

            EPICBeakerOrcView orc = new EPICBeakerOrcView(this.m_PanelSetOrder.ExternalOrderId, this.m_OrderingPhysician, this.m_AccessionOrder.MasterAccessionNo, OrderStatusEnum.Complete, this.m_AccessionOrder.SystemInitiatingOrder, this.m_SendUnsolicited);
            orc.ToXml(document);

            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetOrder.ReportNo);

            string resultStatus = ResultStatusEnum.Final.Value;
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(panelSetOrder.ReportNo);
            if (amendmentCollection.Count != 0) resultStatus = ResultStatusEnum.Correction.Value;

            YellowstonePathology.Business.ClientOrder.Model.UniversalServiceCollection universalServiceIdCollection = Business.ClientOrder.Model.UniversalServiceCollection.GetAll();
            YellowstonePathology.Business.ClientOrder.Model.UniversalService universalService = universalServiceIdCollection.GetByUniversalServiceId(panelSetOrder.UniversalServiceId);


            string secondaryExternalOrderId = this.m_AccessionOrder.SecondaryExternalOrderId;
            if (string.IsNullOrEmpty(this.m_PanelSetOrder.SecondaryExternalOrderId) == false) secondaryExternalOrderId = this.m_PanelSetOrder.SecondaryExternalOrderId;

            string externalOrderId = this.m_PanelSetOrder.ExternalOrderId;
            if(this.m_AccessionOrder.SystemInitiatingOrder == "Optime")
            {
                externalOrderId = this.m_AccessionOrder.ExternalOrderId;
            }

            EPICBeakerObrView obr = new EPICBeakerObrView(externalOrderId, secondaryExternalOrderId, this.m_AccessionOrder.MasterAccessionNo, this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder.SpecimenOrderCollection[0].CollectionDate, this.m_AccessionOrder.SpecimenOrderCollection[0].CollectionTime, this.m_AccessionOrder.AccessionDateTime,
                panelSetOrder.FinalTime, this.m_OrderingPhysician, resultStatus, universalService, this.m_SendUnsolicited, this.m_AccessionOrder.SystemInitiatingOrder, this.m_PanelSetOrder.PanelSetId);
            obr.ToXml(document);

            
            EPICBeakerObxView epicObxView = EPICObxViewFactory.GetObxView(panelSetOrder.PanelSetId, this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo, this.m_ObxCount, this.m_SendUnsolicited, false);
            if (epicObxView != null)
            {                
                epicObxView.ToXml(document);                
            }
            else
            {                
                throw new Exception($"{this.m_PanelSetOrder.PanelSetName} needs to be build. {this.m_PanelSetOrder.ReportNo}");
            }                         

            return document;
        }

        private void WriteDocumentToServer(XElement document)
        {
            string fileExtension = ".HL7.xml";

			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			string serverFileName = Business.Document.CaseDocumentPath.GetPath(orderIdParser) + "\\" + this.m_PanelSetOrder.ReportNo + fileExtension;            
            string interfaceFileName = @"\\YPIIInterface2\ChannelData\Outgoing\SCLHealth\" + this.m_PanelSetOrder.ReportNo + fileExtension;            

            if (this.m_Testing == true)
                interfaceFileName = @"\\ypiiinterface2\ChannelData\Outgoing\1002.OLD\BeakerTesting\" + this.m_PanelSetOrder.ReportNo + fileExtension;

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(serverFileName))
            {
                document.Save(sw);
            }

            if (System.IO.File.Exists(interfaceFileName) == false)
            {
                System.IO.File.Copy(serverFileName, interfaceFileName);
            }            
        }

        private void WriteDocumentToProvation(XElement document)
        {
            string fileExtension = ".HL7.xml";

            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);            
            string interfaceFileName = @"\\YPIIInterface2\ChannelData\Outgoing\Provation\" + this.m_PanelSetOrder.ReportNo + fileExtension;            

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(interfaceFileName))
            {
                document.Save(sw);
            }            
        }

        public void CanSend(YellowstonePathology.Business.Rules.MethodResult result)
        {            
            YellowstonePathology.Business.Test.PanelSetOrder pso = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetOrder.ReportNo);

            if (string.IsNullOrEmpty(this.m_OrderingPhysician.Npi) == true)
            {
                result.Message ="The provider NPI is 0.";
                result.Success = false;
            }
            else if (string.IsNullOrEmpty(this.m_AccessionOrder.SvhAccount) == true)
            {
                result.Message = "The SVH Account is blank.";
                result.Success = false;
            }
            else if (string.IsNullOrEmpty(this.m_AccessionOrder.SvhMedicalRecord) == true)
            {
                result.Message = "The SVH MRN is blank.";
                result.Success = false;
            }
            else if (string.IsNullOrEmpty(this.m_AccessionOrder.UniversalServiceId) == true)
            {
                result.Message = "The Universal Serivce Id in the AccessionOrder is blank.";
                result.Success = false;
            }
            else if (string.IsNullOrEmpty(pso.UniversalServiceId) == true)
            {
                result.Message = "The Universal Serivce Id in the PanelSetOrder is blank.";
                result.Success = false;
            }
            else if (this.m_AccessionOrder.SvhMedicalRecord.ToUpper().Contains("W") == true)
            {
                result.Message = "The Medical Record Number has a W in it.";
                result.Success = false;
            }
            else if (this.m_AccessionOrder.SvhMedicalRecord.ToUpper().Contains("W") == true)
            {
                result.Message = "The Medical Record Number has a W in it.";
                result.Success = false;
            }
        }
	}
}
