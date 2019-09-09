using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
    public class SlideAccessionGateway
    {
        public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch GetMaterialTrackingBatch()
        {
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch result = null;
            return result;
        }

        public static YellowstonePathology.Business.Slide.Model.SlideOrderCollection_Base GetSlideOrdersWithPrintRequest()
        {
            YellowstonePathology.Business.Slide.Model.SlideOrderCollection_Base result = new Slide.Model.SlideOrderCollection_Base();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblSlideOrder	where Status = 'PrintRequested';";
            cmd.CommandType = System.Data.CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "Update tblSlideOrder set Validated = 1, ValidationDate = Now(), Status = 'Validated' where SlideOrderId = @SlideOrderId ";
            cmd.Parameters.AddWithValue("@SlideOrderId", slideOrderId);
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateSlideOrderStatus(string slideOrderId, string status)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "Update tblSlideOrder set tblSlideOrder.Status = @Status where tblSlideOrder.SlideOrderId = @SlideOrderId;";
            cmd.Parameters.AddWithValue("@SlideOrderId", slideOrderId);
            cmd.Parameters.AddWithValue("@Status", status);
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteSlideOrder(string slideOrderId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Delete tblSlideOrder where tblSlideOrder.SlideOrderId = @SlideOrderId ";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@SlideOrderId", slideOrderId);
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static View.AccessionSlideOrderView GetAccessionSlideOrderViewBySlideOrderId(string slideOrderId)
        {
            View.AccessionSlideOrderView result = null;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select ao.MasterAccessionNo, ao.PLastName, ao.PFirstName, ao.ClientId, ao.ClientName, " +
                "ao.PhysicianId, ao.PhysicianName, pso.ReportNo " +
                "from tblSlideOrder asl " +
                "join tblTestOrder t on asl.TestOrderId = t.TestOrderId " +
                "join tblPanelOrder po on t.PanelOrderId = po.PanelOrderId " +
                "join tblPanelSetOrder pso on po.ReportNo = pso.ReportNo " +
                "join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo " +
                "where asl.SlideOrderId = @SlideOrderId; " +
                "Select * from tblSlideOrder where SlideOrderId = @SlideOrderId;";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@SlideOrderId", slideOrderId);
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = new View.AccessionSlideOrderView();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        Slide.Model.SlideOrder slideOrder = new Slide.Model.SlideOrder();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(slideOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.SlideOrder = slideOrder;
                    }
                }
            }
            return result;
        }

        public static View.AccessionSlideOrderViewCollection GetAccessionSlideOrderViewCollectionByBatchId(string batchId)
        {
            View.AccessionSlideOrderViewCollection result = new View.AccessionSlideOrderViewCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText =
                "Select slo.* from tblSlideOrder slo join tblMaterialTrackingLog mtl on slo.SlideOrderId = mtl.MaterialId " +
                "join tblTestOrder t on slo.TestOrderId = t.TestOrderId " +
                "join tblPanelOrder po on t.PanelOrderId = po.PanelOrderId " +
                "where mtl.MaterialTrackingBatchId = @BatchId order by slo.SlideOrderId; " +
                "Select ao.MasterAccessionNo, ao.PLastName, ao.PFirstName, ao.ClientId, ao.ClientName, ao.PhysicianId, " +
                "ao.PhysicianName, po.ReportNo, asl.SlideOrderId " +
                "from tblAccessionOrder ao " +
                "join tblSpecimenOrder so on ao.MasterAccessionNo = so.MasterAccessionNo " +
                "join tblAliquotOrder a on so.specimenOrderId = a.SpecimenOrderId " +
                "join tblSlideOrder asl on a.AliquotOrderId = asl.AliquotOrderId " +
                "join tblMaterialTrackingLog astl on asl.SlideOrderId = astl.MaterialId " +
                "join tblTestOrder t on asl.TestOrderId = t.TestOrderId " +
                "join tblPanelOrder po on t.PanelOrderId = po.PanelOrderId " +
                "where astl.MaterialTrackingBatchId = @BatchId;";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@BatchId", batchId);
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        View.AccessionSlideOrderView accessionSlideOrderView = new View.AccessionSlideOrderView();
                        Slide.Model.SlideOrder slideOrder = new Slide.Model.SlideOrder();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(slideOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        accessionSlideOrderView.SlideOrder = slideOrder;
                        result.Add(accessionSlideOrderView);
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        string slideOrderId = dr["SlideOrderId"].ToString();
                        foreach (View.AccessionSlideOrderView accessionSlideOrderView in result)
                        {
                            if (accessionSlideOrderView.SlideOrder.SlideOrderId == slideOrderId)
                            {
                                YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(accessionSlideOrderView, dr);
                                sqlDataReaderPropertyWriter.WriteProperties();
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection GetMaterialTrackingLogCollectionByBatchId(string batchId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblMaterialTrackingLog where tblMaterialTrackingLog.MaterialTrackingBatchId = @BatchId order by LogDate desc;";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@BatchId", batchId);
            return BuildMaterialTrackingLogCollection(cmd);
        }

        public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection GetMaterialTrackingLogCollectionByBatchIdMasterAccessionNo(string batchId, string masterAccessionNo)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblMaterialTrackingLog where tblMaterialTrackingLog.MasterAccessionNo = @MasterAccessionNo and " +
                "tblMaterialTrackingLog.MaterialTrackingBatchId = @MaterialTrackingBatchId;";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@MasterAccessionNo", masterAccessionNo);
            cmd.Parameters.AddWithValue("@MaterialTrackingBatchId", batchId);
            return BuildMaterialTrackingLogCollection(cmd);
        }

        public static Domain.MaterialLocationCollection GetMaterialLocationCollection()
        {
            YellowstonePathology.Business.Domain.MaterialLocationCollection result = new Domain.MaterialLocationCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblMaterialLocation order by Name;";
            cmd.CommandType = System.Data.CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblMaterialTrackingBatch where OpenDate >= date_add(curdate(), interval -90 day) order by OpenDate desc;";
            cmd.CommandType = System.Data.CommandType.Text;
            return BuildMaterialTrackingBatchCollection(cmd);
        }

        public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection GetMaterialTrackingBatchCollectionByMasterAccessionNo(string masterAccessionNo)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * " +
                "from tblMaterialTrackingBatch " +
                "where MaterialTrackingBatchId in (Select MaterialTrackingBatchId from tblMaterialTrackingLog where " +
                "tblMaterialTrackingLog.MasterAccessionNo = @MasterAccessionNo) " +
                "order by OpenDate desc;";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@MasterAccessionNo", masterAccessionNo);
            return BuildMaterialTrackingBatchCollection(cmd);
        }

        public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection GetMaterialTrackingLogCollectionByMaterialId(string materialId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblMaterialTrackingLog where tblMaterialTrackingLog. MaterialId = @MaterialId;";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@MaterialId", materialId);
            return BuildMaterialTrackingLogCollection(cmd);
        }

        public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection GetMaterialTrackingLogCollectionByMasterAccessionNo(string masterAccessionNo)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblMaterialTrackingLog where tblMaterialTrackingLog.MasterAccessionNo = @MasterAccessionNo order by LogDate desc;";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@MasterAccessionNo", masterAccessionNo);
            return BuildMaterialTrackingLogCollection(cmd);
        }

        public static YellowstonePathology.Business.Slide.Model.SlideOrder GetSlideOrder(string slideOrderId)
        {
            YellowstonePathology.Business.Slide.Model.SlideOrder result = null;

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblSlideOrder where tblSlideOrder.SlideOrderid = @SlideOrderId;";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@SlideOrderId", slideOrderId);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblMaterialTrackingLog where tblMaterialTrackingLog.MaterialId = @MaterialId and " +
                "tblMaterialTrackingLog.MaterialTrackingBatchId = @MaterialTrackingBatchId;";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@MaterialId", slideOrderId);
            cmd.Parameters.AddWithValue("@MaterialTrackingBatchId", materialTrackingBatchId);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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
            string sql = "SELECT " +
                "mtb.MaterialTrackingBatchId AS MaterialTrackingBatchId, " +
                "mtl.MaterialTrackingLogId AS MaterialTrackingLogId, " +
                "ao.MasterAccessionNo AS MasterAccessionNo, " +
                "ao.ClientAccessionNo AS ClientAccessionNo, " +
                "ao.PLastName AS PLastName, " +
                "ao.PFirstName AS PFirstName, " +
                "mtl.MaterialType AS MaterialType, " +
                " mtl.MaterialId AS MaterialId, " +
                "mtl.LogDate AS LogDate, " +
                "(CASE " +
                    "WHEN " +
                    "(`mtl`.`MaterialType` = 'Slide') " +
                    "THEN " +
                        "(CASE " +
                            "WHEN " +
                                "ISNULL(`mtl`.`MaterialLabel`) " +
                                "THEN " +
                                "(SELECT `tblSlideOrder`.`Label` FROM `tblSlideOrder` " +
                                "WHERE (`tblSlideOrder`.`SlideOrderId` = `mtl`.`MaterialId`)) " +
                            "ELSE `mtl`.`MaterialLabel` " +
                        "END) " +
                    "ELSE 'None' " +
                "END) AS `MaterialLabel`, " +
                "mtl.LoggedBy AS `LoggedBy` " +
                "FROM " +
                "tblMaterialTrackingBatch mtb " +
                "JOIN tblMaterialTrackingLog mtl ON mtb.MaterialTrackingBatchId = mtl.MaterialTrackingBatchId " +
                "Left JOIN tblAccessionOrder ao ON mtl.MasterAccessionNo = ao.MasterAccessionNo " +
                "where mtb.MaterialTrackingBatchId = @MaterialTrackingBatchId order by mtl.LogDate desc";

            MySqlCommand cmd = new MySqlCommand();            
            cmd.CommandText = sql;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@MaterialTrackingBatchId", materialTrackingBatchId);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from ViewMaterialTrackingLog where ViewMaterialTrackingLog.MaterialTrackingBatchId = " +
                "@MaterialTrackingBatchId and ViewMaterialTrackingLog.MasterAccessionNo = @MasterAccessionNo;";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@MaterialTrackingBatchId", materialTrackingBatchId);
            cmd.Parameters.AddWithValue("@MasterAccessionNo", masterAccessionNo);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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

        public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog GetMaterialTrackingLog(int materialId, string location, DateTime logDate)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblMaterialTrackingLog where tblMaterialTrackingLog.MaterialId = @ViewMaterialTrackingLogMaterialId and " +
                "tblMaterialTrackingLog.Location = @ViewMaterialTrackingLogLocation and tblMaterialTrackingLog.LogDate = @ViewMaterialTrackingLogLogDate;";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@ViewMaterialTrackingLogMaterialId", materialId);
            cmd.Parameters.AddWithValue("@ViewMaterialTrackingLogLocation", location);
            cmd.Parameters.AddWithValue("@ViewMaterialTrackingLogLogDate", logDate);
            return BuildMaterialTrackingLog(cmd);
        }

        public static YellowstonePathology.Business.Slide.Model.SlideOrderCollection GetSlideOrdersByReportNo(string reportNo)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select distinct s.* " +
                "from tblSlideOrder s " +
                "join tblTestOrder t on s.TestOrderId = t.TestOrderId " +
                "join tblPanelOrder po on t.PanelOrderId = po.PanelOrderId " +
                "where po.ReportNo = @ViewMaterialTrackingLogReportNo order by Label;";
            cmd.Parameters.AddWithValue("@ViewMaterialTrackingLogReportNo", reportNo);
            return BuildSlideOrderCollection(cmd);
        }

        public static YellowstonePathology.Business.Slide.Model.SlideOrderCollection GetSlideOrdersByMasterAccessionNo(string masterAccessionNo)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select s.* " +
                "from tblSlideOrder s " +
                "join tblAliquotOrder ao on s.AliquotOrderid = ao.AliquotOrderId " +
                "join tblSpecimenOrder so on ao.SpecimenOrderId = so.SpecimenOrderId " +
                "where ao.MasterAccessionNo = @MasterAccessionNo order by Label;";
            cmd.Parameters.AddWithValue("@MasterAccessionNo", masterAccessionNo);
            return BuildSlideOrderCollection(cmd);
        }

        public static YellowstonePathology.Business.Slide.Model.SlideOrderCollection GetSlideOrders(string testOrderId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select distinct s.* from tblSlideOrder s where s.TestOrderId = @TestOrderId order by Label;";
            cmd.Parameters.AddWithValue("@TestOrderId", testOrderId);
            return BuildSlideOrderCollection(cmd);
        }

        private static YellowstonePathology.Business.Slide.Model.SlideOrderCollection BuildSlideOrderCollection(MySqlCommand cmd)
        {
            YellowstonePathology.Business.Slide.Model.SlideOrderCollection result = new YellowstonePathology.Business.Slide.Model.SlideOrderCollection();
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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

        private static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection BuildMaterialTrackingLogCollection(MySqlCommand cmd)
        {
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection();
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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

        private static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog BuildMaterialTrackingLog(MySqlCommand cmd)
        {
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = null;
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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

        private static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection BuildMaterialTrackingBatchCollection(MySqlCommand cmd)
        {
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection materialTrackingBatchCollection = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection();
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select Distinct ao.MasterAccessionNo from tblAccessionOrder ao " +
                "join tblSpecimenOrder so on ao.MasterAccessionNo = so.MasterAccessionNo " +
                "join tblAliquotOrder a on so.SpecimenOrderId = a.SpecimenOrderId " +
                "join tblSlideOrder asl on a.AliquotOrderId = asl.AliquotOrderId " +
                "join tblMaterialTrackingLog astl on asl.SlideOrderId = astl.MaterialId " +
                "where astl.MaterialTrackingBatchId = @BatchId;";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@BatchId", materialBatchId);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(dr[0].ToString());
                    }
                }
            }
            return result;
        }

        public static MaterialTracking.Model.BlockSentNotReturnedCollection GetBlocksSentNotReturned()
        {
            MaterialTracking.Model.BlockSentNotReturnedCollection result = new MaterialTracking.Model.BlockSentNotReturnedCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "pBlocksSentNotReturned";
            cmd.CommandType = CommandType.StoredProcedure;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        MaterialTracking.Model.BlockSentNotReturned blockSentNotReturned = new MaterialTracking.Model.BlockSentNotReturned();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(blockSentNotReturned, dr);
                        propertyWriter.WriteProperties();
                        result.Add(blockSentNotReturned);
                    }
                }
            }
            return result;
        }
    }
}
