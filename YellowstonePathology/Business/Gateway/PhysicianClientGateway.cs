using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
	public class PhysicianClientGateway
	{
        public static Domain.Physician GetPhysicianByMasterAccessionNo(string masterAccessionNo)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select ph.* " +
                "from tblPhysician ph " +
                "join tblAccessionOrder ao on ph.PhysicianId = ao.PhysicianId " +
                "where ao.MasterAccessionNo = MasterAccessionNo;";

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("MasterAccessionNo", masterAccessionNo);
			Domain.Physician result = PhysicianClientGateway.GetPhysicianFromCommand(cmd);
			return result;
		}

		public static Domain.Physician GetPhysicianByNpi(string npi)
		{
			Domain.Physician result = null;
			if (string.IsNullOrEmpty(npi) == false && npi != "0")
			{
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandText = "SELECT * FROM tblPhysician where tblPhysician.Npi = Npi;";
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue("Npi", SqlDbType.VarChar).Value = npi;
				result = PhysicianClientGateway.GetPhysicianFromCommand(cmd);
			}
			return result;
		}

		public static Domain.Physician GetPhysicianByPhysicianId(int physicianId)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT * FROM tblPhysician where tblPhysician.PhysicianId = PhysicianId;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("PhysicianId", physicianId);
			Domain.Physician result = PhysicianClientGateway.GetPhysicianFromCommand(cmd);
			return result;
		}

		private static Domain.Physician GetPhysicianFromCommand(MySqlCommand cmd)
		{
			Domain.Physician result = null;
			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
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
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandType = CommandType.Text;
			if(string.IsNullOrEmpty(firstName))
			{
				cmd.CommandText = "SELECT * FROM tblPhysician where tblPhysician.LastName like concat(LastName, '%') " +
                    "order by tblPhysician.LastName, tblPhysician.FirstName;";
			}
			else
			{
				cmd.CommandText = "SELECT * FROM tblPhysician where tblPhysician.FirstName like concat(FirstName, '%') " +
                    "and tblPhysician.LastName like concat(LastName, '%') order by tblPhysician.LastName, tblPhysician.FirstName;";
                cmd.Parameters.AddWithValue("FirstName", firstName);
			}
			cmd.Parameters.AddWithValue("LastName", lastName);
			Domain.PhysicianCollection result = PhysicianClientGateway.GetPhysicianCollectionFromCommand(cmd);

			if (result.Count == 0)
			{
				cmd = new MySqlCommand();
				if(string.IsNullOrEmpty(firstName))
				{
					cmd.CommandText = "SELECT PhysicianID, ClientId, FirstName, LastName, Active, Address, City, State, Zip, " +
                        "Phone, Fax, OutsideConsult, HPVTest, " +
						"HPVInstructionID, HPVTestToPerformID, FullName, HPVStandingOrderCode, ReportDeliveryMethod, " +
                        "DisplayName, HomeBaseClientId, KRASBRAFStandingOrder, " +
						"VoiceCommand, VoiceCommandIsEnabled, Npi, MiddleInitial, Credentials, UserName, ClientsPhysicianId, ObjectId " +
                        "FROM tblPhysician where tblPhysician.LastName like concat(LastName, '%') order by tblPhysician.LastName, " +
                        "tblPhysician.FirstName;";
				}
				else
				{
					cmd.CommandText = "SELECT PhysicianID, ClientId, FirstName, LastName, Active, Address, City, State, Zip, Phone, " +
                        "Fax, OutsideConsult, HPVTest, " +
						"HPVInstructionID, HPVTestToPerformID, FullName, HPVStandingOrderCode, ReportDeliveryMethod, DisplayName, " +
                        "HomeBaseClientId, KRASBRAFStandingOrder, " +
						"VoiceCommand, VoiceCommandIsEnabled, Npi, MiddleInitial, Credentials, UserName, ClientsPhysicianId, ObjectId " +
                        "FROM tblPhysician where tblPhysician.FirstName like concat(FirstName, '%') and tblPhysician.LastName like " +
                        "concat(LastName, '%') order by tblPhysician.LastName, tblPhysician.FirstName;";
					cmd.Parameters.AddWithValue("FirstName", firstName);
				}
				cmd.Parameters.AddWithValue("LastName", lastName);
				result = PhysicianClientGateway.GetPhysicianCollectionFromCommand(cmd);
			}
			return result;
		}        		

		private static Domain.PhysicianCollection GetPhysicianCollectionFromCommand(MySqlCommand cmd)
		{
			Domain.PhysicianCollection result = new Domain.PhysicianCollection();
			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
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
            YellowstonePathology.Business.Client.Model.ClientGroupCollection result = new Client.Model.ClientGroupCollection();
            MySqlCommand cmd = new MySqlCommand("select * from tblClientGroup order by GroupName;");
            cmd.CommandType = CommandType.Text;            

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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
        }

        public static YellowstonePathology.Business.Client.Model.Client GetClientByClientId(int clientId)
		{
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM tblClient where tblClient.ClientId = ClientId; " +
                "SELECT * from tblClientLocation where tblClientLocation.ClientId = ClientId;";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("ClientId", clientId);
            ClientBuilderV2 clientBuilderV2 = new ClientBuilderV2();
            clientBuilderV2.Build(cmd);
            Client.Model.Client result = clientBuilderV2.Client;
            return result;
		}

		public static YellowstonePathology.Business.Client.Model.ClientCollection GetClientsByClientName(string clientName)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM tblClient where tblClient.ClientName like concat(ClientName, '%') order by tblClient.ClientName; " +
                "SELECT * from tblClientLocation where tblClientLocation.ClientId in (SELECT ClientId FROM tblClient where " +
                "tblClient.ClientName like concat(ClientName, '%')) order by tblClientLocation.Location;";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("ClientName", clientName);
            Client.Model.ClientCollection result = BuildClientCollection(cmd);
            return result;
        }

        public static YellowstonePathology.Business.Client.Model.ClientCollection GetAllClients()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM tblClient;";                
            cmd.CommandType = CommandType.Text;            
            Client.Model.ClientCollection result = BuildClientCollection(cmd);
            return result;
        }

        public static View.ClientLocationViewCollection GetClientLocationViewByClientName(string clientName)
		{
            MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "select c.ClientId, c.ClientName, cl.ClientLocationId, cl.Location from tblClientLocation cl " +
                "join tblClient c on cl.ClientId = c.ClientId where c.ClientName like concat(ClientName, '%') Order By 2, 4;";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("ClientName", clientName);
            View.ClientLocationViewCollection result = new View.ClientLocationViewCollection();

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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
		}

        public static Domain.Physician GetPhysicianById(string objectId)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT * FROM tblPhysician where tblPhysician.ObjectId = ObjectId;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("ObjectId", objectId);
			Domain.Physician result = PhysicianClientGateway.GetPhysicianFromCommand(cmd);
			return result;
		}

		public static Domain.PhysicianCollection GetPhysiciansByClientIdV2(int clientId)
		{
			Domain.PhysicianCollection result = new Domain.PhysicianCollection();

			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "select ph.* " +
			   "from tblPhysician ph " +
			   "join tblPhysicianClient pc on ph.ObjectId = pc.ProviderId " +
			   "where pc.ClientId = ClientId order by ph.LastName;";
			cmd.Parameters.AddWithValue("ClientId", clientId);
			cmd.CommandType = CommandType.Text;
			result = PhysicianClientGateway.GetPhysicianCollectionFromCommand(cmd);
			return result;
		}

        public static YellowstonePathology.Business.Client.Model.ClientCollection GetClientCollectionByClientGroupId(int clientGroupId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select c.* " +
                "from tblClientGroup cg " +
                "join tblClientGroupClient cgc on cg.ClientGroupId = cgc.ClientGroupId " +
                "join tblclient c on cgc.ClientId = c.ClientId " +
                "where cgc.ClientGroupId = ClientGroupId order by c.ClientName;";
            cmd.Parameters.AddWithValue("ClientGroupId", clientGroupId);
            cmd.CommandType = CommandType.Text;

            YellowstonePathology.Business.Client.Model.ClientCollection result = BuildClientCollection(cmd);
            return result;
        }

        private static YellowstonePathology.Business.Client.Model.ClientCollection BuildClientCollection(MySqlCommand cmd)
        {
            Client.Model.ClientCollection result = new Client.Model.ClientCollection();
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    while (dr.Read())
                    {
                        Client.Model.Client client = new YellowstonePathology.Business.Client.Model.Client();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(client, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(client);
                    }
                    if (dr.IsClosed == false)
                    {
                        dr.NextResult();
                        while (dr.Read())
                        {
                            YellowstonePathology.Business.Client.Model.ClientLocation clientLocation = new YellowstonePathology.Business.Client.Model.ClientLocation();
                            Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientLocation, dr);
                            sqlDataReaderPropertyWriter.WriteProperties();
                            foreach (Client.Model.Client client in result)
                            {
                                if (client.ClientId == clientLocation.ClientId)
                                {
                                    client.ClientLocationCollection.Add(clientLocation);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static Domain.PhysicianClient GetPhysicianClient(string providerId, int clientId)
		{
			Domain.PhysicianClient result = null;
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select * from tblPhysicianClient where tblPhysicianClient.ProviderId = ProviderId and " +
                "tblPhysicianClient.ClientId = ClientId;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("ProviderId", providerId);
			cmd.Parameters.AddWithValue("ClientId", clientId);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
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
			MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from tblClient c where c.ClientId = ClientId; " +
                "select p.* from tblPhysician p join tblPhysicianClient pc on p.ObjectId = pc.ProviderId where pc.ClientId = ClientId " +
                "order by p.FirstName;";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("ClientId", clientId);
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    while (dr.Read())
                    {
                        result = new View.ClientPhysicianView();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                    if (dr.IsClosed == false)
                    {
                        dr.NextResult();
                        while (dr.Read())
                        {
                            Domain.Physician physician = new Domain.Physician();
                            Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physician, dr);
                            sqlDataReaderPropertyWriter.WriteProperties();
                            result.Physicians.Add(physician);
                        }
                    }
                }
            }
            return result;
		}

		public static YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection GetPhysicianClientNameCollectionV2(string clientName, string physicianName)
		{
			YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection result = new YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "select pc.PhysicianClientId, pc.ClientId, pc.PhysicianId, pc.ProviderId, c.ClientName, p.FirstName, " +
                "p.LastName, c.Telephone, c.Fax " +
				"from tblPhysicianClient pc join tblClient c on pc.ClientId = c.ClientId " +
				"join tblPhysician p on pc.ProviderId = p.ObjectId " +
				"where p.LastName like concat(PhysicianName, '%') and c.ClientName like concat(ClientName, '%') order by c.ClientName, p.FirstName;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("ClientName",clientName);
			cmd.Parameters.AddWithValue("PhysicianName", physicianName);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
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
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "select pc.PhysicianClientId, pc.ClientId, pc.PhysicianId, pc.ProviderId, c.ClientName, " +
                "p.FirstName, p.LastName, c.Telephone, c.Fax " +
				"from tblPhysicianClient pc join tblClient c on pc.ClientId = c.ClientId " +
				"join tblPhysician p on pc.ProviderId = p.ObjectId " +
                "where p.ObjectId = (select ProviderId from tblPhysicianClient where tblPhysicianClient.PhysicianClientId = PhysicianClientId) " +
                "order by c.ClientName;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("PhysicianClientId", physicianClientId);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
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
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select ph.PhysicianId, ph.ObjectId as ProviderId, ph.FirstName, ph.LastName, c.Telephone HomeBasePhone, c.Fax HomeBaseFax " +
				"from tblPhysician ph " +
				"left outer join tblClient c on ph.HomeBaseClientId = c.ClientId " +
				"where ph.LastName like concat(LastName, '%') order by ph.FirstName;";

			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("LastName", physicianLastName);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
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
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select pcd.*, c.ClientId, c.ClientName, ph.PhysicianId, ph.ObjectId as ProviderId, ph.DisplayName " +
                "PhysicianName, c.DistributionType " +
				"from tblPhysicianClient pc " +
				"join tblPhysicianClientDistribution pcd on pc.PhysicianClientId = pcd.PhysicianClientId " +
				"join tblPhysicianClient pc2 on pcd.DistributionId = pc2.PhysicianClientId " +
				"join tblClient c on pc2.ClientId = c.ClientId " +
				"join tblPhysician ph on pc2.ProviderId = ph.ObjectId " +
				"where pc.PhysicianClientId = PhysicianClientId;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("PhysicianClientId", physicianClientId);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
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
            View.PhysicianClientView result = new View.PhysicianClientView();
            MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "select * from tblPhysician where tblPhysician.ObjectId = ObjectId;" +
				" select c.* from tblClient c join tblPhysicianClient pc on c.ClientId = pc.ClientId where pc.ProviderId = ObjectId order by ClientName;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("ObjectId", physicianId);
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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

		public static YellowstonePathology.Business.Client.Model.PhysicianClientCollection GetPhysicianClientListByPhysicianLastNameV2(string physicianLastName)
		{
			YellowstonePathology.Business.Client.Model.PhysicianClientCollection result = new Client.Model.PhysicianClientCollection();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select pp.PhysicianClientId, c.ClientId, c.ClientName, ph.PhysicianId, ph.ObjectId ProviderId, " +
                "ph.DisplayName PhysicianName, c.DistributionType, c.Fax FaxNumber, c.Telephone, c.LongDistance, c.FacilityType, ph.NPI " +
				 "from tblClient c " +
				 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
				 "Join tblPhysician ph on pp.ProviderId = ph.ObjectId " +
				 "where ph.LastName like concat(LastName, '%') order by ph.LastName, ph.FirstName, c.ClientName;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("LastName", physicianLastName);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
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
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select pp.PhysicianClientId, c.ClientId, c.ClientName, ph.PhysicianId, ph.ObjectId ProviderId, " +
                "ph.DisplayName PhysicianName, c.DistributionType, c.Fax FaxNumber, c.Telephone, c.LongDistance, c.FacilityType, ph.NPI " +
				 "from tblClient c " +
				 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
				 "Join tblPhysician ph on pp.ProviderId = ph.ObjectId " +
				 "where c.ClientName like concat(ClientName, '%') and ph.LastName like concat(PhysicianLastName, '%') order by " +
                 "ph.LastName, ph.FirstName, c.ClientName;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("ClientName", clientName);
			cmd.Parameters.AddWithValue("PhysicianLastName", physicianLastName);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
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
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select pp.PhysicianClientId, c.ClientId, c.ClientName, ph.PhysicianId, ph.ObjectId ProviderId, " +
                "ph.DisplayName PhysicianName, c.FacilityType, c.DistributionType, c.Fax FaxNumber, c.Telephone, c.LongDistance, ph.NPI " +
				 "from tblClient c " +
				 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
				 "Join tblPhysician ph on pp.ProviderId = ph.ObjectId " +
				 "where c.ClientId = ClientId order by ph.LastName, ph.FirstName, c.ClientName;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("ClientId", clientId);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
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
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName PhysicianName, c.DistributionType, " +
                "c.Fax FaxNumber, c.LongDistance " +
				 "from tblClient c " +
				 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
				 "Join tblPhysician ph on pp.ProviderId = ph.ObjectId " +
				 "where c.ClientId = ClientId order by ph.LastName, ph.FirstName, c.ClientName;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("ClientId", clientId);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
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
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName PhysicianName, c.DistributionType, " +
                "c.Fax FaxNumber, c.LongDistance " +
				 "from tblClient c " +
				 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
				 "Join tblPhysician ph on pp.ProviderId = ph.ObjectId " +
				 "where c.ClientName like concat(ClientName, '%') and ph.LastName like concat(PhysicianLastName, '%') " +
                 "order by ph.LastName, ph.FirstName, c.ClientName;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("ClientName",clientName);
			cmd.Parameters.AddWithValue("PhysicianLastName", physicianLastName);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
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
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName PhysicianName, c.DistributionType, " +
                "c.Fax FaxNumber, c.LongDistance " +
				 "from tblClient c " +
				 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
				 "Join tblPhysician ph on pp.ProviderId = ph.ObjectId " +
				 "where ph.FirstName like concat(FirstName, '%') and ph.LastName like concat(LastName, '%') order by " +
                 "ph.LastName, ph.FirstName, c.ClientName;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("FirstName", firstName);
			cmd.Parameters.AddWithValue("LastName", lastName);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
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
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName PhysicianName, c.DistributionType, " +
                "c.Fax FaxNumber, c.LongDistance " +
				 "from tblClient c " +
				 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
				 "Join tblPhysician ph on pp.ProviderId = ph.ObjectId " +
				 "where ph.LastName like concat(LastName, '%') order by ph.LastName, ph.FirstName, c.ClientName;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("LastName", lastName);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
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
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select max(PhysicianId) from tblPhysician;";
			cmd.CommandType = CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
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
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select max(ClientId) from tblClient;";
			cmd.CommandType = CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
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
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select max(ClientGroupId) from tblClientGroup;";
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select max(ClientGroupClientId) from tblClientGroupClient;";
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select max(ClientLocationId) from tblClientLocation;";
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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
            
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "delete tblClientGroupClient where tblClientGroupClient.ClientGroupId = ClientGroupId and " +
                "tblClientGroupClient.ClientId = ClientId;";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("ClientGroupId", clientGroupId);
            cmd.Parameters.AddWithValue("ClientId", clientid);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();   
            }            
        }

        public static YellowstonePathology.Business.Client.Model.ClientSupplyCollection GetClientSupplyCollection(string supplyCategory)
		{
			YellowstonePathology.Business.Client.Model.ClientSupplyCollection result = new Client.Model.ClientSupplyCollection();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select * from tblClientSupply where tblClientSupply.supplycategory = SupplyCategory order by tblClientSupply.supplyname;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("SupplyCategory", supplyCategory);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
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
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * from tblClientSupplyOrder where tblClientSupplyOrder.ClientId = ClientId order by OrderDate desc; " +
                "Select * from tblClientSupplyOrderDetail where clientSupplyOrderId in (SELECT clientSupplyOrderId from " +
                "tblClientSupplyOrder where tblClientSupplyOrder.ClientId = ClientId) order by ClientSupplyOrderDetailId;";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("ClientId", clientId);
            YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection result = BuildClientSupplyOrderCollection(cmd);
            return result;
		}

        private static YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection BuildClientSupplyOrderCollection(MySqlCommand cmd)
        {
            Client.Model.ClientSupplyOrderCollection result = new Client.Model.ClientSupplyOrderCollection();
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    while (dr.Read())
                    {
                        Client.Model.ClientSupplyOrder clientSupplyOrder = new YellowstonePathology.Business.Client.Model.ClientSupplyOrder();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientSupplyOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(clientSupplyOrder);
                    }
                    if (dr.IsClosed == false)
                    {
                        dr.NextResult();
                        while (dr.Read())
                        {
                            YellowstonePathology.Business.Client.Model.ClientSupplyOrderDetail clientSupplyOrderDetail = new YellowstonePathology.Business.Client.Model.ClientSupplyOrderDetail();
                            Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientSupplyOrderDetail, dr);
                            sqlDataReaderPropertyWriter.WriteProperties();
                            foreach (Client.Model.ClientSupplyOrder clientSupplyOrder in result)
                            {
                                if (clientSupplyOrder.ClientSupplyOrderId == clientSupplyOrderDetail.clientsupplyorderid)
                                {
                                    clientSupplyOrder.ClientSupplyOrderDetailCollection.Add(clientSupplyOrderDetail);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection GetClientSupplyOrderCollection()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * from tblClientSupplyOrder where OrderDate >= date_add(curdate(), Interval -3 Month) order by OrderDate desc; " +
                "Select * from tblClientSupplyOrderDetail where clientSupplyOrderId in(SELECT clientSupplyOrderId from " +
                "tblClientSupplyOrder where OrderDate >= date_add(curdate(), Interval -3 Month));";
            cmd.CommandType = CommandType.Text;
            YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection result = BuildClientSupplyOrderCollection(cmd);
            return result;
        }

        public static YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection GetClientSupplyOrderCollectionByFinal(bool final)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * from tblClientSupplyOrder where OrderDate >= date_add(curdate(), Interval -3 Month) and " +
                "OrderFinal = Final order by OrderDate desc; " +
                "Select * from tblClientSupplyOrderDetail cd where cd.clientSupplyOrderId in(SELECT ClientSupplyOrderId from " +
                "tblClientSupplyOrder where OrderDate >= date_add(curdate(), Interval -3 Month) and OrderFinal = Final);";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("Final", final);
            YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection result = BuildClientSupplyOrderCollection(cmd);
            return result;
        }

        public static int GetAccessionCountByPhysicianId(int physicianId)
        {
            int result = 0;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select count(*) from tblAccessionOrder where tblAccessionOrder.PhysicianId = PhysicianId;";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("PhysicianId", physicianId);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select count(*) from tblAccessionOrder where tblAccessionOrder.ClientId = ClientId;";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("ClientId",clientId);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblPhysicianClient where tblPhysicianClient.ProviderId = ProviderId;";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("ProviderId", objectId);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblPhysicianClient where tblPhysicianClient.ClientId = ClientId;";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("ClientId", clientId);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblPhysicianClientDistribution wheretblPhysicianClientDistribution. PhysicianClientId = PhysicianClientId;";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("PhysicianClientId", physicianClientId);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblPhysician where tblPhysician.LastName like concat(LastName, '%') order by " +
                "tblPhysician.LastName, tblPhysician.FirstName; " +
                "select c.* from tblClient c join tblPhysician p on c.ClientId = p.HomeBaseClientId where p.LastName like concat(LastName, '%'); " +
                "select pc.* from tblPhysicianClient pc join tblPhysician p on p.ObjectId = pc.ProviderId and pc.ClientId = " +
                "p.HomeBaseClientId where p.LastName like concat(LastName, '%');";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("LastName", physicianLastName);
            YellowstonePathology.Business.Client.Model.ProviderClientCollection result = BuildProviderClientCollection(cmd);
            return result;
        }

        public static YellowstonePathology.Business.Client.Model.ProviderClientCollection GetHomeBaseProviderClientListByProviderFirstLastName(string firstName, string lastName)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblPhysician where tblPhysician.FirstName like concat(FirstName, '%') and " +
                "tblPhysician.LastName like concat(LastName, '%') order by LastName, FirstName; " +
                "select c.* from tblClient c join tblPhysician p on c.ClientId = p.HomeBaseClientId where p.FirstName like " +
                "concat(FirstName, '%') and p.LastName like concat(LastName, '%'); " +
                "select pc.* from tblPhysicianClient pc join tblPhysician p on p.ObjectId = pc.ProviderId and pc.ClientId = " +
                "p.HomeBaseClientId where p.FirstName like concat(FirstName, '%') and p.LastName like concat(LastName, '%');";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("FirstName", firstName);
            cmd.Parameters.AddWithValue("LastName", lastName);
            YellowstonePathology.Business.Client.Model.ProviderClientCollection result = BuildProviderClientCollection(cmd);
            return result;
        }

        private static YellowstonePathology.Business.Client.Model.ProviderClientCollection BuildProviderClientCollection(MySqlCommand cmd)
        {
            YellowstonePathology.Business.Client.Model.ProviderClientCollection result = new Client.Model.ProviderClientCollection();
            YellowstonePathology.Business.Client.Model.ClientCollection clientCollection = new YellowstonePathology.Business.Client.Model.ClientCollection();
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.Model.ProviderClient providerClient = new Client.Model.ProviderClient();
                        YellowstonePathology.Business.Domain.Physician physician = new Domain.Physician();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physician, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        providerClient.Physician = physician;
                        result.Add(providerClient);
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.Model.Client client = new YellowstonePathology.Business.Client.Model.Client();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(client, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        clientCollection.Add(client);
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        string providerClientId = dr["PhysicianClientId"].ToString();
                        string physicianObjectId = dr["ProviderId"].ToString();
                        foreach(Client.Model.ProviderClient providerClient in result)
                        {
                            if(providerClient.Physician.ObjectId == physicianObjectId)
                            {
                                providerClient.PhysicianClientId = providerClientId;
                                break;
                            }
                        }
                    }
                }
            }

            foreach (Client.Model.ProviderClient providerClient in result)
            {
                foreach (Client.Model.Client client in clientCollection)
                {
                    if (client.ClientId == providerClient.Physician.HomeBaseClientId)
                    {
                        providerClient.Client = client;
                        break;
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Client.Model.ClientGroupClientCollection GetClientGroupClientCollection()
        {
            YellowstonePathology.Business.Client.Model.ClientGroupClientCollection result = new Client.Model.ClientGroupClientCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblClientGroupClient;";
            cmd.CommandType = CommandType.Text;            

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblClientGroupClient where ClientGroupId in (" + inClause + ");";
            cmd.CommandType = CommandType.Text;            

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblClientGroupClient where tblClientGroupClient.ClientGroupId = ClientGroupId;";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("ClientGroupId", clientGroupId);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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
