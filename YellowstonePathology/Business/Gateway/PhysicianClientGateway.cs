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

namespace YellowstonePathology.Business.Gateway
{
	public class PhysicianClientGateway
	{
        public static Domain.Physician GetPhysicianByMasterAccessionNo(string masterAccessionNo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select ph.* " +
                "from tblPhysician ph " +
                "join tblAccessionOrder ao on ph.PhysicianId = ao.PhysicianId " +
                "where ao.MasterAccessionNo = @MasterAccessionNo";

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;
			Domain.Physician result = PhysicianClientGateway.GetPhysicianFromCommand(cmd);
			return result;
		}

		public static Domain.Physician GetPhysicianByNpi(string npi)
		{
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianByNpi(npi);
#else
			Domain.Physician result = null;
			if (string.IsNullOrEmpty(npi) == false && npi != "0")
			{
				SqlCommand cmd = new SqlCommand();
				cmd.CommandText = "SELECT * FROM tblPhysician where Npi = @Npi";
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.Add("@Npi", SqlDbType.VarChar).Value = npi;
				result = PhysicianClientGateway.GetPhysicianFromCommand(cmd);
			}
			return result;
#endif
		}

		public static Domain.Physician GetPhysicianByPhysicianId(int physicianId)
		{
#if MONGO
            return PhysicianClientGatewayMongo.GetPhysicianByPhysicianId(physicianId);
#else
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM tblPhysician where PhysicianId = @PhysicianId";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@PhysicianId", SqlDbType.Int).Value = physicianId;
			Domain.Physician result = PhysicianClientGateway.GetPhysicianFromCommand(cmd);
			return result;
#endif
		}

		private static Domain.Physician GetPhysicianFromCommand(SqlCommand cmd)
		{
			Domain.Physician result = null;
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new Domain.Physician();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();

					}
				}
			}
			return result;
		}
        
		public static Domain.PhysicianCollection GetPhysiciansByName(string firstName, string lastName)
		{
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysiciansByName(firstName, lastName);
#else
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.Text;
			if(string.IsNullOrEmpty(firstName))
			{
				cmd.CommandText = "SELECT * FROM tblPhysician where LastName like @LastName + '%' order by LastName, FirstName";
			}
			else
			{
				cmd.CommandText = "SELECT * FROM tblPhysician where FirstName like @FirstName + '%' and LastName like @LastName + '%' order by LastName, FirstName";
				cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = firstName;
			}
			cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lastName;
			Domain.PhysicianCollection result = PhysicianClientGateway.GetPhysicianCollectionFromCommand(cmd);

			if (result.Count == 0)
			{
				cmd = new SqlCommand();
				if(string.IsNullOrEmpty(firstName))
				{
					cmd.CommandText = "SELECT PhysicianID, ClientId, FirstName, LastName, Active, Address, City, State, Zip, Phone, Fax, OutsideConsult, HPVTest, " +
						"HPVInstructionID, HPVTestToPerformID, FullName, HPVStandingOrderCode, ReportDeliveryMethod, DisplayName, HomeBaseClientId, KRASBRAFStandingOrder, " +
						"VoiceCommand, VoiceCommandIsEnabled, Npi, MiddleInitial, Credentials, UserName, ClientsPhysicianId, ObjectId " +
						"FROM tblPhysician where LastName like @LastName + '%' order by LastName, FirstName";
				}
				else
				{
					cmd.CommandText = "SELECT PhysicianID, ClientId, FirstName, LastName, Active, Address, City, State, Zip, Phone, Fax, OutsideConsult, HPVTest, " +
						"HPVInstructionID, HPVTestToPerformID, FullName, HPVStandingOrderCode, ReportDeliveryMethod, DisplayName, HomeBaseClientId, KRASBRAFStandingOrder, " +
						"VoiceCommand, VoiceCommandIsEnabled, Npi, MiddleInitial, Credentials, UserName, ClientsPhysicianId, ObjectId " +
						"FROM tblPhysician where FirstName like @FirstName + '%' and LastName like @LastName + '%' order by LastName, FirstName";
					cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = firstName;
				}
				cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lastName;
				result = PhysicianClientGateway.GetPhysicianCollectionFromCommand(cmd);
			}
			return result;
#endif
		}        		

/*        public static Domain.PhysicianCollection GetPhysiciansByClientId(int clientId)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysiciansByClientId(clientId);
#else
            Domain.PhysicianCollection result = new Domain.PhysicianCollection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select ph.* " +
               "from tblPhysician ph " +
               "join tblPhysicianClient pc on ph.PhysicianId = pc.PhysicianId " +
               "where pc.ClientId = @ClientId order by ph.LastName";   
            cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;
            cmd.CommandType = CommandType.Text;
			result = PhysicianClientGateway.GetPhysicianCollectionFromCommand(cmd);
			return result;
#endif
        }*/

		private static Domain.PhysicianCollection GetPhysicianCollectionFromCommand(SqlCommand cmd)
		{
			Domain.PhysicianCollection result = new Domain.PhysicianCollection();
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Domain.Physician physician = new Domain.Physician();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physician, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(physician);
					}
				}
			}
			return result;
		}

        public static YellowstonePathology.Business.Client.Model.ClientGroupCollection GetClientGroupCollection()
        {
#if MONGO
            return PhysicianClientGatewayMongo.GetClientGroupCollection();
#else
            YellowstonePathology.Business.Client.Model.ClientGroupCollection result = new Client.Model.ClientGroupCollection();
            SqlCommand cmd = new SqlCommand("select * from tblClientGroup order by GroupName");
            cmd.CommandType = CommandType.Text;            

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.Model.ClientGroup clientGroup = new Client.Model.ClientGroup();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientGroup, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(clientGroup);
                    }
                }
            }
            return result;
