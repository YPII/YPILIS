using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules
{
    public class MethodResult
    {
        private string m_Message;
        private bool m_Success;

        public MethodResult()
        {
            this.m_Success = true;
        }

        public string Message
        {
            get { return this.m_Message; }
            set { this.m_Message = value; }
        }

        public bool Success
        {
            get { return this.m_Success; }
            set { this.m_Success = value; }
        }        
    }
}
