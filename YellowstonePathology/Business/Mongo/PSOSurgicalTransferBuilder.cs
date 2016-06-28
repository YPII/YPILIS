using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace YellowstonePathology.Business.Mongo
{
    public class PSOSurgicalTransferBuilder : PSOTransferBuilder
    {
        public PSOSurgicalTransferBuilder()
        {

        }

        public override void Build(MongoDB.Bson.BsonDocument bsonPanelSetOrder, int panelSetId)
        {
            base.Build(bsonPanelSetOrder, panelSetId);

            string reportNo = bsonPanelSetOrder.GetValue("ReportNo").ToString();
            MongoCollection surgicalSpecimenTable = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblSurgicalSpecimen");
            MongoCursor mongoCursor = surgicalSpecimenTable.FindAs<BsonDocument>(Query.EQ("ReportNo", reportNo));

            BsonArray bsonArray = new BsonArray();
            foreach (BsonDocument bsonSurgicalSpecimen in mongoCursor)
            {
				string surgicalSpecimenId = bsonSurgicalSpecimen.GetValue("SurgicalSpecimenId").AsString;
				this.BuildIC(bsonSurgicalSpecimen, surgicalSpecimenId);
				this.BuildStainResult(bsonSurgicalSpecimen, surgicalSpecimenId);
				this.BuildICD9BillingCode(bsonSurgicalSpecimen, surgicalSpecimenId);

                bsonArray.Add(bsonSurgicalSpecimen);
            }

            bsonPanelSetOrder.Add("SurgicalSpecimenCollection", bsonArray);

			MongoCollection surgicalAuditTable = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblSurgicalAudit");
			MongoCursor surgicalAuditCursor = surgicalAuditTable.FindAs<BsonDocument>(Query.EQ("ReportNo", reportNo));

			BsonArray surgicalAuditArray = new BsonArray();
			foreach (BsonDocument bsonSurgicalAudit in surgicalAuditCursor)
			{
				string surgicalAuditId = bsonSurgicalAudit.GetValue("SurgicalAuditId").AsString;
				this.BuildSurgicalSpecimenAudit(bsonSurgicalAudit, surgicalAuditId);

				surgicalAuditArray.Add(bsonSurgicalAudit);
			}

			bsonPanelSetOrder.Add("SurgicalAuditCollection", surgicalAuditArray);
		}

		private void BuildIC(BsonDocument bsonSurgicalSpecimen, string surgicalSpecimenId)
		{
			MongoCollection intraoperativeConsultationResultTable = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblIntraoperativeConsultationResult");
			MongoCursor mongoCursor = intraoperativeConsultationResultTable.FindAs<BsonDocument>(Query.EQ("SurgicalSpecimenId", surgicalSpecimenId));

			BsonArray bsonArray = new BsonArray();
			foreach (BsonDocument bsonintraoperativeConsultationResult in mongoCursor)
			{
				bsonArray.Add(bsonintraoperativeConsultationResult);
			}

			bsonSurgicalSpecimen.Add("IntraoperativeConsultationResultCollection", bsonArray);
		}

		private void BuildStainResult(BsonDocument bsonSurgicalSpecimen, string surgicalSpecimenId)
		{
			MongoCollection stainResultTable = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblStainResult");
			MongoCursor mongoCursor = stainResultTable.FindAs<BsonDocument>(Query.EQ("SurgicalSpecimenId", surgicalSpecimenId));

			BsonArray bsonArray = new BsonArray();
			foreach (BsonDocument bsonStainResult in mongoCursor)
			{
				bsonArray.Add(bsonStainResult);
			}

			bsonSurgicalSpecimen.Add("StainResultItemCollection", bsonArray);
		}

		private void BuildICD9BillingCode(BsonDocument bsonSurgicalSpecimen, string surgicalSpecimenId)
		{
			MongoCollection icd9BillingCodeTable = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblICD9BillingCode");
			MongoCursor mongoCursor = icd9BillingCodeTable.FindAs<BsonDocument>(Query.EQ("SurgicalSpecimenId", surgicalSpecimenId));

			BsonArray bsonArray = new BsonArray();
			foreach (BsonDocument bsonICD9BillingCode in mongoCursor)
			{
				bsonArray.Add(bsonICD9BillingCode);
			}

			bsonSurgicalSpecimen.Add("ICD9BillingCodeCollection", bsonArray);
		}

		private void BuildSurgicalSpecimenAudit(BsonDocument bsonSurgicalAudit, string surgicalAuditId)
		{
			MongoCollection surgicalSpecimenAuditTable = this.m_SQLTransferDatabase.GetCollection<BsonDocument>("tblSurgicalSpecimenAudit");
			MongoCursor mongoCursor = surgicalSpecimenAuditTable.FindAs<BsonDocument>(Query.EQ("SurgicalAuditId", surgicalAuditId));

			BsonArray bsonArray = new BsonArray();
			foreach (BsonDocument bsonSurgicalSpecimenAudit in mongoCursor)
			{
				bsonArray.Add(bsonSurgicalSpecimenAudit);
			}

			bsonSurgicalAudit.Add("SurgicalSpecimenAuditCollection", bsonArray);
		}
	}
}
