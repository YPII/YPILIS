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
    public class SlideAccessionGateway
    {
		public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch GetMaterialTrackingBatch()
        {
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch result = null;
            return result;
        }

		public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch GetOpenBatchForFacilityLocation(string facilityId, string locationId)
		{
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch result = null;

			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select * from tblMaterialTrackingBatch where IsOpen = 1 and FromFacilityId = @FacilityId and FromLocationId = @LocationId";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@FacilityId", SqlDbType.VarChar).Value = facilityId;
			cmd.Parameters.Add("@LocationId", SqlDbType.VarChar).Value = locationId;

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}               		                        

        public static YellowstonePathology.Business.Slide.Model.SlideOrderCollection_Base GetSlideOrdersWithPrintRequest()
        {
            YellowstonePathology.Business.Slide.Model.SlideOrderCollection_Base result = new Slide.Model.SlideOrderCollection_Base();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblSlideOrder	where [Status] = 'PrintRequested'";
            cmd.CommandType = System.Data.CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Slide.Model.SlideOrder_Base slideOrder = new Slide.Model.SlideOrder_Base();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(slideOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(slideOrder);
                    }
                }
            }
            return result;
        }

		public static void ValidateSlideOrder(string slideOrderId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = "Update tblSlideOrder set Validated = 1, ValidationDate = GetDate(), Status = 'Validated' where SlideOrderId = @SlideOrderId ";
			cmd.Parameters.Add("@SlideOrderId", System.Data.SqlDbType.VarChar).Value = slideOrderId;
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

		public static void UpdateSlideOrderStatus(string slideOrderId, string status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = "Update tblSlideOrder set Status = @Status where SlideOrderId = @SlideOrderId ";
			cmd.Parameters.Add("@SlideOrderId", System.Data.SqlDbType.VarChar).Value = slideOrderId;
            cmd.Parameters.Add("Status", System.Data.SqlDbType.VarChar, 50).Value = status;
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        /*public static YellowstonePathology.Business.Test.AliquotOrderCollection GetAliquotOrderCollectionByReportNo(string reportNo)
        {
            YellowstonePathology.Business.Test.AliquotOrderCollection result = new Test.AliquotOrderCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "gwGetAliquotOrderCollectionByReportNo";
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.Add("@ReportNo", System.Data.SqlDbType.VarChar).Value = reportNo;

            XElement collectionElement = null;
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (XmlReader xmlReader = cmd.ExecuteXmlReader())
                {
                    if (xmlReader.Read() == true)
                    {
                        collectionElement = XElement.Load(xmlReader);
                    }
                }
            }

            if (collectionElement != null)
            {
                List<XElement> aliquotElements = (from item in collectionElement.Elements("AliquotOrder") select item).ToList<XElement>();
                foreach (XElement aliquotElement in aliquotElements)
                {
                    YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = new Test.AliquotOrder();
                    YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(aliquotElement, aliquotOrder);
                    xmlPropertyWriter.Write();
                    result.Add(aliquotOrder);
                }
            }
			return result;
		}*/                
        
        public static void DeleteSlideOrder(string slideOrderId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Delete tblSlideOrder where SlideOrderId = @SlideOrderId ";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@SlideOrderId", System.Data.SqlDbType.VarChar).Value = slideOrderId;
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }
        
		public static View.AccessionSlideOrderView GetAccessionSlideOrderViewBySlideOrderId(string slideOrderId)
		{
			SqlCommand cmd = new SqlCommand();            

            cmd.CommandText = "Select ao.MasterAccessionNo, ao.PLastName, ao.PFirstName, ao.ClientId, ao.ClientName, ao.PhysicianId, ao.PhysicianName, pso.ReportNo,  " +
	            "(Select slo.* from tblSlideOrder slo  where slo.SlideOrderId = asl.SlideOrderId for xml path('SlideOrder'), type) " +
	            "from tblSlideOrder asl " +		        
		        "join tblTestOrder t on asl.TestOrderId = t.TestOrderId " +
		        "join tblPanelOrder po on t.PanelOrderId = po.PanelOrderId " +
		        "join tblPanelSetOrder pso on po.ReportNo = pso.ReportNo " +
		        "join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo " +
		        "join tblAliquotOrder a on asl.AliquotOrderId = a.AliquotOrderId " +
		        "join tblSpecimenOrder so on a.SpecimenOrderId = so.SpecimenOrderId " +
	            "where asl.SlideOrderId = @slideOrderId " +
		        "for xml path('AccessionSlideOrderView'), type";

			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@SlideOrderId", System.Data.SqlDbType.VarChar).Value = slideOrderId;

            XElement viewElement = null;
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (XmlReader xmlReader = cmd.ExecuteXmlReader())
                {
                    if (xmlReader.Read() == true)
                    {
                        viewElement = XElement.Load(xmlReader);
                    }
                }
            }

            AccessionSlideOrderViewBuilder accessionSlideOrderViewBuilder = new AccessionSlideOrderViewBuilder();
            accessionSlideOrderViewBuilder.Build(viewElement);
            return accessionSlideOrderViewBuilder.AccessionSlideOrderView;
		}

		public static View.AccessionSlideOrderViewCollection GetAccessionSlideOrderViewCollectionByBatchId(string batchId)
		{
			SqlCommand cmd = new SqlCommand();           
			cmd.CommandText = "Select ao.MasterAccessionNo, ao.PLastName, ao.PFirstName, ao.ClientId, ao.ClientName, ao.PhysicianId, ao.PhysicianName, po.ReportNo,  " + 
				"(Select slo.* " +
				"from tblSlideOrder slo where slo.SlideOrderId = astl.MaterialId for xml path('SlideOrder'), type)  " + 
				"from tblAccessionOrder ao   " + 
				"join tblSpecimenOrder so on ao.MasterAccessionNo = so.MasterAccessionNo   " + 
				"join tblAliquotOrder a on so.specimenOrderId = a.SpecimenOrderId   " + 
				"join tblSlideOrder asl on a.AliquotOrderId = asl.AliquotOrderId   " + 
				"join tblMaterialTrackingLog astl on asl.SlideOrderId = astl.MaterialId  " + 				
				"join tblTestOrder t on asl.TestOrderId = t.TestOrderId  " + 
				"join tblPanelOrder po on t.PanelOrderId = po.PanelOrderId  " + 
                "where astl.MaterialTrackingBatchId = @BatchId " + 
                "for xml path('AccessionSlideOrderView'), type, root('AccessionSlideOrderViewCollection')";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@BatchId", System.Data.SqlDbType.VarChar).Value = batchId;

            XElement resultElement = null;
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (XmlReader xmlReader = cmd.ExecuteXmlReader())
                {
                    if (xmlReader.Read() == true)
                    {
                        resultElement = XElement.Load(xmlReader);
                    }
                }
            }
            return BuildAccessionSlideOrderViewCollection(resultElement);
		}

		public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection GetMaterialTrackingLogCollectionByBatchDate(DateTime batchDate)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select * from tblMaterialTrackingLog where LogDate = @LogDate";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@LogDate", System.Data.SqlDbType.DateTime).Value = batchDate;
            return BuildMaterialTrackingLogCollection(cmd);
		}

		public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection GetMaterialTrackingLogCollectionByBatchId(string batchId)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select * from tblMaterialTrackingLog where MaterialTrackingBatchId = @BatchId order by LogDate desc";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@BatchId", System.Data.SqlDbType.VarChar).Value = batchId;			
			return BuildMaterialTrackingLogCollection(cmd);
		}

		public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection GetMaterialTrackingLogCollectionByBatchIdMasterAccessionNo(string batchId, string masterAccessionNo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblMaterialTrackingLog where MasterAccessionNo = @MasterAccessionNo and MaterialTrackingBatchId = @MaterialTrackingBatchId";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@MasterAccessionNo", System.Data.SqlDbType.VarChar).Value = masterAccessionNo;
            cmd.Parameters.Add("@MaterialTrackingBatchId", System.Data.SqlDbType.VarChar).Value = batchId;
            return BuildMaterialTrackingLogCollection(cmd);
        }

		public static Domain.MaterialLocationCollection GetMaterialLocationCollection()
		{
            YellowstonePathology.Business.Domain.MaterialLocationCollection result = new Domain.MaterialLocationCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select * from tblMaterialLocation order by Name";
            cmd.CommandType = System.Data.CommandType.Text;

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Domain.MaterialLocation materialLocation = new YellowstonePathology.Business.Domain.MaterialLocation();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(materialLocation, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(materialLocation);
                    }
                }
            }
            return result;
		}

		public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection GetMaterialTrackingBatchCollection()
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select top 100 * from tblMaterialTrackingBatch order by OpenDate desc";
			cmd.CommandType = System.Data.CommandType.Text;			
			return BuildMaterialTrackingBatchCollection(cmd);
		}

		public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection GetMaterialTrackingBatchCollectionByMasterAccessionNo(string masterAccessionNo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * " +
	            "from tblMaterialTrackingBatch " +
	            "where MaterialTrackingBatchId in (Select MaterialTrackingBatchId from tblMaterialTrackingLog where MasterAccessionNo = @MasterAccessionNo) " +
	            "order by OpenDate desc";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;
            return BuildMaterialTrackingBatchCollection(cmd);
        }

		public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection GetMaterialTrackingBatchCollection(string facilityId, string locationId)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select top 100 * from tblMaterialTrackingBatch where FromFacilityId = @FacilityId and FromLocationId = @LocationId order by OpenDate desc";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@FacilityId", SqlDbType.VarChar).Value = facilityId;
			cmd.Parameters.Add("@LocationId", SqlDbType.VarChar).Value = locationId;
			return BuildMaterialTrackingBatchCollection(cmd);
		}

		public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection GetMaterialTrackingLogCollectionByMaterialId(string materialId)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select * from tblMaterialTrackingLog where MaterialId = @MaterialId";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@MaterialId", System.Data.SqlDbType.VarChar).Value = materialId;
			return BuildMaterialTrackingLogCollection(cmd);
		}

		public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection GetMaterialTrackingLogCollectionByMasterAccessionNo(string masterAccessionNo)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select * from tblMaterialTrackingLog where MasterAccessionNo = @MasterAccessionNo order by LogDate desc";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@MasterAccessionNo", System.Data.SqlDbType.VarChar).Value = masterAccessionNo;
			return BuildMaterialTrackingLogCollection(cmd);
		}

        public static YellowstonePathology.Business.Slide.Model.SlideOrder GetSlideOrder(string slideOrderId)
        {
            YellowstonePathology.Business.Slide.Model.SlideOrder result = null;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblSlideOrder where SlideOrderid = @SlideOrderId";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@SlideOrderId", System.Data.SqlDbType.VarChar).Value = slideOrderId;

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = new YellowstonePathology.Business.Slide.Model.SlideOrder();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
                        propertyWriter.WriteProperties();                        
                    }
                }
            }

            return result;
        }

		public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogView GetMaterialTrackingLogView(string slideOrderId, string materialTrackingBatchId)
        {
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogView result = null;
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblMaterialTrackingLog where MaterialId = @MaterialId and MaterialTrackingBatchId = @MaterialTrackingBatchId";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@MaterialId", System.Data.SqlDbType.VarChar).Value = slideOrderId;
            cmd.Parameters.Add("@MaterialTrackingBatchId", System.Data.SqlDbType.VarChar).Value = materialTrackingBatchId;

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
						result = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogView();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
                        propertyWriter.WriteProperties();
                    }
                }
            }            

            return result;
        }

		public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogViewCollection GetMaterialTrackingLogViewCollectionByBatchId(string materialTrackingBatchId)
        {
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogViewCollection result = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogViewCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from ViewMaterialTrackingLog where MaterialTrackingBatchId = @MaterialTrackingBatchId order by LogDate desc";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@MaterialTrackingBatchId", System.Data.SqlDbType.VarChar).Value = materialTrackingBatchId;

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
						YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogView materialTrackingLogView = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogView();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(materialTrackingLogView, dr);
                        propertyWriter.WriteProperties();
                        result.Add(materialTrackingLogView);
                    }
                }
            }

            return result;
        }

		public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogViewCollection GetMaterialTrackingLogViewCollectionByBatchIdMasterAccessionNo(string materialTrackingBatchId, string masterAccessionNo)
        {
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogViewCollection result = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogViewCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from ViewMaterialTrackingLog where MaterialTrackingBatchId = @MaterialTrackingBatchId and MasterAccessionNo = @MasterAccessionNo";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@MaterialTrackingBatchId", System.Data.SqlDbType.VarChar).Value = materialTrackingBatchId;
            cmd.Parameters.Add("@MasterAccessionNo", System.Data.SqlDbType.VarChar).Value = masterAccessionNo;

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
						YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogView materialTrackingLogView = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogView();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(materialTrackingLogView, dr);
                        propertyWriter.WriteProperties();
                        result.Add(materialTrackingLogView);
                    }
                }
            }

            return result;
        }

		public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog GetMaterialTrackingLog(int materialId, string locationName, DateTime LogDate)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select * from tblMaterialTrackingLog where MaterialId = @MaterialId and LocationName = @LocationName and LogDate = @LogDate";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@MaterialId", System.Data.SqlDbType.Int).Value = materialId;
			cmd.Parameters.Add("@LocationName", System.Data.SqlDbType.VarChar).Value = locationName;
			cmd.Parameters.Add("@LogDate", System.Data.SqlDbType.DateTime).Value = LogDate;
			return BuildMaterialTrackingLog(cmd);
		}

		public static YellowstonePathology.Business.Slide.Model.SlideOrderCollection GetSlideOrdersByReportNo(string reportNo)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = "select distinct s.* " +
                "from tblSlideOrder s " +                
				"join tblTestOrder t on s.TestOrderId = t.TestOrderId " +
				"join tblPanelOrder po on t.PanelOrderId = po.PanelOrderId " +
				"where po.ReportNo = @ReportNo order by Label ";
			cmd.Parameters.Add("@ReportNo", System.Data.SqlDbType.VarChar).Value = reportNo;
			return BuildSlideOrderCollection(cmd);
		}

        public static YellowstonePathology.Business.Slide.Model.SlideOrderCollection GetSlideOrdersByMasterAccessionNo(string masterAccessionNo)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select s.* " +
                "from tblSlideOrder s " +
                "join tblAliquotOrder ao on s.AliquotOrderid = ao.AliquotOrderId " +
                "join tblSpecimenOrder so on ao.SpecimenOrderId = so.SpecimenOrderId " +
                "where MasterAccessionNo = @MasterAccessionNo order by Label ";
            cmd.Parameters.Add("@MasterAccessionNo", System.Data.SqlDbType.VarChar).Value = masterAccessionNo;
			return BuildSlideOrderCollection(cmd);
		}        

		public static YellowstonePathology.Business.Slide.Model.SlideOrderCollection GetSlideOrders(string testOrderId)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = "select distinct s.* from tblSlideOrder where TestOrderId = @TestOrderId order by Label";
			cmd.Parameters.Add("@TestOrderId", System.Data.SqlDbType.VarChar).Value = testOrderId;
			return BuildSlideOrderCollection(cmd);
		}        

		private static View.AccessionSlideOrderViewCollection BuildAccessionSlideOrderViewCollection(XElement sourceElement)
		{
			View.AccessionSlideOrderViewCollection accessionSlideOrderViewCollection = new View.AccessionSlideOrderViewCollection();
			if (sourceElement != null)
			{
				foreach (XElement accessionSlideOrderViewElement in sourceElement.Elements("AccessionSlideOrderView"))
				{
					AccessionSlideOrderViewBuilder builder = new AccessionSlideOrderViewBuilder();
					builder.Build(accessionSlideOrderViewElement);
					if (builder.AccessionSlideOrderView != null)
					{
						accessionSlideOrderViewCollection.Add(builder.AccessionSlideOrderView);
					}
				}
			}
			return accessionSlideOrderViewCollection;
		}       
        

		private static YellowstonePathology.Business.Slide.Model.SlideOrderCollection BuildSlideOrderCollection(SqlCommand cmd)
		{
			YellowstonePathology.Business.Slide.Model.SlideOrderCollection result = new YellowstonePathology.Business.Slide.Model.SlideOrderCollection();
			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = new YellowstonePathology.Business.Slide.Model.SlideOrder();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(slideOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(slideOrder);
					}
				}
			}
			return result;
		}

		private static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection BuildMaterialTrackingLogCollection(SqlCommand cmd)
		{
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection();
			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(materialTrackingLog, dr);
                        propertyWriter.WriteProperties();
						materialTrackingLogCollection.Add(materialTrackingLog);                        
					}
				}
			}
			return materialTrackingLogCollection;
		}

		private static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog BuildMaterialTrackingLog(SqlCommand cmd)
		{
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = null;
			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						materialTrackingLog = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(materialTrackingLog, dr);
                        propertyWriter.WriteProperties();
					}
				}
			}
			return materialTrackingLog;
		}

		private static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection BuildMaterialTrackingBatchCollection(SqlCommand cmd)
        {
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection materialTrackingBatchCollection = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection();
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
						YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(materialTrackingBatch, dr);
                        propertyWriter.WriteProperties();
                        materialTrackingBatchCollection.Add(materialTrackingBatch);
                    }
                }
            }
            return materialTrackingBatchCollection;
        }                

		public static List<string> GetMasterAccessionNosForMaterialBatch(string materialBatchId)
		{
			List<string> result = new List<string>();
			SqlCommand cmd = new SqlCommand();           
			cmd.CommandText = "Select Distinct ao.MasterAccessionNo from tblAccessionOrder ao " + 
				"join tblSpecimenOrder so on ao.MasterAccessionNo = so.MasterAccessionNo " + 
				"join tblAliquotOrder a on so.specimenOrderId = a.SpecimenOrderId " + 
				"join tblSlideOrder asl on a.AliquotOrderId = asl.AliquotOrderId " + 
				"join tblMaterialTrackingLog astl on asl.SlideOrderId = astl.MaterialId " + 				
                "where astl.MaterialTrackingBatchId = @BatchId ";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@BatchId", System.Data.SqlDbType.VarChar).Value = materialBatchId;

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
						result.Add(dr[0].ToString());
                    }
                }
            }
			return result;
		}
	}
}
