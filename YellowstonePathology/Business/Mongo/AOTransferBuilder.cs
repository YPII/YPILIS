using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;


namespace YellowstonePathology.Business.Mongo
{
    public class AOTransferBuilder
    {
        private MongoDatabase m_SQLTransferDatabase;
        private MongoDatabase m_LISDatabase;

        public AOTransferBuilder()
        {
            this.m_SQLTransferDatabase = MongoTestServer.Instance.SQLTransfer;
            this.m_LISDatabase = MongoTestServer.Instance.LIS;
        }
        
        public void Build()
        {            
            MongoCollection accessionOrderCollection = this.m_LISDatabase.GetCollection<BsonDocument>("AccessionOrder");
            MongoCollection accessionOrderTable = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblAccessionOrder");
            MongoCursor mongoCursor = accessionOrderTable.FindAs<BsonDocument>(Query.GTE("AccessionDate", BsonDateTime.Create(DateTime.Parse("1/1/2014"))));

            int cnt = 0;

            foreach (BsonDocument bsonParent in mongoCursor)
            {                
                this.BuildSpecimenOrder(bsonParent);
                this.BuildPanelSetOrder(bsonParent);
                this.BuildICD9BillingCodeCollection(bsonParent);
                this.BuildTaskOrderCollection(bsonParent);

                accessionOrderCollection.Save(bsonParent);                

                cnt += 1;
                if (cnt == 1000) break;                               
            }            
        }        

        private void BuildSpecimenOrder(BsonDocument bsonParent)
        {                        
            MongoCollection specimenOrderTable = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblSpecimenOrder");            
            MongoCursor mongoCursor = specimenOrderTable.FindAs<BsonDocument>(Query.EQ("MasterAccessionNo", bsonParent.GetValue("MasterAccessionNo")));            
            BsonArray array = new BsonArray();            
                        
            foreach (BsonDocument bsonChild in mongoCursor)
            {
				bsonChild.Add("AssemblyQualifiedName", typeof(YellowstonePathology.Business.Specimen.Model.SpecimenOrder).AssemblyQualifiedName);
                array.Add(bsonChild);                
                this.BuildAliquotOrder(bsonChild);
            }
            
            bsonParent.Add("SpecimenOrderCollection", array);                        
        }

        private void BuildAliquotOrder(BsonDocument bsonParent)
        {            
            MongoCollection aliquotOrderTable = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblAliquotOrder");            
            MongoCursor mongoCursor = aliquotOrderTable.FindAs<BsonDocument>(Query.EQ("SpecimenOrderId", bsonParent.GetValue("SpecimenOrderId")));
            BsonArray array = new BsonArray();

            foreach (BsonDocument bsonChild in mongoCursor)
            {                
                array.Add(bsonChild);                
            }

            bsonParent.Add("AliquotOrderCollection", array);         
        }

        private void BuildPanelSetOrder(BsonDocument bsonParent)
        {
            MongoCollection panelSetOrderTable = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblPanelSetOrder");
            MongoCursor mongoCursor = panelSetOrderTable.FindAs<BsonDocument>(Query.EQ("MasterAccessionNo", bsonParent.GetValue("MasterAccessionNo")));
            BsonArray array = new BsonArray();

            foreach (BsonDocument bsonPSO in mongoCursor)
            {                
                int panelSetId = bsonPSO.GetValue("PanelSetId").ToInt32();
                PSOTransferBuilder psoTransferBuilder = PSOTransferBuilder.GetPSOTransferBuilder(panelSetId);
                psoTransferBuilder.Build(bsonPSO, panelSetId);
                array.Add(bsonPSO);
            }

            bsonParent.Add("PanelSetOrderCollection", array);
        }

        private void BuildICD9BillingCodeCollection(BsonDocument bsonParent)
        {
            MongoCollection mongoCollection = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblICD9BillingCodeCollection");
            MongoCursor mongoCursor = mongoCollection.FindAs<BsonDocument>(Query.EQ("MasterAccessionNo", bsonParent.GetValue("MasterAccessionNo")));
            BsonArray array = new BsonArray();

            foreach (BsonDocument bsonChild in mongoCursor)
            {
                array.Add(bsonChild);
            }

            bsonParent.Add("ICD9BillingCodeCollection", array);
        }

        private void BuildTaskOrderCollection(BsonDocument bsonParent)
        {
            MongoCollection mongoCollection = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblTaskOrder");
            MongoCursor mongoCursor = mongoCollection.FindAs<BsonDocument>(Query.EQ("MasterAccessionNo", bsonParent.GetValue("MasterAccessionNo")));
            BsonArray array = new BsonArray();

            foreach (BsonDocument bsonChild in mongoCursor)
            {
                array.Add(bsonChild);
            }

            bsonParent.Add("TaskOrderCollection", array);
        }
    }
}
