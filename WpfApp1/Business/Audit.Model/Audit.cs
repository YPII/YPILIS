using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class Audit
    {
        protected AuditStatusEnum m_Status;
        protected bool m_ActionRequired;
        protected StringBuilder m_Message;

        public Audit()
        {
            this.m_ActionRequired = false;
            this.m_Message = new StringBuilder();            
        }

        public virtual void Run()
        {         
            
        }

        public AuditStatusEnum Status
        {
            get { return this.m_Status; }
            set { this.m_Status = value; }
        }

        public bool ActionRequired
        {
            get { return this.m_ActionRequired; }
            set { this.m_ActionRequired = value; }
        }

        public StringBuilder Message
        {
            get { return this.m_Message; }            
            set { this.m_Message = value;}
        }        
    }
}
