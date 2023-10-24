using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules
{
    public class ExecutionMessage
    {        
        string m_Message;
        bool m_Halted;
        bool m_ContinueExecutionOnHalt;

        public ExecutionMessage()
        {

        }        

        public string Message
        {
            get { return this.m_Message; }
            set { this.m_Message = value; }
        }

        public bool Halted
        {
            get { return this.m_Halted; }
            set { this.m_Halted = value; }
        }        

        public bool ContinueExecutionOnHalt
        {
            get { return this.m_ContinueExecutionOnHalt; }
            set { this.m_ContinueExecutionOnHalt = value; }
        }        
    }
}