#endif
        }

		public static YellowstonePathology.Business.Client.Model.Client GetClientByClientId(int clientId)
		{
#if MONGO
            return PhysicianClientGatewayMongo.GetClientByClientId(clientId);
#else
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT c.*, (SELECT * from tblClientLocation where ClientId = c.ClientId for xml path('ClientLocation'), type) ClientLocationCollection " +
				" FROM tblClient c where c.ClientId = @ClientId for xml Path('Client'), type";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;
			ClientBuilder builder = new ClientBuilder();
			XElement document = PhysicianClientGateway.GetXElementFromCommand(cmd);
			builder.Build(document);
			return builder.Client;
#endif
		}

		private static XElement GetXElementFromCommand(SqlCommand cmd)
		{
			XElement result = null;
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (XmlReader xmlReader = cmd.ExecuteXmlReader())
				{
					if (xmlReader.Read() == true)
					{
						result = XElement.Load(xmlReader, LoadOptions.PreserveWhitespace);
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Client.Model.ClientCollection GetClientsByClientName(string clientName)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetClientsByClientName(clientName);
#else
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT c.*, (SELECT * from tblClientLocation where ClientId = c.ClientId order by Location for xml path('ClientLocation'), type) ClientLocationCollection " +
                "FROM tblClient c where c.ClientName like @ClientName + '%' order by ClientName for xml Path('Client'), Root('ClientCollection'), type";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ClientName", SqlDbType.VarChar).Value = clientName;
            XElement resultElement = PhysicianClientGateway.GetXElementFromCommand(cmd);
            return BuildClientCollection(resultElement);
#endif
		}

/*        public static Domain.ClientCollection GetClientsByPhysicianId(int physicianId)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetClientsByPhysicianId(physicianId);
#else
            Domain.ClientCollection result = new Domain.ClientCollection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select c.* " +
               "from tblClient c " +
               "join tblPhysicianClient pc on c.ClientId = pc.ClientId " +
               "where pc.PhysicianId = @PhysicianId ";
            cmd.Parameters.Add("@PhysicianId", SqlDbType.Int).Value = physicianId;
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.Model.Client client = new YellowstonePathology.Business.Client.Model.Client();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(client, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(client);
                    }
                }
            }
            return result;
#endif
        }*/

/*        public static Domain.PhysicianClient GetPhysicianClient(int physicianId, int clientId)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClient(physicianId, clientId);
#else
            Domain.PhysicianClient result = null;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblPhysicianClient where PhysicianId = @PhysicianId and ClientId = @ClientId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@PhysicianId", SqlDbType.Int).Value = physicianId;
            cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = new Domain.PhysicianClient();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();                        
                    }
                }
            }

            return result;
#endif
		}

		public static View.ClientPhysicianView GetClientPhysicianViewByClientId(int clientId)
		{
#if MONGO
			return PhysicianClientGatewayMongo.GetClientPhysicianViewByClientId(clientId);
#else
			View.ClientPhysicianView result = null;
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "with phys as(select p.* from tblPhysician p join tblPhysicianClient pc on p.PhysicianId = pc.PhysicianId where pc.ClientId = @ClientId) " +
				"select c.*," +
				" ( select phys.*" +
				"   from phys order by phys.FirstName for xml Path('Physician'), type) Physicians" +
				" from tblClient c where c.ClientId = @ClientId for xml Path('Client'), root('ClientPhysicianView')";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;
			XElement resultElement = PhysicianClientGateway.GetXElementFromCommand(cmd);
			if (resultElement != null)
			{
				result = PhysicianClientGateway.BuildClientPhysicianView(resultElement);
			}
			return result;
#endif
		}*/

		private static View.ClientPhysicianView BuildClientPhysicianView(XElement sourceElement)
		{
			View.ClientPhysicianView result = new View.ClientPhysicianView();
			XElement clientElement = sourceElement.Element("Client");
			YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(clientElement, result);
			xmlPropertyWriter.Write();

			XElement physiciansElement = clientElement.Element("Physicians");
			if(physiciansElement != null)
			{
				List<XElement> physicianElements = physiciansElement.Elements("Physician").ToList<XElement>();
				foreach (XElement physicianElement in physicianElements)
				{
					Domain.Physician physician = new Domain.Physician();
					YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter1 = new Persistence.XmlPropertyWriter(physicianElement, physician);
					xmlPropertyWriter1.Write();
					result.Physicians.Add(physician);
				}
			}
			return result;
		}

		/*public static YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection GetPhysicianClientNameCollection(string clientName, string physicianName)
		{
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientNameCollection(clientName, physicianName);
#else
			YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection result = new YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select pc.PhysicianClientId, pc.ClientId, pc.PhysicianId, c.ClientName, p.FirstName, p.LastName, c.Telephone, c.Fax " +
				"from tblPhysicianClient pc join tblClient c on pc.ClientId = c.ClientId " +
				"join tblPhysician p on pc.PhysicianId = p.PhysicianId " +
				"where p.LastName like @PhysicianName + '%' and c.ClientName like @ClientName + '%' order by c.ClientName, p.FirstName ";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ClientName", SqlDbType.VarChar).Value = clientName;
			cmd.Parameters.Add("@PhysicianName", SqlDbType.VarChar).Value = physicianName;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Client.Model.PhysicianClientName physicianClientName = new YellowstonePathology.Business.Client.Model.PhysicianClientName();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientName, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(physicianClientName);
					}
				}
			}
			return result;
#endif
		}

		public static YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection GetPhysicianClientNameCollection(int physicianClientId)
		{
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientNameCollection(physicianClientId);
#else
			YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection result = new YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select pc.PhysicianClientId, pc.ClientId, pc.PhysicianId, c.ClientName, p.FirstName, p.LastName, c.Telephone, c.Fax " +
				"from tblPhysicianClient pc join tblClient c on pc.ClientId = c.ClientId " +
				"join tblPhysician p on pc.PhysicianId = p.PhysicianId " +
				"where p.PhysicianId = (select physicianId from tblPhysicianClient where PhysicianClientId = @PhysicianClientId) order by c.ClientName";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@PhysicianClientId", SqlDbType.Int).Value = physicianClientId;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Client.Model.PhysicianClientName physicianClientName = new YellowstonePathology.Business.Client.Model.PhysicianClientName();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientName, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(physicianClientName);
					}
				}
			}
			return result;
#endif
		}

        public static YellowstonePathology.Business.Client.Model.PhysicianNameViewCollection GetPhysicianNameViewCollectionByPhysicianLastName(string physicianLastName)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianNameViewCollectionByPhysicianLastName(physicianLastName);
#else
			YellowstonePathology.Business.Client.Model.PhysicianNameViewCollection result = new YellowstonePathology.Business.Client.Model.PhysicianNameViewCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select ph.PhysicianId, ph.FirstName, ph.LastName, c.Telephone [HomeBasePhone], c.Fax [HomeBaseFax] " +
                "from tblPhysician ph " +
                "left outer join tblClient c on ph.HomeBaseClientId = c.ClientId " +
                "where ph.LastName like @LastName + '%' order by ph.FirstName ";

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = physicianLastName;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.Model.PhysicianNameView physicianNameView = new YellowstonePathology.Business.Client.Model.PhysicianNameView();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianNameView, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianNameView);
                    }
                }
            }
            return result;
#endif
        }

		public static List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> GetPhysicianClientDistributions(int physicianClientId)
		{
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientDistributions(physicianClientId);
#else
			List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> result = new List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView>();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select pcd.*, c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType " +
				"from tblPhysicianClient pc " +
				"join tblPhysicianClientDistribution pcd on pc.PhysicianClientId = pcd.PhysicianClientId " +
				"join tblPhysicianClient pc2 on pcd.DistributionId = pc2.PhysicianClientId " +
				"join tblClient c on pc2.ClientId = c.ClientId " +
				"join tblPhysician ph on pc2.Physicianid = ph.PhysicianId " +
				"where pc.PhysicianClientId = @PhysicianClientId ";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@PhysicianClientId", SqlDbType.Int).Value = physicianClientId;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Client.Model.PhysicianClientDistribution physicianClientDistribution = new YellowstonePathology.Business.Client.Model.PhysicianClientDistribution();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistribution, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView physicianClientDistributionView = new YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView(physicianClientDistribution);
						sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistributionView, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(physicianClientDistributionView);
					}
				}
			}
			return result;
#endif
		}

		public static View.PhysicianClientView GetPhysicianClientView(int physicianId)
		{
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientView(physicianId);
#else
            SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select * from tblPhysician where PhysicianId = @PhysicianId;" +
				" select c.* from tblClient c join tblPhysicianClient pc on c.ClientId = pc.ClientId where pc.PhysicianId = @PhysicianId order by ClientName";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@PhysicianId", SqlDbType.Int).Value = physicianId;
			return BuildPhysicianClientView(cmd);
#endif
        }*/

		/*public static View.ClientSearchView GetClientSearchViewByClientName(string clientName)
		{
#if MONGO
			return PhysicianClientGatewayMongo.GetClientSearchViewByClientName(clientName);
#else
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select ClientId, ClientName, Address, Telephone, Fax from tblClient where clientName like @ClientName + '%' Order By 2 for xml Path('ClientSearchViewItem'), root('ClientSearchView')";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ClientName", SqlDbType.VarChar).Value = clientName;
			View.ClientSearchView results = Persistence.SqlCommandHelper.ExecuteCollectionCommand<View.ClientSearchView>(cmd);
			if (results == null)
			{
				results = new View.ClientSearchView();
			}
			return results;
#endif
		}*/

		public static View.ClientLocationViewCollection GetClientLocationViewByClientName(string clientName)
		{
#if MONGO
			return PhysicianClientGatewayMongo.GetClientLocationViewByClientName(clientName);
#else
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select c.ClientId, c.ClientName, cl.ClientLocationId, cl.Location from tblClientLocation cl join tblClient c on cl.ClientId = c.ClientId where c.ClientName like @ClientName + '%' Order By 2, 4";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ClientName", SqlDbType.VarChar).Value = clientName;
            View.ClientLocationViewCollection result = new View.ClientLocationViewCollection();

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        View.ClientLocationView clientLocationView = new View.ClientLocationView();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientLocationView, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(clientLocationView);
                    }
                }
            }

            return result;
