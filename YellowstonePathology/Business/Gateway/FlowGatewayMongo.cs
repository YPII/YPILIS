using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;

namespace YellowstonePathology.Business.Gateway
{
	public class FlowGatewayMongo
	{
		public FlowGatewayMongo() { }

		public static Flow.FlowLogList GetByLeukemiaNotFinal()
		{
			YellowstonePathology.Business.Flow.FlowLogList result = new YellowstonePathology.Business.Flow.FlowLogList();
			YellowstonePathology.Business.Mongo.Server transferServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection collection = transferServer.Database.GetCollection<BsonDocument>("AccessionOrder");
			MongoCursor cursor = collection.FindAs<BsonDocument>(Query.ElemMatch("PanelSetOrderCollection", Query.And(Query.EQ("PanelSetId", BsonValue.Create(20)),
				Query.EQ("Final", BsonValue.Create(false)))))
				.SetSortOrder(SortBy.Descending("AccessionDate", "PanelSetOrderCollection.ReportNo"));

			foreach (BsonDocument bsonDocument in cursor)
			{
				foreach (BsonDocument panelSetDocument in bsonDocument.GetValue("PanelSetOrderCollection").AsBsonArray)
				{
					if (panelSetDocument.GetValue("PanelSetId").AsInt32 == 20)
					{
						YellowstonePathology.Business.Flow.FlowLogListItem flowLogListItem = BuildFlowLog(bsonDocument, panelSetDocument);
						result.Add(flowLogListItem);
					}
				}
			}
			return result;
		}


		public static Flow.FlowLogList GetByTestType(int panelSetId)
		{
			YellowstonePathology.Business.Flow.FlowLogList result = new YellowstonePathology.Business.Flow.FlowLogList();
			YellowstonePathology.Business.Mongo.Server transferServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection collection = transferServer.Database.GetCollection<BsonDocument>("AccessionOrder");
			MongoCursor cursor = collection.FindAs<BsonDocument>(Query.ElemMatch("PanelSetOrderCollection", Query.EQ("PanelSetId", BsonValue.Create(panelSetId))))
				.SetSortOrder(SortBy.Descending("AccessionDate", "PanelSetOrderCollection.ReportNo"));

			foreach (BsonDocument bsonDocument in cursor)
			{
				foreach (BsonDocument panelSetDocument in bsonDocument.GetValue("PanelSetOrderCollection").AsBsonArray)
				{
					if (panelSetDocument.GetValue("PanelSetId").AsInt32 == panelSetId)
					{
						YellowstonePathology.Business.Flow.FlowLogListItem flowLogListItem = BuildFlowLog(bsonDocument, panelSetDocument);
						result.Add(flowLogListItem);

					}
				}
			}
			return result;
		}

		public static Flow.FlowLogList GetFlowLogListByReportNo(string reportNo)
		{
			YellowstonePathology.Business.Flow.FlowLogList result = new YellowstonePathology.Business.Flow.FlowLogList();
			YellowstonePathology.Business.Mongo.Server transferServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection collection = transferServer.Database.GetCollection<BsonDocument>("AccessionOrder");
			MongoCursor cursor = collection.FindAs<BsonDocument>(Query.ElemMatch("PanelSetOrderCollection", Query.EQ("ReportNo", BsonValue.Create(reportNo))));

			foreach (BsonDocument bsonDocument in cursor)
			{
				foreach (BsonDocument panelSetDocument in bsonDocument.GetValue("PanelSetOrderCollection").AsBsonArray)
				{
					if (panelSetDocument.GetValue("ReportNo").AsString == reportNo)
					{
						YellowstonePathology.Business.Flow.FlowLogListItem flowLogListItem = BuildFlowLog(bsonDocument, panelSetDocument);
						result.Add(flowLogListItem);

						break;
					}
				}
			}
			return result;
		}

		public static Flow.FlowLogList GetByAccessionMonth(DateTime date)
		{
			YellowstonePathology.Business.Flow.FlowLogList result = new YellowstonePathology.Business.Flow.FlowLogList();
			List<BsonValue> values = new List<BsonValue>();
			values.Add(BsonValue.Create(19));
			values.Add(BsonValue.Create(143));

			YellowstonePathology.Business.Mongo.Server transferServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection collection = transferServer.Database.GetCollection<BsonDocument>("AccessionOrder");
			MongoCursor cursor = collection.FindAs<BsonDocument>(Query.And(Query.GTE("AccessionDate", BsonValue.Create(date)),
				Query.LT("AccessionDate", BsonValue.Create(date.AddMonths(1))),
				Query.ElemMatch("PanelSetOrderCollection", Query.And(Query.EQ("CaseType", BsonValue.Create("Flow Cytometry")),
				Query.NotIn("PanelSetId", values)))))
				.SetSortOrder(SortBy.Descending("AccessionDate", "PanelSetOrderCollection.ReportNo"));


			foreach (BsonDocument bsonDocument in cursor)
			{
				foreach (BsonDocument panelSetDocument in bsonDocument.GetValue("PanelSetOrderCollection").AsBsonArray)
				{
					int panelSetId = panelSetDocument.GetValue("PanelSetId").AsInt32;
					if (panelSetDocument.GetValue("CaseType").AsString == "Flow Cytometry" && panelSetId != 19 && panelSetId != 143)
					{
						YellowstonePathology.Business.Flow.FlowLogListItem flowLogListItem = BuildFlowLog(bsonDocument, panelSetDocument);
						result.Add(flowLogListItem);
						break;
					}
				}
			}
			return result;
		}

