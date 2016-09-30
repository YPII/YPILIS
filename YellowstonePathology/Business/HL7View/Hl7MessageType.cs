using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View
{
    public class Hl7MessageType
    {
        private string m_MessageType;
        private string m_TriggerEvent;
        private string m_Structure;

        public Hl7MessageType(string messageType, string triggerEvent, string structure)
        {
            this.m_MessageType = messageType;
            this.m_TriggerEvent = triggerEvent;
            this.m_Structure = structure;
        }

        public string MessageType
        {
            get { return this.m_MessageType; }
            set { this.m_MessageType = value; }
        }

        public string TriggerEvent
        {
            get { return this.m_TriggerEvent; }
            set { this.m_TriggerEvent = value; }
        }

        public string Structure
        {
            get { return this.m_TriggerEvent; }
            set { this.m_TriggerEvent = value; }
        }
        
    }
}
