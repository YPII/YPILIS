using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace YellowstonePathology.Business.Mongo
{
    public class PSOCytologyTransferBuilder : PSOTransferBuilder
    {
        public PSOCytologyTransferBuilder()
        {

        }

        public override void Build(MongoDB.Bson.BsonDocument bsonPanelSetOrder, int panelSetId)
        {
            base.Build(bsonPanelSetOrder, panelSetId);

            BsonElement panelOrderCollectionElement = bsonPanelSetOrder.GetElement("PanelOrderCollection");
            BsonArray panelOrderCollectionArray = bsonPanelSetOrder["PanelOrderCollection"].AsBsonArray;

            MongoCollection panelOrderCytologyTable = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblPanelOrderCytology");            

            foreach (BsonDocument bsonPanelOrder in panelOrderCollectionArray)
            {
                int panelId = bsonPanelOrder.GetValue("PanelOrderId").ToInt32();
                if (panelId == 38) //cant' be acid wash
                {                    
                    BsonDocument bsonPODerived = panelOrderCytologyTable.FindOneAs<BsonDocument>(Query.EQ("PanelOrderId", bsonPanelOrder.GetValue("PanelOrderId")));
                    foreach (BsonElement bsonElement in bsonPODerived)
                    {
                        if (bsonElement.Name != "_id" && bsonElement.Name != "PanelOrderId")
                        {
                            bsonPanelOrder.Add(bsonElement);
                        }
                    }
                }
            }
        }
    }
}
