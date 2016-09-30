using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class TestEPICStatusMessage : EPICStatusMessage
    {
        public TestEPICStatusMessage(string clientOrderId, OrderStatus orderStatus, YellowstonePathology.Business.ClientOrder.Model.UniversalService universalServiceId, object writer) 
            : base(clientOrderId, orderStatus, universalServiceId, writer)
        {

        }

        public TestEPICStatusMessage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder, OrderStatus orderStatus, YellowstonePathology.Business.ClientOrder.Model.UniversalService universalServiceId) 
            : base(clientOrder, orderStatus, universalServiceId)
		{

        }

        public override void SetupFileNames()
        {
            string newGuid = Guid.NewGuid().ToString();
            this.m_ServerFileName = @"\\YPIIInterface1\ChannelData\Outgoing\1002\Test\In\" + newGuid + ".xml";
            this.m_InterfaceFilename = @"\\YPIIInterface1\ChannelData\Outgoing\1002\Test\" + newGuid + ".xml";     
        }
    }
}
