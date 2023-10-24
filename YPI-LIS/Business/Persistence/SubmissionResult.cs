using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace YellowstonePathology.Business.Persistence
{
    [DataContract]
    public class SubmissionResult
    {
        private bool m_Success;
        private string m_Message;
        private bool m_HasInsertCommands;
        private bool m_HasInsertLastCommands;
        private bool m_HasDeleteFirstCommands;
        private bool m_HasDeleteCommands;
        private bool m_HasUpdateCommands;

        public SubmissionResult()
        {
            this.m_Success = true;
        }

        [DataMember]
        public bool Success
        {
            get { return this.m_Success; }
            set { this.m_Success = value; }
        }

        [DataMember]
        public string Message
        {
            get { return this.m_Message; }
            set { this.m_Message = value; }
        }

        [DataMember]
        public bool HasInsertCommands
        {
            get { return this.m_HasInsertCommands; }
            set { this.m_HasInsertCommands = value; }
        }

        [DataMember]
        public bool HasInsertLastCommands
        {
            get { return this.m_HasInsertLastCommands; }
            set { this.m_HasInsertLastCommands = value; }
        }

        [DataMember]
        public bool HasDeleteFirstCommands
        {
            get { return this.m_HasDeleteFirstCommands; }
            set { this.m_HasDeleteFirstCommands = value; }
        }

        [DataMember]
        public bool HasDeleteCommands
        {
            get { return this.m_HasDeleteCommands; }
            set { this.m_HasDeleteCommands = value; }
        }

        [DataMember]
        public bool HasUpdateCommands
        {
            get { return this.m_HasUpdateCommands; }
            set { this.m_HasUpdateCommands = value; }
        }
    }
}
