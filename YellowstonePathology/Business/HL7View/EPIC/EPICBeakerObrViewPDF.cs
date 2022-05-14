using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EPIC
{
	public class EPICBeakerObrViewPDF
	{        
        private string m_ExternalOrderId;
        private string m_SecondaryExternalOrderId;
        private string m_DateFormat = "yyyyMMddHHmm";

        private string m_MasterAccessionNo;
        private string m_ReportNo;
        private string m_ObservationResultStatus;
        private Nullable<DateTime> m_AccessionTime;
        private Nullable<DateTime> m_CollectionTime;
        private Nullable<DateTime> m_CollectionDate;
        private Nullable<DateTime> m_FinalTime;
        private bool m_SendUnsolicited;
        private string m_SystemInitiatingOrder;
        private int m_PanelSetId;
        private string m_ClientId;

        YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;
        YellowstonePathology.Business.ClientOrder.Model.UniversalService m_UniversalService;

        public EPICBeakerObrViewPDF(string externalOrderId, string secondaryExternalOrderId, string masterAccessionNo, string reportNo, Nullable<DateTime> collectionDate, Nullable<DateTime> collectionTime, Nullable<DateTime> accessionTime, Nullable<DateTime> finalTime,
            YellowstonePathology.Business.Domain.Physician orderingPhysician, string observationResultStatus, YellowstonePathology.Business.ClientOrder.Model.UniversalService universalService, bool sendUnsolicited, string systemInitiatingOrder, int panelSetId, string clientId)
        {         
            this.m_ExternalOrderId = externalOrderId;
            this.m_SecondaryExternalOrderId = secondaryExternalOrderId;
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_ReportNo = reportNo;
            this.m_CollectionTime = collectionTime;
            this.m_CollectionDate = collectionDate;
            this.m_AccessionTime = accessionTime;
            this.m_FinalTime = finalTime;
            this.m_OrderingPhysician = orderingPhysician;
            this.m_ObservationResultStatus = observationResultStatus;
            this.m_UniversalService = universalService;
            this.m_SendUnsolicited = sendUnsolicited;
            this.m_SystemInitiatingOrder = systemInitiatingOrder;
            this.m_PanelSetId = panelSetId;
            this.m_ClientId = clientId;
        }               

        public void ToXml(XElement document)
        {            
            XElement obrElement = new XElement("OBR");
            document.Add(obrElement);

            XElement obr01Element = new XElement("OBR.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.1.1", "1", obr01Element);            
            obrElement.Add(obr01Element);
            
            if (this.OkToAddOBR2() == true)
            {
                XElement obr02Element = new XElement("OBR.2");
                YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.2.1", this.m_ExternalOrderId, obr02Element);
                obrElement.Add(obr02Element);                
            }

            XElement obr03Element = new XElement("OBR.3");            
            string obr3Value = "";

            if(this.m_SendUnsolicited == false && string.IsNullOrEmpty(this.m_ExternalOrderId) == false)
            {
                obr3Value = this.m_ExternalOrderId;
            }
            
            obr03Element.Value = obr3Value;
            obrElement.Add(obr03Element);

            //Lab/Pathology Result
            //Lab/Pathology Result_CC
            string pathologyRecord = "Lab/Pathology Result";
            if (this.m_ClientId == "54") pathologyRecord = "Lab/Pathology Result_CC"; //Billings OB/GYN

            XElement obr04Element = new XElement("OBR.4");                        
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.4.1", pathologyRecord, obr04Element);            
            obrElement.Add(obr04Element);

			YellowstonePathology.Business.Helper.DateTimeJoiner collectionDateJoiner = new Business.Helper.DateTimeJoiner(this.m_CollectionDate.Value, "yyyyMMddHHmm", this.m_CollectionTime, "yyyyMMddHHmm");
            XElement obr07Element = new XElement("OBR.7");            
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.7.1", collectionDateJoiner.DisplayString, obr07Element);
            obrElement.Add(obr07Element);

            XElement obr14Element = new XElement("OBR.14");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.14.1",this.m_AccessionTime.Value.ToString(m_DateFormat) , obr14Element);                        
            obrElement.Add(obr14Element);            

            XElement obr16Element = new XElement("OBR.16");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.1",this.m_OrderingPhysician.Npi.ToString() , obr16Element);                        
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.2",this.m_OrderingPhysician.LastName , obr16Element);                        
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.3",this.m_OrderingPhysician.FirstName , obr16Element);                        
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.4",this.m_OrderingPhysician.MiddleInitial , obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.5", string.Empty, obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.6", string.Empty, obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.7", string.Empty, obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.8", string.Empty, obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.9","NPI" , obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.10", string.Empty, obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.11", string.Empty, obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.12", string.Empty, obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.13", "NPI", obr16Element);                                    
            obrElement.Add(obr16Element);

            if(this.m_SystemInitiatingOrder == "Beaker" && this.m_SendUnsolicited == false)
            {
                XElement obr18Element = new XElement("OBR.18");
                YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.18.1", "Beaker", obr18Element);
                obrElement.Add(obr18Element);
            }

            XElement obr22Element = new XElement("OBR.22");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.22.1", DateTime.Now.ToString(m_DateFormat), obr22Element);                                                
            obrElement.Add(obr22Element);            

            XElement obr25Element = new XElement("OBR.25");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.25.1", this.m_ObservationResultStatus, obr25Element);                        
            obrElement.Add(obr25Element);            
        }

        private bool OkToAddOBR2()
        {
            bool result = true;
            if (this.m_SendUnsolicited == true) result = false;
            if (this.m_SystemInitiatingOrder != "beaker" && this.m_PanelSetId != 13) result = false;
            return result;
        }

        public void WriteUniversalServiceId(XElement obr04Element, int panelSetId)
        {
            string code = string.Empty;
            string testName = string.Empty;            
        }
	}
}
