using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace YellowstonePathology.Business.Mongo
{
    public class Server
    {               
        protected string m_ConnectionString;
        protected MongoClient m_MongoClient;
        protected MongoServer m_MongoServer;
        protected MongoDatabase m_MongoDatabase;

        public Server(string connectionString, string databaseName)
        {
            this.m_ConnectionString = connectionString;
            this.m_MongoClient = new MongoClient(this.m_ConnectionString);
            this.m_MongoServer = this.m_MongoClient.GetServer();
            this.m_MongoDatabase = this.m_MongoServer.GetDatabase(databaseName);
        }

        public MongoDatabase Database
        {
            get { return this.m_MongoDatabase; }
        }
    }
}
