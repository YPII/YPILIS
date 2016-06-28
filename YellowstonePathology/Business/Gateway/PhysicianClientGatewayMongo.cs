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
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;

namespace YellowstonePathology.Business.Gateway
{
	public class PhysicianClientGatewayMongo
	{        
        public static YellowstonePathology.Business.Client.Model.ClientGroupCollection GetClientGroupCollection()
        {
            YellowstonePathology.Business.Client.Model.ClientGroupCollection result = new Client.Model.ClientGroupCollection();

            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
            MongoCollection collection = server.Database.GetCollection<BsonDocument>("ClientGroup");
            MongoCursor cursor = collection.FindAllAs<BsonDocument>();
            foreach (BsonDocument bsonDocument in cursor)
            {
                YellowstonePathology.Business.Client.Model.ClientGroup clientGroup = new Client.Model.ClientGroup();
                YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(bsonDocument, clientGroup);
                result.Add(clientGroup);
            }

            return result;
        }

        public static YellowstonePathology.Business.Client.Model.Client GetClientByClientId(int clientId)
        {
            YellowstonePathology.Business.Client.Model.Client result = new YellowstonePathology.Business.Client.Model.Client();            
            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
            MongoCollection collection = server.Database.GetCollection<BsonDocument>("Client");
            BsonDocument bsonDocument = collection.FindOneAs<BsonDocument>(Query.EQ("ClientId", BsonValue.Create(clientId)));
            YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(bsonDocument, result);
            return result;
        }

        public static YellowstonePathology.Business.Client.Model.ClientCollection GetAllClients()
        {            
            YellowstonePathology.Business.Client.Model.ClientCollection result = new YellowstonePathology.Business.Client.Model.ClientCollection();
            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
            MongoCollection collection = server.Database.GetCollection<BsonDocument>("Client");
            MongoCursor cursor = collection.FindAllAs<BsonDocument>();

            foreach (BsonDocument bsonDocument in cursor)
            {
                YellowstonePathology.Business.Client.Model.Client client = new YellowstonePathology.Business.Client.Model.Client();
                YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(bsonDocument, client);
                result.Add(client);
            }

            return result;
        }

		public static Domain.Physician GetPhysicianByPhysicianId(int physicianId)
		{
			YellowstonePathology.Business.Domain.Physician result = new YellowstonePathology.Business.Domain.Physician();
			YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
            MongoCollection collection = server.Database.GetCollection<BsonDocument>("Physician");
			BsonDocument bsonDocument = collection.FindOneAs<BsonDocument>(Query.EQ("PhysicianId", BsonValue.Create(physicianId)));
			YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(bsonDocument, result);
			return result;
		}

		public static Domain.Physician GetPhysicianByNpi(string npi)
		{
			YellowstonePathology.Business.Domain.Physician result = new YellowstonePathology.Business.Domain.Physician();
            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
            MongoCollection collection = server.Database.GetCollection<BsonDocument>("Physician");
			BsonDocument bsonDocument = collection.FindOneAs<BsonDocument>(Query.EQ("Npi", BsonValue.Create(npi)));
			YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(bsonDocument, result);
			return result;
		}

        public static Domain.PhysicianClient GetPhysicianClient(int physicianId, int clientId)
        {
			YellowstonePathology.Business.Domain.PhysicianClient result = new YellowstonePathology.Business.Domain.PhysicianClient();

            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
            MongoCollection collection = server.Database.GetCollection<BsonDocument>("PhysicianClient");
			BsonDocument bsonDocument = collection.FindOneAs<BsonDocument>(Query.And(Query.EQ("PhysicianId", BsonValue.Create(physicianId)), Query.EQ("ClientId", BsonValue.Create(clientId))));
			YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(bsonDocument, result);
			return result;
		}

		public static YellowstonePathology.Business.Client.Model.ClientCollection GetClientsByClientName(string clientName)
		{
			YellowstonePathology.Business.Client.Model.ClientCollection result = new YellowstonePathology.Business.Client.Model.ClientCollection();
            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
            MongoCollection collection = server.Database.GetCollection<BsonDocument>("Client");
			MongoCursor cursor = collection.FindAs<BsonDocument>(Query.Matches("ClientName", BsonRegularExpression.Create("^" + clientName + ".*", "i")));

			foreach (BsonDocument bsonDocument in cursor)
			{
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)YellowstonePathology.Business.Mongo.BSONObjectBuilder.Build(bsonDocument, typeof(YellowstonePathology.Business.Client.Model.Client));
				result.Add(client);
			}

			return result;
		}

