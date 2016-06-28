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
	public class AccessionOrderGatewayMongo
	{
        public AccessionOrderGatewayMongo()
        {   
                     
        }
        public static DateTime GetLastDateTransferred()
        {            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select max(DateTransferred) from tblMongoDatesTransferred";            
            cmd.CommandType = CommandType.Text;

            string date = string.Empty;
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        date = dr[0].ToString();                        
                    }
                }
            }
            return DateTime.Parse(date);
        }

        public static void InsertLastDateTransferrred(DateTime lastDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert tblMongoDatesTransferred (DateTransferred) values (@DateTransferred)";
            cmd.Parameters.Add("@DateTransferred", SqlDbType.DateTime).Value = lastDate;
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static Test.AccessionOrder GetAccessionOrderByMasterAccessionNo(string masterAccessionNo)
		{
			YellowstonePathology.Business.Mongo.Server transferServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection collection = transferServer.Database.GetCollection<BsonDocument>("AccessionOrder");
			BsonDocument bsonDocument = collection.FindOneAs<BsonDocument>(Query.EQ("MasterAccessionNo", BsonValue.Create(masterAccessionNo)));
			YellowstonePathology.Business.Test.AccessionOrder result = (YellowstonePathology.Business.Test.AccessionOrder)Mongo.BSONObjectBuilder.Build(bsonDocument, typeof(YellowstonePathology.Business.Test.AccessionOrder));
			return result;
		}

		public static Test.AccessionOrder GetAccessionOrderByReportNo(string reportNo)
		{
			YellowstonePathology.Business.Mongo.Server transferServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection collection = transferServer.Database.GetCollection<BsonDocument>("AccessionOrder");
			BsonDocument bsonDocument = collection.FindOneAs<BsonDocument>(Query.EQ("PanelSetOrderCollection.ReportNo", BsonValue.Create(reportNo)));
			YellowstonePathology.Business.Test.AccessionOrder result = (YellowstonePathology.Business.Test.AccessionOrder)Mongo.BSONObjectBuilder.Build(bsonDocument, typeof(YellowstonePathology.Business.Test.AccessionOrder));
			return result;
		}

		public static YellowstonePathology.Business.Test.AccessionOrderView GetAccessionOrderView(string masterAccessionNo)
		{
			YellowstonePathology.Business.Mongo.Server transferServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection collection = transferServer.Database.GetCollection<BsonDocument>("AccessionOrder");
			BsonDocument bsonDocument = collection.FindOneAs<BsonDocument>(Query.EQ("MasterAccessionNo", BsonValue.Create(masterAccessionNo)));
			YellowstonePathology.Business.Test.AccessionOrderView result = (YellowstonePathology.Business.Test.AccessionOrderView)Mongo.BSONObjectBuilder.Build(bsonDocument, typeof(YellowstonePathology.Business.Test.AccessionOrderView));
			return result;
		}

		public static YellowstonePathology.Business.Test.PanelSetOrderView GetCaseToSchedule(string reportNo)
		{
			YellowstonePathology.Business.Test.PanelSetOrderView result = null;
			YellowstonePathology.Business.Mongo.Server transferServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection collection = transferServer.Database.GetCollection<BsonDocument>("AccessionOrder");
			BsonDocument bsonDocument = collection.FindOneAs<BsonDocument>(Query.EQ("PanelSetOrderCollection.ReportNo", BsonValue.Create(reportNo)));
			BsonArray panelSetOrders = bsonDocument.GetValue("PanelSetOrderCollection").AsBsonArray;
			foreach (BsonDocument panelSetOrderDocument in panelSetOrders)
			{
				if (panelSetOrderDocument.GetValue("ReportNo").AsString == reportNo)
				{
					result = (YellowstonePathology.Business.Test.PanelSetOrderView)Mongo.BSONObjectBuilder.Build(panelSetOrderDocument, typeof(YellowstonePathology.Business.Test.PanelSetOrderView));
					break;
				}
			}
			return result;
		}

		public static List<YellowstonePathology.Business.Test.PanelSetOrderView> GetCasesToSchedule()
		{
			List<YellowstonePathology.Business.Test.PanelSetOrderView> result = new List<Test.PanelSetOrderView>();
			YellowstonePathology.Business.Mongo.Server transferServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection collection = transferServer.Database.GetCollection<BsonDocument>("AccessionOrder");
			
			MongoCursor cursor = collection.FindAs<BsonDocument>(Query.ElemMatch("PanelSetOrderCollection", Query.And(Query.EQ("Final", BsonValue.Create(true)),
				Query.EQ("ScheduledPublishTime", BsonNull.Value), Query.EQ("Published", BsonValue.Create(false)))));

			foreach (BsonDocument accessionOrders in cursor)
			{
				BsonArray panelSetOrders = accessionOrders.GetValue("PanelSetOrderCollection").AsBsonArray;
				foreach (BsonDocument panelSetOrderDocument in panelSetOrders)
				{
					if (panelSetOrderDocument.GetValue("Final").AsBoolean == true && 
						panelSetOrderDocument.GetValue("Published").AsBoolean == false &&
						panelSetOrderDocument.GetValue("ScheduledPublishTime").IsBsonNull)
					{
						YellowstonePathology.Business.Test.PanelSetOrderView panelSetOrderView = (YellowstonePathology.Business.Test.PanelSetOrderView)Mongo.BSONObjectBuilder.Build(panelSetOrderDocument, typeof(YellowstonePathology.Business.Test.PanelSetOrderView));
						result.Add(panelSetOrderView);
					}
				}
			}
			return result;
		}

		public static List<YellowstonePathology.Business.Test.PanelSetOrderView> GetNextCasesToPublish()
		{
			BsonValue currentDate = BsonValue.Create(DateTime.Now);
			List<YellowstonePathology.Business.Test.PanelSetOrderView> result = GetCasesWithQualifyingScheduledPublishTime(currentDate);
			List<YellowstonePathology.Business.Test.PanelSetOrderView> moreResults = GetCasesWithQualifyingScheduledDistributionTime(currentDate);

			foreach (YellowstonePathology.Business.Test.PanelSetOrderView possiblePanelSetOrderView in moreResults)
			{
				bool exists = false;
				foreach (YellowstonePathology.Business.Test.PanelSetOrderView existingPanelSetOrderView in result)
				{
					if (existingPanelSetOrderView.ReportNo == possiblePanelSetOrderView.ReportNo)
					{
						exists = true;
						break;
					}
				}

				if (exists == false)
				{
					result.Add(possiblePanelSetOrderView);
				}
			}

			return result;
		}

		private static List<YellowstonePathology.Business.Test.PanelSetOrderView> GetCasesWithQualifyingScheduledPublishTime(BsonValue currentDate)
		{
			List<YellowstonePathology.Business.Test.PanelSetOrderView> result = new List<Test.PanelSetOrderView>();
			YellowstonePathology.Business.Mongo.Server transferServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection collection = transferServer.Database.GetCollection<BsonDocument>("AccessionOrder");

			MongoCursor accessionOrderCursor = collection.FindAs<BsonDocument>(Query.ElemMatch("PanelSetOrderCollection", Query.And(Query.EQ("Final", BsonValue.Create(true)),
				Query.LTE("ScheduledPublishTime", currentDate))));


			foreach (BsonDocument accessionOrderDocument in accessionOrderCursor)
			{
				BsonArray panelSetOrders = accessionOrderDocument.GetValue("PanelSetOrderCollection").AsBsonArray;
				foreach (BsonDocument panelSetOrderDocument in panelSetOrders)
				{
					if (panelSetOrderDocument.GetValue("Final").AsBoolean == true &&
						panelSetOrderDocument.GetValue("ScheduledPublishTime").IsBsonNull == false &&
						panelSetOrderDocument.GetValue("ScheduledPublishTime") <= currentDate)
					{
						YellowstonePathology.Business.Test.PanelSetOrderView panelSetOrderView = (YellowstonePathology.Business.Test.PanelSetOrderView)Mongo.BSONObjectBuilder.Build(panelSetOrderDocument, typeof(YellowstonePathology.Business.Test.PanelSetOrderView));
						result.Add(panelSetOrderView);
					}
				}
			}

			return result;
		}

		private static List<YellowstonePathology.Business.Test.PanelSetOrderView> GetCasesWithQualifyingScheduledDistributionTime(BsonValue currentDate)
		{
			List<YellowstonePathology.Business.Test.PanelSetOrderView> result = new List<Test.PanelSetOrderView>();
			YellowstonePathology.Business.Mongo.Server transferServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection collection = transferServer.Database.GetCollection<BsonDocument>("AccessionOrder");

			MongoCursor accessionOrderCursor = collection.FindAs<BsonDocument>(Query.ElemMatch("PanelSetOrderCollection", Query.And(Query.EQ("Final", BsonValue.Create(true)),
				Query.LTE("TestOrderReportDistributionCollection.ScheduledDistributionTime", currentDate))));


			foreach (BsonDocument accessionOrderDocument in accessionOrderCursor)
			{
				BsonArray panelSetOrders = accessionOrderDocument.GetValue("PanelSetOrderCollection").AsBsonArray;
				foreach (BsonDocument panelSetOrderDocument in panelSetOrders)
				{
					if (panelSetOrderDocument.GetValue("Final").AsBoolean == true)
					{
						BsonArray testOrderReportDistributions = panelSetOrderDocument.GetValue("TestOrderReportDistributionCollection").AsBsonArray;
						foreach (BsonDocument testOrderReportDistributionDocument in testOrderReportDistributions)
						{
							if (testOrderReportDistributionDocument.GetValue("ScheduledDistributionTime").IsBsonNull == false &&
								testOrderReportDistributionDocument.GetValue("ScheduledDistributionTime") <= currentDate)
							{
								YellowstonePathology.Business.Test.PanelSetOrderView panelSetOrderView = (YellowstonePathology.Business.Test.PanelSetOrderView)Mongo.BSONObjectBuilder.Build(panelSetOrderDocument, typeof(YellowstonePathology.Business.Test.PanelSetOrderView));
								result.Add(panelSetOrderView);
							}
						}
					}
				}
			}

			return result;
		}

		public static YellowstonePathology.Business.Facility.Model.HostCollection GetHostCollection()
		{
			YellowstonePathology.Business.Facility.Model.HostCollection result = new YellowstonePathology.Business.Facility.Model.HostCollection();

			YellowstonePathology.Business.Mongo.Server transferServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection collection = transferServer.Database.GetCollection<BsonDocument>("Host");
			MongoCursor cursor = collection.FindAllAs<BsonDocument>();
			foreach (BsonDocument bsonDocument in cursor)
			{
				YellowstonePathology.Business.Facility.Model.Host host = new YellowstonePathology.Business.Facility.Model.Host();
				YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(bsonDocument, host);
				result.Add(host);
			}
			return result;
		}

		public static YellowstonePathology.Business.Typing.TypingShortcutCollection GetTypingShortcutCollectionByUser(int userId)
		{
			YellowstonePathology.Business.Typing.TypingShortcutCollection result = new Typing.TypingShortcutCollection();

			YellowstonePathology.Business.Mongo.Server transferServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection collection = transferServer.Database.GetCollection<BsonDocument>("TypingShortcut");
			MongoCursor cursor = collection.FindAs<BsonDocument>(Query.EQ("UserId", BsonValue.Create(userId))).SetSortOrder(SortBy.Ascending("Shortcut"));
			foreach (BsonDocument bsonDocument in cursor)
			{
				YellowstonePathology.Business.Typing.TypingShortcut typingShortcut = new YellowstonePathology.Business.Typing.TypingShortcut();
				YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(bsonDocument, typingShortcut);
				result.Add(typingShortcut);
			}
			return result;
		}

		public static YellowstonePathology.Business.NeogenomicsResultCollection GetNeogenomicsResultCollection()
		{
			YellowstonePathology.Business.NeogenomicsResultCollection result = new YellowstonePathology.Business.NeogenomicsResultCollection();

			YellowstonePathology.Business.Mongo.Server transferServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection collection = transferServer.Database.GetCollection<BsonDocument>("NeogenomicsResult");
			MongoCursor cursor = collection.FindAllAs<BsonDocument>().SetSortOrder(SortBy.Descending("DateResultReceived"));
			foreach (BsonDocument bsonDocument in cursor)
			{
				YellowstonePathology.Business.NeogenomicsResult neogenomicsResult = new YellowstonePathology.Business.NeogenomicsResult();
				YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(bsonDocument, neogenomicsResult);
				result.Add(neogenomicsResult);
			}
			return result;
		}
	}
}
