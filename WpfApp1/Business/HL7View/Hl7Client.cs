using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View
{
    public class Hl7Client
    {
        private string m_ReceivingFacility;
        private string m_ReceivingApplication;

        public Hl7Client(string receivingFacility, string receivingApplication)
        {
            this.m_ReceivingFacility = receivingFacility;
            this.m_ReceivingApplication = receivingApplication;
        }

        public string ReceivingFacility
        {
            get { return this.m_ReceivingFacility; }
            set { this.m_ReceivingFacility = value; }
        }

        public string ReceivingApplication
        {
            get { return this.m_ReceivingApplication; }
            set { this.m_ReceivingApplication = value; }
        }
    }
}