        public static Domain.PhysicianCollection GetPhysiciansByClientId(int clientId)
        {
            Domain.PhysicianCollection result = new Domain.PhysicianCollection();
            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
            MongoCollection physicianClientCollection = server.Database.GetCollection<BsonDocument>("PhysicianClient");
            MongoCursor physicianClientCursor = physicianClientCollection.FindAs<BsonDocument>(Query.EQ("ClientId", BsonValue.Create(clientId)));

            List<BsonValue> physicianIdList = new List<BsonValue>();
            foreach (BsonDocument bsonDocument in physicianClientCursor)
            {
                physicianIdList.Add(bsonDocument.GetValue("PhysicianId"));
            }

            MongoCollection physicianCollection = server.Database.GetCollection<BsonDocument>("Physician");
            MongoCursor physicianCursor = physicianCollection.FindAs<BsonDocument>(Query.In("PhysicianId", physicianIdList));
            foreach (BsonDocument bsonDocument in physicianCursor)
            {
                Domain.Physician physician = new Domain.Physician();
                YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(bsonDocument, physician);
                result.Add(physician);
            }

            return result;
        }

        public static Domain.ClientCollection GetClientsByPhysicianId(int physicianId)
        {
            Domain.ClientCollection result = new Domain.ClientCollection();
            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
            MongoCollection physicianClientCollection = server.Database.GetCollection<BsonDocument>("PhysicianClient");
            MongoCursor physicianClientCursor = physicianClientCollection.FindAs<BsonDocument>(Query.EQ("PhysicianId", BsonValue.Create(physicianId)));

            List<BsonValue> clientIdList = new List<BsonValue>();
            foreach (BsonDocument bsonDocument in physicianClientCursor)
            {
                clientIdList.Add(bsonDocument.GetValue("ClientId").AsInt32);
            }

            MongoCollection clientCollection = server.Database.GetCollection<BsonDocument>("Client");
            MongoCursor clientCursor = clientCollection.FindAs<BsonDocument>(Query.In("ClientId", clientIdList));
            foreach (BsonDocument bsonDocument in clientCursor)
            {
                YellowstonePathology.Business.Client.Model.Client client = new YellowstonePathology.Business.Client.Model.Client();
                YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(bsonDocument, client);
                result.Add(client);
            }

            return result;
        }

		public static View.ClientSearchView GetClientSearchViewByClientName(string clientName)
		{
			YellowstonePathology.Business.View.ClientSearchView result = new YellowstonePathology.Business.View.ClientSearchView();
            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
            MongoCollection collection = server.Database.GetCollection<BsonDocument>("Client");
			MongoCursor cursor = collection.FindAs<BsonDocument>(Query.Matches("ClientName", BsonRegularExpression.Create("^" + clientName + ".*", "i"))).SetSortOrder(SortBy.Ascending("ClientName"));

			foreach (BsonDocument bsonDocument in cursor)
			{
				YellowstonePathology.Business.View.ClientSearchViewItem clientSearchViewItem = new YellowstonePathology.Business.View.ClientSearchViewItem();
				YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(bsonDocument, clientSearchViewItem);
				result.Add(clientSearchViewItem);
			}

			return result;
		}

		public static Domain.PhysicianCollection GetPhysiciansByName(string firstName, string lastName)
		{
			YellowstonePathology.Business.Domain.PhysicianCollection result = new YellowstonePathology.Business.Domain.PhysicianCollection();
            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
            MongoCollection collection = server.Database.GetCollection<BsonDocument>("Physician");
			MongoCursor cursor = null;
			if (string.IsNullOrEmpty(firstName) == false)
			{
				cursor = collection.FindAs<BsonDocument>(Query.And(Query.Matches("LastName", BsonRegularExpression.Create("^" + lastName + ".*", "i")),
					Query.Matches("FirstName", BsonRegularExpression.Create("^" + firstName + ".*", "i")))).SetSortOrder(SortBy.Ascending("LastName", "FirstName"));
			}
			else
			{
				cursor = collection.FindAs<BsonDocument>(Query.Matches("LastName", BsonRegularExpression.Create("^" + lastName + ".*", "i"))).SetSortOrder(SortBy.Ascending("LastName", "FirstName"));
			}

			foreach (BsonDocument bsonDocument in cursor)
			{
				YellowstonePathology.Business.Domain.Physician physician = new YellowstonePathology.Business.Domain.Physician();
				YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(bsonDocument, physician);
				result.Add(physician);
			}

			return result;
		}

