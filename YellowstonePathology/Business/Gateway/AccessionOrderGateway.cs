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

namespace YellowstonePathology.Business.Gateway
{
	public class AccessionOrderGateway
	{
        public static YellowstonePathology.UI.EmbeddingBreastCaseList GetEmbeddingBreastCasesCollection()
        {
            YellowstonePathology.UI.EmbeddingBreastCaseList result = new UI.EmbeddingBreastCaseList();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select ao.MasterAccessionNo, ao.PFirstName, ao.PLastName, so.Collectiontime, so.ProcessorRun, so.FixationStartTime, so.FixationEndTime, datediff(hh, fixationstarttime, fixationendtime) [FixationDuration] " + 
                "from tblAccessionOrder ao " +
                "join tblspecimenOrder so on ao.masterAccessionNo = so.MasterAccessionNo " +
                "where charindex('Breast', so.Description) > 0 " +
                "and ao.AccessionDate >= dateadd(d, -30, getdate()) " +
                "order by ao.AccessionTime desc";
            cmd.CommandType = CommandType.Text;            

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.UI.EmbeddingBreastCaseListItem item = new UI.EmbeddingBreastCaseListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(item, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(item);
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.UI.EmbeddingNotScannedList GetEmbeddingNotScannedCollection(DateTime accessionDate)
        {
            YellowstonePathology.UI.EmbeddingNotScannedList result = new UI.EmbeddingNotScannedList();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select a.AliquotOrderId, pso.PanelSetName, so.Description " +
                "from tblAccessionOrder ao " +
                "join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblSpecimenOrder so on ao.MasterAccessionno = so.MasterAccessionNo " +
                "join tblAliquotOrder a on so.SpecimenOrderId = a.SpecimenOrderId " +
                "where ao.AccessionDate = @AccessionDate and aliquotType = 'Block' and embeddingVerified = 0" +
                "and so.RequiresGrossExamination = 1 and so.ProcessorRunId <> 'HOLD'";

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@AccessionDate", SqlDbType.DateTime).Value = accessionDate;            

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.UI.EmbeddingNotScannedListItem item = new UI.EmbeddingNotScannedListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(item, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(item);
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.Business.Test.AliquotOrderCollection GetEmbeddingCollection(DateTime embeddingVerifiedDate)
        {
            YellowstonePathology.Business.Test.AliquotOrderCollection result = new Test.AliquotOrderCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select a.*, null as [TestOrderId] " +
                "from tblAccessionOrder ao " +
                "join tblSpecimenOrder s on ao.MasterAccessionNo = s.masterAccessionNo " +
                "join tblAliquotOrder a on s.SpecimenOrderId = a.SpecimenOrderId " +
                "where a.EmbeddingVerifiedDate between @EmbeddingVerifiedDate and @EmbeddingVerifiedDatePlus1 " +
                "order by a.EmbeddingVerifiedDate desc";

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@EmbeddingVerifiedDate", SqlDbType.DateTime).Value = embeddingVerifiedDate;
            cmd.Parameters.Add("@EmbeddingVerifiedDatePlus1", SqlDbType.DateTime).Value = embeddingVerifiedDate.AddDays(1);

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = new Test.AliquotOrder();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(aliquotOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(aliquotOrder);
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection GetSpecimenOrderHoldCollection()
        {
            YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection result = new Specimen.Model.SpecimenOrderCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblSpecimenOrder where ProcessorRunId = 'HOLD'";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = new Specimen.Model.SpecimenOrder();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(specimenOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(specimenOrder);
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection GetSpecimenOrderCollection(DateTime embeddingVerifiedDate)
        {
            YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection result = new Specimen.Model.SpecimenOrderCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select s.* from tblSpecimenOrder s " +
                "where exists(select * from tblAliquotOrder where specimenOrderId = s.SpecimenOrderId and EmbeddingVerifiedDate between @EmbeddingVerifiedDate and @EmbeddingVerifiedDatePlus1)";
            cmd.Parameters.Add("@EmbeddingVerifiedDate", SqlDbType.VarChar).Value = embeddingVerifiedDate;
            cmd.Parameters.Add("@EmbeddingVerifiedDatePlus1", SqlDbType.VarChar).Value = embeddingVerifiedDate.AddDays(1);
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = new Specimen.Model.SpecimenOrder();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(specimenOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(specimenOrder);
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection GetSpecimenOrderCollection(string containId)
        {
            YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection result = new Specimen.Model.SpecimenOrderCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblSpecimenOrder where ContainId = @ContainerId";

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ContainerId", SqlDbType.DateTime).Value = containId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = new Specimen.Model.SpecimenOrder();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(specimenOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(specimenOrder);
                    }
                }
            }
            return result;
        }
        
        public static YellowstonePathology.Business.Typing.TypingShortcutCollection GetTypingShortcutCollectionByUser(int userId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * From tblTypingShortcut where UserId = @UserId or Type = 'Global' order by Shortcut";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

            YellowstonePathology.Business.Typing.TypingShortcutCollection typingShorcutCollection = new Typing.TypingShortcutCollection();
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Typing.TypingShortcut typingShortcut = new Typing.TypingShortcut();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(typingShortcut, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();                        
                        typingShorcutCollection.Add(typingShortcut);
                    }
                }
            }
            return typingShorcutCollection;
        }

        public static string GetMasterAccessionNoFromReportNo(string reportNo)
        {
            string result = null;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select MasterAccessionNo from tblPanelSetOrder where ReportNo = @ReportNo";
            cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                result = (string)cmd.ExecuteScalar();
            }
            return result;
        }

        public static string GetMasterAccessionNoFromContainerId(string containerId)
        {
            string result = null;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select MasterAccessionNo from tblSpecimenOrder where ContainerId = @ContainerId";
            cmd.Parameters.Add("@containerId", SqlDbType.VarChar).Value = containerId;
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                result = (string)cmd.ExecuteScalar();
            }
            return result;
        }

        public static string GetMasterAccessionNoFromAliquotOrderId(string aliquotOrderId)
        {
            string result = null;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select MasterAccessionNo " +
                "from tblSpecimenOrder so " +
                "join tblAliquotOrder ao on so.SpecimenORderId = ao.SpecimenOrderId " +
                "where ao.AliquotOrderId = @AliquotOrderId";

            cmd.Parameters.Add("@AliquotOrderId", SqlDbType.VarChar).Value = aliquotOrderId;
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                result = (string)cmd.ExecuteScalar();
            }

            return result;
        }

        public static void SetPanelSetOrderAsCancelledTest(string reportNo)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Update tblPanelSetOrder set PanelSetid = 66, panelSetName = 'Test Cancelled', CaseType = 'Test Cancelled' where Reportno = @ReportNo";
			cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
			cmd.CommandType = CommandType.Text;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				cmd.ExecuteNonQuery();
			}
		}

		public static void InsertTestCancelledTestOrder(string reportNo, int cancelledTestId, string cancelledTestName)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Insert tblTestCancelledTestOrder (ReportNo, CancelledTestId, CancelledTestname) values (@ReportNo, @CancelledTestId, @CancelledTestName)";
			cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
			cmd.Parameters.Add("@CancelledTestId", SqlDbType.Int).Value = cancelledTestId;
			cmd.Parameters.Add("@CancelledTestName", SqlDbType.VarChar).Value = cancelledTestName;
			cmd.CommandType = CommandType.Text;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				cmd.ExecuteNonQuery();
			}
		}

        public static YellowstonePathology.Business.Test.PanelSetOrderView GetCaseToSchedule(string reportNo)
        {
#if MONGO
            return AccessionOrderGatewayMongo.GetCaseToSchedule(reportNo);
#else
			YellowstonePathology.Business.Test.PanelSetOrderView result = null;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblPanelSetOrder where ReportNo = @ReportNo";
            cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = new Test.PanelSetOrderView();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        
                    }
                }
            }
            return result;
#endif
        }

        public static List<YellowstonePathology.Business.Test.PanelSetOrderView> GetCasesToSchedule()
        {
#if MONGO
            return AccessionOrderGatewayMongo.GetCasesToSchedule();
#else
			List<YellowstonePathology.Business.Test.PanelSetOrderView> result = new List<Test.PanelSetOrderView>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblPanelSetOrder pso where pso.Final = 1 and pso.ScheduledPublishTime is null and pso.Published = 0";
            cmd.CommandType = CommandType.Text;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Test.PanelSetOrderView panelSetOrderView = new Test.PanelSetOrderView();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(panelSetOrderView, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(panelSetOrderView);
					}
				}
			}
			return result;
#endif
		}

		public static List<YellowstonePathology.Business.Test.PanelSetOrderView> GetNextCasesToPublish()
		{
#if MONGO
            return AccessionOrderGatewayMongo.GetNextCasesToPublish();
#else
			List<YellowstonePathology.Business.Test.PanelSetOrderView> result = new List<Test.PanelSetOrderView>();
			SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblPanelSetOrder pso where pso.Final = 1 and pso.finalTime < dateAdd(mi, -15, getdate()) and pso.ScheduledPublishTime <= getdate() union " +
                "Select pso.* from tblPanelSetOrder pso join tblTestOrderReportDistribution trd on pso.ReportNo = trd.ReportNo " +
                "where pso.Final = 1 and pso.finalTime < dateAdd(mi, -15, getdate()) and trd.ScheduledDistributionTime <= getdate() and pso.Distribute = 1";
			cmd.CommandType = CommandType.Text;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Test.PanelSetOrderView panelSetOrderView = new Test.PanelSetOrderView();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(panelSetOrderView, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(panelSetOrderView);
					}
				}
			}
			return result;
