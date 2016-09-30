using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.WPH
{
	public class WPHOBRView
    {        
        private string m_ExternalOrderId;
        private string m_DateFormat = "yyyyMMddHHmm";

        private string m_MasterAccessionNo;
        private string m_ReportNo;
        private string m_ObservationResultStatus;
        private Nullable<DateTime> m_AccessionTime;
        private Nullable<DateTime> m_CollectionTime;
        private Nullable<DateTime> m_CollectionDate;
        private Nullable<DateTime> m_FinalTime;
        private bool m_SendUnsolicited;

        private YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;
        private YellowstonePathology.Business.ClientOrder.Model.UniversalService m_UniversalService;
        private YellowstonePathology.Business.User.SystemUser m_SigningPathologist;

        public WPHOBRView(string externalOrderId, string masterAccessionNo, string reportNo, Nullable<DateTime> collectionDate, Nullable<DateTime> collectionTime, Nullable<DateTime> accessionTime, Nullable<DateTime> finalTime,
            YellowstonePathology.Business.Domain.Physician orderingPhysician, YellowstonePathology.Business.User.SystemUser signingPathologist, string observationResultStatus, YellowstonePathology.Business.ClientOrder.Model.UniversalService universalService, bool sendUnsolicited)
        {         
            this.m_ExternalOrderId = externalOrderId;
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_ReportNo = reportNo;
            this.m_CollectionTime = collectionTime;
            this.m_CollectionDate = collectionDate;
            this.m_AccessionTime = accessionTime;
            this.m_FinalTime = finalTime;
            this.m_OrderingPhysician = orderingPhysician;
            this.m_SigningPathologist = signingPathologist;
            this.m_ObservationResultStatus = observationResultStatus;
            this.m_UniversalService = universalService;
            this.m_SendUnsolicited = sendUnsolicited;
        }               

        public void ToXml(XElement document)
        {            
            XElement obrElement = new XElement("OBR");
            document.Add(obrElement);

            XElement obr01Element = new XElement("OBR.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.1.1", "1", obr01Element);            
            obrElement.Add(obr01Element);
            

            //If this is an add on test then do not write this element.
            XElement obr02Element = new XElement("OBR.2");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.2.1", this.m_ExternalOrderId, obr02Element);
            obrElement.Add(obr02Element);                

            XElement obr03Element = new XElement("OBR.3");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.3.1", this.m_ReportNo, obr03Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.3.2", "YPILIS", obr03Element);
            obrElement.Add(obr03Element);            

            XElement obr04Element = new XElement("OBR.4");                        
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.4.1", "PATH", obr04Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.4.2", "PATH", obr04Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.4.3", "Pathology", obr04Element);

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

            XElement obr20Element = new XElement("OBR.20");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.20.1", "OM", obr20Element);
            obrElement.Add(obr20Element);

            XElement obr21Element = new XElement("OBR.21");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.21.1", "PATH", obr21Element);
            obrElement.Add(obr21Element);

            XElement obr22Element = new XElement("OBR.22");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.22.1", DateTime.Now.ToString(m_DateFormat), obr22Element);                                                
            obrElement.Add(obr22Element);            

            XElement obr25Element = new XElement("OBR.25");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.25.1", this.m_ObservationResultStatus, obr25Element);
            obrElement.Add(obr25Element);

            XElement obr32Element = new XElement("OBR.32");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.1", this.m_SigningPathologist.GetWPHMneumonic(), obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.2", this.m_SigningPathologist.LastName, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.3", this.m_SigningPathologist.FirstName, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.4", this.m_SigningPathologist.MiddleInitial, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.5", string.Empty, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.6", string.Empty, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.7", string.Empty, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.8", string.Empty, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.9", "NPI", obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.10", string.Empty, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.11", string.Empty, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.12", string.Empty, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.13", "NPI", obr32Element);
            obrElement.Add(obr32Element);

            XElement obr35Element = new XElement("OBR.35");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.35.1", "RRD", obr35Element);
            obrElement.Add(obr35Element);

        }

        public void WriteUniversalServiceId(XElement obr04Element, int panelSetId)
        {
            string code = string.Empty;
            string testName = string.Empty;            
        }
	}
}
