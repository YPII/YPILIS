using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace YellowstonePathology.Business.Persistence
{
    [DataContract]    
    public class TransferObject
    {
        private object m_Key;
        private object m_Value;

        public TransferObject()
        {

        }

        public TransferObject(object key, object value)
        {
            this.m_Key = key;
            this.m_Value = value;
        }

        [DataMember]
        public object Key
        {
            get { return this.m_Key; }
            set { this.m_Key = value; }
        }

        [DataMember]
        public object Value
        {
            get { return this.m_Value; }
            set { this.m_Value = value; }
        }
    }
}