#endif
		}

		private static YellowstonePathology.Business.Client.Model.ClientCollection BuildClientCollection(XElement sourceElement)
        {
			YellowstonePathology.Business.Client.Model.ClientCollection clientCollection = new YellowstonePathology.Business.Client.Model.ClientCollection();
            if (sourceElement != null)
            {
                foreach (XElement clientElement in sourceElement.Elements("Client"))
                {
					ClientBuilder builder = new ClientBuilder();
					builder.Build(clientElement);
					YellowstonePathology.Business.Client.Model.Client client = builder.Client;
                    clientCollection.Add(client);
                }
            }
            return clientCollection;
        }

		private static View.PhysicianClientView BuildPhysicianClientView(SqlCommand cmd)
		{
			View.PhysicianClientView result = new View.PhysicianClientView();
			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}

					dr.NextResult();

					while (dr.Read())
					{
						YellowstonePathology.Business.Client.Model.Client client = new YellowstonePathology.Business.Client.Model.Client();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(client, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Clients.Add(client);
					}
				}
			}
			return result;
		}

        /*public static YellowstonePathology.Business.Client.PhysicianClientCollection GetPhysicianClientListByPhysicianLastName(string physicianLastName)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientListByPhysicianLastName(physicianLastName);
#else
			YellowstonePathology.Business.Client.PhysicianClientCollection result = new Client.PhysicianClientCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select pp.PhysicianClientId, c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.LongDistance, c.FacilityType, ph.NPI " +
                 "from tblClient c " +
                 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
                 "Join tblPhysician ph on pp.PhysicianId = ph.PhysicianId " +
                 "where ph.LastName like @LastName + '%' order by ph.LastName, ph.FirstName, c.ClientName";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(@"LastName", SqlDbType.VarChar, 50).Value = physicianLastName;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.PhysicianClient physicianClient = new Client.PhysicianClient();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClient, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianClient);
                    }
                }
            }
            return result;
#endif           
        }

        public static YellowstonePathology.Business.Client.PhysicianClientCollection GetPhysicianClientListByClientPhysicianLastName(string clientName, string physicianLastName)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientListByClientPhysicianLastName(clientName, physicianLastName);
#else
			YellowstonePathology.Business.Client.PhysicianClientCollection result = new Client.PhysicianClientCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select pp.PhysicianClientId, c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.LongDistance, c.FacilityType, ph.NPI " +
                 "from tblClient c " +
                 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
                 "Join tblPhysician ph on pp.PhysicianId = ph.PhysicianId " +
                 "where c.ClientName like @ClientName + '%' and ph.LastName like @PhysicianLastName + '%' order by ph.LastName, ph.FirstName, c.ClientName";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ClientName", SqlDbType.VarChar, 50).Value = clientName;
            cmd.Parameters.Add("@PhysicianLastName", SqlDbType.VarChar, 50).Value = physicianLastName;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.PhysicianClient physicianClient = new Client.PhysicianClient();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClient, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianClient);
                    }
                }
            }
            return result;
#endif
        }

        public static YellowstonePathology.Business.Client.PhysicianClientCollection GetPhysicianClientListByClientId(int clientId)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientListByClientId(clientId);
#else
			YellowstonePathology.Business.Client.PhysicianClientCollection result = new Client.PhysicianClientCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select pp.PhysicianClientId, c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.FacilityType, c.DistributionType, c.Fax [FaxNumber], c.LongDistance, ph.NPI " +
                 "from tblClient c " +
                 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
                 "Join tblPhysician ph on pp.PhysicianId = ph.PhysicianId " +
                 "where c.ClientId = @ClientId order by ph.LastName, ph.FirstName, c.ClientName";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(@"ClientId", SqlDbType.Int).Value = clientId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.PhysicianClient physicianClient = new Client.PhysicianClient();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClient, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianClient);
                    }
                }
            }
            return result;
#endif
		}

        public static Business.Client.PhysicianClientDistributionCollection GetPhysicianClientDistributionByClientId(int clientId)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientDistributionByClientId(clientId);
#else
			Business.Client.PhysicianClientDistributionCollection result = new Client.PhysicianClientDistributionCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.LongDistance " +
                 "from tblClient c " +
                 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
                 "Join tblPhysician ph on pp.PhysicianId = ph.PhysicianId " +
                 "where c.ClientId = @ClientId order by ph.LastName, ph.FirstName, c.ClientName";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(@"ClientId", SqlDbType.Int).Value = clientId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Business.Client.PhysicianClientDistribution physicianClientDistribution = new Client.PhysicianClientDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianClientDistribution);
                    }
                }
            }
            return result;
#endif
        }

        public static Business.Client.PhysicianClientDistributionCollection GetPhysicianClientDistributionByClientPhysicianLastName(string clientName, string physicianLastName)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientDistributionByClientPhysicianLastName(clientName, physicianLastName);
#else
			Business.Client.PhysicianClientDistributionCollection result = new Client.PhysicianClientDistributionCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.LongDistance " +
                 "from tblClient c " +
                 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
                 "Join tblPhysician ph on pp.PhysicianId = ph.PhysicianId " +
                 "where c.ClientName like @ClientName + '%' and ph.LastName like @PhysicianLastName + '%' order by ph.LastName, ph.FirstName, c.ClientName";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ClientName", SqlDbType.VarChar, 50).Value = clientName;
            cmd.Parameters.Add("@PhysicianLastName", SqlDbType.VarChar, 50).Value = physicianLastName;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Business.Client.PhysicianClientDistribution physicianClientDistribution = new Business.Client.PhysicianClientDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianClientDistribution);
                    }
                }
            }
            return result;
#endif
        }

        public static Business.Client.PhysicianClientDistributionCollection GetPhysicianClientDistributionByPhysicianFirstLastName(string firstName, string lastName)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientDistributionByPhysicianFirstLastName(firstName, lastName);
#else
			Business.Client.PhysicianClientDistributionCollection result = new Client.PhysicianClientDistributionCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.LongDistance " +
                 "from tblClient c " +
                 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
                 "Join tblPhysician ph on pp.PhysicianId = ph.PhysicianId " +
                 "where ph.FirstName like @FirstName + '%' and ph.LastName like @LastName + '%' order by ph.LastName, ph.FirstName, c.ClientName";
            cmd.CommandType = CommandType.Text;            
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = firstName;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = lastName;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Business.Client.PhysicianClientDistribution physicianClientDistribution = new Business.Client.PhysicianClientDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianClientDistribution);
                    }
                }
            }
            return result;
#endif
        }

        public static Business.Client.PhysicianClientDistributionCollection GetPhysicianClientDistributionByPhysicianLastName(string lastName)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientDistributionByPhysicianLastName(lastName);
#else
			Business.Client.PhysicianClientDistributionCollection result = new Client.PhysicianClientDistributionCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.LongDistance " +
                 "from tblClient c " +
                 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
                 "Join tblPhysician ph on pp.PhysicianId = ph.PhysicianId " +
                 "where ph.LastName like @LastName + '%' order by ph.LastName, ph.FirstName, c.ClientName";
            cmd.CommandType = CommandType.Text;            
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = lastName;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Business.Client.PhysicianClientDistribution physicianClientDistribution = new Business.Client.PhysicianClientDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianClientDistribution);
                    }
                }
            }
            return result;
#endif
        }*/