		public static View.ClientPhysicianView GetClientPhysicianViewByClientId(int clientId)
		{
			YellowstonePathology.Business.View.ClientPhysicianView result = new YellowstonePathology.Business.View.ClientPhysicianView();
            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
            MongoCollection clientCollection = server.Database.GetCollection<BsonDocument>("Client");
			BsonDocument clientBsonDocument = clientCollection.FindOneAs<BsonDocument>(Query.EQ("ClientId", BsonValue.Create(clientId)));
			YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(clientBsonDocument, result);

            MongoCollection physicianClientCollection = server.Database.GetCollection<BsonDocument>("PhysicianClient");
			MongoCursor physicianClientCursor = physicianClientCollection.FindAs<BsonDocument>(Query.EQ("ClientId", BsonValue.Create(clientId)));
			List<BsonValue> physicianIdList = new List<BsonValue>();
			foreach (BsonDocument bsonDocument in physicianClientCursor)
			{
				physicianIdList.Add(bsonDocument.GetValue("PhysicianId"));
			}

            MongoCollection physicianCollection = server.Database.GetCollection<BsonDocument>("Physician");
			MongoCursor physicianCursor = physicianCollection.FindAs<BsonDocument>(Query.In("PhysicianId", physicianIdList)).SetSortOrder(SortBy.Ascending("FirstName"));
			foreach (BsonDocument bsonDocument in physicianCursor)
			{
				YellowstonePathology.Business.Domain.Physician physician = new YellowstonePathology.Business.Domain.Physician();
				YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(bsonDocument, physician);
				result.Physicians.Add(physician);
			}

			return result;
		}

        public static View.PhysicianClientView GetPhysicianClientView(int physicianId)
        {
            View.PhysicianClientView physicianClientView = new View.PhysicianClientView();

            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
            MongoCollection physicianCollection = server.Database.GetCollection<BsonDocument>("Physician");
            BsonDocument physicianDocument = physicianCollection.FindOneAs<BsonDocument>(Query.EQ("PhysicianId", BsonValue.Create(physicianId)));
            YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(physicianDocument, physicianClientView);
            
            MongoCollection physicianClientCollection = server.Database.GetCollection<BsonDocument>("PhysicianClient");
            MongoCursor physicianClientCursor = physicianClientCollection.FindAs<BsonDocument>(Query.EQ("PhysicianId", BsonValue.Create(physicianClientView.PhysicianId)));
            List<BsonValue> clientIdList = new List<BsonValue>();
            foreach (BsonDocument bsonDocument in physicianClientCursor)
            {
                clientIdList.Add(bsonDocument.GetValue("ClientId"));
            }

            MongoCollection clientCollection = server.Database.GetCollection<BsonDocument>("Client");
            MongoCursor clientCursor = clientCollection.FindAs<BsonDocument>(Query.In("ClientId", clientIdList));
            foreach (BsonDocument bsonDocument in clientCursor)
            {
                YellowstonePathology.Business.Client.Model.Client client = new YellowstonePathology.Business.Client.Model.Client();
                YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(bsonDocument, client);
                physicianClientView.Clients.Add(client);
            }

            return physicianClientView;
        }

		public static YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection GetPhysicianClientNameCollection(string clientName, string physicianName)
		{
			YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection result = new YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection();
			YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection physicianClientCollection = server.Database.GetCollection<BsonDocument>("PhysicianClient");
			MongoCollection physicianCollection = server.Database.GetCollection<BsonDocument>("Physician");
			MongoCollection clientCollection = server.Database.GetCollection<BsonDocument>("Client");
			MongoCursor clientCursor = clientCollection.FindAs<BsonDocument>(Query.Matches("ClientName", BsonRegularExpression.Create("^" + clientName + ".*", "i"))).SetSortOrder(SortBy.Ascending("ClientName"));

			string physicianClientId = string.Empty;
			long count = clientCursor.Count();
			foreach (BsonDocument clientDocument in clientCursor)
			{
				BsonValue clientId = clientDocument.GetValue("ClientId");
				MongoCursor physicianClientCursor = physicianClientCollection.FindAs<BsonDocument>(Query.EQ("ClientId", clientId));
				List<BsonValue> physicianIdList = new List<BsonValue>();

				foreach (BsonDocument physicianClientDocument in physicianClientCursor)
				{
					physicianIdList.Add(physicianClientDocument.GetValue("PhysicianId"));
				}

				MongoCursor physicianCursor = physicianCollection.FindAs<BsonDocument>(Query.And(Query.In("PhysicianId", physicianIdList),
					Query.Matches("LastName", BsonRegularExpression.Create("^" + physicianName + ".*", "i")))).SetSortOrder(SortBy.Ascending("LastName", "FirstName"));

				foreach (BsonDocument physicianDocument in physicianCursor)
				{
					foreach (BsonDocument physicianClientDocument in physicianClientCursor)
					{
						if (physicianClientDocument.GetValue("PhysicianId").Equals(physicianDocument.GetValue("PhysicianId")) &&
							physicianClientDocument.GetValue("ClientId").Equals(clientId))
						{
							physicianClientId = Mongo.ValueHelper.GetStringValue(physicianClientDocument.GetValue("PhysicianClientId"));
							break;
						}
					}

					YellowstonePathology.Business.Client.Model.PhysicianClientName physicianClientName = new YellowstonePathology.Business.Client.Model.PhysicianClientName();
					physicianClientName.ClientId = clientId.AsInt32;
					physicianClientName.ClientName = Mongo.ValueHelper.GetStringValue(clientDocument.GetValue("ClientName"));
					physicianClientName.Fax = Mongo.ValueHelper.GetStringValue(clientDocument.GetValue("Fax"));
					physicianClientName.FirstName = Mongo.ValueHelper.GetStringValue(physicianDocument.GetValue("FirstName"));
					physicianClientName.LastName = Mongo.ValueHelper.GetStringValue(physicianDocument.GetValue("LastName"));
					physicianClientName.PhysicianClientId = physicianClientId;
					physicianClientName.PhysicianId = physicianDocument.GetValue("PhysicianId").AsInt32;
					physicianClientName.Telephone = Mongo.ValueHelper.GetStringValue(clientDocument.GetValue("Telephone"));

					result.Add(physicianClientName);
				}
			}

			return result;
		}

