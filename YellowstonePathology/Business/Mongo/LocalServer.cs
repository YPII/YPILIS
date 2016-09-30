using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace YellowstonePathology.Business.Mongo
{
    public class LocalServer : Server
    {
        public const string LocalLISDatabaseName = "LocalLIS";

        public LocalServer(string databaseName) 
            : base("mongodb://LocalHost", databaseName)
        {
            
        }        
    }
}
