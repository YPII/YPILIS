using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Store
{
    public class RedisStoreDefinition
    {
        protected Dictionary<string, int> m_DBMapping;
        protected string m_IPAddress;
        protected string m_Port;
        protected string m_ConnectionArgs;

        public RedisStoreDefinition()
        {
        
        }

        public string IPAddress
        {
            get { return this.IPAddress; }            
        }

        public string Port
        {
            get { return this.m_Port; }
        }

        public string ConnectionArgs
        {
            get { return this.m_ConnectionArgs; }
        }

        public string ConnectionString
        {
            get { return IPAddressPort + ", " + this.m_ConnectionArgs; }
        }

        public string IPAddressPort
        {
            get { return this.m_IPAddress + ":" + this.m_Port; }
        }

        public Dictionary<string, int> DBMapping
        {
            get { return this.m_DBMapping; }
        }
        
    }
}
