using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace YellowstonePathology.Business.Persistence
{
    [DataContract]    
    public class RemoteObjectTransferAgent
    {
        private List<TransferObject> m_RegisteredObjects;
        private List<TransferObject> m_RegisteredRootInserts;
        private List<TransferObject> m_RegisteredRootDeletes;
        private object m_ObjectToSubmit;

        public RemoteObjectTransferAgent()
        {
            this.m_RegisteredObjects = new List<TransferObject>();
            this.m_RegisteredRootDeletes = new List<TransferObject>();
            this.m_RegisteredRootInserts = new List<TransferObject>();
        }

        [DataMember]
        public object ObjectToSubmit
        {
            get { return this.m_ObjectToSubmit; }
            set { this.m_ObjectToSubmit = value; }
        }

        [DataMember]
        public List<TransferObject> RegisteredObjects
        {
            get { return this.m_RegisteredObjects; }
            set { this.m_RegisteredObjects = value; }
        }

        [DataMember]
        public List<TransferObject> RegisteredRootInserts
        {
            get { return this.m_RegisteredRootInserts; }
            set { this.m_RegisteredRootInserts = value; }
        }

        [DataMember]
        public List<TransferObject> RegisteredRootDeletes
        {
            get { return this.m_RegisteredRootDeletes; }
            set { this.m_RegisteredRootDeletes = value; }
        }
    }
}
