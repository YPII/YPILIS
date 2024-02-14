using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EMA
{
    public class EMAResultView : IResultView
    {        
        private int m_ObxCount;        
        private bool m_SendUnsolicited;
        private bool m_Testing;        

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;

        public EMAResultView(string reportNo, Business.Test.AccessionOrder accessionOrder, bool testing)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
            this.m_Testing = testing;            

            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);

            if (this.m_AccessionOrder.UniversalServiceId.ToUpper() != this.m_PanelSetOrder.UniversalServiceId.ToUpper())
            {
                this.m_SendUnsolicited = true;
            }

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

            result.Success = true;
            result.Message = "An HL7 message was created and sent to the interface.";         
        }

        private XElement CreateDocument()
        {
            XElement document = new XElement("HL7Message");
            this.m_ObxCount = 1;

            EMAHl7Client client = new EMAHl7Client();
            OruR01 messageType = new OruR01();

            string locationCode = "YPIIBILLINGS";            

            EMAMSHView msh = new EMAMSHView(client, messageType, locationCode);
            msh.ToXml(document);

            EMAPIDView pid = new EMAPIDView(this.m_AccessionOrder.SvhMedicalRecord, this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PBirthdate,
                this.m_AccessionOrder.PSex, this.m_AccessionOrder.PSSN);
            pid.ToXml(document);

            Business.Domain.Physician orderingPhysician = Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);

            Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical = (Business.Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetOrder.ReportNo);
            int specimenNumber = 1;            
            foreach (Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
            {
                int obxCount = 1;
                string emaOrderId = this.m_AccessionOrder.ExternalOrderId;
                char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                string emaSpecimenOrderId = $"{emaOrderId}-{alphabet[specimenNumber - 1]}";
                EMAORCView orc = new EMAORCView(emaSpecimenOrderId, emaOrderId, this.m_AccessionOrder.MasterAccessionNo);
                orc.ToXml(document);
                
                EMAOBRView obr = new EMAOBRView(emaSpecimenOrderId, this.m_AccessionOrder.MasterAccessionNo, orderingPhysician);
                obr.ToXml(document);

                //OBX|1|TX|Site^Site|1|RIGHT SUPERIOR LATERAL NECK (SHAVE REMOVAL)||||||F||||
                Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(surgicalSpecimen.SpecimenOrderId);                
                EMAOBXView.AddNextObxElement("Site^Site", specimenOrder.Description, document, "F", obxCount);
                obxCount += 1;
                EMAOBXView.AddNextObxElement("FD^Final Diagnosis", surgicalSpecimen.Diagnosis, document, "F", obxCount);
                obxCount += 1;
                EMAOBXView.AddNextObxElement("CMT^Comment", panelSetOrderSurgical.Comment, document, "F", obxCount);

                specimenNumber += 1;                
            }

            int nteCount = 1;
            EMAOBXView.AddNextNteElement("Gross Description", document, nteCount);
            nteCount += 1;
            EMAOBXView.AddNextNteElement(panelSetOrderSurgical.GrossX, document, nteCount);
            nteCount += 1;
            EMAOBXView.AddNextNteElement("Microscopic Description", document, nteCount);
            nteCount += 1;
            EMAOBXView.AddNextNteElement(panelSetOrderSurgical.MicroscopicX, document, nteCount);

            Business.OrderIdParser orderIdParser = new OrderIdParser(this.m_PanelSetOrder.ReportNo);
            string pdfFileName = Business.Document.CaseDocument.GetCaseFileNamePDF(orderIdParser);
            EMAOBXView.AddPDFSegments(pdfFileName, document, 1);

            /*
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetOrder.ReportNo);            

            ResultStatus resultStatus = ResultStatusEnum.Final;
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(panelSetOrder.ReportNo);
            if (amendmentCollection.Count != 0) resultStatus = ResultStatusEnum.Correction;

            YellowstonePathology.Business.ClientOrder.Model.UniversalServiceCollection universalServiceIdCollection = Business.ClientOrder.Model.UniversalServiceCollection.GetAll();
            YellowstonePathology.Business.ClientOrder.Model.UniversalService universalService = universalServiceIdCollection.GetByUniversalServiceId(panelSetOrder.UniversalServiceId);

            EMAOBRView obr = new EMAOBRView(this.m_PanelSetOrder.ExternalOrderId, this.m_AccessionOrder.MasterAccessionNo, this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder.SpecimenOrderCollection[0].CollectionDate, this.m_AccessionOrder.SpecimenOrderCollection[0].CollectionTime, this.m_AccessionOrder.AccessionDateTime,
                panelSetOrder.FinalTime, this.m_OrderingPhysician, resultStatus.Value, universalService, this.m_SendUnsolicited);
            obr.ToXml(document);            

            Business.PanelSet.Model.PanelSetCollection panelSetCollection = Business.PanelSet.Model.PanelSetCollection.GetAll();
            Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(panelSetOrder.PanelSetId);

            Business.OrderIdParser orderIdParser = new OrderIdParser(this.m_PanelSetOrder.ReportNo);
            string pdfFileName = Business.Document.CaseDocument.GetCaseFileNamePDF(orderIdParser);

            if (panelSet.NmhObxView != null)
            {
                EMAOBXView emaObxView = (EMAOBXView)Activator.CreateInstance(panelSet.EmaObxView, this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo, this.m_ObxCount);
                emaObxView.ToXml(document);
                this.m_ObxCount = emaObxView.ObxCount;
                emaObxView.AddPDFSegments(pdfFileName, document);
            }
            else
            {
                //EMAOBXView emaOBXView = new EMAOBXView(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo, this.m_ObxCount);
                //emaOBXView.AddPDFSegments(pdfFileName, document);
            }
            */

            return document;
        }        

        private void WriteDocumentToServer(XElement document)
        {
            string fileExtension = ".HL7.xml";

			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			string serverFileName = Business.Document.CaseDocumentPath.GetPath(orderIdParser) + "\\" + this.m_PanelSetOrder.ReportNo + fileExtension;
            string interfaceFileName = @"\\APSInterface\ChannelData\YellowstoneDermatology\Production\Out\" + this.m_PanelSetOrder.ReportNo + fileExtension;
            if (this.m_Testing == true) interfaceFileName = @"\\APSInterface\ChannelData\YellowstoneDermatology\Test\Out\" + this.m_PanelSetOrder.ReportNo + fileExtension;            
            
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(serverFileName))
            {
                document.Save(sw);
            }

            if (System.IO.File.Exists(interfaceFileName) == false)
            {
                System.IO.File.Copy(serverFileName, interfaceFileName);
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
        }
	}
}
