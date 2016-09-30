using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace YellowstonePathology.Business.Mongo
{
    public class TestServer : Server
    {
        public const string TablesDatabaseName = "Tables";
        public const string LISDatabaseName = "LIS";
        public const string SQLTransferDatabasename = "SQLTransfer";

        public TestServer(string databaseName)
            : base("mongodb://10.1.2.18", databaseName)
        {
            
        }        
    }
}