		public static YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection GetPhysicianClientNameCollection(string physicianClientId)
		{
			YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection result = new YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection();

			YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection physicianClientCollection = server.Database.GetCollection<BsonDocument>("PhysicianClient");
			MongoCollection physicianCollection = server.Database.GetCollection<BsonDocument>("Physician");
			MongoCollection clientCollection = server.Database.GetCollection<BsonDocument>("Client");

			BsonDocument pcDocument = physicianClientCollection.FindOneAs<BsonDocument>(Query.EQ("PhysicianClientId", physicianClientId));
			BsonValue physicianId = pcDocument.GetValue("PhysicianId");
			BsonDocument physicianDocument = physicianCollection.FindOneAs<BsonDocument>(Query.EQ("PhysicianId", physicianId));
			MongoCursor physicianClientCursor = physicianClientCollection.FindAs<BsonDocument>(Query.EQ("PhysicianId", physicianId));
			List<BsonValue> clientIdList = new List<BsonValue>();
			foreach(BsonDocument physicianClientDocument in physicianClientCursor)
			{
				clientIdList.Add(physicianClientDocument.GetValue("ClientId"));
			}
			MongoCursor clientCursor = clientCollection.FindAs<BsonDocument>(Query.In("ClientId", clientIdList)).SetSortOrder(SortBy.Ascending("ClientName"));
			foreach(BsonDocument clientDocument in clientCursor)
			{
				foreach (BsonDocument physicianClientDocument in physicianClientCursor)
				{
					if (physicianClientDocument.GetValue("ClientId").Equals(clientDocument.GetValue("ClientId")))
					{
						physicianClientId = Mongo.ValueHelper.GetStringValue(physicianClientDocument.GetValue("PhysicianClientId"));
						break;
					}
				}
					YellowstonePathology.Business.Client.Model.PhysicianClientName physicianClientName = new YellowstonePathology.Business.Client.Model.PhysicianClientName();
					physicianClientName.ClientId = clientDocument.GetValue("ClientId").AsInt32;
					physicianClientName.ClientName = Mongo.ValueHelper.GetStringValue(clientDocument.GetValue("ClientName"));
					physicianClientName.Fax = Mongo.ValueHelper.GetStringValue(clientDocument.GetValue("Fax"));
					physicianClientName.FirstName = Mongo.ValueHelper.GetStringValue(physicianDocument.GetValue("FirstName"));
					physicianClientName.LastName = Mongo.ValueHelper.GetStringValue(physicianDocument.GetValue("LastName"));
					physicianClientName.PhysicianClientId = physicianClientId;
					physicianClientName.PhysicianId = physicianDocument.GetValue("PhysicianId").AsInt32;
					physicianClientName.Telephone = Mongo.ValueHelper.GetStringValue(clientDocument.GetValue("Telephone"));

					result.Add(physicianClientName);
			}

			return result;
		}

		public static YellowstonePathology.Business.Client.Model.PhysicianNameViewCollection GetPhysicianNameViewCollectionByPhysicianLastName(string physicianLastName)
		{
			YellowstonePathology.Business.Client.Model.PhysicianNameViewCollection result = new YellowstonePathology.Business.Client.Model.PhysicianNameViewCollection();
			YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection clientCollection = server.Database.GetCollection<BsonDocument>("Client");
			MongoCollection collection = server.Database.GetCollection<BsonDocument>("Physician");
			MongoCursor cursor = collection.FindAs<BsonDocument>(Query.Matches("LastName", BsonRegularExpression.Create("^" + physicianLastName + ".*", "i"))).SetSortOrder(SortBy.Ascending("FirstName"));

			foreach (BsonDocument physicianDocument in cursor)
			{
				BsonDocument clientDocument = clientCollection.FindOneAs<BsonDocument>(Query.EQ("ClientId", physicianDocument.GetValue("HomeBaseClientId")));
				YellowstonePathology.Business.Client.Model.PhysicianNameView physicianNameView = new YellowstonePathology.Business.Client.Model.PhysicianNameView();
				physicianNameView.PhysicianId = physicianDocument.GetValue("PhysicianId").AsInt32;
				physicianNameView.FirstName = Mongo.ValueHelper.GetStringValue(physicianDocument.GetValue("FirstName"));
				physicianNameView.LastName = Mongo.ValueHelper.GetStringValue(physicianDocument.GetValue("LastName"));
				physicianNameView.HomeBaseFax = Mongo.ValueHelper.GetStringValue(clientDocument.GetValue("Fax"));
				physicianNameView.HomeBasePhone = Mongo.ValueHelper.GetStringValue(clientDocument.GetValue("Telephone"));

				result.Add(physicianNameView);
			}

			return result;
		}

