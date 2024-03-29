﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.WYDOH
{
	public class WYDOHOBRView
	{
        private YellowstonePathology.Business.User.SystemUser m_SigningPathologist;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private string m_DateFormat = "yyyyMMddHHmm";
        private string m_ReportNo;
        private string m_ObservationResultStatus;
		private YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;

		public WYDOHOBRView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, YellowstonePathology.Business.Domain.Physician orderingPhysician)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_ReportNo = reportNo;
			this.m_OrderingPhysician = orderingPhysician;

            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(13);
            this.m_SigningPathologist = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(panelSetOrder.AssignedToId);

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(panelSetOrder.ReportNo);
            if (amendmentCollection.Count == 0)
            {
                this.m_ObservationResultStatus = "F";
            }
            else
            {
                this.m_ObservationResultStatus = "C";
            }            
		}        

        public void ToXml(XElement document)
        {
            XElement obrElement = new XElement("OBR");
            document.Add(obrElement);

            XElement obr01Element = new XElement("OBR.1");
            XElement obr0101Element = new XElement("OBR.1.1", "1");
            obr01Element.Add(obr0101Element);
            obrElement.Add(obr01Element);
            

            XElement obr02Element = new XElement("OBR.2");
            XElement obr0201Element = new XElement("OBR.2.1", this.m_ReportNo);
            XElement obr0202Element = new XElement("OBR.2.2", "YPILIS");
            obr02Element.Add(obr0201Element);
            obr02Element.Add(obr0202Element);
            obrElement.Add(obr02Element);

            XElement obr04Element = new XElement("OBR.4");
            XElement obr0401Element = new XElement("OBR.4.1", "YPI");
            XElement obr0402Element = new XElement("OBR.4.2", "Pathology Procedure/Specimen");
			XElement obr0403Element = new XElement("OBR.4.3", "LN");
            obr04Element.Add(obr0401Element);
            obr04Element.Add(obr0402Element);
			obr04Element.Add(obr0403Element);
			obrElement.Add(obr04Element);

            XElement obr07Element = new XElement("OBR.7");
            XElement obr0701Element = new XElement("OBR.7.1", this.m_AccessionOrder.AccessionDate.Value.ToString(m_DateFormat));
            obr07Element.Add(obr0701Element);
            obrElement.Add(obr07Element);

            XElement obr14Element = new XElement("OBR.14");
            XElement obr1401Element = new XElement("OBR.14.1", this.m_AccessionOrder.AccessionDate.Value.ToString(m_DateFormat));
            obr14Element.Add(obr1401Element);
            obrElement.Add(obr14Element);

			XElement obr16Element = new XElement("OBR.16");
			XElement obr1601Element = new XElement("OBR.16.1", this.m_OrderingPhysician.Npi);
			XElement obr1602Element = new XElement("OBR.16.2", this.m_OrderingPhysician.LastName);
			XElement obr1603Element = new XElement("OBR.16.3", this.m_OrderingPhysician.FirstName);
			XElement obr1604Element = new XElement("OBR.16.4", this.m_OrderingPhysician.GetNormalizedMiddleInitial());
			obr16Element.Add(obr1601Element);
			obr16Element.Add(obr1602Element);
			obr16Element.Add(obr1603Element);
			YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(obr16Element, obr1604Element);
			obrElement.Add(obr16Element);

            XElement obr20Element = new XElement("OBR.20");
            XElement obr2001Element = new XElement("OBR.20.1", this.m_ReportNo);
            XElement obr2002Element = new XElement("OBR.20.2", "YPILIS");
            obr20Element.Add(obr2001Element);
            obr20Element.Add(obr2002Element);
            obrElement.Add(obr20Element);

            XElement obr22Element = new XElement("OBR.22");
            XElement obr2201Element = new XElement("OBR.22.1", DateTime.Now.ToString(m_DateFormat));
            obr22Element.Add(obr2201Element);
            obrElement.Add(obr22Element);

            XElement obr25Element = new XElement("OBR.25");
            XElement obr2501Element = new XElement("OBR.25.1", this.m_ObservationResultStatus);
            obr25Element.Add(obr2501Element);
            obrElement.Add(obr25Element);

            XElement obr32Element = new XElement("OBR.32");

			StringBuilder obr3201Data = new StringBuilder();
			obr3201Data.Append(this.m_SigningPathologist.NationalProviderId);			
			XElement obr3201Element = new XElement("OBR.32.1", obr3201Data.ToString());
            
			YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(obr32Element, obr3201Element);
			YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(obrElement, obr32Element);
		}
	}
}