		public static Flow.FlowLogList GetByPatientName(string patientName)
		{
			YellowstonePathology.Business.Flow.FlowLogList result = new YellowstonePathology.Business.Flow.FlowLogList();
			List<BsonValue> values = new List<BsonValue>();
			values.Add(BsonValue.Create(19));
			values.Add(BsonValue.Create(143));

			YellowstonePathology.Business.Mongo.Server transferServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection collection = transferServer.Database.GetCollection<BsonDocument>("AccessionOrder");

			MongoCursor cursor = null;
			string[] commaSplit = patientName.Split(',');
			switch (commaSplit.Length)
			{
				case 1:
					cursor = collection.FindAs<BsonDocument>(Query.And(Query.Matches("PLastName", BsonRegularExpression.Create(commaSplit[0] + ".*", "i")),
						Query.ElemMatch("PanelSetOrderCollection", Query.And(Query.EQ("CaseType", BsonValue.Create("Flow Cytometry")),
						Query.NotIn("PanelSetId", values)))))
						.SetSortOrder(SortBy.Descending("AccessionDate", "PanelSetOrderCollection.ReportNo"));
					break;
				case 2:
					cursor = collection.FindAs<BsonDocument>(Query.And(Query.Matches("PLastName", BsonRegularExpression.Create(commaSplit[0] + ".*", "i")),
						Query.Matches("PFirstName", BsonRegularExpression.Create(commaSplit[1].Trim() + ".*", "i")),
						Query.ElemMatch("PanelSetOrderCollection", Query.And(Query.EQ("CaseType", BsonValue.Create("Flow Cytometry")),
						Query.NotIn("PanelSetId", values)))))
						.SetSortOrder(SortBy.Descending("AccessionDate", "PanelSetOrderCollection.ReportNo"));
					break;
			}

			foreach (BsonDocument bsonDocument in cursor)
			{
				foreach (BsonDocument panelSetDocument in bsonDocument.GetValue("PanelSetOrderCollection").AsBsonArray)
				{
					int panelSetId = panelSetDocument.GetValue("PanelSetId").AsInt32;
					if (panelSetDocument.GetValue("CaseType").AsString == "Flow Cytometry" && panelSetId != 19 && panelSetId != 143)
					{
						YellowstonePathology.Business.Flow.FlowLogListItem flowLogListItem = BuildFlowLog(bsonDocument, panelSetDocument);
						result.Add(flowLogListItem);
						break;
					}
				}
			}
			return result;
		}

		public static Flow.FlowLogList GetByPathologistId(int pathologistId)
		{
			YellowstonePathology.Business.Flow.FlowLogList result = new YellowstonePathology.Business.Flow.FlowLogList();
			DateTime matchDate = DateTime.Today.AddYears(-1);
			List<BsonValue> values = new List<BsonValue>();
			values.Add(BsonValue.Create(19));
			values.Add(BsonValue.Create(143));

			YellowstonePathology.Business.Mongo.Server transferServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection collection = transferServer.Database.GetCollection<BsonDocument>("AccessionOrder");
			MongoCursor cursor = collection.FindAs<BsonDocument>(Query.And(Query.GTE("AccessionDate", BsonValue.Create(matchDate)),
				Query.ElemMatch("PanelSetOrderCollection", Query.And(Query.EQ("CaseType", BsonValue.Create("Flow Cytometry")),
				Query.NotIn("PanelSetId", values), Query.EQ("AssignedToId", BsonValue.Create(pathologistId))))))
				.SetSortOrder(SortBy.Descending("AccessionDate", "PanelSetOrderCollection.ReportNo"));


			foreach (BsonDocument bsonDocument in cursor)
			{
				foreach (BsonDocument panelSetDocument in bsonDocument.GetValue("PanelSetOrderCollection").AsBsonArray)
				{
					int panelSetId = panelSetDocument.GetValue("PanelSetId").AsInt32;
					if (panelSetDocument.GetValue("CaseType").AsString == "Flow Cytometry" && panelSetId != 19 && panelSetId != 143)
					{
						YellowstonePathology.Business.Flow.FlowLogListItem flowLogListItem = BuildFlowLog(bsonDocument, panelSetDocument);
						result.Add(flowLogListItem);
						break;
					}
				}
			}
			return result;
		}

