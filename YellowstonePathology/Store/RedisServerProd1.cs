using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace YellowstonePathology.Store
{
    public class RedisServerProd1 : IRedisServer
    {
        private static RedisServerProd1 instance = null;
        private static readonly object padlock = new object();

        protected string m_IPAddress;
        protected string m_Port;
        protected string m_ConnectionArgs;

        private ConnectionMultiplexer m_Connection;
        private IServer m_Server;
        private ISubscriber m_Subscriber;

        RedisServerProd1()
        {
            this.m_IPAddress = "10.1.2.70";
            //this.m_IPAddress = "10.1.1.54";
            //this.m_Port = "6379";
            this.m_Port = "32299";
            this.m_ConnectionArgs = "ConnectTimeout=5000, SyncTimeout=5000";

            this.m_Connection = ConnectionMultiplexer.Connect(this.ConnectionString);
            this.m_Server = this.m_Connection.GetServer(this.IPAddressPort);
            this.m_Subscriber = this.m_Connection.GetSubscriber();
        }

        public static RedisServerProd1 Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new RedisServerProd1();
                    }
                    return instance;
                }
            }
        }        

        public IDatabase GetDB(int databaseNumber)
        {
            return this.m_Connection.GetDatabase(databaseNumber);
        }

        public ISubscriber Subscriber
        {
            get { return this.m_Subscriber; }
        }

        public IServer Server
        {
            get { return this.m_Server; }
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
    }
}
