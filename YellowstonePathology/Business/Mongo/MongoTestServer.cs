using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace YellowstonePathology.Business.Mongo
{
    public sealed class MongoTestServer
    {
        public static string LISDatabaseName = "LIS";
        public static string SQLTransferDatabasename = "SQLTransfer";

        private const string ConnectionString = "mongodb://10.1.2.18";

        private static readonly MongoTestServer _instance = new MongoTestServer();
                
        private MongoServer m_MongoServer;
        private MongoClient m_MongoClient;

        private MongoDatabase m_LIS;
        private MongoDatabase m_SQLTransfer;        

        MongoTestServer()
        {            
            this.m_MongoClient = new MongoClient(ConnectionString);
            this.m_MongoServer = this.m_MongoClient.GetServer();     
            this.m_LIS = this.m_MongoServer.GetDatabase(LISDatabaseName);
            this.m_SQLTransfer = this.m_MongoServer.GetDatabase(SQLTransferDatabasename);
        }

        public MongoDatabase LIS
        {
            get { return this.m_LIS; }
        }

        public MongoDatabase SQLTransfer
        {
            get { return this.m_SQLTransfer; }
        }

        public static MongoTestServer Instance
        {
            get
            {
                return _instance;
            }
        }        
    }
}