		private static YellowstonePathology.Business.Flow.FlowLogListItem BuildFlowLog(BsonDocument accessionDocument, BsonDocument panelSetDocument)
		{
			YellowstonePathology.Business.Flow.FlowLogListItem result = new YellowstonePathology.Business.Flow.FlowLogListItem();
			result.PLastName = accessionDocument.GetValue("PLastName").AsString;
			result.PFirstName = accessionDocument.GetValue("PFirstName").AsString;
			result.AccessionDate = accessionDocument.GetValue("AccessionDate").AsDateTime;
			result.ObjectId = panelSetDocument.GetValue("_id").AsObjectId.ToString();
			result.ReportNo = panelSetDocument.GetValue("ReportNo").AsString;
			result.FinalDate = panelSetDocument.GetValue("FinalDate").AsNullableDateTime;
			result.TestName = panelSetDocument.GetValue("PanelSetName").AsString;
			return result;
		}

		public static Flow.FlowMarkerCollection GetFlowMarkerCollectionByReportNo(string reportNo)
		{
			YellowstonePathology.Business.Flow.FlowMarkerCollection result = new YellowstonePathology.Business.Flow.FlowMarkerCollection();
			YellowstonePathology.Business.Mongo.Server transferServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection collection = transferServer.Database.GetCollection<BsonDocument>("FlowMarkers");
			MongoCursor cursor = collection.FindAs<BsonDocument>(Query.EQ("ReportNo", BsonValue.Create(reportNo)));

			List<BsonValue> markerNames = new List<BsonValue>();
			foreach (BsonDocument bsonDocument in cursor)
			{
				markerNames.Add(bsonDocument.GetValue("Name"));

			}
			MongoCollection markerCollection = transferServer.Database.GetCollection<BsonDocument>("Markers");
			MongoCursor markerCursor = markerCollection.FindAs<BsonDocument>(Query.In("MarkerName", markerNames)).SetSortOrder(SortBy.Ascending("OrderFlag", "MarkerName"));

			foreach (BsonDocument bsonDocument in cursor)
			{
				foreach (BsonDocument markerDocument in markerCursor)
				{
					if(bsonDocument.GetValue("Name").Equals(markerDocument.GetValue("MarkerName")))
					{
						YellowstonePathology.Business.Flow.FlowMarkerItem flowMarkerItem = new Flow.FlowMarkerItem();
						YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(bsonDocument, flowMarkerItem);
						result.Add(flowMarkerItem);
						break;
					}
				}
			}

			return result;
		}

		public static Flow.FlowMarkerCollection GetFlowMarkerCollectionByPanelId(string reportNo, int panelId)
		{
			YellowstonePathology.Business.Flow.FlowMarkerCollection result = new YellowstonePathology.Business.Flow.FlowMarkerCollection();
			YellowstonePathology.Business.Mongo.Server transferServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection collection = transferServer.Database.GetCollection<BsonDocument>("FlowMarkerPanel");
			MongoCursor cursor = collection.FindAs<BsonDocument>(Query.EQ("PanelId", BsonValue.Create(panelId)));

			List<BsonValue> markerNames = new List<BsonValue>();
			foreach (BsonDocument bsonDocument in cursor)
			{
				markerNames.Add(bsonDocument.GetValue("MarkerName"));

			}
			MongoCollection markerCollection = transferServer.Database.GetCollection<BsonDocument>("Markers");
			MongoCursor markerCursor = markerCollection.FindAs<BsonDocument>(Query.In("MarkerName", markerNames)).SetSortOrder(SortBy.Ascending("OrderFlag", "MarkerName"));

			foreach (BsonDocument bsonDocument in cursor)
			{
				foreach (BsonDocument markerDocument in markerCursor)
				{
					if (bsonDocument.GetValue("MarkerName").Equals(markerDocument.GetValue("MarkerName")))
					{
						YellowstonePathology.Business.Flow.FlowMarkerItem flowMarkerItem = new Flow.FlowMarkerItem();
						flowMarkerItem.Name = bsonDocument.GetValue("MarkerName").AsString;
						flowMarkerItem.MarkerUsed = true;
						flowMarkerItem.Intensity = Mongo.ValueHelper.GetStringValue(bsonDocument.GetValue("Intensity"));
						flowMarkerItem.Interpretation = Mongo.ValueHelper.GetStringValue(bsonDocument.GetValue("Interpretation"));

						result.Add(flowMarkerItem);
						break;
					}
				}
			}

			return result;
		}
	}
}
