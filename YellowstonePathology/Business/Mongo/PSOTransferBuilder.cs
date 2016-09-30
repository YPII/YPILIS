using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace YellowstonePathology.Business.Mongo
{
    public class PSOTransferBuilder
    {
        protected MongoDatabase m_SQLTransferDatabase;
        protected MongoDatabase m_LISDatabase;

        public PSOTransferBuilder()
        {
            this.m_SQLTransferDatabase = MongoTestServer.Instance.SQLTransfer;
            this.m_LISDatabase = MongoTestServer.Instance.LIS;
        }

        public virtual void Build(BsonDocument bsonPanelSetOrder, int panelSetId)
        {                        
            MongoCollection panelSetCollection = this.m_LISDatabase.GetCollection<BsonDocument>("PanelSet");                            
            BsonDocument panelSetBsonDocument = panelSetCollection.FindOneAs<BsonDocument>(Query.EQ("PanelSetId", panelSetId));
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = new YellowstonePathology.Business.PanelSet.Model.PanelSet();
            YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(panelSetBsonDocument, panelSet);

            if (panelSet.ResultDocumentSource == YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase)
            {
                MongoCollection psoDerivedCollection = this.m_SQLTransferDatabase.GetCollection<BsonDocument>(panelSet.PanelSetOrderTableName);
                BsonDocument psoDerivedBsonDocument = psoDerivedCollection.FindOneAs<BsonDocument>(Query.EQ("ReportNo", bsonPanelSetOrder.GetValue("ReportNo")));

                foreach (BsonElement bsonElement in psoDerivedBsonDocument)
                {
                    if (bsonElement.Name != "_id" && bsonElement.Name != "ReportNo")
                    {
                        bsonPanelSetOrder.Add(bsonElement);
                    }
                }
            }

            this.BuildPanelOrder(bsonPanelSetOrder);
            this.BuildPanelSetOrderCPTCode(bsonPanelSetOrder);
            this.BuildPanelSetOrderCPTCodeBill(bsonPanelSetOrder);
            this.BuildTestOrderReportDistribution(bsonPanelSetOrder);
            this.BuildTestOrderReportDistributionLog(bsonPanelSetOrder);
            this.BuildAmendmentCollection(bsonPanelSetOrder);
        }

        private void BuildPanelSetOrderCPTCode(BsonDocument bsonPSO)
        {
            MongoCollection panelSetOrderCPTCodeTable = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblPanelSetOrderCPTCode");
            MongoCursor mongoCursor = panelSetOrderCPTCodeTable.FindAs<BsonDocument>(Query.EQ("ReportNo", bsonPSO.GetValue("ReportNo")));
            BsonArray array = new BsonArray();

            foreach (BsonDocument bsonChild in mongoCursor)
            {
                array.Add(bsonChild);
            }

            bsonPSO.Add("PanelSetOrderCPTCodeCollection", array);
        }

        private void BuildPanelSetOrderCPTCodeBill(BsonDocument bsonPSO)
        {
            MongoCollection panelSetOrderCPTCodeBillTable = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblPanelSetOrderCPTCodeBill");
            MongoCursor mongoCursor = panelSetOrderCPTCodeBillTable.FindAs<BsonDocument>(Query.EQ("ReportNo", bsonPSO.GetValue("ReportNo")));
            BsonArray array = new BsonArray();

            foreach (BsonDocument bsonChild in mongoCursor)
            {
                array.Add(bsonChild);
            }

            bsonPSO.Add("PanelSetOrderCPTCodeBillCollection", array);
        }

        private void BuildTestOrderReportDistribution(BsonDocument bsonPSO)
        {
            MongoCollection testOrderReportDistributionTable = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblTestOrderReportDistribution");
            MongoCursor mongoCursor = testOrderReportDistributionTable.FindAs<BsonDocument>(Query.EQ("ReportNo", bsonPSO.GetValue("ReportNo")));
            BsonArray array = new BsonArray();

            foreach (BsonDocument bsonChild in mongoCursor)
            {
                array.Add(bsonChild);                
            }

            bsonPSO.Add("TestOrderReportDistributionCollection", array);
        }

        private void BuildTestOrderReportDistributionLog(BsonDocument bsonPSO)
        {
            MongoCollection testOrderReportDistributionLogTable = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblTestOrderReportDistributionLog");
            MongoCursor mongoCursor = testOrderReportDistributionLogTable.FindAs<BsonDocument>(Query.EQ("ReportNo", bsonPSO.GetValue("ReportNo")));
            BsonArray array = new BsonArray();

            foreach (BsonDocument bsonChild in mongoCursor)
            {
                array.Add(bsonChild);                
            }

            bsonPSO.Add("TestOrderReportDistributionLogCollection", array);
        }

        private void BuildAmendmentCollection(BsonDocument bsonPSO)
        {
            MongoCollection mongoCollection = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblAmendment");
            MongoCursor mongoCursor = mongoCollection.FindAs<BsonDocument>(Query.EQ("ReportNo", bsonPSO.GetValue("ReportNo")));
            BsonArray array = new BsonArray();

            foreach (BsonDocument bsonChild in mongoCursor)
            {
                array.Add(bsonChild);
            }

            bsonPSO.Add("AmendmentCollection", array);
        }        

        private void BuildPanelOrder(BsonDocument bsonPSO)
        {
            MongoCollection panelOrderTable = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblPanelOrder");
            MongoCursor mongoCursor = panelOrderTable.FindAs<BsonDocument>(Query.EQ("ReportNo", bsonPSO.GetValue("ReportNo")));
            BsonArray array = new BsonArray();

            foreach (BsonDocument bsonChild in mongoCursor)
            {
                array.Add(bsonChild);
                this.BuildTestOrder(bsonChild);
            }

            bsonPSO.Add("PanelOrderCollection", array);             
        }

        private void BuildTestOrder(BsonDocument bsonPO)
        {
            MongoCollection testOrderTable = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblTestOrder");
            MongoCursor mongoCursor = testOrderTable.FindAs<BsonDocument>(Query.EQ("PanelOrderId", bsonPO.GetValue("PanelOrderId")));
            BsonArray array = new BsonArray();

            foreach (BsonDocument bsonChild in mongoCursor)
            {
                array.Add(bsonChild);                
            }

            bsonPO.Add("TestOrderCollection", array);             
        }

        public static PSOTransferBuilder GetPSOTransferBuilder(int panelSetId)
        {
            PSOTransferBuilder result = null;
            switch (panelSetId)
            {
                case 13:
                    result = new PSOSurgicalTransferBuilder();
                    break;
                case 15:
                    result = new PSOCytologyTransferBuilder();
                    break;
                default:
                    result = new PSOTransferBuilder();
                    break;
            }
            return result;
        }
    }
}
