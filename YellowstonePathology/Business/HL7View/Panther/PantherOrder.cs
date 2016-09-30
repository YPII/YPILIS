using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.Panther
{
    public class PantherOrder
    {                        
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
        private YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;
        private PantherAssay m_Assay;
        private string m_ActionCode;

        public PantherOrder(PantherAssay assay, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AliquotOrder aliquotOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder,
            string actionCode)
        {            
            this.m_Assay = assay;
            this.m_SpecimenOrder = specimenOrder;
            this.m_AliquotOrder = aliquotOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = panelSetOrder;
            this.m_ActionCode = actionCode;		
		}                

        public void Send()
        {                        
            XElement document = CreateDocument();
            this.WriteDocumentToServer(document);            
        }

        private XElement CreateDocument()
        {
            XElement document = new XElement("HL7Message");            

            PantherMSH msh = new PantherMSH();
            msh.ToXml(document);

            PantherPID pid = new PantherPID(this.m_AccessionOrder.PatientId, this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PBirthdate, this.m_AccessionOrder.PSex);
            pid.ToXml(document);

            PantherORC orc = new PantherORC(this.m_SpecimenOrder, this.m_AliquotOrder, this.m_PanelSetOrder, this.m_ActionCode);
            orc.ToXml(document);

            PantherOBR obr = new PantherOBR(this.m_PanelSetOrder.ReportNo, this.m_Assay, this.m_SpecimenOrder);
            obr.ToXml(document);                                   

            return document;
        }

        private void WriteDocumentToServer(XElement document)
        {
            string fileExtension = ".HL7.xml";

			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			string serverFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + "\\" + this.m_PanelSetOrder.ReportNo + fileExtension;
            string interfaceFileName = @"\\ypiiinterface1\ChannelData\Outgoing\Panther\" + this.m_PanelSetOrder.ReportNo + fileExtension;            
            
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(serverFileName))
            {
                document.Save(sw);
            }

            if (System.IO.File.Exists(interfaceFileName) == false)
            {
                System.IO.File.Copy(serverFileName, interfaceFileName);
            }            
        }        
	}
}