#endif
		}

        public static List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> GetNextTORD()
        {
            List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> result = new List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select top 10 * from tblTestOrderReportDistribution";            
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(testOrderReportDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(testOrderReportDistribution);
                    }
                }
            }
            return result;
        }

        public static List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> GetScheduledDistribution(string reportNo)
        {
            List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> result = new List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblTestOrderReportDistribution where ScheduledDistributionTime is not null and ReportNo = @ReportNo";
            cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(testOrderReportDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(testOrderReportDistribution);
                    }
                }
            }
            return result;
        }

        public static List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> GetUnscheduledDistribution()
        {
            List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> result = new List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblTestOrderReportDistribution tor	join tblPanelSetOrder pso on tor.ReportNo = pso.ReportNo where tor.[Distributed] = 0 and tor.ScheduledDistributionTime is null and pso.Final = 1 and pso.Distribute = 1 and pso.HoldDistribution = 0"; 
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(testOrderReportDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(testOrderReportDistribution);
                    }
                }
            }
            return result;
        }

        public static List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> GetUnscheduledAmendments()
        {
            List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> result = new List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select trd.* " +
                "from tblAmendment a " +
		        "join tblTestOrderReportDistribution trd on a.ReportNo = trd.ReportNo " +
		        "where trd.TimeOfLastDistribution < a.finalTime and trd.ScheduledDistributionTime is null";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(testOrderReportDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(testOrderReportDistribution);
                    }
                }
            }
            return result;
        }      

        public static List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> GetUnscheduledDistribution(string reportNo)
        {
            List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> result = new List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblTestOrderReportDistribution where [Distributed] = 0 and ScheduledDistributionTime is null and ReportNo = @ReportNo";
            cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(testOrderReportDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(testOrderReportDistribution);
                    }
                }
            }
            return result;
        }              

		public static YellowstonePathology.Business.Test.AccessionOrderView GetAccessionOrderView(string masterAccessionNo)
		{
#if MONGO
            return AccessionOrderGatewayMongo.GetAccessionOrderView(masterAccessionNo);
#else
			YellowstonePathology.Business.Test.AccessionOrderView result = null;
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select * from tblAccessionOrder where MasterAccessionNo = @MasterAccessionNo";
			cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;
			cmd.CommandType = CommandType.Text;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new Test.AccessionOrderView();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
#endif
		}

        public static List<YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder> GetSurgicalTestOrder()
        {
			List<YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder> result = new List<YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder>();

            SqlCommand cmd = new SqlCommand();            
            cmd.CommandText = "Select * " +
	            "from tblPanelSetOrder pso " +
		        "join tblSurgicalTestOrder sto on pso.ReportNo = sto.ReportNo " +
		        "where pso.OrderDate >= '1/1/2015' and cancersummary is not null";            
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder sto = new YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(sto, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(sto);
                    }
                }
            }
            return result;
        }

        public static List<YellowstonePathology.Business.Test.PanelSetOrderView> GetUnsetDistributions()
        {
            List<YellowstonePathology.Business.Test.PanelSetOrderView> result = new List<Test.PanelSetOrderView>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblPanelSetOrder pso where final = 1 and distribute = 1 and not exists (Select * from tblTestOrderReportDistribution where reportNo = pso.ReportNo)";            
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PanelSetOrderView panelSetOrderView = new Test.PanelSetOrderView();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(panelSetOrderView, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(panelSetOrderView);
                    }
                }
            }
            return result;
        }        

		public static XElement GetAccessionOrderDocumentByReportNo(string reportNo)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "gwGetAccessionByReportNo_A8";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
			XElement document = AccessionOrderGateway.GetAccessionOrderElement(cmd);
			return document;
		}

		private static XElement GetAccessionOrderElement(SqlCommand cmd)
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

        public static YellowstonePathology.Business.Patient.Model.SVHBillingDataCollection GetSVHBillingDataCollection(string mrn)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select * from tblSVHBillingData where MRN = @MRN";
			cmd.Parameters.Add("@MRN", SqlDbType.VarChar).Value = mrn;
			cmd.CommandType = CommandType.Text;

            YellowstonePathology.Business.Patient.Model.SVHBillingDataCollection result = new YellowstonePathology.Business.Patient.Model.SVHBillingDataCollection();
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
                        YellowstonePathology.Business.Patient.Model.SVHBillingData svhBillingData = new YellowstonePathology.Business.Patient.Model.SVHBillingData();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(svhBillingData, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(svhBillingData);
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Facility.Model.HostCollection GetHostCollection()
		{
#if MONGO
			return AccessionOrderGatewayMongo.GetHostCollection();
#else
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select * from tblHost";
			cmd.CommandType = CommandType.Text;

			YellowstonePathology.Business.Facility.Model.HostCollection hostCollection = new YellowstonePathology.Business.Facility.Model.HostCollection();
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Facility.Model.Host host = new YellowstonePathology.Business.Facility.Model.Host();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(host, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						hostCollection.Add(host);
					}
				}
			}
			return hostCollection;
