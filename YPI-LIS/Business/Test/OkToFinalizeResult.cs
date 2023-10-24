using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test
{
    public class OkToFinalizeResult
    {
        private bool m_OK;
        private bool m_ShowWarningMessage;        
        private string m_Message;

        public OkToFinalizeResult()
        {

        }

        public bool OK
        {
            get { return this.m_OK; }
            set { this.m_OK = value;}
        }

        public bool ShowWarningMessage
        {
            get { return this.m_ShowWarningMessage; }
            set { this.m_ShowWarningMessage = value; }
        }

        public string Message
        {
            get { return this.m_Message; }
            set { this.m_Message = value; }
        }
    }
}