		public static YellowstonePathology.Business.Client.Model.PhysicianClientCollection GetPhysicianClientListByPhysicianLastName(string physicianLastName)
		{
			YellowstonePathology.Business.Client.Model.PhysicianClientCollection result = new Client.Model.PhysicianClientCollection();
			YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection physicianClientCollection = server.Database.GetCollection<BsonDocument>("PhysicianClient");
			MongoCollection physicianCollection = server.Database.GetCollection<BsonDocument>("Physician");
			MongoCollection clientCollection = server.Database.GetCollection<BsonDocument>("Client");
			MongoCursor physicianCursor = physicianCollection.FindAs<BsonDocument>(Query.Matches("LastName", BsonRegularExpression.Create("^" + physicianLastName + ".*", "i"))).SetSortOrder(SortBy.Ascending("LastName", "FirstName"));

			foreach (BsonDocument physicianDocument in physicianCursor)
			{
				BsonValue physicianId = physicianDocument.GetValue("PhysicianId");
				MongoCursor physicianClientCursor = physicianClientCollection.FindAs<BsonDocument>(Query.EQ("PhysicianId", physicianId));
				List<BsonValue> clientIdList = new List<BsonValue>();
				foreach(BsonDocument physicianClientDocument in physicianClientCursor)
				{
					clientIdList.Add(physicianClientDocument.GetValue("ClientId"));
				}

				MongoCursor clientCursor = clientCollection.FindAs<BsonDocument>(Query.In("ClientId", clientIdList)).SetSortOrder(SortBy.Ascending("ClientName"));
				foreach (BsonDocument clientDocument in clientCursor)
				{
					BsonValue physicianClientId = null;
					foreach(BsonDocument physicianClientDocument in physicianClientCursor)
					{
						if(physicianClientDocument.GetValue("ClientId").Equals(clientDocument.GetValue("ClientId")))
						{
							physicianClientId = physicianClientDocument.GetValue("PhysicianClientId");
							break;
						}
					}

					YellowstonePathology.Business.Client.Model.PhysicianClient physicianClient = BuildPhysicianClient(clientDocument, physicianDocument, physicianClientId);
					result.Add(physicianClient);
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Client.Model.PhysicianClientCollection GetPhysicianClientListByClientPhysicianLastName(string clientName, string physicianLastName)
		{
			YellowstonePathology.Business.Client.Model.PhysicianClientCollection result = new Client.Model.PhysicianClientCollection();
			YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection physicianClientCollection = server.Database.GetCollection<BsonDocument>("PhysicianClient");
			MongoCollection physicianCollection = server.Database.GetCollection<BsonDocument>("Physician");
			MongoCollection clientCollection = server.Database.GetCollection<BsonDocument>("Client");
			MongoCursor physicianCursor = physicianCollection.FindAs<BsonDocument>(Query.Matches("LastName", BsonRegularExpression.Create("^" + physicianLastName + ".*", "i"))).SetSortOrder(SortBy.Ascending("LastName", "FirstName"));

			foreach (BsonDocument physicianDocument in physicianCursor)
			{
				BsonValue physicianId = physicianDocument.GetValue("PhysicianId");
				MongoCursor physicianClientCursor = physicianClientCollection.FindAs<BsonDocument>(Query.EQ("PhysicianId", physicianId));
				List<BsonValue> clientIdList = new List<BsonValue>();
				foreach (BsonDocument physicianClientDocument in physicianClientCursor)
				{
					clientIdList.Add(physicianClientDocument.GetValue("ClientId"));
				}

				MongoCursor clientCursor = clientCollection.FindAs<BsonDocument>(Query.And(Query.In("ClientId", clientIdList),
					Query.Matches("ClientName", BsonRegularExpression.Create("^" + clientName + ".*", "i")))).SetSortOrder(SortBy.Ascending("ClientName"));
				foreach (BsonDocument clientDocument in clientCursor)
				{
					BsonValue physicianClientId = null;
					foreach (BsonDocument physicianClientDocument in physicianClientCursor)
					{
						if (physicianClientDocument.GetValue("ClientId").Equals(clientDocument.GetValue("ClientId")))
						{
							physicianClientId = physicianClientDocument.GetValue("PhysicianClientId");
							break;
						}
					}

					YellowstonePathology.Business.Client.Model.PhysicianClient physicianClient = BuildPhysicianClient(clientDocument, physicianDocument, physicianClientId);
					result.Add(physicianClient);
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Client.Model.PhysicianClientCollection GetPhysicianClientListByClientId(int clientId)
		{
			YellowstonePathology.Business.Client.Model.PhysicianClientCollection result = new Client.Model.PhysicianClientCollection();
			BsonValue bsonClientId = BsonValue.Create(clientId);
			YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection physicianClientCollection = server.Database.GetCollection<BsonDocument>("PhysicianClient");
			MongoCollection physicianCollection = server.Database.GetCollection<BsonDocument>("Physician");
			MongoCollection clientCollection = server.Database.GetCollection<BsonDocument>("Client");
			BsonDocument clientDocument = clientCollection.FindOneAs<BsonDocument>(Query.EQ("ClientId", bsonClientId));
			MongoCursor physicianClientCursor = physicianClientCollection.FindAs<BsonDocument>(Query.EQ("ClientId", bsonClientId));
			List<BsonValue> physicianIdList = new List<BsonValue>();

			foreach (BsonDocument physicianClientDocument in physicianClientCursor)
			{
				physicianIdList.Add(physicianClientDocument.GetValue("PhysicianId"));
			}

			MongoCursor physicianCursor = physicianCollection.FindAs<BsonDocument>(Query.In("PhysicianId", physicianIdList)).SetSortOrder(SortBy.Ascending("LastName", "FirstName"));

			BsonValue bsonPhysicianClientId = null;
			foreach (BsonDocument physicianDocument in physicianCursor)
			{
				foreach (BsonDocument physicianClientDocument in physicianClientCursor)
				{
					if (physicianClientDocument.GetValue("PhysicianId").Equals(physicianDocument.GetValue("PhysicianId")))
					{
						bsonPhysicianClientId = physicianClientDocument.GetValue("PhysicianClientId");
						break;
					}
				}

				YellowstonePathology.Business.Client.Model.PhysicianClient physicianClient = BuildPhysicianClient(clientDocument, physicianDocument, bsonPhysicianClientId);
				result.Add(physicianClient);
			}
			return result;
		}

		private static YellowstonePathology.Business.Client.Model.PhysicianClient BuildPhysicianClient(BsonDocument clientDocument, BsonDocument physicianDocument, BsonValue physicianClientId)
		{
			YellowstonePathology.Business.Client.Model.PhysicianClient result = new Client.Model.PhysicianClient();
			result.ClientId = clientDocument.GetValue("ClientId").AsInt32;
			result.ClientName = Mongo.ValueHelper.GetStringValue(clientDocument.GetValue("ClientName"));
			result.DistributionType = Mongo.ValueHelper.GetStringValue(clientDocument.GetValue("DistributionType"));
			result.FacilityType = Mongo.ValueHelper.GetStringValue(clientDocument.GetValue("FacilityType"));
			result.FaxNumber = Mongo.ValueHelper.GetStringValue(clientDocument.GetValue("Fax"));
			result.LongDistance = clientDocument.GetValue("LongDistance").AsBoolean;
			result.PhysicianClientId = Mongo.ValueHelper.GetStringValue(physicianClientId);
			result.PhysicianId = physicianDocument.GetValue("PhysicianId").AsInt32;
			result.PhysicianName = Mongo.ValueHelper.GetStringValue(physicianDocument.GetValue("DisplayName"));

			return result;
		}

		public static View.ClientLocationViewCollection GetClientLocationViewByClientName(string clientName)
		{
			YellowstonePathology.Business.View.ClientLocationViewCollection result = new View.ClientLocationViewCollection();
			YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection collection = server.Database.GetCollection<BsonDocument>("Client");
			MongoCursor cursor = collection.FindAs<BsonDocument>(Query.Matches("ClientName", BsonRegularExpression.Create("^" + clientName + ".*", "i")));

			foreach (BsonDocument bsonDocument in cursor)
			{
				BsonArray locations = bsonDocument.GetValue("ClientLocationCollection").AsBsonArray;
				foreach (BsonDocument location in locations)
				{
					YellowstonePathology.Business.View.ClientLocationView clientLocationView = new View.ClientLocationView(bsonDocument.GetValue("ClientId").AsInt32,
						location.GetValue("ClientLocationId").AsInt32, Mongo.ValueHelper.GetStringValue(bsonDocument.GetValue("ClientName")), Mongo.ValueHelper.GetStringValue(location.GetValue("Location")));
					result.Add(clientLocationView);
				}
			}

			return result;
		}

		public static List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> GetPhysicianClientDistributions(int physicianClientId)
		{
			List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> result = new List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView>();
			YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection physicianClientCollection = server.Database.GetCollection<BsonDocument>("PhysicianClient");
			MongoCollection physicianCollection = server.Database.GetCollection<BsonDocument>("Physician");
			MongoCollection clientCollection = server.Database.GetCollection<BsonDocument>("Client");
			MongoCollection physicianClientDistributionCollection = server.Database.GetCollection<BsonDocument>("PhysicianClientDistribution");
			MongoCursor physicianClientDistributionCursor = physicianClientDistributionCollection.FindAs<BsonDocument>(Query.EQ("PhysicianClientID", BsonValue.Create(physicianClientId)));

			foreach(BsonDocument physicianClientDistributionDocument in physicianClientDistributionCursor)
			{
				BsonDocument physicianClientDocument = physicianClientCollection.FindOneAs<BsonDocument>(Query.EQ("PhysicianClientId", physicianClientDistributionDocument.GetValue("DistributionID")));
				BsonDocument clientDocument = clientCollection.FindOneAs<BsonDocument>(Query.EQ("ClientId", physicianClientDocument.GetValue("ClientId")));
				BsonDocument physicianDocument = physicianCollection.FindOneAs<BsonDocument>(Query.EQ("PhysicianId", physicianClientDocument.GetValue("PhysicianId")));

				YellowstonePathology.Business.Client.Model.PhysicianClientDistribution physicianClientDistribution = (YellowstonePathology.Business.Client.Model.PhysicianClientDistribution)Mongo.BSONObjectBuilder.Build(physicianClientDistributionDocument, typeof(YellowstonePathology.Business.Client.Model.PhysicianClientDistribution));

				YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView physicianClientDistributionView = new YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView(physicianClientDistribution);
				physicianClientDistributionView.ClientId = clientDocument.GetValue("ClientId").AsInt32;
				physicianClientDistributionView.ClientName = Mongo.ValueHelper.GetStringValue(clientDocument.GetValue("ClientName"));
				physicianClientDistributionView.DistributionType = Mongo.ValueHelper.GetStringValue(clientDocument.GetValue("DistributionType"));
				physicianClientDistributionView.PhysicianId = physicianDocument.GetValue("PhysicianId").AsInt32;
				physicianClientDistributionView.PhysicianName = Mongo.ValueHelper.GetStringValue(physicianDocument.GetValue("DisplayName"));

				result.Add(physicianClientDistributionView);
			}
			return result;
		}

		public static YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList GetPhysicianClientDistributionByClientId(int clientId)
		{
			Business.Client.Model.PhysicianClientDistributionList result = new Client.Model.PhysicianClientDistributionList();
			YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection physicianClientCollection = server.Database.GetCollection<BsonDocument>("PhysicianClient");
			MongoCollection physicianCollection = server.Database.GetCollection<BsonDocument>("Physician");
			MongoCollection clientCollection = server.Database.GetCollection<BsonDocument>("Client");
			BsonDocument clientDocument = clientCollection.FindOneAs<BsonDocument>(Query.EQ("ClientId", BsonValue.Create(clientId)));
			MongoCursor physicianClientCursor = physicianClientCollection.FindAs<BsonDocument>(Query.EQ("ClientId", BsonValue.Create(clientId)));

			List<BsonValue> physicianIdList = new List<BsonValue>();
			foreach (BsonDocument physicianClientDocument in physicianClientCursor)
			{
				physicianIdList.Add(physicianClientDocument.GetValue("PhysicianId"));
			}

			MongoCursor physicianCursor = physicianCollection.FindAs<BsonDocument>(Query.In("PhysicianId", physicianIdList)).SetSortOrder(SortBy.Ascending("LastName", "FirstName"));

			foreach (BsonDocument physicianDocument in physicianCursor)
			{
				YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistribution = BuildPhysicianClientDistribution(clientDocument, physicianDocument);
				result.Add(physicianClientDistribution);
			}

			return result;
		}

		public static Business.Client.Model.PhysicianClientDistributionList GetPhysicianClientDistributionByClientPhysicianLastName(string clientName, string physicianLastName)
		{
			Business.Client.Model.PhysicianClientDistributionList result = new Client.Model.PhysicianClientDistributionList();
			YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection physicianClientCollection = server.Database.GetCollection<BsonDocument>("PhysicianClient");
			MongoCollection physicianCollection = server.Database.GetCollection<BsonDocument>("Physician");
			MongoCollection clientCollection = server.Database.GetCollection<BsonDocument>("Client");
			
			MongoCursor physicianCursor = physicianCollection.FindAs<BsonDocument>(Query.Matches("LastName", BsonRegularExpression.Create("^" + physicianLastName + ".*", "i"))).SetSortOrder(SortBy.Ascending("LastName", "FirstName"));

			foreach (BsonDocument physicianDocument in physicianCursor)
			{
				MongoCursor physicianClientCursor = physicianClientCollection.FindAs<BsonDocument>(Query.EQ("PhysicianId", physicianDocument.GetValue("PhysicianId")));
				List<BsonValue> clientIdList = new List<BsonValue>();
				foreach (BsonDocument physicianClientDocument in physicianClientCursor)
				{
					clientIdList.Add(physicianClientDocument.GetValue("ClientId"));
				}

				MongoCursor clientCursor = clientCollection.FindAs<BsonDocument>(Query.And(Query.In("ClientId", clientIdList),
					Query.Matches("ClientName", BsonRegularExpression.Create("^" + clientName + ".*", "i")))).SetSortOrder(SortBy.Ascending("ClientName"));
				foreach (BsonDocument clientDocument in clientCursor)
				{
					YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistribution = BuildPhysicianClientDistribution(clientDocument, physicianDocument);
					result.Add(physicianClientDistribution);
				}
			}
			
			return result;
		}

		public static Business.Client.Model.PhysicianClientDistributionList GetPhysicianClientDistributionByPhysicianFirstLastName(string firstName, string lastName)
		{
			Business.Client.Model.PhysicianClientDistributionList result = new Client.Model.PhysicianClientDistributionList();
			YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection physicianClientCollection = server.Database.GetCollection<BsonDocument>("PhysicianClient");
			MongoCollection physicianCollection = server.Database.GetCollection<BsonDocument>("Physician");
			MongoCollection clientCollection = server.Database.GetCollection<BsonDocument>("Client");

			MongoCursor physicianCursor = physicianCollection.FindAs<BsonDocument>(Query.And(Query.Matches("LastName", BsonRegularExpression.Create("^" + lastName + ".*", "i")),
					Query.Matches("FirstName", BsonRegularExpression.Create("^" + firstName + ".*", "i")))).SetSortOrder(SortBy.Ascending("LastName", "FirstName"));

			foreach (BsonDocument physicianDocument in physicianCursor)
			{
				MongoCursor physicianClientCursor = physicianClientCollection.FindAs<BsonDocument>(Query.EQ("PhysicianId", physicianDocument.GetValue("PhysicianId")));
				List<BsonValue> clientIdList = new List<BsonValue>();
				foreach (BsonDocument physicianClientDocument in physicianClientCursor)
				{
					clientIdList.Add(physicianClientDocument.GetValue("ClientId"));
				}

				MongoCursor clientCursor = clientCollection.FindAs<BsonDocument>(Query.In("ClientId", clientIdList)).SetSortOrder(SortBy.Ascending("ClientName"));
				foreach (BsonDocument clientDocument in clientCursor)
				{
					YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistribution = BuildPhysicianClientDistribution(clientDocument, physicianDocument);
					result.Add(physicianClientDistribution);
				}
			}

			return result;
		}

		public static Business.Client.Model.PhysicianClientDistributionList GetPhysicianClientDistributionByPhysicianLastName(string lastName)
		{
			Business.Client.Model.PhysicianClientDistributionList result = new Client.Model.PhysicianClientDistributionList();
			YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
			MongoCollection physicianClientCollection = server.Database.GetCollection<BsonDocument>("PhysicianClient");
			MongoCollection physicianCollection = server.Database.GetCollection<BsonDocument>("Physician");
			MongoCollection clientCollection = server.Database.GetCollection<BsonDocument>("Client");

			MongoCursor physicianCursor = physicianCollection.FindAs<BsonDocument>(Query.Matches("LastName", BsonRegularExpression.Create("^" + lastName + ".*", "i"))).SetSortOrder(SortBy.Ascending("LastName", "FirstName"));

			foreach (BsonDocument physicianDocument in physicianCursor)
			{
				MongoCursor physicianClientCursor = physicianClientCollection.FindAs<BsonDocument>(Query.EQ("PhysicianId", physicianDocument.GetValue("PhysicianId")));
				List<BsonValue> clientIdList = new List<BsonValue>();
				foreach (BsonDocument physicianClientDocument in physicianClientCursor)
				{
					clientIdList.Add(physicianClientDocument.GetValue("ClientId"));
				}

				MongoCursor clientCursor = clientCollection.FindAs<BsonDocument>(Query.In("ClientId", clientIdList)).SetSortOrder(SortBy.Ascending("ClientName"));
				foreach (BsonDocument clientDocument in clientCursor)
				{
					YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistribution = BuildPhysicianClientDistribution(clientDocument, physicianDocument);
					result.Add(physicianClientDistribution);
				}
			}
			
			return result;
		}

		private static YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem BuildPhysicianClientDistribution(BsonDocument clientDocument, BsonDocument physicianDocument)
		{
			YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem result = new YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem();
			result.ClientId = clientDocument.GetValue("ClientId").AsInt32;
			result.ClientName = Mongo.ValueHelper.GetStringValue(clientDocument.GetValue("ClientName"));
			result.LongDistance = clientDocument.GetValue("LongDistance").AsBoolean;
			result.FaxNumber = Mongo.ValueHelper.GetStringValue(clientDocument.GetValue("Fax"));
			result.DistributionType = Mongo.ValueHelper.GetStringValue(clientDocument.GetValue("DistributionType"));
			result.PhysicianId = physicianDocument.GetValue("PhysicianId").AsInt32;
			result.PhysicianName = Mongo.ValueHelper.GetStringValue(physicianDocument.GetValue("DisplayName"));

			return result;
		}
	}
}
