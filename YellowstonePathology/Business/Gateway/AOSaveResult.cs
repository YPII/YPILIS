using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Gateway
{
    public class AOSaveResult
    {
        private bool m_OK;
        private string m_Message;

        public AOSaveResult()
        {

        }

        public bool OK
        {
            get { return this.m_OK; }
            set { this.m_OK = value; }
        }

        public string Message
        {
            get { return this.m_Message; }
            set { this.m_Message = value; }
        }
    }
}
