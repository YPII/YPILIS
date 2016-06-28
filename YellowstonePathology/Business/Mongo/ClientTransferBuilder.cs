using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;


namespace YellowstonePathology.Business.Mongo
{
    public class ClientTransferBuilder
    {
        private MongoDatabase m_SQLTransferDatabase;
        private MongoDatabase m_LISDatabase;

        public ClientTransferBuilder()
        {
            this.m_SQLTransferDatabase = MongoTestServer.Instance.SQLTransfer;
            this.m_LISDatabase = MongoTestServer.Instance.LIS;
        }
        
        public void Build()
        {                        
            MongoCollection clientCollection = this.m_LISDatabase.GetCollection<BsonDocument>("Client");
            MongoCollection clientTable = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblClient");
            MongoCursor mongoCursor = clientTable.FindAllAs<BsonDocument>();
            
            foreach (BsonDocument bsonParent in mongoCursor)
            {
                BuildClientLocation(bsonParent);
                clientCollection.Save(bsonParent);                
            }
        }

        private void BuildClientLocation(BsonDocument bsonParent)
        {
            MongoCollection clientLocationTable = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblClientLocation");
            MongoCursor mongoCursor = clientLocationTable.FindAs<BsonDocument>(Query.EQ("ClientId", bsonParent.GetValue("ClientId").AsInt32));
            BsonArray array = new BsonArray();

            foreach (BsonDocument bsonChild in mongoCursor)
            {                
                array.Add(bsonChild);                
            }

            bsonParent.Add("ClientLocationCollection", array);
        }
    }
}
