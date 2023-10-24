using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Documents;

namespace YellowstonePathology.Business.XPSDocument
{    
    public class XpsAdditionalTesting
    {        

        Business.Test.AccessionOrder m_AccessionOrder;
        Business.Test.PanelSetOrder m_PanelSetOrder;
        string m_Template = @"\\fileserver\Documents\ReportTemplates\XmlTemplates\AdditionalTestingNotification.2.xps";
        string m_DestinationPath;
        Dictionary<string, string> m_PlaceHolders;

        public XpsAdditionalTesting(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = panelSetOrder;
            Business.OrderIdParser orderIdParser = new OrderIdParser(panelSetOrder.ReportNo);
            this.m_DestinationPath = Business.Document.CaseDocument.GetNotificationDocumentFilePath(orderIdParser);

            this.m_PlaceHolders = new Dictionary<string, string>();
            this.m_PlaceHolders.Add(@"1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ,./\-_", string.Empty);
            this.m_PlaceHolders.Add(@"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ,-", string.Empty);
            this.m_PlaceHolders.Add("physician_name", this.m_AccessionOrder.PhysicianName);
            this.m_PlaceHolders.Add("client_name", this.m_AccessionOrder.ClientName);
            this.m_PlaceHolders.Add("report_number", this.m_PanelSetOrder.ReportNo);
            this.m_PlaceHolders.Add("accession_date", this.m_AccessionOrder.AccessionDate.ToString());
            this.m_PlaceHolders.Add("collection_date", this.m_AccessionOrder.CollectionDate.ToString());
            this.m_PlaceHolders.Add("patient_name", this.m_AccessionOrder.PatientDisplayName);
            this.m_PlaceHolders.Add("patient_birthdate", this.m_AccessionOrder.PBirthdate.ToString());
            this.m_PlaceHolders.Add("additional_testing", this.m_PanelSetOrder.PanelSetName);
        }

        public void CreateXPS()
        {   
            ReplaceCallBackType cb = new ReplaceCallBackType(this.ReplaceText);
            XPSTemplate template = new XPSTemplate();
            template.CreateNewXPSFromSource(this.m_Template, this.m_DestinationPath, cb);
        }

        public void ReplaceText(XmlNode node, XmlAttribute attribute)
        {   
            foreach(KeyValuePair<string, string> item in this.m_PlaceHolders)
            {                
                if (attribute.Value.Contains(item.Key))
                {
                    node.Attributes["UnicodeString"].Value = node.Attributes["UnicodeString"].Value.Replace(item.Key, item.Value);
                    node.Attributes["Indices"].Value = "";
                }
            }            
        }
    }
}
