using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Gateway
{    
    public class ClientOrderGateway
    {
        private static string ServerSqlConnectionString = "Data Source=TestSQL;Initial Catalog=YPIData;Integrated Security=True";        

        public static YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection GetOrderBrowserListItemsByOrderDate(DateTime orderDate)
        {
            YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection resultCollection = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select ClientOrderId, PanelSetId, PLastName, PFirstName, ProviderName, ClientName, OrderedBy, OrderTime, Submitted, Received, OrderType " +
                "from tblClientOrder " +
                "Where OrderDate = @OrderDate " + 
                "Order by OrderTime desc";

            cmd.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = orderDate;
            cmd.CommandType = System.Data.CommandType.Text;

            using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem();
                        YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
                        orderBrowserListItem.WriteProperties(propertyWriter);
                        resultCollection.Add(orderBrowserListItem);
                    }
                }
            }

            return resultCollection;
        }

        public static YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection GetOrderBrowserListItemsByHoldStatus()
        {
            YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection resultCollection = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select ClientOrderId, PanelSetId, PLastName, PFirstName, ProviderName, ClientName, OrderedBy, OrderTime, Submitted, Received, OrderType " +
                "from tblClientOrder " +
                "Where Hold = 1 " +
                "Order by OrderTime desc";
            
            cmd.CommandType = System.Data.CommandType.Text;

            using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem();
                        YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
                        orderBrowserListItem.WriteProperties(propertyWriter);
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
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select ClientOrderId, ClientName, PanelSetId, PLastName, PFirstName, ProviderName, OrderedBy, OrderTime, Submitted, Received, OrderType " +
                "from tblClientOrder " +
                "Where ClientId in (" + clientIdString + ") and " +
                "OrderTime >  dateadd(dd, -7, getdate()) and " +
                "SystemInitiatingOrder <> 'YPIILIS' " +
                "Order by OrderTime desc";

            cmd.CommandType = System.Data.CommandType.Text;

            using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem();
                        YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
                        orderBrowserListItem.WriteProperties(propertyWriter);
                        resultCollection.Add(orderBrowserListItem);
                    }
                }
            }

            return resultCollection;
        }

		public static YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection GetOrderBrowserListItemsByMasterAccessionNo(string masterAccessionNo)
		{
			YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection resultCollection = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;
			cmd.CommandText = "Select ClientOrderId, PanelSetId, PLastName, PFirstName, ProviderName, ClientName, OrderedBy, OrderTime, Submitted, Received, OrderType " +
				"from tblClientOrder " +
				"Where MasterAccessionNo = @MasterAccessionNo " +
				"Order by OrderTime desc";

			cmd.CommandType = System.Data.CommandType.Text;

			using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem();
						YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
						orderBrowserListItem.WriteProperties(propertyWriter);
						resultCollection.Add(orderBrowserListItem);
					}
				}
			}

			return resultCollection;
		}

		public static YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection GetOrderBrowserListItemsByPatientName(string lastName, string firstName)
		{
			YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection resultCollection = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.Parameters.Add("@PLastName", SqlDbType.VarChar).Value = lastName;
			if (string.IsNullOrEmpty(firstName) == true)
			{
				cmd.CommandText = "Select ClientOrderId, PanelSetId, PLastName, PFirstName, ProviderName, ClientName, OrderedBy, OrderTime, Submitted, Received, OrderType " +
					"from tblClientOrder Where PLastName like @PLastName + '%' Order by OrderTime desc";
			}
			else
			{
				cmd.Parameters.Add("@PFirstName", SqlDbType.VarChar).Value = firstName;
				cmd.CommandText = "Select ClientOrderId, PanelSetId, PLastName, PFirstName, ProviderName, ClientName, OrderedBy, OrderTime, Submitted, Received, OrderType " +
					"from tblClientOrder Where PLastName like @PLastName + '%' and  PFirstName like @PFirstName + '%' Order by OrderTime desc";
			}

			cmd.CommandType = System.Data.CommandType.Text;

			using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = new YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem();
						YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
						orderBrowserListItem.WriteProperties(propertyWriter);
						resultCollection.Add(orderBrowserListItem);
					}
				}
			}

			return resultCollection;
		}

        public static YellowstonePathology.Business.Client.Model.PhysicianCollection GetPhysiciansByClientId(int clientId)
        {
            YellowstonePathology.Business.Client.Model.PhysicianCollection physicianCollection = null;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select ph.* " +
                "from tblPhysician ph " +
                "join tblPhysicianClient pc on ph.PhysicianId = pc.PhysicianId " +
                "join tblClient c on pc.ClientId = c.ClientId " +
                "where c.ClientId = @ClientId for xml path('Physician'), root('PhysicianCollection')";
            cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;
            cmd.CommandType = System.Data.CommandType.Text;

            try
            {
                //physicianCollection = YellowstonePathology.Business.Domain.Persistence.SqlXmlPersistence.CrudOperations.ExecuteXmlCommand<YellowstonePathology.YpiConnect.Contract.Domain.PhysicianCollection>(cmd, Business.Domain.Persistence.DataLocationEnum.ProductionData);
            }
            catch (Exception)
            {
                physicianCollection = new YellowstonePathology.Business.Client.Model.PhysicianCollection();
            }

            return physicianCollection;
        }

		public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetClientOrdersByMasterAccessionNo(string masterAccessionNo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "gwGetClientOrdersByMasterAccessionNo";

            SqlParameter masterAccessionNoParameter = new SqlParameter("@MasterAccessionNo", SqlDbType.VarChar, 100);
            masterAccessionNoParameter.Value = masterAccessionNo;
            cmd.Parameters.Add(masterAccessionNoParameter);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection();

            using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    BuildClientOrderCollection(dr, clientOrderCollection);
                }
            }

            return clientOrderCollection;
        }

        public static YellowstonePathology.Business.ClientOrder.Model.ClientOrder GetClientOrderByExternalOrderId(string externalOrderId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "gwGetClientOrderByExternalOrderId";

            SqlParameter externalOrderIdParameter = new SqlParameter("@ExternalOrderId", SqlDbType.VarChar, 100);
            externalOrderIdParameter.Value = externalOrderId;
            cmd.Parameters.Add(externalOrderIdParameter);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = null;

            using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
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
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "gwGetClientOrdersBySvhAccount";

            SqlParameter svhAccountParameter = new SqlParameter("@SvhAccount", SqlDbType.VarChar, 100);
            svhAccountParameter.Value = svhAccountNo;
            cmd.Parameters.Add(svhAccountParameter);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection();

            using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    BuildClientOrderCollection(dr, clientOrderCollection);                    
                }
            }
            
            return clientOrderCollection;
        }

        public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetClientOrdersByExternalOrderId(string externalOrderId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "gwGetClientOrdersByExternalOrderId";

            cmd.Parameters.Add("@ExternalOrderId", SqlDbType.VarChar).Value = externalOrderId;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection();

            using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    BuildClientOrderCollection(dr, clientOrderCollection);
                }
            }

            return clientOrderCollection;
        }

		public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetClientOrdersBySvhMedicalRecord(string svhMedicalRecord)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "gwGetClientOrdersBySvhMedicalRecord";

			cmd.Parameters.Add("@SvhMedicalRecord", SqlDbType.VarChar).Value = svhMedicalRecord;
			cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection();

			using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					BuildClientOrderCollection(dr, clientOrderCollection);
				}
			}

			return clientOrderCollection;
		}

        public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetClientOrdersByOrderDate(DateTime orderDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "gwGetClientOrdersByOrderDate";

            SqlParameter orderDateParameter = new SqlParameter("@OrderDate", SqlDbType.DateTime);
            orderDateParameter.Value = orderDate;
            cmd.Parameters.Add(orderDateParameter);            

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection();

            using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    BuildClientOrderCollection(dr, clientOrderCollection);
                }
            }

            return clientOrderCollection;
        }        

		public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetClientOrdersByPatientName(string pFirstName, string pLastName)
		{			
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "gwGetClientOrdersByPatientName";

            SqlParameter firstNameParameter = new SqlParameter("@PFirstName", SqlDbType.VarChar, 100);
            firstNameParameter.Value = pFirstName;
            cmd.Parameters.Add(firstNameParameter);

            SqlParameter lastNameParameter = new SqlParameter("@PLastName", SqlDbType.VarChar, 100);
            lastNameParameter.Value = pLastName;
            cmd.Parameters.Add(lastNameParameter);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection();

            using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    BuildClientOrderCollection(dr, clientOrderCollection);
                }
            }
            
			return clientOrderCollection;
		}        

		public static string GetClientOrderByContainerId(string containerId)
		{
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select co.ClientOrderId " +
                "from tblClientOrder co " +
                "join tblClientOrderDetail cod on co.ClientOrderId = cod.ClientOrderId " +
                "where cod.Containerid = @ContainerId";

            SqlParameter containerIdParameter = new SqlParameter("@ContainerId", SqlDbType.VarChar, 100);
            containerIdParameter.Value = containerId;
            cmd.Parameters.Add(containerIdParameter);
            cmd.CommandType = System.Data.CommandType.Text;

            string result = null;

            using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                result = (string)cmd.ExecuteScalar();
            }			

            return result;
		}

        public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetClientOrderCollectionByContainerIdString(string containerIdString)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "gwGetClientOrdersByContainerIdString";

            SqlParameter containerIdStringParameter = new SqlParameter("@ContainerIdString", SqlDbType.VarChar);
            containerIdStringParameter.Value = containerIdString;
            cmd.Parameters.Add(containerIdStringParameter);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection();

            using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    BuildClientOrderCollection(dr, clientOrderCollection);
                }
            }

            return clientOrderCollection;
        }

		public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail GetClientOrderDetailByContainerId(string containerId)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "gwGetClientOrderDetailByContainerId";
			cmd.Parameters.Add("@ContainerId", SqlDbType.VarChar).Value = containerId;
			cmd.CommandType = System.Data.CommandType.StoredProcedure;

			return BuildClientOrderDetail(cmd);
		}                

		public static string GetContainerIdByLast12Characters(string last12Characters)
		{
			string result = string.Empty;
			using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
			{
				cn.Open();
				SqlCommand cmd = new SqlCommand("Select ContainerId from tblClientOrderDetail where ContainerId like '%' + @Last12Characters");
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.Parameters.Add("@Last12Characters", SqlDbType.VarChar).Value = last12Characters;
				cmd.Connection = cn;

				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = dr.GetValue(0) as string;
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.ClientOrder.Model.ShipmentListItemCollection GetShipmentListItemCollection(int clientId)
		{
			YellowstonePathology.Business.ClientOrder.Model.ShipmentListItemCollection resultCollection = new YellowstonePathology.Business.ClientOrder.Model.ShipmentListItemCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = " Select * from tblShipment where ClientId = @ClientId and (ShipDate is null or datediff(dd, ShipDate, getdate()) < 8) order by ShipDate Desc";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;

			using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.ClientOrder.Model.ShipmentListItem shipmentListItem = new YellowstonePathology.Business.ClientOrder.Model.ShipmentListItem();
						YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
						shipmentListItem.WriteProperties(propertyWriter);
						resultCollection.Add(shipmentListItem);
					}
				}
			}

			return resultCollection;
		}


		public static YellowstonePathology.Business.ClientOrder.Model.ShipmentReturnResult GetShipment(string shipmentId)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetShipment";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ShipmentId", SqlDbType.VarChar).Value = shipmentId;

            YellowstonePathology.Business.ClientOrder.Model.ShipmentReturnResult shipmentReturnResult = new YellowstonePathology.Business.ClientOrder.Model.ShipmentReturnResult();

			YellowstonePathology.Business.ClientOrder.Model.Shipment shipment = new YellowstonePathology.Business.ClientOrder.Model.Shipment();
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection clientOrderDetailCollection = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection();

            shipmentReturnResult.Shipment = shipment;
            shipmentReturnResult.ClientOrderDetailCollection = clientOrderDetailCollection;

			using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(shipment, dr);
						propertyWriter.WriteProperties();
					}

					dr.NextResult();

					BuildClientOrderDetailCollection(clientOrderDetailCollection, dr);
				}
			}			

            return shipmentReturnResult;
		}

		public XElement PackingSlipReport(string shipmentId)
		{
			SqlCommand cmd = new SqlCommand("ws_PackingSlipReport");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("ShipmentId", SqlDbType.VarChar).Value = shipmentId;

            throw new Exception("This needs to be fixed");
            /*
			XElement result = YellowstonePathology.Business.Domain.Persistence.SqlXmlPersistence.CrudOperations.ExecuteCommand(cmd, Business.Domain.Persistence.DataLocationEnum.ProductionData);
			return result;
            */
		}

		public YellowstonePathology.Business.ClientOrder.Model.OrderTypeCollection GetAllOrderTypes()
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select * from tblOrderType order by priority";
			cmd.CommandType = System.Data.CommandType.Text;

			YellowstonePathology.Business.ClientOrder.Model.OrderTypeCollection result = BuildOrderTypeCollection(cmd);
			return result;
		}

		public YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection GetAllOrderCategories()
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select * from tblOrderCategory order by priority; " +
				"Select * from tblOrderType order by priority";
			cmd.CommandType = System.Data.CommandType.Text;

			YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection result = BuildOrderCategoryCollection(cmd);
			return result;
		}

		public YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection GetOrderCategory(string orderCategoryId)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select * from tblOrderCategory where OrderCategoryId = @OrderCategoryId order by Priority; " +
				"Select * from tblOrderType where OrderCategoryId = @OrderCategoryId order by Priority";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@OrderCategoryId", SqlDbType.VarChar).Value = orderCategoryId;

			YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection result = BuildOrderCategoryCollection(cmd);
			return result;
		}				

		public static YellowstonePathology.Business.ClientOrder.Model.ContainerIdLookupResponse DoesContainerIdExist(string containerId, string clientOrderDetailId)
		{
            YellowstonePathology.Business.ClientOrder.Model.ContainerIdLookupResponse result = new YellowstonePathology.Business.ClientOrder.Model.ContainerIdLookupResponse();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select count(*) from tblClientOrderDetail where ContainerId = @ContainerId and ClientOrderDetailId <> @ClientOrderDetailId";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@ContainerId", SqlDbType.VarChar).Value = containerId;
			cmd.Parameters.Add("@ClientOrderDetailId", SqlDbType.VarChar).Value = clientOrderDetailId;
			using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						int count = (int)dr[0];
						if (count > 0)
						{
							result.Found = true;
						}
					}
				}
			}
			return result;
		}        		

		public static void BuildClientOrderDetailCollection(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection clientOrderDetailCollection, SqlDataReader dr)
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

                    YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail = YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailFactory.GetClientOrderDetail(orderTypeCode, Persistence.PersistenceModeEnum.UpdateChangedProperties);
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientOrderDetail, dr);
                        propertyWriter.WriteProperties();
                        clientOrderDetailCollection.Add(clientOrderDetail);
                    }
                }
            }
            else
            {
                dr.NextResult();
                dr.NextResult();
            }
		}

        public static YellowstonePathology.Business.ClientOrder.Model.ClientOrder BuildClientOrder(SqlDataReader dr)
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

            clientOrder = YellowstonePathology.Business.ClientOrder.Model.ClientOrderFactory.GetClientOrder(panelSetId);
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

        public static void BuildClientOrderCollection(SqlDataReader dr, YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection)
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

        public static YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail BuildClientOrderDetail(SqlCommand cmd)
        {
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail = null; 
            using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
					string orderTypeCode = null;
					while (dr.Read())
					{
						orderTypeCode = dr["OrderTypeCode"].ToString();
					}

                    if (string.IsNullOrEmpty(orderTypeCode) == false)
                    {
                        dr.NextResult();

                        clientOrderDetail = YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailFactory.GetClientOrderDetail(orderTypeCode, Persistence.PersistenceModeEnum.UpdateChangedProperties);
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

		private static YellowstonePathology.Business.ClientOrder.Model.OrderTypeCollection BuildOrderTypeCollection(SqlCommand cmd)
		{
            YellowstonePathology.Business.ClientOrder.Model.OrderTypeCollection orderTypeCollection = new YellowstonePathology.Business.ClientOrder.Model.OrderTypeCollection();
			using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
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

		private static YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection BuildOrderCategoryCollection(SqlCommand cmd)
		{
            YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection orderCategoryCollection = new YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection();
			using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
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