//**********************
		public static Domain.Physician GetPhysicianById(string objectId)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM tblPhysician where ObjectId = @ObjectId";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ObjectId", SqlDbType.VarChar).Value = objectId;
			Domain.Physician result = PhysicianClientGateway.GetPhysicianFromCommand(cmd);
			return result;
		}

		public static Domain.PhysicianCollection GetPhysiciansByClientIdV2(int clientId)
		{
			Domain.PhysicianCollection result = new Domain.PhysicianCollection();

			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select ph.* " +
			   "from tblPhysician ph " +
			   "join tblPhysicianClient pc on ph.ObjectId = pc.ProviderId " +
			   "where pc.ClientId = @ClientId order by ph.LastName";
			cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;
			cmd.CommandType = CommandType.Text;
			result = PhysicianClientGateway.GetPhysicianCollectionFromCommand(cmd);
			return result;
		}

		public static Domain.ClientCollection GetClientsByProviderId(string objectId)
		{
			Domain.ClientCollection result = new Domain.ClientCollection();

			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select c.* " +
			   "from tblClient c " +
			   "join tblPhysicianClient pc on c.ClientId = pc.ClientId " +
			   "where pc.ProviderId = @ObjectId order by ClientName ";
			cmd.Parameters.Add("@ObjectId", SqlDbType.VarChar).Value = objectId;
			cmd.CommandType = CommandType.Text;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Client.Model.Client client = new YellowstonePathology.Business.Client.Model.Client();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(client, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(client);
					}
				}
			}
			return result;
		}

        public static YellowstonePathology.Business.Client.Model.ClientCollection GetClientCollectionByClientGroupId(int clientGroupId)
        {
            YellowstonePathology.Business.Client.Model.ClientCollection result = new Client.Model.ClientCollection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select c.* " +
                "from tblClientGroup cg " +
                "join tblClientGroupClient cgc on cg.ClientGroupId = cgc.ClientGroupId " +
                "join tblclient c on cgc.ClientId = c.ClientId " +
                "where cgc.ClientGroupId = @ClientGroupId order by c.ClientName";

            cmd.Parameters.Add("@ClientGroupId", SqlDbType.Int).Value = clientGroupId;
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.Model.Client client = new YellowstonePathology.Business.Client.Model.Client();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(client, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(client);
                    }
                }
            }
            return result;
        }

        public static Domain.PhysicianClient GetPhysicianClient(string providerId, int clientId)
		{
			Domain.PhysicianClient result = null;
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select * from tblPhysicianClient where ProviderId = @ProviderId and ClientId = @ClientId";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ProviderId", SqlDbType.VarChar).Value = providerId;
			cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new Domain.PhysicianClient();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}

			return result;
		}

		public static View.ClientPhysicianView GetClientPhysicianViewByClientIdV2(int clientId)
		{
			View.ClientPhysicianView result = null;
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "with phys as(select p.* from tblPhysician p join tblPhysicianClient pc on p.ObjectId = pc.ProviderId where pc.ClientId = @ClientId) " +
				"select c.*," +
				" ( select phys.*" +
				"   from phys order by phys.FirstName for xml Path('Physician'), type) Physicians" +
				" from tblClient c where c.ClientId = @ClientId for xml Path('Client'), root('ClientPhysicianView')";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;
			XElement resultElement = PhysicianClientGateway.GetXElementFromCommand(cmd);
			if (resultElement != null)
			{
				result = PhysicianClientGateway.BuildClientPhysicianView(resultElement);
			}
			return result;
		}

		public static YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection GetPhysicianClientNameCollectionV2(string clientName, string physicianName)
		{
			YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection result = new YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select pc.PhysicianClientId, pc.ClientId, pc.PhysicianId, pc.ProviderId, c.ClientName, p.FirstName, p.LastName, c.Telephone, c.Fax " +
				"from tblPhysicianClient pc join tblClient c on pc.ClientId = c.ClientId " +
				"join tblPhysician p on pc.ProviderId = p.ObjectId " +
				"where p.LastName like @PhysicianName + '%' and c.ClientName like @ClientName + '%' order by c.ClientName, p.FirstName ";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ClientName", SqlDbType.VarChar).Value = clientName;
			cmd.Parameters.Add("@PhysicianName", SqlDbType.VarChar).Value = physicianName;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Client.Model.PhysicianClientName physicianClientName = new YellowstonePathology.Business.Client.Model.PhysicianClientName();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientName, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(physicianClientName);
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection GetPhysicianClientNameCollectionV2(string physicianClientId)
		{
			YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection result = new YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select pc.PhysicianClientId, pc.ClientId, pc.PhysicianId, pc.ProviderId, c.ClientName, p.FirstName, p.LastName, c.Telephone, c.Fax " +
				"from tblPhysicianClient pc join tblClient c on pc.ClientId = c.ClientId " +
				"join tblPhysician p on pc.ProviderId = p.ObjectId " +
				"where p.ObjectId = (select ProviderId from tblPhysicianClient where PhysicianClientId = @PhysicianClientId) order by c.ClientName";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@PhysicianClientId", SqlDbType.VarChar).Value = physicianClientId;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Client.Model.PhysicianClientName physicianClientName = new YellowstonePathology.Business.Client.Model.PhysicianClientName();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientName, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(physicianClientName);
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Client.Model.PhysicianNameViewCollection GetPhysicianNameViewCollectionByPhysicianLastNameV2(string physicianLastName)
		{
			YellowstonePathology.Business.Client.Model.PhysicianNameViewCollection result = new YellowstonePathology.Business.Client.Model.PhysicianNameViewCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select ph.PhysicianId, ph.ObjectId as ProviderId, ph.FirstName, ph.LastName, c.Telephone [HomeBasePhone], c.Fax [HomeBaseFax] " +
				"from tblPhysician ph " +
				"left outer join tblClient c on ph.HomeBaseClientId = c.ClientId " +
				"where ph.LastName like @LastName + '%' order by ph.FirstName ";

			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = physicianLastName;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Client.Model.PhysicianNameView physicianNameView = new YellowstonePathology.Business.Client.Model.PhysicianNameView();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianNameView, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(physicianNameView);
					}
				}
			}
			return result;
		}

		public static List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> GetPhysicianClientDistributionsV2(string physicianClientId)
		{
			List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> result = new List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView>();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select pcd.*, c.ClientId, c.ClientName, ph.PhysicianId, ph.ObjectId as ProviderId, ph.DisplayName [PhysicianName], c.DistributionType " +
				"from tblPhysicianClient pc " +
				"join tblPhysicianClientDistribution pcd on pc.PhysicianClientId = pcd.PhysicianClientId " +
				"join tblPhysicianClient pc2 on pcd.DistributionId = pc2.PhysicianClientId " +
				"join tblClient c on pc2.ClientId = c.ClientId " +
				"join tblPhysician ph on pc2.ProviderId = ph.ObjectId " +
				"where pc.PhysicianClientId = @PhysicianClientId ";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@PhysicianClientId", SqlDbType.VarChar).Value = physicianClientId;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Client.Model.PhysicianClientDistribution physicianClientDistribution = new YellowstonePathology.Business.Client.Model.PhysicianClientDistribution();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistribution, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView physicianClientDistributionView = new YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView(physicianClientDistribution);
						sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistributionView, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(physicianClientDistributionView);
					}
				}
			}
			return result;
		}

		public static View.PhysicianClientView GetPhysicianClientView(string physicianId)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select * from tblPhysician where ObjectId = @ObjectId;" +
				" select c.* from tblClient c join tblPhysicianClient pc on c.ClientId = pc.ClientId where pc.ProviderId = @ObjectId order by ClientName";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ObjectId", SqlDbType.VarChar).Value = physicianId;
			return BuildPhysicianClientView(cmd);
		}

		public static YellowstonePathology.Business.Client.Model.PhysicianClientCollection GetPhysicianClientListByPhysicianLastNameV2(string physicianLastName)
		{
			YellowstonePathology.Business.Client.Model.PhysicianClientCollection result = new Client.Model.PhysicianClientCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select pp.PhysicianClientId, c.ClientId, c.ClientName, ph.PhysicianId, ph.ObjectId [ProviderId], ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.Telephone, c.LongDistance, c.FacilityType, ph.NPI " +
				 "from tblClient c " +
				 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
				 "Join tblPhysician ph on pp.ProviderId = ph.ObjectId " +
				 "where ph.LastName like @LastName + '%' order by ph.LastName, ph.FirstName, c.ClientName";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add(@"LastName", SqlDbType.VarChar, 50).Value = physicianLastName;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Client.Model.PhysicianClient physicianClient = new Client.Model.PhysicianClient();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClient, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(physicianClient);
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Client.Model.PhysicianClientCollection GetPhysicianClientListByClientPhysicianLastNameV2(string clientName, string physicianLastName)
		{
			YellowstonePathology.Business.Client.Model.PhysicianClientCollection result = new Client.Model.PhysicianClientCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select pp.PhysicianClientId, c.ClientId, c.ClientName, ph.PhysicianId, ph.ObjectId [ProviderId], ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.Telephone, c.LongDistance, c.FacilityType, ph.NPI " +
				 "from tblClient c " +
				 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
				 "Join tblPhysician ph on pp.ProviderId = ph.ObjectId " +
				 "where c.ClientName like @ClientName + '%' and ph.LastName like @PhysicianLastName + '%' order by ph.LastName, ph.FirstName, c.ClientName";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ClientName", SqlDbType.VarChar, 50).Value = clientName;
			cmd.Parameters.Add("@PhysicianLastName", SqlDbType.VarChar, 50).Value = physicianLastName;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Client.Model.PhysicianClient physicianClient = new Client.Model.PhysicianClient();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClient, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(physicianClient);
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Client.Model.PhysicianClientCollection GetPhysicianClientListByClientIdV2(int clientId)
		{
			YellowstonePathology.Business.Client.Model.PhysicianClientCollection result = new Client.Model.PhysicianClientCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select pp.PhysicianClientId, c.ClientId, c.ClientName, ph.PhysicianId, ph.ObjectId [ProviderId], ph.DisplayName [PhysicianName], c.FacilityType, c.DistributionType, c.Fax [FaxNumber], c.Telephone, c.LongDistance, ph.NPI " +
				 "from tblClient c " +
				 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
				 "Join tblPhysician ph on pp.ProviderId = ph.ObjectId " +
				 "where c.ClientId = @ClientId order by ph.LastName, ph.FirstName, c.ClientName";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add(@"ClientId", SqlDbType.Int).Value = clientId;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Client.Model.PhysicianClient physicianClient = new Client.Model.PhysicianClient();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClient, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(physicianClient);
					}
				}
			}
			return result;
		}

		public static Business.Client.Model.PhysicianClientDistributionList GetPhysicianClientDistributionByClientIdV2(int clientId)
		{
			Business.Client.Model.PhysicianClientDistributionList result = new Client.Model.PhysicianClientDistributionList();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.LongDistance " +
				 "from tblClient c " +
				 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
				 "Join tblPhysician ph on pp.ProviderId = ph.ObjectId " +
				 "where c.ClientId = @ClientId order by ph.LastName, ph.FirstName, c.ClientName";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add(@"ClientId", SqlDbType.Int).Value = clientId;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistribution = new Client.Model.PhysicianClientDistributionListItem();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistribution, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(physicianClientDistribution);
					}
				}
			}
			return result;
		}

		public static Business.Client.Model.PhysicianClientDistributionList GetPhysicianClientDistributionByClientPhysicianLastNameV2(string clientName, string physicianLastName)
		{
			Business.Client.Model.PhysicianClientDistributionList result = new Client.Model.PhysicianClientDistributionList();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.LongDistance " +
				 "from tblClient c " +
				 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
				 "Join tblPhysician ph on pp.ProviderId = ph.ObjectId " +
				 "where c.ClientName like @ClientName + '%' and ph.LastName like @PhysicianLastName + '%' order by ph.LastName, ph.FirstName, c.ClientName";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ClientName", SqlDbType.VarChar, 50).Value = clientName;
			cmd.Parameters.Add("@PhysicianLastName", SqlDbType.VarChar, 50).Value = physicianLastName;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistribution = new Business.Client.Model.PhysicianClientDistributionListItem();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistribution, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(physicianClientDistribution);
					}
				}
			}
			return result;
		}

		public static Business.Client.Model.PhysicianClientDistributionList GetPhysicianClientDistributionByPhysicianFirstLastNameV2(string firstName, string lastName)
		{
			Business.Client.Model.PhysicianClientDistributionList result = new Client.Model.PhysicianClientDistributionList();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.LongDistance " +
				 "from tblClient c " +
				 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
				 "Join tblPhysician ph on pp.ProviderId = ph.ObjectId " +
				 "where ph.FirstName like @FirstName + '%' and ph.LastName like @LastName + '%' order by ph.LastName, ph.FirstName, c.ClientName";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = firstName;
			cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = lastName;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistribution = new Business.Client.Model.PhysicianClientDistributionListItem();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistribution, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(physicianClientDistribution);
					}
				}
			}
			return result;
		}

		public static Business.Client.Model.PhysicianClientDistributionList GetPhysicianClientDistributionByPhysicianLastNameV2(string lastName)
		{
			Business.Client.Model.PhysicianClientDistributionList result = new Client.Model.PhysicianClientDistributionList();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.LongDistance " +
				 "from tblClient c " +
				 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
				 "Join tblPhysician ph on pp.ProviderId = ph.ObjectId " +
				 "where ph.LastName like @LastName + '%' order by ph.LastName, ph.FirstName, c.ClientName";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = lastName;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistribution = new Business.Client.Model.PhysicianClientDistributionListItem();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistribution, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(physicianClientDistribution);
					}
				}
			}
			return result;
		}

		public static int GetLargestPhysicianId()
		{
			int result = 0;
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select max(PhysicianId) from tblPhysician";
			cmd.CommandType = CommandType.Text;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = (Int32)dr[0];
					}
				}
			}

			return result;
		}

		public static int GetLargestClientId()
		{
			int result = 0;
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select max(ClientId) from tblClient";
			cmd.CommandType = CommandType.Text;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = (Int32)dr[0];
					}
				}
			}

			return result;
		}

        public static int GetLargestClientGroupId()
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select max(ClientGroupId) from tblClientGroup";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = (Int32)dr[0];
                    }
                }
            }

            return result;
        }

        public static int GetLargestClientGroupClientId()
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select max(ClientGroupClientId) from tblClientGroupClient";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = (Int32)dr[0];
                    }
                }
            }

            return result;
        }

        public static int GetLargestClientLocationId()
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select max(ClientLocationId) from tblClientLocation";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = (Int32)dr[0];
                    }
                }
            }

            return result;
        }

        public static void DeleteClientGroupClient(int clientid, int clientGroupId)
        {
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "delete tblClientGroupClient where ClientGroupId = @ClientGroupId and ClientId = @ClientId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ClientGroupId", SqlDbType.Int).Value = clientGroupId;
            cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientid;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();   
            }            
        }

        public static YellowstonePathology.Business.Client.Model.ClientSupplyCollection GetClientSupplyCollection(string supplyCategory)
		{
			YellowstonePathology.Business.Client.Model.ClientSupplyCollection result = new Client.Model.ClientSupplyCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select * from tblClientSupply where supplycategory = @SupplyCategory order by supplyname";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@SupplyCategory", SqlDbType.VarChar).Value = supplyCategory;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Client.Model.ClientSupply clientSupply = new Client.Model.ClientSupply();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientSupply, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(clientSupply);
					}
				}
			}

			return result;
		}

		public static YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection GetClientSupplyOrderCollectionByClientId(int clientId)
		{
			YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection result = new Client.Model.ClientSupplyOrderCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT c.*," +
				" (Select cd.* " +
				"  from tblClientSupplyOrderDetail cd where cd.clientSupplyOrderId = c.clientSupplyOrderId for xml path('ClientSupplyOrderDetail'), type) ClientSupplyOrderDetailCollection" +
				" from tblClientSupplyOrder c where c.ClientId = @ClientId order by c.OrderDate desc for xml path('ClientSupplyOrder'), root('ClientSupplyOrderCollection')";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;

			XElement collectionElement = PhysicianClientGateway.GetXElementFromCommand(cmd);
			if (collectionElement != null)
			{
				List<XElement> clientSupplyOrderList = collectionElement.Elements("ClientSupplyOrder").ToList();
				foreach (XElement clientSupplyOrderElement in clientSupplyOrderList)
				{
					YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder = BuildClientSupplyOrder(clientSupplyOrderElement);
					result.Add(clientSupplyOrder);

				}
			}
			return result;
		}

        public static YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection GetClientSupplyOrderCollection()
        {
            YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection result = new Client.Model.ClientSupplyOrderCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT c.*," +
                " (Select cd.* " +
                "  from tblClientSupplyOrderDetail cd where cd.clientSupplyOrderId = c.clientSupplyOrderId for xml path('ClientSupplyOrderDetail'), type) ClientSupplyOrderDetailCollection" +
                " from tblClientSupplyOrder c where c.OrderDate >= dateadd(mm, -3, getdate()) order by c.OrderDate desc for xml path('ClientSupplyOrder'), root('ClientSupplyOrderCollection')";
            cmd.CommandType = CommandType.Text;            

            XElement collectionElement = PhysicianClientGateway.GetXElementFromCommand(cmd);
            if (collectionElement != null)
            {
                List<XElement> clientSupplyOrderList = collectionElement.Elements("ClientSupplyOrder").ToList();
                foreach (XElement clientSupplyOrderElement in clientSupplyOrderList)
                {
                    YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder = BuildClientSupplyOrder(clientSupplyOrderElement);
                    result.Add(clientSupplyOrder);

                }
            }
            return result;
        }

        public static YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection GetClientSupplyOrderCollectionByFinal(bool final)
        {
            YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection result = new Client.Model.ClientSupplyOrderCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT c.*," +
                " (Select cd.* " +
                "  from tblClientSupplyOrderDetail cd where cd.clientSupplyOrderId = c.clientSupplyOrderId for xml path('ClientSupplyOrderDetail'), type) ClientSupplyOrderDetailCollection" +
                " from tblClientSupplyOrder c where c.OrderDate >= dateadd(mm, -3, getdate()) and c.OrderFinal = @Final order by c.OrderDate desc for xml path('ClientSupplyOrder'), root('ClientSupplyOrderCollection')";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@Final", SqlDbType.Bit).Value = final;

            XElement collectionElement = PhysicianClientGateway.GetXElementFromCommand(cmd);
            if (collectionElement != null)
            {
                List<XElement> clientSupplyOrderList = collectionElement.Elements("ClientSupplyOrder").ToList();
                foreach (XElement clientSupplyOrderElement in clientSupplyOrderList)
                {
                    YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder = BuildClientSupplyOrder(clientSupplyOrderElement);
                    result.Add(clientSupplyOrder);

                }
            }
            return result;
        }

        private static YellowstonePathology.Business.Client.Model.ClientSupplyOrder BuildClientSupplyOrder(XElement sourceElement)
		{
			YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder = new Client.Model.ClientSupplyOrder();
			YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(sourceElement, clientSupplyOrder);
			xmlPropertyWriter.Write();

			List<XElement> clientSupplyOrderDetailElements = (from item in sourceElement.Elements("ClientSupplyOrderDetailCollection")
															select item).ToList<XElement>();
			foreach (XElement clientSupplyOrderDetailElement in clientSupplyOrderDetailElements.Elements("ClientSupplyOrderDetail"))
			{
				YellowstonePathology.Business.Client.Model.ClientSupplyOrderDetail clientSupplyOrderDetail = new YellowstonePathology.Business.Client.Model.ClientSupplyOrderDetail();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriterDetail = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(clientSupplyOrderDetailElement, clientSupplyOrderDetail);
				xmlPropertyWriterDetail.Write();
				clientSupplyOrder.ClientSupplyOrderDetailCollection.Add(clientSupplyOrderDetail);
			}
			return clientSupplyOrder;
		}

        public static int GetAccessionCountByPhysicianId(int physicianId)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select count(*) from tblAccessionOrder where PhysicianId = @PhysicianId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@PhysicianId", SqlDbType.Int).Value = physicianId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = (Int32)dr[0];
                    }
                }
            }

            return result;
        }

        public static int GetAccessionCountByClientId(int clientId)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select count(*) from tblAccessionOrder where ClientId = @ClientId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = (Int32)dr[0];
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Domain.PhysicianClientCollection GetPhysicianClientCollectionByProviderId(string objectId)
        {
            YellowstonePathology.Business.Domain.PhysicianClientCollection result = new Domain.PhysicianClientCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblPhysicianClient where ProviderId = @ProviderId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ProviderId", SqlDbType.VarChar).Value = objectId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Domain.PhysicianClient physicianClient = new Domain.PhysicianClient();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClient, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianClient);
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.Business.Domain.PhysicianClientCollection GetPhysicianClientCollectionByClientId(int clientId)
        {
            YellowstonePathology.Business.Domain.PhysicianClientCollection result = new Domain.PhysicianClientCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblPhysicianClient where ClientId = @ClientId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Domain.PhysicianClient physicianClient = new Domain.PhysicianClient();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClient, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianClient);
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.Business.Client.Model.PhysicianClientDistributionCollection GetPhysicianClientDistributionByPhysicianClientId(string physicianClientId)
        {
            YellowstonePathology.Business.Client.Model.PhysicianClientDistributionCollection result = new Client.Model.PhysicianClientDistributionCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblPhysicianClientDistribution where PhysicianClientId = @PhysicianClientId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@PhysicianClientId", SqlDbType.VarChar).Value = physicianClientId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.Model.PhysicianClientDistribution physicianClientDistribution = new Client.Model.PhysicianClientDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianClientDistribution);
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.Business.Client.Model.ProviderClientCollection GetHomeBaseProviderClientListByProviderLastName(string physicianLastName)
        {
            YellowstonePathology.Business.Client.Model.ProviderClientCollection result = new Client.Model.ProviderClientCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select p.*, " +
                "(select pc.* from tblPhysicianClient pc where p.ObjectId = pc.ProviderId and pc.ClientId = p.HomeBaseClientId for xml Path('ProviderClient'), type),  " +
                "(select c.* from tblClient c where c.ClientId = p.HomeBaseClientId for xml Path('Client'), type) "+
                "from tblPhysician p where p.LastName like @LastName + '%' order by p.LastName, p.FirstName for xml Path('Physician'), root('ProviderClientCollection')";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = physicianLastName;

            XElement providerClientCollectionElement = GetXElementFromCommand(cmd);
            if (providerClientCollectionElement != null)
            {
                List<XElement> providerElements = providerClientCollectionElement.Elements("Physician").ToList();
                foreach (XElement providerElement in providerElements)
                {
                    YellowstonePathology.Business.Client.Model.ProviderClient providerClient = BuildHomeBaseProviderClient(providerElement);
                    result.Add(providerClient);
                }
            }
            return result;
        }

        public static YellowstonePathology.Business.Client.Model.ProviderClientCollection GetHomeBaseProviderClientListByProviderFirstLastName(string firstName, string lastName)
        {
            YellowstonePathology.Business.Client.Model.ProviderClientCollection result = new Client.Model.ProviderClientCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select p.*, " +
                "(select pc.* from tblPhysicianClient pc where p.ObjectId = pc.ProviderId and pc.ClientId = p.HomeBaseClientId for xml Path('ProviderClient'), type),  " +
                "(select c.* from tblClient c where c.ClientId = p.HomeBaseClientId for xml Path('Client'), type) " +
                "from tblPhysician p where p.FirstName like @FirstName + '%' and p.LastName like @LastName + '%' order by p.LastName, p.FirstName for xml Path('Physician'), root('ProviderClientCollection')";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = firstName;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = lastName;

            XElement providerClientCollectionElement = GetXElementFromCommand(cmd);
            if (providerClientCollectionElement != null)
            {
                List<XElement> providerElements = providerClientCollectionElement.Elements("Physician").ToList();
                foreach (XElement providerElement in providerElements)
                {
                    YellowstonePathology.Business.Client.Model.ProviderClient providerClient = BuildHomeBaseProviderClient(providerElement);
                    result.Add(providerClient);
                }
            }
            return result;
        }

        private static YellowstonePathology.Business.Client.Model.ProviderClient BuildHomeBaseProviderClient(XElement providerElement)
        {
            YellowstonePathology.Business.Client.Model.ProviderClient result = new Client.Model.ProviderClient();
            YellowstonePathology.Business.Domain.Physician physician = new Domain.Physician();
            YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriterP = new Persistence.XmlPropertyWriter(providerElement, physician);
            xmlPropertyWriterP.Write();
            result.Physician = physician;

            XElement clientElement = providerElement.Element("Client");
            if (clientElement != null)
            {
                YellowstonePathology.Business.Client.Model.Client client = new Client.Model.Client();
                YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriterC = new Persistence.XmlPropertyWriter(clientElement, client);
                xmlPropertyWriterC.Write();
                result.Client = client;
            }

            XElement providerClientElement = providerElement.Element("ProviderClient");
            if (providerClientElement != null)
            {
                YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriterPC = new Persistence.XmlPropertyWriter(providerClientElement, result);
                xmlPropertyWriterPC.Write();
            }
            return result;
        }

        public static YellowstonePathology.Business.Client.Model.ClientGroupClientCollection GetClientGroupClientCollection()
        {
            YellowstonePathology.Business.Client.Model.ClientGroupClientCollection result = new Client.Model.ClientGroupClientCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblClientGroupClient";
            cmd.CommandType = CommandType.Text;            

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.Model.ClientGroupClient clientGroupClient = new Client.Model.ClientGroupClient();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientGroupClient, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(clientGroupClient);
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.Business.Client.Model.ClientGroupClientCollection GetClientGroupClientCollectionByClientGroupId(List<int> clientGroupIds)
        {
            string inClause = YellowstonePathology.Business.Helper.IdListHelper.ToIdString(clientGroupIds);
            YellowstonePathology.Business.Client.Model.ClientGroupClientCollection result = new Client.Model.ClientGroupClientCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblClientGroupClient where ClientGroupId in (" + inClause + ")";
            cmd.CommandType = CommandType.Text;            

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.Model.ClientGroupClient clientGroupClient = new Client.Model.ClientGroupClient();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientGroupClient, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(clientGroupClient);
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.Business.Client.Model.ClientGroupClientCollection GetClientGroupClientCollectionByClientGroupId(int clientGroupId)
        {
            YellowstonePathology.Business.Client.Model.ClientGroupClientCollection result = new Client.Model.ClientGroupClientCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblClientGroupClient where ClientGroupId = @ClientGroupId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ClientGroupId", SqlDbType.Int).Value = clientGroupId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.Model.ClientGroupClient clientGroupClient = new Client.Model.ClientGroupClient();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientGroupClient, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(clientGroupClient);
                    }
                }
            }
            return result;
        }

    }
}
