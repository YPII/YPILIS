using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
    public class WestGrandFamilyOrderTest
    {
        ClientOrder m_ClientOrder;        

        public WestGrandFamilyOrderTest()
        {            
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            this.m_ClientOrder = new ClientOrder(objectId);
            this.m_ClientOrder.ClientOrderId = Guid.NewGuid().ToString();            
            this.m_ClientOrder.OrderDate = DateTime.Today;
            this.m_ClientOrder.OrderTime = DateTime.Now;
            this.m_ClientOrder.ClientId = 242;
            this.m_ClientOrder.ClientName = "West Grand Family Medicine-SVPN";
            this.m_ClientOrder.ProviderName = "Brenda Kirkland";
            this.m_ClientOrder.ProviderId = "1780623181";
            this.m_ClientOrder.SystemInitiatingOrder = "EPIC";
            this.m_ClientOrder.ExternalOrderId = "123456789";
            this.m_ClientOrder.PLastName = "MOUSE";
            this.m_ClientOrder.PFirstName = "MICKEY";
            this.m_ClientOrder.PBirthdate = DateTime.Parse("8/10/1966");
            this.m_ClientOrder.OrderedBy = "BROOMHILDA";
            this.m_ClientOrder.PanelSetId = null;
            this.m_ClientOrder.OrderType = "Routine Surgical Pathology";
            this.m_ClientOrder.SvhAccountNo = "123456789";
            this.m_ClientOrder.SvhMedicalRecord = "123456789";
            this.m_ClientOrder.ClientLocationId = 1002;
        }

        public void InsertClientOrder(object writer)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(this.m_ClientOrder, writer);
        }
    }
}
