using System;
using System.Data;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{    
    public class ClientOrderGateway
    {
        public static YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection GetOrderBrowserListItemsByOrderDateSVHCOVID(DateTime orderDate)
        {
            YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection resultCollection = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select ClientOrderId, OrderStatus, PanelSetId, PLastName, PFirstName, ProviderName, ClientName, " +
                "OrderedBy, OrderTime, Submitted, Received, OrderType, ExternalOrderId, DateOrderReceived " +
                "from tblClientOrder " +
                "Where tblClientOrder.OrderDate = @OrderDate and orderType = 'COVYPI' and accessioned = 0 and reconciled = 0 " +
                "Order by OrderTime desc;";

            cmd.Parameters.AddWithValue("@OrderDate", orderDate);
            cmd.CommandType = System.Data.CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Business.Persistence.SqlDataReaderPropertyWriter(orderBrowserListItem, dr);
                        propertyWriter.WriteProperties();
                        resultCollection.Add(orderBrowserListItem);
                    }
                }
            }

            return resultCollection;
        }

        public static YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection GetOrderBrowserListItemsByBSDOrders()
        {
            YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection resultCollection = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select ClientOrderId, OrderStatus, PanelSetId, PLastName, PFirstName, ProviderName, ClientName, " +
                "OrderedBy, OrderTime, Submitted, Received, OrderType, ExternalOrderId, DateOrderReceived " +
                "from tblClientOrder " +
                "Where accessioned = 0 and clientId = 1203 and reconciled = 0 " +
                "Order by OrderTime desc, DateOrderReceived desc;";
            
            cmd.CommandType = System.Data.CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Business.Persistence.SqlDataReaderPropertyWriter(orderBrowserListItem, dr);
                        propertyWriter.WriteProperties();
                        resultCollection.Add(orderBrowserListItem);
                    }
                }
            }

            return resultCollection;
        }


        public static YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection GetOrderBrowserListItemsByPlacentaOrders()
        {
            YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection resultCollection = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select ClientOrderId, OrderStatus, PanelSetId, PLastName, PFirstName, ProviderName, ClientName, " +
                "OrderedBy, OrderTime, Submitted, Received, OrderType, ExternalOrderId, DateOrderReceived " +
                "from tblClientOrder " +
                "Where accessioned = 0 and orderType = 'PSPYPI' and clientId in (select clientId from tblClientGroupClient where clientGroupId = 1) and orderDate >= DATE_ADD(curdate(), INTERVAL -7 DAY) " +
                "Order by OrderTime desc, DateOrderReceived desc;";

            cmd.CommandType = System.Data.CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Business.Persistence.SqlDataReaderPropertyWriter(orderBrowserListItem, dr);
                        propertyWriter.WriteProperties();
                        resultCollection.Add(orderBrowserListItem);
                    }
                }
            }

            return resultCollection;
        }


        public static YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection GetOrderBrowserListItemsByOrderDate(DateTime orderDate)
        {
            YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection resultCollection = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select ClientOrderId, OrderStatus, PanelSetId, PLastName, PFirstName, ProviderName, ClientName, " +
                "OrderedBy, OrderTime, Submitted, Received, OrderType, ExternalOrderId, DateOrderReceived " +
                "from tblClientOrder " +
                "Where tblClientOrder.OrderDate = @OrderDate " + 
                "Order by OrderTime desc;";

            cmd.Parameters.AddWithValue("@OrderDate", orderDate);
            cmd.CommandType = System.Data.CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Business.Persistence.SqlDataReaderPropertyWriter(orderBrowserListItem, dr);
                        propertyWriter.WriteProperties();
                        resultCollection.Add(orderBrowserListItem);
                    }
                }
            }

            return resultCollection;
        }

        public static YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection GetOrderBrowserListItemsByHoldStatus()
        {
            YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection resultCollection = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select ClientOrderId, OrderStatus, PanelSetId, PLastName, PFirstName, ProviderName, ClientName, " +
                "OrderedBy, OrderTime, Submitted, Received, OrderType, ExternalOrderId, DateOrderReceived " +
                "from tblClientOrder " +
                "Where Hold = 1 " +
                "Order by OrderTime desc, DateOrderReceived desc;";
            
            cmd.CommandType = System.Data.CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Business.Persistence.SqlDataReaderPropertyWriter(orderBrowserListItem, dr);
                        propertyWriter.WriteProperties();
                        resultCollection.Add(orderBrowserListItem);
                    }
                }
            }

            return resultCollection;
        }


        public static YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection GetRecentOrderBrowserListItemsByClientId(int clientId)
        {
            string clientIdString = clientId.ToString();
            if (clientId == 649 || clientId == 650)
            {
                clientIdString = "649, 650";
            }
            else if (clientId == 723)
            {
                clientIdString = "649, 650, 723";
            }

            YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection resultCollection = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select ClientOrderId, OrderStatus, ClientName, PanelSetId, PLastName, PFirstName, ProviderName, " +
                "OrderedBy, OrderTime, Submitted, Received, OrderType, ExternalOrderId, DateOrderReceived " +
                "from tblClientOrder " +
                "Where ClientId in (" + clientIdString + ") and " +
                "OrderTime >  date_add(curdate(), Interval -7 Day) and " +
                "SystemInitiatingOrder <> 'YPIILIS' " +
                "Order by OrderTime desc;";

            cmd.CommandType = System.Data.CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Business.Persistence.SqlDataReaderPropertyWriter(orderBrowserListItem, dr);
                        propertyWriter.WriteProperties();
                        resultCollection.Add(orderBrowserListItem);
                    }
                }
            }

            return resultCollection;
        }

		public static YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection GetOrderBrowserListItemsByMasterAccessionNo(string masterAccessionNo)
		{
			YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection resultCollection = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection();
			MySqlCommand cmd = new MySqlCommand();
			cmd.Parameters.AddWithValue("@MasterAccessionNo", masterAccessionNo);
			cmd.CommandText = "Select ClientOrderId, OrderStatus, PanelSetId, PLastName, PFirstName, ProviderName, ClientName, " +
                "OrderedBy, OrderTime, Submitted, Received, OrderType, ExternalOrderId, DateOrderReceived " +
                "from tblClientOrder " +
                "Where tblClientOrder.MasterAccessionNo = @MasterAccessionNo " +
				"Order by OrderTime desc;";

			cmd.CommandType = System.Data.CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Business.Persistence.SqlDataReaderPropertyWriter(orderBrowserListItem, dr);
                        propertyWriter.WriteProperties();
						resultCollection.Add(orderBrowserListItem);
					}
				}
			}

			return resultCollection;
		}

        public static YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection GetOrderBrowserListItemsByContainerId(string containerId)
        {
            YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection resultCollection = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Parameters.AddWithValue("@ContainerId", containerId);
            cmd.CommandText = "Select co.ClientOrderId, co.OrderStatus, co.PanelSetId, co.PLastName, co.PFirstName, co.ProviderName, co.ClientName, " +
                "co.OrderedBy, co.OrderTime, co.Submitted, co.Received, co.OrderType, co.ExternalOrderId, co.DateOrderReceived " +
                "from tblClientOrder co " +
                "join tblClientOrderDetail cod on co.ClientOrderid = cod.ClientOrderId " +
                "Where cod.ContainerId = @ContainerId " +
                "Order by OrderTime desc;";

            cmd.CommandType = System.Data.CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Business.Persistence.SqlDataReaderPropertyWriter(orderBrowserListItem, dr);
                        propertyWriter.WriteProperties();
                        resultCollection.Add(orderBrowserListItem);
                    }
                }
            }

            return resultCollection;
        }

        public static YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection GetOrderBrowserListItemsByNotSigned()
        {
            YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection resultCollection = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection();
            MySqlCommand cmd = new MySqlCommand();            
            cmd.CommandText = "Select co.ClientOrderId, co.OrderStatus, co.PanelSetId, co.PLastName, co.PFirstName, co.ProviderName, co.ClientName, " +
                "co.OrderedBy, co.OrderTime, co.Submitted, co.Received, co.OrderType, co.ExternalOrderId, co.DateOrderReceived " +
                "from tblClientOrder co " +
                "join tblClientOrderDetail cod on co.ClientOrderid = cod.ClientOrderId " +
                "where providerid = '1972692416' and co.orderType = 'SARS-CoV-2' and DateSigned is null " +
                "Order by OrderTime desc;";

            cmd.CommandType = System.Data.CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Business.Persistence.SqlDataReaderPropertyWriter(orderBrowserListItem, dr);
                        propertyWriter.WriteProperties();
                        resultCollection.Add(orderBrowserListItem);
                    }
                }
            }

            return resultCollection;
        }

        public static YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection GetOrderBrowserListItemsByPatientName(string lastName, string firstName)
		{
			YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection resultCollection = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection();
			MySqlCommand cmd = new MySqlCommand();
			cmd.Parameters.AddWithValue("@PLastName", lastName);
			if (string.IsNullOrEmpty(firstName) == true)
			{
				cmd.CommandText = "Select ClientOrderId, OrderStatus, PanelSetId, PLastName, PFirstName, ProviderName, " +
                    "ClientName, OrderedBy, OrderTime, Submitted, Received, OrderType, ExternalOrderId, DateOrderReceived " +
                    "from tblClientOrder Where tblClientOrder.PLastName like concat(@PLastName, '%') Order by OrderTime desc, DateOrderReceived desc;";
			}
			else
			{
				cmd.Parameters.AddWithValue("@PFirstName", firstName);
				cmd.CommandText = "Select ClientOrderId, OrderStatus, PanelSetId, PLastName, PFirstName, ProviderName, " +
                    "ClientName, OrderedBy, OrderTime, Submitted, Received, OrderType, ExternalOrderId, DateOrderReceived " +
                    "from tblClientOrder Where tblClientOrder.PLastName like concat(@PLastName, '%') and PFirstName like " +
                    "concat(@PFirstName, '%') Order by OrderTime desc, DateOrderReceived desc;";
			}

			cmd.CommandType = System.Data.CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Business.Persistence.SqlDataReaderPropertyWriter(orderBrowserListItem, dr);
                        propertyWriter.WriteProperties();
						resultCollection.Add(orderBrowserListItem);
					}
				}
			}

			return resultCollection;
		}

        public static string GetClientOrderByContainerId(string containerId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select co.ClientOrderId " +
                "from tblClientOrder co " +
                "join tblClientOrderDetail cod on co.ClientOrderId = cod.ClientOrderId " +
                "where cod.ContainerId = @ContainerId;";

            cmd.Parameters.AddWithValue("@ContainerId", containerId);
            cmd.CommandType = System.Data.CommandType.Text;

            string result = null;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                result = (string)cmd.ExecuteScalar();
            }

            return result;
        }

        public static YellowstonePathology.Business.Client.Model.PhysicianCollection GetPhysiciansByClientId(int clientId)
        {
            YellowstonePathology.Business.Client.Model.PhysicianCollection physicianCollection = new Client.Model.PhysicianCollection();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select ph.* " +
                "from tblPhysician ph " +
                "join tblPhysicianClient pc on ph.PhysicianId = pc.PhysicianId " +
                "join tblClient c on pc.ClientId = c.ClientId " +
                "where c.ClientId = @ClientId";
            cmd.Parameters.AddWithValue("@ClientId", clientId);
            cmd.CommandType = System.Data.CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        Client.Model.Physician physician = new Client.Model.Physician();
                        Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physician, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        physicianCollection.Add(physician);
                    }
                }
            }

            return physicianCollection;
        }

		public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetClientOrdersByMasterAccessionNo(string masterAccessionNo)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "gwGetClientOrdersByMasterAccessionNo";
            cmd.Parameters.AddWithValue("MasterAccessionNo", masterAccessionNo);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection();

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    BuildClientOrderCollection(dr, clientOrderCollection);
                }
            }

            return clientOrderCollection;
        }        

        public static YellowstonePathology.Business.ClientOrder.Model.ClientOrder GetClientOrderByExternalOrderId(string externalOrderId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "gwGetClientOrderByExternalOrderId";
            cmd.Parameters.AddWithValue("ExternalOrderId", externalOrderId);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = null;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    clientOrder = BuildClientOrder(dr);
                    dr.NextResult();
                    BuildClientOrderDetailCollection(clientOrder.ClientOrderDetailCollection, dr);
                }
            }

			if (clientOrder.ClientOrderId == null)
			{
				return null;
			}

			return clientOrder;
        }

        public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetClientOrdersBySvhAccountNo(string svhAccountNo)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "gwGetClientOrdersBySvhAccount";
            cmd.Parameters.AddWithValue("SvhAccount", svhAccountNo);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection();

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    BuildClientOrderCollection(dr, clientOrderCollection);                    
                }
            }
            
            return clientOrderCollection;
        }

        public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetRecentClientOrdersBySvhAccountNo(string svhAccountNo)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "gwGetRecentClientOrdersBySvhAccount";
            cmd.Parameters.AddWithValue("SvhAccount", svhAccountNo);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection();

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    BuildClientOrderCollection(dr, clientOrderCollection);
                }
            }

            return clientOrderCollection;
        }

        public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetClientOrdersByExternalOrderId(string externalOrderId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "gwGetClientOrdersByExternalOrderId";
            cmd.Parameters.AddWithValue("ExternalOrderId", externalOrderId);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection();

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    BuildClientOrderCollection(dr, clientOrderCollection);
                }
            }

            return clientOrderCollection;
        }

		public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetClientOrdersBySvhMedicalRecord(string svhMedicalRecord)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "gwGetClientOrdersBySvhMedicalRecord";
			cmd.Parameters.AddWithValue("SvhMedicalRecord", svhMedicalRecord);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection();

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					BuildClientOrderCollection(dr, clientOrderCollection);
				}
			}

			return clientOrderCollection;
		}

        public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetClientOrdersBySql(string sql)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = System.Data.CommandType.Text;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection();

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    BuildClientOrderCollection(dr, clientOrderCollection);
                }
            }

            return clientOrderCollection;
        }

        public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetClientOrdersByANumber(string aNumber)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "gwGetClientOrdersByANumber";
            cmd.Parameters.AddWithValue("ANumber", aNumber);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection();

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    BuildClientOrderCollection(dr, clientOrderCollection);
                }
            }

            return clientOrderCollection;
        }

        public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetClientOrdersByOrderDate(DateTime orderDate)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "gwGetClientOrdersByOrderDate";
            cmd.Parameters.AddWithValue("OrderDate", orderDate);            

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection();

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    BuildClientOrderCollection(dr, clientOrderCollection);
                }
            }

            return clientOrderCollection;
        }        

		public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetClientOrdersByPatientName(string pFirstName, string pLastName)
		{			
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "gwGetClientOrdersByPatientName";
            cmd.Parameters.AddWithValue("PFirstName", pFirstName);
            cmd.Parameters.AddWithValue("PLastName", pLastName);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection();

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    BuildClientOrderCollection(dr, clientOrderCollection);
                }
            }
            
			return clientOrderCollection;
		}

        

        public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetClientOrderCollectionByContainerIdString(string containerIdString)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "gwGetClientOrdersByContainerIdString";
            cmd.Parameters.AddWithValue("ContainerIdString", containerIdString);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection();

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    BuildClientOrderCollection(dr, clientOrderCollection);
                }
            }

            return clientOrderCollection;
        }

		public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail GetClientOrderDetailByContainerId(string containerId)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "gwGetClientOrderDetailByContainerId";
			cmd.Parameters.AddWithValue("ContainerId", containerId);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;

			return BuildClientOrderDetail(cmd);
		}                

		public static string GetContainerIdByLast12Characters(string last12Characters)
		{
			string result = string.Empty;
			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				MySqlCommand cmd = new MySqlCommand("Select ContainerId from tblClientOrderDetail where ContainerId like concat('%', @Last12Characters);");
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.Parameters.AddWithValue("@Last12Characters", last12Characters);
				cmd.Connection = cn;

				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = dr.GetValue(0) as string;
					}
				}
			}
			return result;
		}

		public YellowstonePathology.Business.ClientOrder.Model.OrderTypeCollection GetAllOrderTypes()
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select * from tblOrderType order by priority;";
			cmd.CommandType = System.Data.CommandType.Text;

			YellowstonePathology.Business.ClientOrder.Model.OrderTypeCollection result = BuildOrderTypeCollection(cmd);
			return result;
		}

		public YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection GetAllOrderCategories()
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select * from tblOrderCategory order by priority; " +
				"Select * from tblOrderType order by priority;";
			cmd.CommandType = System.Data.CommandType.Text;

			YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection result = BuildOrderCategoryCollection(cmd);
			return result;
		}

		public YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection GetOrderCategory(string orderCategoryId)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select * from tblOrderCategory where tblOrderCategory.OrderCategoryId = @OrderCategoryId order by Priority; " +
                "Select * from tblOrderType where tblOrderType.OrderCategoryId = @OrderCategoryId order by Priority;";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.AddWithValue("@OrderCategoryId", orderCategoryId);

			YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection result = BuildOrderCategoryCollection(cmd);
			return result;
		}				

		public static void BuildClientOrderDetailCollection(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection clientOrderDetailCollection, MySqlDataReader dr)
		{        
            int clientOrderDetailCount = 0;
            while (dr.Read())
            {                
                clientOrderDetailCount = Convert.ToInt32(dr["ClientOrderDetailCount"].ToString());
            }

            if (clientOrderDetailCount > 0)
            {
                for (int i = 0; i < clientOrderDetailCount; i++)
                {
                    dr.NextResult();

                    string orderTypeCode = null;
                    while (dr.Read())
                    {
                        orderTypeCode = dr["OrderTypeCode"].ToString();
                    }

                    dr.NextResult();

                    YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail = Business.ClientOrder.Model.ClientOrderDetailFactory.GetClientOrderDetail(orderTypeCode, Persistence.PersistenceModeEnum.UpdateChangedProperties);
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientOrderDetail, dr);
                        propertyWriter.WriteProperties();
                        clientOrderDetailCollection.Add(clientOrderDetail);
                    }
                }
            }            
		}

        public static YellowstonePathology.Business.ClientOrder.Model.ClientOrder BuildClientOrder(MySqlDataReader dr)
        {
            YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = null;
            Nullable<int> panelSetId = null;
            while (dr.Read())
            {                
                if (dr["PanelSetId"] != DBNull.Value)
                {
                    panelSetId = Convert.ToInt32(dr["PanelSetId"].ToString());
                }
            }

            clientOrder = Business.ClientOrder.Model.ClientOrderFactory.GetClientOrder(panelSetId);
            dr.NextResult();

            while (dr.Read())
            {
                YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientOrder, dr);
                propertyWriter.WriteProperties();
            }

            dr.NextResult();
            while (dr.Read())
            {
                YellowstonePathology.Business.Client.Model.ClientLocation clientLocation = new YellowstonePathology.Business.Client.Model.ClientLocation();
				YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter(clientLocation, dr);
				propertyWriter.WriteProperties();
                clientOrder.ClientLocation = clientLocation;
            }

            return clientOrder;
        }

        public static void BuildClientOrderCollection(MySqlDataReader dr, YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection)
        {
            int clientOrderIdCount = 0;
            while (dr.Read())
            {
                if (dr["ClientOrderIdCount"] != DBNull.Value)
                {
                    clientOrderIdCount = Convert.ToInt32(dr["ClientOrderIdCount"].ToString());
                }
            }            

            for (int i = 0; i < clientOrderIdCount; i++)
            {
                dr.NextResult();
                YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = BuildClientOrder(dr);
                dr.NextResult();
                BuildClientOrderDetailCollection(clientOrder.ClientOrderDetailCollection, dr);

				if (clientOrder.ClientOrderId != null)
				{
					clientOrderCollection.Add(clientOrder);
				}
            }
        }

        public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail BuildClientOrderDetail(MySqlCommand cmd)
        {
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail = null; 
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
					string orderTypeCode = null;
					while (dr.Read())
					{
						orderTypeCode = dr["OrderTypeCode"].ToString();
					}

                    if (string.IsNullOrEmpty(orderTypeCode) == false)
                    {
                        dr.NextResult();

                        clientOrderDetail = Business.ClientOrder.Model.ClientOrderDetailFactory.GetClientOrderDetail(orderTypeCode, Persistence.PersistenceModeEnum.UpdateChangedProperties);
                        while (dr.Read())
                        {
                            YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientOrderDetail, dr);
                            propertyWriter.WriteProperties();
                        }
                    }
                    else
                    {
                        dr.NextResult();
                        
                        while (dr.Read())
                        {
                            clientOrderDetail = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail();
                            YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientOrderDetail, dr);
                            propertyWriter.WriteProperties();
                        }
                    }
                }
            }
            return clientOrderDetail;
        }                

		private static YellowstonePathology.Business.ClientOrder.Model.OrderTypeCollection BuildOrderTypeCollection(MySqlCommand cmd)
		{
            YellowstonePathology.Business.ClientOrder.Model.OrderTypeCollection orderTypeCollection = new YellowstonePathology.Business.ClientOrder.Model.OrderTypeCollection();
			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
                        YellowstonePathology.Business.ClientOrder.Model.OrderType orderType = new YellowstonePathology.Business.ClientOrder.Model.OrderType();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter(orderType, dr);
						propertyWriter.WriteProperties();
						orderTypeCollection.Add(orderType);
					}
				}
			}

			return orderTypeCollection;
		}

		private static YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection BuildOrderCategoryCollection(MySqlCommand cmd)
		{
            YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection orderCategoryCollection = new YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection();
			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
                        YellowstonePathology.Business.ClientOrder.Model.OrderCategory orderCategory = new YellowstonePathology.Business.ClientOrder.Model.OrderCategory();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter(orderCategory, dr);
						propertyWriter.WriteProperties();
						orderCategoryCollection.Add(orderCategory);
					}

					dr.NextResult();

					while (dr.Read())
					{
                        YellowstonePathology.Business.ClientOrder.Model.OrderType orderType = new YellowstonePathology.Business.ClientOrder.Model.OrderType();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter(orderType, dr);
						propertyWriter.WriteProperties();

						foreach (YellowstonePathology.Business.ClientOrder.Model.OrderCategory orderCategory in orderCategoryCollection)
						{
							if (orderCategory.OrderCategoryId == orderType.OrderCategoryId)
							{
								orderCategory.OrderTypeCollection.Add(orderType);
								break;
							}
						}
					}
				}
			}

			return orderCategoryCollection;
		}				
	}
}
