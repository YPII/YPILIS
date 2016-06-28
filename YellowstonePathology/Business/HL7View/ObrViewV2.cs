using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View
{
	public class ObrViewV2
	{
        private YellowstonePathology.Business.User.SystemUser m_SigningPathologist;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private string m_DateFormat = "yyyyMMddHHmm";
        private string m_ReportNo;
        private string m_ObservationResultStatus;

		public ObrViewV2(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_ReportNo = reportNo;

            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(13);
            this.m_SigningPathologist = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(panelSetOrder.AssignedToId);

            if (panelSetOrder.AmendmentCollection.Count == 0)
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
            

            XElement obr03Element = new XElement("OBR.3");
            XElement obr0301Element = new XElement("OBR.3.1", this.m_ReportNo);
            XElement obr0302Element = new XElement("OBR.3.2", "YPILIS");
            obr03Element.Add(obr0301Element);
            obr03Element.Add(obr0302Element);
            obrElement.Add(obr03Element);

            XElement obr04Element = new XElement("OBR.4");
            XElement obr0401Element = new XElement("OBR.4.1", "YPI");
            XElement obr0402Element = new XElement("OBR.4.2", "Pathology Procedure/Specimen");
            obr04Element.Add(obr0401Element);
            obr04Element.Add(obr0402Element);
            obrElement.Add(obr04Element);

            XElement obr07Element = new XElement("OBR.7");
            XElement obr0701Element = new XElement("OBR.7.1", this.m_AccessionOrder.AccessionDate.Value.ToString(m_DateFormat));
            obr07Element.Add(obr0701Element);
            obrElement.Add(obr07Element);

            XElement obr14Element = new XElement("OBR.14");
            XElement obr1401Element = new XElement("OBR.14.1", this.m_AccessionOrder.AccessionDate.Value.ToString(m_DateFormat));
            obr14Element.Add(obr1401Element);
            obrElement.Add(obr14Element);

            XElement obr22Element = new XElement("OBR.22");
            XElement obr2201Element = new XElement("OBR.22.1", DateTime.Now.ToString(m_DateFormat));
            obr22Element.Add(obr2201Element);
            obrElement.Add(obr22Element);

            XElement obr25Element = new XElement("OBR.25");
            XElement obr2501Element = new XElement("OBR.25.1", this.m_ObservationResultStatus);
            obr25Element.Add(obr2501Element);
            obrElement.Add(obr25Element);

            XElement obr32Element = new XElement("OBR.32");                        
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.1", this.m_SigningPathologist.NationalProviderId, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.2", this.m_SigningPathologist.LastName, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.3", this.m_SigningPathologist.FirstName, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.4", string.Empty, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.5", string.Empty, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.6", string.Empty, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.7", string.Empty, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.8", string.Empty, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.9", "NPI", obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.10", string.Empty, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.11", string.Empty, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.12", string.Empty, obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.32.13", "NPI", obr32Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(obrElement, obr32Element);        
        }
	}
}
