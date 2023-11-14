using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EPIC
{
	public class EPICStatusObrView
	{        
        private string m_ExternalOrderId;
        private string m_DateFormat = "yyyyMMddHHmm";
        private string m_YpiOrderId;
        private string m_ObservationResultStatus;
        private Nullable<DateTime> m_AccessionTime;
        private Nullable<DateTime> m_FinalTime;

        YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;
        YellowstonePathology.Business.ClientOrder.Model.UniversalService m_UniversalService;

        public EPICStatusObrView(string externalOrderId, string ypiOrderId, Nullable<DateTime> accessionTime, Nullable<DateTime> finalTime, 
            YellowstonePathology.Business.Domain.Physician orderingPhysician, string observationResultStatus, YellowstonePathology.Business.ClientOrder.Model.UniversalService universalService)
        {         
            this.m_ExternalOrderId = externalOrderId;
            this.m_YpiOrderId = ypiOrderId;
            this.m_AccessionTime = accessionTime;
            this.m_FinalTime = finalTime;
            this.m_OrderingPhysician = orderingPhysician;
            this.m_ObservationResultStatus = observationResultStatus;
            this.m_UniversalService = universalService;
        }               

        public void ToXml(XElement document)
        {
            XElement obrElement = new XElement("OBR");
            document.Add(obrElement);

            XElement obr01Element = new XElement("OBR.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.1.1", "1", obr01Element);            
            obrElement.Add(obr01Element);
            
            XElement obr02Element = new XElement("OBR.2");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.2.1", this.m_ExternalOrderId, obr02Element);                        
            obrElement.Add(obr02Element);

            if (string.IsNullOrEmpty(this.m_YpiOrderId) == false)
            {
                XElement obr03Element = new XElement("OBR.3");
                YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.3.1", this.m_YpiOrderId, obr03Element);
                YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.3.2", "YPILIS", obr03Element);
                obrElement.Add(obr03Element);
            }

            XElement obr04Element = new XElement("OBR.4");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.4.1", this.m_UniversalService.UniversalServiceId, obr04Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.4.2", this.m_UniversalService.ServiceName, obr04Element);             
            obrElement.Add(obr04Element);            

            XElement obr07Element = new XElement("OBR.7");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.7.1", this.m_AccessionTime.Value.ToString(m_DateFormat), obr07Element);
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

            XElement obr22Element = new XElement("OBR.22");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.22.1", DateTime.Now.ToString(m_DateFormat), obr22Element);                                                
            obrElement.Add(obr22Element);            

            XElement obr25Element = new XElement("OBR.25");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.25.1", this.m_ObservationResultStatus, obr25Element);
            obrElement.Add(obr25Element);            
        }
	}
}
