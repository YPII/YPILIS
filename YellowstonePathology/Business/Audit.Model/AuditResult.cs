using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class AuditResult
    {
        private AuditStatusEnum m_Status;        
        private string m_Message;

        public AuditResult()
        {

        }

        public AuditStatusEnum Status
        {
            get { return this.m_Status; }
            set { this.m_Status = value; }
        }        

        public string Message
        {
            get { return this.m_Message; }
            set { this.m_Message = value; }
        }
    }
}
