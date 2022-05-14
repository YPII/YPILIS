using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EPICFT1ResultView
    {
        private Business.Test.AccessionOrder m_AccessionOrder;
        private Business.Test.PanelSetOrderCPTCodeBill m_PanelSetOrderCPTCodeBill;
        private Business.Test.PanelSetOrder m_PanelSetOrder;

        public EPICFT1ResultView(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill)
        {                        
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrderCPTCodeBill = panelSetOrderCPTCodeBill;
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetOrderCPTCodeBill.ReportNo);
        }                       

        public void Publish(string basePath)
        {            
            XElement document = new XElement("HL7Message");

            Hl7Client client = null;
            YellowstonePathology.Business.Client.Model.ClientGroupClientCollection hrhGroup = Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId("2");
            if(hrhGroup.ClientIdExists(this.m_AccessionOrder.ClientId) == true)
            {
                client = new EPICHRHClient();                
            }
            else
            {
                client = new EPICHl7Client();
            }

            DFTP03 messageType = new DFTP03();
            
            YellowstonePathology.Business.Domain.Physician orderingPhysician = Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);

            string locationCode = "YPIIBILLINGS";            

            EPICMshView msh = new EPICMshView(client, messageType, locationCode);
            msh.ToXml(document);

            EpicPidView pid = new EpicPidView(this.m_PanelSetOrderCPTCodeBill.MedicalRecord, this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PBirthdate,
                this.m_AccessionOrder.PSex, this.m_PanelSetOrderCPTCodeBill.Account, this.m_AccessionOrder.PSSN);
            pid.ToXml(document);
            
            Business.Billing.Model.CptCode cptCode = Store.AppDataStore.Instance.CPTCodeCollection.GetClone(this.m_PanelSetOrderCPTCodeBill.CPTCode, this.m_PanelSetOrderCPTCodeBill.Modifier);
                
            DateTime transactionDate = m_AccessionOrder.CollectionDate.Value;
            DateTime transactionPostingDate = this.m_PanelSetOrderCPTCodeBill.PostDate.Value;

            EPICFT1View epicFT1View = new EPICFT1View(cptCode, transactionDate, transactionPostingDate, this.m_PanelSetOrderCPTCodeBill.Quantity.ToString(), orderingPhysician, this.m_AccessionOrder.MasterAccessionNo, this.m_AccessionOrder.CollectionDate.Value);
            epicFT1View.ToXml(document, 1);
                        
            string fileName = System.IO.Path.Combine(basePath, this.m_PanelSetOrderCPTCodeBill.PanelSetOrderCPTCodeBillId + "." + cptCode.Code + ".hl7.xml");
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName))
            {
                document.Save(sw);
            }
        }        
    }
}