#endif
		}		

		public static string GetNextMasterAccessionNo()
		{
			string result = null;

			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "gwGetNextMasterAccessionNo";
			cmd.CommandType = CommandType.StoredProcedure;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = dr["MasterAccessionNo"].ToString();
					}
				}
			}

			return result;
		}

		public static string NextReportNo(int panelSetId, string masterAccessionNo)
		{
			string newReportNo = string.Empty;
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "pGetNextReportNo";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;
			cmd.Parameters.Add("@PanelSetId", SqlDbType.Int).Value = panelSetId;
			SqlParameter newReportParameter = new SqlParameter("@NewReportNo", SqlDbType.VarChar, 12, ParameterDirection.Output, false, 12, 12, "@NewReportNo", DataRowVersion.Current, newReportNo);
			cmd.Parameters.Add(newReportParameter);
			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				SqlDataReader dr = cmd.ExecuteReader();
				string result = cmd.Parameters["@NewReportNo"].Value.ToString();
				return result;
			}
		}

		public static SpecialStain.StainResultOptionList GetStainResultOptionListByStainResultId(string stainResultId)
		{
			SpecialStain.StainResultOptionList result = new SpecialStain.StainResultOptionList();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = " select sro.StainResult, sro.StainResultOptionId from tblStainResult sr join tblTestOrder tt on sr.TestOrderId = tt.TestOrderId " +
				"join tblTest t on tt.TestId = t.TestId " +
				"join tblStainResultOptionGroup srog on t.StainResultGroupId = srog.StainResultGroupId " +
				"join tblStainResultOption sro on sro.StainResultOptionId = srog.StainResultOptionId " +
				"where sr.StainResultId = @StainResultId";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@StainResultId", SqlDbType.VarChar).Value = stainResultId;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						SpecialStain.StainResultOption stainResultOption = new SpecialStain.StainResultOption();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(stainResultOption, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(stainResultOption);
					}
				}
			}

			return result;
		}

        public static Surgical.PathologistHistoryList GetPathologistPatientHistory(string patientId)
        {
            Surgical.PathologistHistoryList result = new Surgical.PathologistHistoryList();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "prcGetPatientHistory_5";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PatientId", SqlDbType.VarChar).Value = patientId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Surgical.PathologistHistoryItem pathologistHistoryItem = new Surgical.PathologistHistoryItem();
                        Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pathologistHistoryItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pathologistHistoryItem);
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        Surgical.PathologistHistoryItemListItem pathologistHistoryItemListItem = new Surgical.PathologistHistoryItemListItem();
                        Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pathologistHistoryItemListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        foreach(Surgical.PathologistHistoryItem pathologistHistoryItem in result)
                        {
                            if(pathologistHistoryItemListItem.ReportNo == pathologistHistoryItem.ReportNo)
                            {
                                pathologistHistoryItem.PathologistHistoryItemList.Add(pathologistHistoryItemListItem);
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }

		public static Surgical.SurgicalBillingItemCollection GetSurgicalBillingItemCollectionByDate(DateTime date)
		{
			YellowstonePathology.Business.Surgical.SurgicalBillingItemCollection result = new Surgical.SurgicalBillingItemCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM tblSurgicalBilling where AccessionDate = @Date";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = date;
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Surgical.SurgicalBillingItem surgicalBilling = new Business.Surgical.SurgicalBillingItem();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(surgicalBilling, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(surgicalBilling);
					}
				}
			}

			return result;
		}

		public static Surgical.SurgicalOrderList GetSurgicalOrderListByAccessionDate(DateTime accessionDate)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT pso.ReportNo, a.AccessionDate, a.PFirstName + ' ' + a.PLastName AS PatientName, pso.AcceptedDate, " +
				"pso.FinalDate, pso.OriginatingLocation, su.DisplayName AS Pathologist, su.UserId AS PathologistId, pso.Audited " +
				"FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
				"JOIN tblSystemUser su on pso.AssignedToId = su.UserId " +
				"WHERE a.AccessionDate  = @AccessionDate and pso.PanelSetId = 13 " +
				"ORDER BY a.AccessionTime"; // for xml path('SurgicalOrderListItem'), root('SurgicalOrderList')";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@AccessionDate", SqlDbType.VarChar).Value = accessionDate.ToShortDateString();

			Surgical.SurgicalOrderList result = AccessionOrderGateway.BuildSurgicalOrderList(cmd);
			return result;
		}

		public static Surgical.SurgicalOrderList GetSurgicalOrderListByAccessionDatePQRI(DateTime finalDate)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT pso.ReportNo, a.AccessionDate, a.PFirstName + ' ' + a.PLastName AS PatientName, pso.AcceptedDate, " +
				"pso.FinalDate, pso.OriginatingLocation, su.DisplayName AS Pathologist, su.UserId AS PathologistId, pso.Audited " +
				"FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
				"JOIN tblSurgicalTestOrder sr ON pso.ReportNo = sr.ReportNo " +
				"JOIN tblSystemUser su on pso.AssignedToId = su.UserId " +
				"WHERE pso.FinalDate  = @FinalDate " +
				"and a.MasterAccessionNo in (Select MasterAccessionNo from tblSpecimenOrder where MasterAccessionNo = a.MasterAccessionNo " +
				"and " +
				"charindex('Prostate', Description) > 0 or " +
				"charindex('Breast', Description) > 0 or " +
				"(charindex('Colon', Description) > 0 and charindex('Rectum', Description) > 0) or " +
				"charindex('Esophagus', Description) > 0) " +
				"ORDER BY pso.OrderTime";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@FinalDate", SqlDbType.VarChar).Value = finalDate.ToShortDateString();

			Surgical.SurgicalOrderList result = AccessionOrderGateway.BuildSurgicalOrderList(cmd);
			return result;
		}

		public static Surgical.SurgicalOrderList GetSurgicalOrderListByFinalDate(DateTime finalDate)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT pso.ReportNo, a.AccessionDate, a.PFirstName + ' ' + a.PLastName AS PatientName, pso.AcceptedDate, " +
				"pso.FinalDate, pso.OriginatingLocation, su.DisplayName AS Pathologist, su.UserId AS PathologistId, pso.Audited " +
				"FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
				"JOIN tblSystemUser su on pso.AssignedToId = su.UserId " +
				"WHERE pso.FinalDate  = @FinalDate and pso.PanelSetId = 13 " +
				"ORDER BY pso.OrderTime";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@FinalDate", SqlDbType.VarChar).Value = finalDate.ToShortDateString();

			Surgical.SurgicalOrderList result = AccessionOrderGateway.BuildSurgicalOrderList(cmd);
			return result;
		}

		public static Surgical.SurgicalOrderList GetSurgicalOrderListByNotAudited()
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT pso.ReportNo, a.AccessionDate, a.PFirstName + ' ' + a.PLastName AS PatientName, pso.AcceptedDate, " +
				"pso.FinalDate, pso.OriginatingLocation, su.DisplayName AS Pathologist, su.UserId AS PathologistId, pso.Audited " +
				"FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
				"JOIN tblSystemUser su on pso.AssignedToId = su.UserId " +
				"WHERE pso.Final = 1 AND pso.Audited = 0 and pso.PanelSetId = 13 " +
				"ORDER BY pso.OrderTime";
			cmd.CommandType = CommandType.Text;

			Surgical.SurgicalOrderList result = AccessionOrderGateway.BuildSurgicalOrderList(cmd);
			return result;
		}

		public static Surgical.SurgicalOrderList GetSurgicalOrderListByIntraoperative()
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT DISTINCT pso.ReportNo, a.AccessionDate, a.PFirstName + ' ' + a.PLastName AS PatientName, pso.AcceptedDate, " +
				"pso.FinalDate, pso.OriginatingLocation, su.DisplayName AS Pathologist, su.UserId AS PathologistId, pso.Audited " +
				"FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
				"JOIN tblSurgicalSpecimenResult ssr ON pso.ReportNo = ssr.ReportNo" +
				"JOIN tblSystemUser su on pso.AssignedToId = su.UserId WHERE ssr.ImmediatePerformedBy is not null AND ssr.ImmediatePerformedBy <> ' ' " +
				"AND ssr.SurgicalSpecimenId not in (SELECT SurgicalSpecimenId FROM tblIntraoperativeConsultationResult) " +
				"ORDER BY a.AccessionTime";
			cmd.CommandType = CommandType.Text;

			Surgical.SurgicalOrderList result = AccessionOrderGateway.BuildSurgicalOrderList(cmd);
			return result;
		}

		public static Surgical.SurgicalOrderList GetSurgicalOrderListByNoSignature()
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT pso.ReportNo, a.AccessionDate, a.PFirstName + ' ' + a.PLastName AS PatientName, pso.AcceptedDate, " +
				"pso.FinalDate, pso.OriginatingLocation, su.DisplayName AS Pathologist, su.UserId AS PathologistId, pso.Audited " +
				"FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
				"JOIN tblSystemUser su on pso.AssignedToId = su.UserId " +
				"WHERE pso.Final = 1 AND pso.SignatureAudit = 1 and pso.PanelSetId = 13 " +
				"ORDER BY pso.OrderTime";
			cmd.CommandType = CommandType.Text;

			Surgical.SurgicalOrderList result = AccessionOrderGateway.BuildSurgicalOrderList(cmd);
			return result;
		}

        public static Surgical.SurgicalOrderList GetSurgicalOrderListByNoGross()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from " +
                "(SELECT pso.ReportNo, a.AccessionDate, a.PFirstName + ' ' + a.PLastName AS PatientName, pso.AcceptedDate, " +
                "pso.FinalDate, pso.OriginatingLocation, su.DisplayName AS Pathologist, su.UserId AS PathologistId, pso.Audited, a.AccessionTime " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "JOIN tblSurgicalTestOrder sto ON pso.ReportNo = sto.ReportNo " +
                "JOIN tblSystemUser su on pso.AssignedToId = su.UserId " +
                "WHERE charindex('???', convert(varchar(max), sto.Grossx)) > 0 and a.AccessionDate >= dateadd(dd, -10, getdate())) T1 " +
                "ORDER BY AccessionTime";
            cmd.CommandType = CommandType.Text;

			Surgical.SurgicalOrderList result = AccessionOrderGateway.BuildSurgicalOrderList(cmd);
			return result;
        }

        public static Surgical.SurgicalOrderList GetSurgicalOrderListByNoClinicalInfo()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from " +
                "(SELECT pso.ReportNo, a.AccessionDate, a.PFirstName + ' ' + a.PLastName AS PatientName, pso.AcceptedDate, " +
                "pso.FinalDate, pso.OriginatingLocation, su.DisplayName AS Pathologist, su.UserId AS PathologistId, pso.Audited, a.AccessionTime " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "JOIN tblSurgicalTestOrder sto ON pso.ReportNo = sto.ReportNo " +
                "JOIN tblSystemUser su on pso.AssignedToId = su.UserId " +
                "WHERE convert(varchar(max), a.ClinicalHistory) = '???' and a.AccessionDate >= dateadd(dd, -10, getdate())) T1 " +
                "ORDER BY AccessionTime";
            cmd.CommandType = CommandType.Text;

			Surgical.SurgicalOrderList result = AccessionOrderGateway.BuildSurgicalOrderList(cmd);
			return result;
        }

		public static Surgical.SurgicalOrderList GetSurgicalOrderListForSvhClientOrder(DateTime date)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT pso.ReportNo, a.AccessionDate, a.PFirstName + ' ' + a.PLastName AS PatientName, pso.AcceptedDate, " +
				"pso.FinalDate, pso.OriginatingLocation, su.DisplayName AS Pathologist, su.UserId AS PathologistId, pso.Audited " +
				"FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
				"Left outer JOIN tblSystemUser su on pso.AssignedToId = su.UserId " +
				"WHERE a.AccessionDate = @AccessionDate and a.ClientId in (558,33,57,123,126,230,242,250,253,313,505,558,622,744,758,759,760,820,845,873,969,979,1025,1058,1124,1151,1306,1313,1321) " +
				"and pso.PanelSetId = 13 ORDER BY pso.OrderTime";

			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@AccessionDate", SqlDbType.DateTime).Value = date;

			Surgical.SurgicalOrderList result = AccessionOrderGateway.BuildSurgicalOrderList(cmd);
			return result;
		}

		private static Surgical.SurgicalOrderList BuildSurgicalOrderList(SqlCommand cmd)
		{
			Surgical.SurgicalOrderList result = new Surgical.SurgicalOrderList();
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						Surgical.SurgicalOrderListItem surgicalOrderListItem = new Surgical.SurgicalOrderListItem();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(surgicalOrderListItem, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(surgicalOrderListItem);
					}
				}
			}

			return result;
		}

		// This view needs work
		public static Surgical.SurgicalRescreenItemCollection GetSurgicalRescreenItemCollectionByDate(DateTime date)
		{
			Surgical.SurgicalRescreenItemCollection result = new Surgical.SurgicalRescreenItemCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select Distinct SpecimenOrderId, AccessionDate, AccessionNo, Description, PLastName, isnull(RescreenStatus, '') RescreenStatus from ViewAccessionOrderSurgicalResult_1 where AccessionDate = @Date " +
				"and (charindex('Breast', Description) > 0 or charindex('Prostate', Description) > 0 or charindex('Pituitary', Description) > 0 or charindex('Brain', Description) > 0 or charindex('Thyroid', Description) > 0 " +
				"or charindex('Lung', Description) > 0)";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = date;
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						Surgical.SurgicalRescreenItem surgicalRescreenItem = new Surgical.SurgicalRescreenItem();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(surgicalRescreenItem, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(surgicalRescreenItem);
					}
				}
			}

			return result;
		}

		public static Surgical.SurgicalMasterLogList GetSurgicalMasterLogList(DateTime reportDate)
		{
			Surgical.SurgicalMasterLogList result = new Surgical.SurgicalMasterLogList();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "declare @rpts table " +
                "( " +
                "AccessionTime datetime, " +
                "ReportNo varchar(20), " +
                "AccessioningFacilityId varchar(100), " +
                "PFirstName varchar(100), " +
                "PLastName varchar(100), " +
                "PBirthdate datetime, " +
                "PhysicianName varchar(100), " +
                "ClientName varchar(100), " +
                "AliquotCount int " +
                ") " +
                "insert @rpts " +
                "SELECT Distinct a.AccessionTime, pso.ReportNo, a.AccessioningFacilityId, a.PFirstName, a.PLastName, " +
                "a.PBirthdate, a.PhysicianName, a.ClientName, Count(*) AliquotCount " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "JOIN tblSpecimenORder so on a.MasterAccessionNo = so.MasterAccessionNo " +
                "LEFT OUTER JOIN tblAliquotOrder ao on so.SpecimenOrderId = ao.SpecimenOrderId " +
                "WHERE AccessionDate = @ReportDate and pso.PanelSetId in (13, 50)  " +
                " group by a.AccessionTime, pso.ReportNo, a.AccessioningFacilityId, a.PFirstName, a.PLastName, " +
                "a.PBirthdate, a.PhysicianName, a.ClientName " +
                "Order By AccessionTime " +
                "SELECT rpts.* From @rpts rpts Order By AccessionTime " +
                "Select ssr.DiagnosisId, so.Description, ssr.ReportNo " +
                "FROM tblSurgicalSpecimen ssr " +
                "JOIN tblSpecimenOrder so ON ssr.SpecimenOrderId = so.SpecimenOrderId " +
                "join @rpts rpts on ssr.ReportNo = rpts.ReportNo order by 1";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ReportDate", SqlDbType.VarChar, 20).Value = reportDate.ToShortDateString();
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Surgical.SurgicalMasterLogItem surgicalMasterLogItem = new Surgical.SurgicalMasterLogItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(surgicalMasterLogItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(surgicalMasterLogItem);
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Surgical.MasterLogItem masterLogItem = new Surgical.MasterLogItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(masterLogItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();

                        foreach(Surgical.SurgicalMasterLogItem surgicalMasterLogItem in result)
                        {
                            if(masterLogItem.ReportNo == surgicalMasterLogItem.ReportNo)
                            {
                                surgicalMasterLogItem.MasterLogList.Add(masterLogItem);
                                break;
                            }
                        }
                    }
                }
            }
            return result;
		}

		public static YellowstonePathology.Business.Domain.OrderLogCollection GetOrderLogCollectionByReportDate(DateTime reportDate)
		{
			YellowstonePathology.Business.Domain.OrderLogCollection results = new Domain.OrderLogCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT pso.ReportNo, su.Initials, TestName, CASE ao.LabelPrefix WHEN 'FS' then 'FS' + ao.Label + ' - ' + isnull(so.Description, '') " +
			  "WHEN 'CB' then 'CB' + ao.Label + ' - ' + isnull(so.Description, '') ELSE isnull(ao.Label,'') + ' - ' + isnull(so.Description, '') END as [Description], po.OrderTime, " +
			  "isnull(sr.ProcedureComment, '') as ProcedureComment FROM tblPanelOrder po Left Outer JOIN tblSystemUser su ON po.OrderedById = su.userID " +
			  "JOIN tblPanelSetOrder pso ON po.ReportNo = pso.ReportNo JOIN tblTestOrder ot on ot.PanelOrderId = po.PanelOrderId " +
			  "JOIN tblAliquotOrder ao ON ot.AliquotOrderId = ao.AliquotOrderId " +
			  "JOIN tblSpecimenOrder so ON ao.SpecimenOrderId = so.SpecimenOrderId LEFT OUTER JOIN tblStainResult sr ON  sr.TestOrderId = ot.TestOrderId " +
			  "WHERE po.OrderDate = @OrderDate AND po.PanelId in (19, 21) and ot.TestId not in (49) ORDER BY 5 Asc, 1";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@OrderDate", SqlDbType.VarChar, 20).Value = reportDate.ToShortDateString();

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Domain.OrderLogItem orderLogItem = new YellowstonePathology.Business.Domain.OrderLogItem();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(orderLogItem, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						results.Add(orderLogItem);
					}
				}
			}
			return results;
		}

		public static View.RecentAccessionViewCollection GetRecentAccessionOrders(string pLastName, string pFirstName)
		{
			YellowstonePathology.Business.View.RecentAccessionViewCollection result = new View.RecentAccessionViewCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select ao.MasterAccessionNo, pso.ReportNo, ao.PFirstName, ao.PLastName, ao.AccessionTime, ao.ClientName, ao.PhysicianName, ao.CollectionTime " +
				"from tblAccessionOrder ao " +
				"left outer join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo " +
				"where ao.PFirstName = @PFirstName and ao.PLastName = @PLastName and datediff(d, ao.AccessionDate, getdate()) <= 7 ";
			cmd.Parameters.Add("@PLastName", SqlDbType.VarChar).Value = pLastName;
			cmd.Parameters.Add("@PFirstName", SqlDbType.VarChar).Value = pFirstName;

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.View.RecentAccessionView recentAccessionView = new YellowstonePathology.Business.View.RecentAccessionView();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(recentAccessionView, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(recentAccessionView);
					}
				}
			}
			return result;
		}

		public static ReportNoCollection GetReportNumbers()
		{
			SqlCommand cmd = new SqlCommand("gwGetReportNumbers");
			cmd.CommandType = CommandType.StoredProcedure;

			YellowstonePathology.Business.ReportNoCollection reportNoCollection = new ReportNoCollection();

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.ReportNo reportNo = new ReportNo(dr.GetString(0));
						reportNoCollection.Add(reportNo);
					}
				}
			}

			return reportNoCollection;
		}

		public static ReportNoCollection GetReportNumbersByPostDate(DateTime postDate)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "Select Distinct ReportNo from tblPanelSetOrderCPTCodeBill where PostDate = @PostDate";
			cmd.Parameters.Add("@PostDate", SqlDbType.DateTime).Value = postDate;

			YellowstonePathology.Business.ReportNoCollection reportNoCollection = new ReportNoCollection();

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.ReportNo reportNo = new ReportNo(dr.GetString(0));
						reportNoCollection.Add(reportNo);
					}
				}
			}

			return reportNoCollection;
		}


        public static List<YellowstonePathology.Business.Patient.Model.SVHBillingData> GetPatientImportDataList(string reportNo)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "select svb.* " +
				"from tblAccessionOrder ao " +
				"join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo " +
				"join tblSVHBillingData svb on ao.SVHMedicalRecord = svb.MRN " +
				"where pso.ReportNo = @ReportNo " +
				"order by FileDate desc";
			cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;

            List<YellowstonePathology.Business.Patient.Model.SVHBillingData> result = new List<YellowstonePathology.Business.Patient.Model.SVHBillingData>();

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
                        YellowstonePathology.Business.Patient.Model.SVHBillingData svhBillingData = new YellowstonePathology.Business.Patient.Model.SVHBillingData();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(svhBillingData, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(svhBillingData);
					}
				}
			}

			return result;
		}

		public static bool CanDeleteBatch(int panelOrderBatchId)
		{
			SqlCommand cmd = new SqlCommand("Select count(*) from tblPanelOrder where PanelOrderBatchId = @PanelOrderBatchId");
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@PanelOrderBatchId", SqlDbType.Int).Value = panelOrderBatchId;
			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				SqlDataReader dr = cmd.ExecuteReader();
				dr.Read();
				int cnt = Convert.ToInt32(dr.GetValue(0));
				return cnt == 0 ? true : false;
			}
		}

		public static int GetCountOpenCytologyCasesByCollectionDateRange(DateTime startDate, DateTime endDate)
		{
			SqlCommand cmd = new SqlCommand("select count(*) from tblAccessionOrder ao join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo where " +
				"ao.CollectionDate between @StartDate and @EndDate and pso.PanelSetId = 15 and pso.Final = 0");
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;
			cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate;
			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				SqlDataReader dr = cmd.ExecuteReader();
				dr.Read();
				int result = Convert.ToInt32(dr.GetValue(0));
				return result;
			}
		}

		public static void SetPatientId(string masterAccessionNo, string patientId)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.Text;

			cmd.CommandText = "Update tblAccessionOrder set PatientId = @PatientId where MasterAccessionNo = @MasterAccessionNo";
			cmd.Parameters.Add(new SqlParameter("@MasterAccessionNo", SqlDbType.VarChar)).Value = masterAccessionNo;
			cmd.Parameters.Add(new SqlParameter("@PatientId", SqlDbType.VarChar, 15)).Value = patientId;

			using (SqlConnection cn = new SqlConnection(BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				cmd.ExecuteNonQuery();
			}
		}

		public static YellowstonePathology.Business.Domain.PatientHistory GetPatientHistory(string patientId)
		{
			YellowstonePathology.Business.Domain.PatientHistory result = new Domain.PatientHistory();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "pGetPatientHistoryResults";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@PatientId", SqlDbType.VarChar).Value = patientId;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Domain.PatientHistoryResult patientHistoryResult = new Domain.PatientHistoryResult();
						YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
						patientHistoryResult.WriteProperties(propertyWriter);
						result.Add(patientHistoryResult);
					}
				}
			}

			return result;
		}

		public static string GetPanelOrderIdsToAcknowledge()
		{
			StringBuilder result = new StringBuilder();
			SqlCommand cmd = new SqlCommand("pGetListOfPanelOrderIdsToAcknowledge");
			cmd.CommandType = CommandType.StoredProcedure;
			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result.Append(dr[0].ToString() + ",");
					}
				}
			}

			if (result.Length > 0) result = result.Remove(result.Length - 1, 1);
			return result.ToString();
		}

        public static YellowstonePathology.Business.Test.PantherAliquotList GetPantherOrdersNotAliquoted()
        {
            YellowstonePathology.Business.Test.PantherAliquotList result = new Test.PantherAliquotList();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select a.MasterAccessionNo, a.AccessionTime, a.PLastName, a.PFirstName, ao.AliquotType, ao.ValidationDate, ao.ValidatedBy " +
                "from tblAccessionOrder a " +
                "join tblSpecimenOrder so on a.MasterAccessionNo = so.MasterAccessionNo " +
                "join tblAliquotOrder ao on so.SpecimenOrderId = ao.SpecimenOrderId " +                
                "where ao.AliquotType = 'Panther Aliquot' " +
                "and ao.Validated = 0 order by a.AccessionTime";

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherAliquotListItem pantherAliquotListItem = new Test.PantherAliquotListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherAliquotListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherAliquotListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersNotAcceptedHPV()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select pso.masterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, a.PFirstName, pso.ResultCode, pso.AcceptedTime, pso.FinalTime, psoh.Result, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblHPVTestOrder psoh on pso.ReportNo = psoh.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +                 
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Accepted = 0 order by pso.OrderTime";          

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = new Test.PantherOrderListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersNotAcceptedNGCT()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select pso.masterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, a.PFirstName, pso.AcceptedTime, pso.FinalTime, null as Result, ngct.NeisseriaGonorrhoeaeResult, ngct.ChlamydiaTrachomatisResult, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblNGCTTestOrder ngct on pso.ReportNo = ngct.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Accepted = 0 order by pso.OrderTime";

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItemNGCT pantherOrderListItem = new Test.PantherOrderListItemNGCT();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersNotFinalNGCT()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select pso.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, a.PFirstName, pso.AcceptedTime, pso.FinalTime, null as Result, ngct.NeisseriaGonorrhoeaeResult, ngct.ChlamydiaTrachomatisResult, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblNGCTTestOrder ngct on pso.ReportNo = ngct.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Accepted = 1 and pso.Final = 0 order by pso.FinalTime desc";

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItemNGCT pantherOrderListItem = new Test.PantherOrderListItemNGCT();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersFinalNGCT()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select pso.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, a.PFirstName, pso.AcceptedTime, pso.FinalTime, null as Result, ngct.NeisseriaGonorrhoeaeResult, ngct.ChlamydiaTrachomatisResult, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblNGCTTestOrder ngct on pso.ReportNo = ngct.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Final = 1 order by pso.FinalTime desc";

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItemNGCT pantherOrderListItem = new Test.PantherOrderListItemNGCT();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersNotFinalHPV()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select pso.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, a.PFirstName, pso.ResultCode, pso.AcceptedTime, pso.FinalTime, psoh.Result, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblHPVTestOrder psoh on pso.ReportNo = psoh.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Accepted = 1 and pso.Final = 0 order by pso.OrderTime";

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = new Test.PantherOrderListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersFinalHPV()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select pso.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, a.PFirstName, pso.ResultCode, pso.AcceptedTime, pso.FinalTime, psoh.Result, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblHPVTestOrder psoh on pso.ReportNo = psoh.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Final = 1 and pso.FinalDate >= dateAdd(mm, -3, pso.FinalDate) order by pso.FinalTime desc";

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = new Test.PantherOrderListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersNotAcceptedHPV1618()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select pso.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, a.PFirstName, pso.AcceptedTime, pso.FinalTime, null as Result, hpv.HPV16Result, hpv.HPV18Result, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblPanelSetOrderHPV1618 hpv on pso.ReportNo = hpv.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Accepted = 0 order by pso.OrderTime";

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItemHPV1618 pantherOrderListItem = new Test.PantherOrderListItemHPV1618();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersNotAcceptedTrichomonas()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select pso.MasterAccessionno, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, a.PFirstName, pso.AcceptedTime, pso.FinalTime, null as Result, t.Result, null, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblTrichomonasTestOrder t on pso.ReportNo = t.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Accepted = 0 order by pso.OrderTime";

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = new Test.PantherOrderListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersNotFinalHPV1618()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select pso.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, a.PFirstName, pso.AcceptedTime, pso.FinalTime, null as Result, hpv.HPV16Result, hpv.HPV18Result, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblPanelSetOrderHPV1618 hpv on pso.ReportNo = hpv.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Accepted = 1 and pso.Final = 0 order by pso.FinalTime desc";

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItemHPV1618 pantherOrderListItem = new Test.PantherOrderListItemHPV1618();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersNotFinalTrichomonas()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select pso.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, a.PFirstName, pso.AcceptedTime, pso.FinalTime, t.Result, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblTrichomonasTestOrder t on pso.ReportNo = t.ReportNo " +                
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Accepted = 1 and pso.Final = 0 order by pso.FinalTime desc";

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = new Test.PantherOrderListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersFinalHPV1618()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select pso.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, a.PFirstName, pso.AcceptedTime, pso.FinalTime, null as Result, hpv.HPV16Result, hpv.HPV18Result, pso.HoldDistribution " +            
                "from tblPanelSetOrder pso " +
                "join tblPanelSetOrderHPV1618 hpv on pso.ReportNo = hpv.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Final = 1 order by pso.FinalTime desc";

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItemHPV1618 pantherOrderListItem = new Test.PantherOrderListItemHPV1618();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersFinalTrichomonas()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;            
            cmd.CommandText = "select pso.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, a.PFirstName, pso.AcceptedTime, pso.FinalTime, t.Result, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblTrichomonasTestOrder t on pso.ReportNo = t.ReportNo " +                
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Final = 1 order by pso.FinalTime desc";

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = new Test.PantherOrderListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersFinalWHP()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select pso.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, a.PFirstName, pso.ResultCode, pso.AcceptedTime, pso.FinalTime, null as Result, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblWomensHealthProfileTestOrder psowhp on pso.ReportNo = psowhp.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where pso.Final = 1 and pso.FinalDate >= dateAdd(mm, -1, getDate()) order by pso.FinalTime desc";

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = new Test.PantherOrderListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersNotFinalWHP()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select pso.MasterAccessionNo, pso.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, a.PFirstName, pso.ResultCode, pso.AcceptedTime, pso.FinalTime, null as Result, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblWomensHealthProfileTestOrder psowhp on pso.ReportNo = psowhp.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where pso.Final = 0 order by pso.OrderTime";

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = new Test.PantherOrderListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

		public static YellowstonePathology.Business.Task.Model.TaskOrderCollection GetTaskOrderCollection(string acknowledgementType)
		{
            SqlCommand cmd = new SqlCommand("select * from tblTaskOrder where AcknowledgementType = @AcknowledgementType and " +
                "OrderDate between dateadd(dd, -15, GetDate()) and GetDate() order by OrderDate desc " +
                "select * from tblTaskOrderDetail where TaskOrderId in(select TaskOrderId from tblTaskOrder where " +
                "AcknowledgementType = @AcknowledgementType and OrderDate between dateadd(dd, -15, GetDate()) and GetDate()) " +
                "order by TaskOrderDetailId");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@AcknowledgementType", SqlDbType.VarChar).Value = acknowledgementType;
			YellowstonePathology.Business.Task.Model.TaskOrderCollection result = BuildTaskOrderCollection(cmd);
            return result;
		}

		public static YellowstonePathology.Business.Task.Model.TaskOrderCollection GetDailyTaskOrderCollection()
		{
			YellowstonePathology.Business.Task.Model.TaskOrderCollection result = new YellowstonePathology.Business.Task.Model.TaskOrderCollection();
            string sql = "Select * from tblTaskOrder where AcknowledgementType = 'Daily' " +
	            "and Acknowledged = 0 " +
	            "and TaskDate <= GetDate() order by TaskDate desc";
            SqlCommand cmd = new SqlCommand(sql);	
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@AcknowledgementType", SqlDbType.VarChar).Value = Task.Model.TaskAcknowledgementType.Daily;

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read() == true)
					{
						YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = new Task.Model.TaskOrder();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(taskOrder, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(taskOrder);
					}
				}
			}

			return result;
		}

		public static YellowstonePathology.Business.Task.Model.TaskOrderCollection GetDailyTaskOrderHistoryCollection(int daysBack)
		{
			YellowstonePathology.Business.Task.Model.TaskOrderCollection result = new YellowstonePathology.Business.Task.Model.TaskOrderCollection();
			XElement collectionElement = new XElement("Document");
			string sql = "Select * from tblTaskOrder where AcknowledgementType = 'Daily' " +
				"and TaskDate between @StartDate and @EndDate order by TaskDate desc";
			SqlCommand cmd = new SqlCommand(sql);
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = DateTime.Today.AddDays(-daysBack);
			cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = DateTime.Today;

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read() == true)
					{
						YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = new Task.Model.TaskOrder();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(taskOrder, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(taskOrder);
					}
				}
			}

			return result;
		}

		public static DateTime GetNewestDailyTaskOrderTaskDate(string taskId)
		{
			DateTime result = DateTime.Today;
			SqlCommand cmd = new SqlCommand("Select max(TaskDate) from tblTaskOrder where AcknowledgementType = @AcknowledgementType and TaskId = @TaskId");
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@AcknowledgementType", SqlDbType.VarChar).Value = Task.Model.TaskAcknowledgementType.Daily;
			cmd.Parameters.Add("@TaskId", SqlDbType.VarChar).Value = taskId;

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read() == true)
					{
						DateTime? date = dr[0] as DateTime?;
						if (date.HasValue)
						{
							result = date.Value.AddDays(1);
						}
					}
				}
			}

			return result;
		}

		public static DateTime GetNewestWeeklyTaskOrderTaskDate(string taskId)
		{
			DateTime result = DateTime.Today;
			int currentDay = (int)result.DayOfWeek;
			int daysToAdd = 8 - currentDay;
			if (daysToAdd > 7)
			{
				daysToAdd -= 7;
			}
			result = result.AddDays(daysToAdd);
			SqlCommand cmd = new SqlCommand("Select max(TaskDate) from tblTaskOrder where AcknowledgementType = @AcknowledgementType and TaskId = @TaskId");
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@AcknowledgementType", SqlDbType.VarChar).Value = Task.Model.TaskAcknowledgementType.Daily;
			cmd.Parameters.Add("@TaskId", SqlDbType.VarChar).Value = taskId;

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read() == true)
					{
						DateTime? date = dr[0] as DateTime?;
						if (date.HasValue)
						{
							result = date.Value.AddDays(7);
						}
					}
				}
			}

			return result;
		}

		public static YellowstonePathology.Business.Task.Model.TaskOrderCollection GetTasksNotAcknowledged(string assignedTo, string acknowledgementType)
		{
            SqlCommand cmd = new SqlCommand("select * from tblTaskOrder where AcknowledgementType = @AcknowledgementType and TaskOrderId in " +
                "(Select TaskOrderId from tblTaskOrderDetail where Acknowledged = 0 and AssignedTo = @AssignedTo) order by OrderDate desc " +
                "select * from tblTaskOrderDetail where Acknowledged = 0 and AssignedTo = @AssignedTo order by TaskOrderDetailId");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@AssignedTo", SqlDbType.VarChar).Value = assignedTo;
            cmd.Parameters.Add("@AcknowledgementType", SqlDbType.VarChar).Value = acknowledgementType;
            YellowstonePathology.Business.Task.Model.TaskOrderCollection result = BuildTaskOrderCollection(cmd);
            return result;
		}

		/*public static YellowstonePathology.Business.Task.Model.TaskOrder BuildTaskOrder(XElement taskOrderElement)
		{
			YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = new YellowstonePathology.Business.Task.Model.TaskOrder();
			YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(taskOrderElement, taskOrder);
			xmlPropertyWriter.Write();
			List<XElement> taskOrderDetailElements = (from item in taskOrderElement.Elements("TaskOrderDetailCollection") select item).ToList<XElement>();
			foreach (XElement taskOrderDetailElement in taskOrderDetailElements.Elements("TaskOrderDetail"))
			{
				YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = BuildTaskOrderDetail(taskOrderDetailElement);
				taskOrder.TaskOrderDetailCollection.Add(taskOrderDetail);
			}
			return taskOrder;
		}

		public static YellowstonePathology.Business.Task.Model.TaskOrderDetail BuildTaskOrderDetail(XElement taskOrderDetailElement)
		{
			YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = new YellowstonePathology.Business.Task.Model.TaskOrderDetail();
			YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(taskOrderDetailElement, taskOrderDetail);
			xmlPropertyWriter.Write();
			return taskOrderDetail;
		}*/

        private static Task.Model.TaskOrderCollection BuildTaskOrderCollection(SqlCommand cmd)
        {
            YellowstonePathology.Business.Task.Model.TaskOrderCollection result = new YellowstonePathology.Business.Task.Model.TaskOrderCollection();
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Task.Model.TaskOrder taskOrder = new Task.Model.TaskOrder();
                        Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(taskOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(taskOrder);
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        Task.Model.TaskOrderDetail taskOrderDetail = new Task.Model.TaskOrderDetail();
                        Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(taskOrderDetail, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        foreach (Task.Model.TaskOrder taskOrder in result)
                        {
                            if (taskOrderDetail.TaskOrderId == taskOrder.TaskOrderId)
                            {
                                taskOrder.TaskOrderDetailCollection.Add(taskOrderDetail);
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }

    public static Test.PanelOrder BuildPanelOrder(XElement panelOrderElement)
		{
            YellowstonePathology.Business.Panel.Model.PanelCollection panelCollection = YellowstonePathology.Business.Panel.Model.PanelCollection.GetAll();
			int panelId = Convert.ToInt32(panelOrderElement.Element("PanelId").Value);
            YellowstonePathology.Business.Panel.Model.Panel panel = panelCollection.GetPanel(panelId);
			Test.PanelOrder panelOrder = Test.PanelOrderFactory.GetPanelOrder(panel);

			YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(panelOrderElement, panelOrder);
			xmlPropertyWriter.Write();
			List<XElement> testOrderElements = (from item in panelOrderElement.Elements("TestOrderCollection") select item).ToList<XElement>();
			foreach (XElement testOrderElement in testOrderElements.Elements("TestOrder"))
			{
				YellowstonePathology.Business.Test.Model.TestOrder testOrder = BuildTestOrder(testOrderElement);
				panelOrder.TestOrderCollection.Add(testOrder);
			}
			return panelOrder;
		}

		public static YellowstonePathology.Business.Test.Model.TestOrder BuildTestOrder(XElement testOrderElement)
		{
			YellowstonePathology.Business.Test.Model.TestOrder testOrder = new YellowstonePathology.Business.Test.Model.TestOrder();
			YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(testOrderElement, testOrder);
			xmlPropertyWriter.Write();
			return testOrder;
		}

		public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView GetMaterialTrackingScannedItemViewByAliquotOrderId(string aliquotOrderId)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select ao.AliquotOrderId [MaterialId], ao.AliquotType [MaterialType], a.MasterAccessionNo, a.PLastName, a.PFirstName, ao.Label [MaterialLabel]" +
				"from tblAliquotOrder ao " +
				"join tblSpecimenOrder so on ao.SpecimenOrderId = so.SpecimenOrderId " +
				"join tblAccessionOrder a on so.MasterAccessionNo = a.MasterAccessionNo " +
				"where ao.AliquotOrderid = @AliquotOrderId ";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@AliquotOrderId", SqlDbType.VarChar).Value = aliquotOrderId;

			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView result = null;
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}

        public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView GetMaterialTrackingScannedItemViewByContainerId(string containerId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select so.ContainerId [MaterialId], 'Container' [MaterialType], ao.MasterAccessionNo, ao.PLastName, ao.PFirstName, so.Description [MaterialLabel]" +
                "from tblAccessionOrder ao " +
                "join tblSpecimenOrder so on ao.MasterAccessionNo = so.MasterAccessionNo " +                
                "where so.ContainerId = @ContainerId ";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ContainerId", SqlDbType.VarChar).Value = "CTNR" + containerId;

            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView result = null;
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }
            return result;
        }

		public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView GetMaterialTrackingScannedItemViewBySlideOrderId(string slideOrderId)
		{

			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select s.SlideOrderId [MaterialId], 'Slide' [MaterialType], a.MasterAccessionNo, a.PLastName, a.PFirstName, s.Label [MaterialLabel]" +
				"from tblSlideOrder s " +
				"join tblAliquotOrder ao on s.AliquotOrderId = ao.AliquotOrderId " +
				"join tblSpecimenOrder so on ao.SpecimenOrderId = so.SpecimenOrderId " +
				"join tblAccessionOrder a on so.MasterAccessionNo = a.MasterAccessionNo " +
				"where s.SlideOrderid = @SlideOrderId ";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@SlideOrderId", SqlDbType.VarChar).Value = slideOrderId;

			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView result = null;
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Monitor.Model.DistributionCollection GetPendingDistributions()
		{
			YellowstonePathology.Business.Monitor.Model.DistributionCollection result = new YellowstonePathology.Business.Monitor.Model.DistributionCollection();
			SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select ao.AccessionTime, pso.ReportNo, pso.PanelSetName, pso.FinalTime, ao.PhysicianName, ao.ClientName, datediff(mi, trd.ScheduledDistributionTime, getdate())	[MinutesSinceScheduled], trd.[Distributed] " +
	            "from tblAccessionOrder ao " +
		        "join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo " +
		        "join tblTestOrderReportDistribution trd on pso.ReportNo = trd.ReportNo " +
                "where pso.Distribute = 1 and pso.Final = 1 and pso.finalTime < dateAdd(mi, -15, getdate()) and trd.[Distributed] = 0 and trd.ScheduledDistributionTime is not null " +
				"Order By trd.ScheduledDistributionTime";

			cmd.CommandType = CommandType.Text;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Monitor.Model.Distribution distribution = new YellowstonePathology.Business.Monitor.Model.Distribution();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(distribution, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(distribution);
					}
				}
			}

			return result;
		}		

		public static YellowstonePathology.Business.Monitor.Model.CytologyScreeningCollection GetPendingCytologyScreening()
		{
			YellowstonePathology.Business.Monitor.Model.CytologyScreeningCollection result = new YellowstonePathology.Business.Monitor.Model.CytologyScreeningCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select pso.ReportNo, ao.AccessionTime, cpo.ScreeningType, cpo.ScreenedByName, su1.DisplayName [AssignedToName], po.AcceptedTime [ScreeningFinalTime], pso.FinalTime [CaseFinalTime], ao.ClientName, ao.PhysicianName [ProviderName], pso.Final,  " +
				"(Select count(*) from tblPanelOrder where Reportno = pso.ReportNo) as [ScreeningCount] " +
				"from tblAccessionOrder ao join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo " +
				"join tblPanelOrder po on pso.ReportNo = po.ReportNo " +
				"join tblPanelOrderCytology cpo on po.PanelOrderId = cpo.PanelORderId " +
				"left outer join tblSystemUser su on po.OrderedById = su.UserId " +
				"left outer join tblSystemUser su1 on po.AssignedToId = su1.UserId " +
				"Where pso.PanelSetId = 15 And po.Accepted  = 0 and pso.Final = 0 " +
				"Order By ao.AccessionTime";
			cmd.CommandType = CommandType.Text;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Monitor.Model.CytologyScreening cytologyScreening = new YellowstonePathology.Business.Monitor.Model.CytologyScreening();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(cytologyScreening, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(cytologyScreening);
					}
				}
			}

			return result;
		}

		public static YellowstonePathology.Business.Monitor.Model.PendingTestCollection GetPendingTestCollection()
		{
			YellowstonePathology.Business.Monitor.Model.PendingTestCollection result = new YellowstonePathology.Business.Monitor.Model.PendingTestCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select pso.ReportNo, pso.PanelSetName [TestName], pso.ExpectedFinalTime, pso.OrderTime, ao.ClientName, ao.PhysicianName [ProviderName], su.DisplayName [AssignedTo], pso.IsDelayed " +
				"from tblPanelSetOrder pso " +
				"join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo	" +
				"join tblSystemUser su on pso.AssignedToId = su.UserId " +
				"where final = 0 and panelSetId <> 212 " +  //Will not show Missing Information Tests!
				"order by ExpectedFinalTime";
			cmd.CommandType = CommandType.Text;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Monitor.Model.PendingTest pendingTest = new YellowstonePathology.Business.Monitor.Model.PendingTest();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pendingTest, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(pendingTest);
					}
				}
			}

			return result;
		}

        public static YellowstonePathology.Business.Monitor.Model.MissingInformationCollection GetMissingInformationCollection()
        {
            YellowstonePathology.Business.Monitor.Model.MissingInformationCollection result = new YellowstonePathology.Business.Monitor.Model.MissingInformationCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select pso.MasterAccessionNo, pso.ReportNo, pso.PanelSetName [TestName], pso.ExpectedFinalTime, pso.OrderTime, mit.FirstCallComment, mit.SecondCallComment, ao.PhysicianName [ProviderName] " +
                "from tblPanelSetOrder pso " +
                "join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo	" +
                "join tblMissingInformationTestOrder mit on pso.ReportNo = mit.ReportNo " +
                "join tblSystemUser su on pso.AssignedToId = su.UserId " +
                "where pso.PanelSetId = 212 and pso.Final = 0" +
                "order by ExpectedFinalTime";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Monitor.Model.MissingInformation missingInformation = new YellowstonePathology.Business.Monitor.Model.MissingInformation();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(missingInformation, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(missingInformation);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.NeogenomicsResultCollection GetNeogenomicsResultCollection()
		{            
#if MONGO
            return AccessionOrderGatewayMongo.GetNeogenomicsResultCollection();
#else
			YellowstonePathology.Business.NeogenomicsResultCollection result = new YellowstonePathology.Business.NeogenomicsResultCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select * from tblNeogenomicsResult order by DateResultReceived desc";
			cmd.CommandType = CommandType.Text;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.NeogenomicsResult neogenomicsResult = new YellowstonePathology.Business.NeogenomicsResult();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(neogenomicsResult, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(neogenomicsResult);
					}
				}
			}
			return result;
#endif
		}

		public static XElement GetAccessionOrderXMLDocument(string masterAccessionNo)
		{            
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "buildAccessionOrderXML";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;

			XElement result = null;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;

				using (XmlReader xmlReader = cmd.ExecuteXmlReader())
				{
					result = XElement.Load(xmlReader);
				}
			}

			return result;
		}		

		public static YellowstonePathology.Business.Test.Model.StainTest GetStainTestByTestId(int testId)
		{
			YellowstonePathology.Business.Test.Model.StainTest result = null;
			SqlCommand cmd = new SqlCommand("SELECT * from tblStainTest where TestId = @TestId");
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@TestId", SqlDbType.Int).Value = testId;

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new YellowstonePathology.Business.Test.Model.StainTest();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Domain.ImmunoComment GetImmunoCommentByImmunocommentId(int immunoCommentId)
		{
			YellowstonePathology.Business.Domain.ImmunoComment result = null;
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * from tblImmunoComment where immunocommentid = @ImmunoCommentId";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@ImmunoCommentId", SqlDbType.Int).Value = immunoCommentId;

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new YellowstonePathology.Business.Domain.ImmunoComment();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Domain.OrderCommentCollection GetAllLabEvents()
		{
			YellowstonePathology.Business.Domain.OrderCommentCollection result = new Domain.OrderCommentCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * from tblOrderComment order by OrderCommentId";
			cmd.CommandType = System.Data.CommandType.Text;

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Domain.OrderComment orderComment = new Domain.OrderComment();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(orderComment, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(orderComment);
					}
				}
			}
			return result;
		}

		public static Domain.OrderComment GetLabEventByEventId(int orderCommentId)
		{
			Domain.OrderComment result = new Domain.OrderComment();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * from tblOrderComment where OrderCommentId = @OrderCommentId";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@OrderCommentId", System.Data.SqlDbType.Int).Value = orderCommentId;

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new YellowstonePathology.Business.Domain.OrderComment();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.PanelSet.Model.PanelSetCaseTypeCollection GetPanelSetCaseTypeCollection()
		{
			YellowstonePathology.Business.PanelSet.Model.PanelSetCaseTypeCollection result = new YellowstonePathology.Business.PanelSet.Model.PanelSetCaseTypeCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select distinct CaseType from tblPanelSet where CaseType is not null";
			cmd.CommandType = CommandType.Text;

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.PanelSet.Model.PanelSetCaseType panelSetCaseType = new YellowstonePathology.Business.PanelSet.Model.PanelSetCaseType();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(panelSetCaseType, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(panelSetCaseType);
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Domain.Cytology.OtherConditionCollection GetOtherConditions()
		{
			YellowstonePathology.Business.Domain.Cytology.OtherConditionCollection result = new Domain.Cytology.OtherConditionCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT LineID, OtherCondition OtherConditionText from tblCytologyOtherConditions order by OtherCondition";
			cmd.CommandType = System.Data.CommandType.Text;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Domain.Cytology.OtherCondition otherCondition = new Domain.Cytology.OtherCondition();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(otherCondition, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(otherCondition);
					}
				}
			}

			return result;
		}

		public static YellowstonePathology.Business.Domain.Cytology.OtherCondition GetOtherConditionById(int otherConditionId)
		{
			YellowstonePathology.Business.Domain.Cytology.OtherCondition result = null;
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT LineID, OtherCondition OtherConditionText from tblCytologyOtherConditions where LineId = @LineId";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@LineId", SqlDbType.Int).Value = otherConditionId;

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new YellowstonePathology.Business.Domain.Cytology.OtherCondition();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Domain.Cytology.CytologyReportCommentCollection GetCytologyReportComments()
		{
			YellowstonePathology.Business.Domain.Cytology.CytologyReportCommentCollection result = new Domain.Cytology.CytologyReportCommentCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT CommentID as CommentId, Comment, AbbreviatedComment from tblCytologyReportComment order by AbbreviatedComment";
			cmd.CommandType = System.Data.CommandType.Text;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Domain.Cytology.CytologyReportComment cytologyReportComment = new Domain.Cytology.CytologyReportComment();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(cytologyReportComment, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(cytologyReportComment);
					}
				}
			}

			return result;
		}

		public static YellowstonePathology.Business.Domain.Cytology.CytologyReportComment GetCytologyReportCommentById(int commentId)
		{
			YellowstonePathology.Business.Domain.Cytology.CytologyReportComment result = null;
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT CommentID as CommentId, Comment, AbbreviatedComment from tblCytologyReportComment where CommentId = @CommentId";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@CommentId", SqlDbType.Int).Value = commentId;

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new YellowstonePathology.Business.Domain.Cytology.CytologyReportComment();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Cytology.Model.ScreeningImpressionCollection GetScreeningImpressions()
		{
			YellowstonePathology.Business.Cytology.Model.ScreeningImpressionCollection result = new Cytology.Model.ScreeningImpressionCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * from tblCytologyScreeningImpression order by ResultCode";
			cmd.CommandType = System.Data.CommandType.Text;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Cytology.Model.ScreeningImpression screeningImpression = new YellowstonePathology.Business.Cytology.Model.ScreeningImpression();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(screeningImpression, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(screeningImpression);
					}
				}
			}

			return result;
		}

		public static YellowstonePathology.Business.Cytology.Model.ScreeningImpression GetScreeningImpressionByResultCode(string resultCode)
		{
			YellowstonePathology.Business.Cytology.Model.ScreeningImpression result = null;
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * from tblCytologyScreeningImpression where ResultCode = @ResultCode";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@ResultCode", SqlDbType.VarChar).Value = resultCode;

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new YellowstonePathology.Business.Cytology.Model.ScreeningImpression();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyCollection GetSpecimenAdequacy()
		{
			YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyCollection result = new Cytology.Model.SpecimenAdequacyCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * from tblSpecimenAdequacy";
			cmd.CommandType = System.Data.CommandType.Text;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Cytology.Model.SpecimenAdequacy specimenAdequacy = new YellowstonePathology.Business.Cytology.Model.SpecimenAdequacy();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(specimenAdequacy, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(specimenAdequacy);
					}
				}
			}

			return result;
		}

		public static YellowstonePathology.Business.Cytology.Model.SpecimenAdequacy GetSpecimenAdequacyByResultCode(string resultCode)
		{
			YellowstonePathology.Business.Cytology.Model.SpecimenAdequacy result = null;
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * from tblSpecimenAdequacy where ResultCode = @ResultCode";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@ResultCode", SqlDbType.VarChar).Value = resultCode;

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new YellowstonePathology.Business.Cytology.Model.SpecimenAdequacy();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyCommentCollection GetSpecimenAdequacyComments()
		{
			YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyCommentCollection result = new Cytology.Model.SpecimenAdequacyCommentCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT CommentID as CommentId, Comment from tblCytologySAComments order by Comment";
			cmd.CommandType = System.Data.CommandType.Text;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyComment specimenAdequacyComment = new YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyComment();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(specimenAdequacyComment, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(specimenAdequacyComment);
					}
				}
			}

			return result;
		}

        public static List<Business.MasterAccessionNo> GetMasterAccessionNoList(DateTime accessionDate)
        {
            List<Business.MasterAccessionNo> result = new List<Business.MasterAccessionNo>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select MasterAccessionNo from tblAccessionOrder where AccessionDate = '" + accessionDate.ToString() + "'";
            cmd.CommandType = CommandType.Text;
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Business.MasterAccessionNo man = Business.MasterAccessionNo.Parse(dr[0].ToString(), true);
                        result.Add(man);
                    }
                }
            }
            return result;
        }

        public static List<Business.MasterAccessionNo> GetCasesWithUnscheduledAmendments()
        {
            List<Business.MasterAccessionNo> result = new List<Business.MasterAccessionNo>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select distinct pso.MasterAccessionNo " +
                "from tblAmendment a " +
                "join tblTestOrderReportDistribution trd on a.ReportNo = trd.ReportNo " +
                "join tblPanelSetOrder pso on trd.ReportNo = pso.ReportNo " +
                "where trd.TimeOfLastDistribution < a.finalTime and trd.ScheduledDistributionTime is null and a.finalTime < dateAdd(mi, -15, getdate()) and a.DistributeOnFinal = 1 ";

            cmd.CommandType = CommandType.Text;
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Business.MasterAccessionNo man = Business.MasterAccessionNo.Parse(dr[0].ToString(), true);
                        result.Add(man);
                    }
                }
            }
            return result;
        }

        public static List<Business.MasterAccessionNo> GetCasesWithUnsetDistributions()
        {
            List<Business.MasterAccessionNo> result = new List<Business.MasterAccessionNo>();

            SqlCommand cmd = new SqlCommand();
            //cmd.CommandText = "Select distinct MasterAccessionNo from tblPanelSetOrder pso where final = 1 and distribute = 1 and not exists (Select * from tblTestOrderReportDistribution where reportNo = pso.ReportNo)";
            cmd.CommandText = "Select distinct MasterAccessionNo from tblPanelSetOrder pso where final = 1 and pso.finalTime < dateAdd(mi, -15, getdate()) and distribute = 1 and not exists (Select * from tblTestOrderReportDistribution where reportNo = pso.ReportNo)";
            cmd.CommandType = CommandType.Text;
            SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString);       
            cn.Open();
            cmd.Connection = cn;
            SqlDataReader dr = null;

            try
            {
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Business.MasterAccessionNo man = Business.MasterAccessionNo.Parse(dr[0].ToString(), true);
                    result.Add(man);
                }
            }
            catch(System.Data.SqlClient.SqlException e)
            {
                if(e.Number == 1205) //1205 is an sql deadlock
                {
                    System.Threading.Thread.Sleep(5000);
                    GetCasesWithUnsetDistributions();
                }
                else
                {
                    throw;
                }
            }   
            finally
            {
                dr.Close();
                cn.Close();
            }         
            
            return result;
        }

        public static List<Business.MasterAccessionNo> GetCasesWithUnscheduledDistributions()
        {
            List<Business.MasterAccessionNo> result = new List<Business.MasterAccessionNo>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select distinct pso.MasterAccessionNo from tblTestOrderReportDistribution tor	" +
                "join tblPanelSetOrder pso on tor.ReportNo = pso.ReportNo " +
                "where tor.[Distributed] = 0 and tor.ScheduledDistributionTime is null and pso.Final = 1 and pso.finalTime < dateAdd(mi, -15, getdate()) and pso.Distribute = 1 and pso.HoldDistribution = 0";

            cmd.CommandType = CommandType.Text;
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Business.MasterAccessionNo man = Business.MasterAccessionNo.Parse(dr[0].ToString(), true);
                        result.Add(man);
                    }
                }
            }
            return result;
        }

        public static List<Business.MasterAccessionNo> GetCasesWithUnscheduledPublish()
        {
            List<Business.MasterAccessionNo> result = new List<Business.MasterAccessionNo>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select Distinct MasterAccessionNo from tblPanelSetOrder pso where pso.Final = 1 and pso.finalTime < dateAdd(mi, -15, getdate()) and pso.ScheduledPublishTime is null and pso.Published = 0";

            cmd.CommandType = CommandType.Text;
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Business.MasterAccessionNo man = Business.MasterAccessionNo.Parse(dr[0].ToString(), true);
                        result.Add(man);
                    }
                }
            }
            return result;
        }

        public static List<Business.ReportNo> GetNextReportNumbersToPublish()
        {
            List<Business.ReportNo> result = new List<Business.ReportNo>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select Distinct pso.ReportNo from tblPanelSetOrder pso where pso.Final = 1 and pso.ScheduledPublishTime <= getdate() union " +
                "Select pso.* from tblPanelSetOrder pso join tblTestOrderReportDistribution trd on pso.ReportNo = trd.ReportNo " +
                "where pso.Final = 1 and trd.ScheduledDistributionTime <= getdate() and pso.Distribute = 1";

            cmd.CommandType = CommandType.Text;
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Business.ReportNo rno = new Business.ReportNo(dr[0].ToString());
                        result.Add(rno);
                    }
                }
            }
            return result;
        }
    }
}
