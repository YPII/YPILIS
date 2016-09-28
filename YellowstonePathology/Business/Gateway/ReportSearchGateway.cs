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
	public class ReportSearchGateway
	{
		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByReportNo(string reportNo)
        {            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.[MasterAccessionNo], pso.[ReportNo], a.AccessionTime [AccessionDate],  pso.[PanelSetId], a.[PFirstName] + ' ' + a.[PLastName] AS [PatientName], " +
                "a.[PLastName], a.[PFirstName], a.[ClientName], a.[PhysicianName], a.[PBirthdate], pso.[OriginatingLocation], pso.[FinalDate], pso.PanelSetName, su.UserName as [OrderedBy], " +
                "a.ColorCode, '' ForeignAccessionNo " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.[MasterAccessionNo] = pso.[MasterAccessionNo] " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.ReportNo = @ReportNo";
            cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByPanelSetFinalDate(DateTime finalDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.[MasterAccessionNo], pso.[ReportNo], a.AccessionTime [AccessionDate],  pso.[PanelSetId], a.[PFirstName] + ' ' + a.[PLastName] AS [PatientName], " +
                "a.[PLastName], a.[PFirstName], a.[ClientName], a.[PhysicianName], a.[PBirthdate], pso.[OriginatingLocation], pso.[FinalDate], pso.PanelSetName, su.UserName as [OrderedBy], " +
                "a.ColorCode, '' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.[MasterAccessionNo] = pso.[MasterAccessionNo] " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.FinalDate = @FinalDate order by pso.[ReportNo]";
            cmd.Parameters.Add("@FinalDate", SqlDbType.DateTime).Value = finalDate;
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByNotPosted()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.[MasterAccessionNo], pso.[ReportNo], a.AccessionTime [AccessionDate],  pso.[PanelSetId], a.[PFirstName] + ' ' + a.[PLastName] AS [PatientName], " +
                "a.[PLastName], a.[PFirstName], a.[ClientName], a.[PhysicianName], a.[PBirthdate], pso.[OriginatingLocation], pso.[FinalDate], pso.PanelSetName, su.UserName as [OrderedBy], " +
                "a.ColorCode, '' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.[MasterAccessionNo] = pso.[MasterAccessionNo] " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.Final = 1 and pso.IsPosted = 0 and pso.OrderDate >= '1/1/2014' and pso.IsBillable = 1 Order By pso.FinalDate, pso.PanelSetId, a.AccessionTime";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByPostDate(DateTime postDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct pso.[MasterAccessionNo], pso.[ReportNo], a.AccessionTime [AccessionDate],  pso.[PanelSetId], a.[PFirstName] + ' ' + a.[PLastName] AS [PatientName], " +
                "a.[PLastName], a.[PFirstName], a.[ClientName], a.[PhysicianName], a.[PBirthdate], pso.[OriginatingLocation], pso.[FinalDate], pso.PanelSetName, su.UserName as [OrderedBy], " +
                "a.ColorCode, '' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a  " +
                "JOIN tblPanelSetOrder pso ON a.[MasterAccessionNo] = pso.[MasterAccessionNo] " +
                "join tblPanelSetOrderCPTCodeBill psocpt on pso.ReportNo = psocpt.ReportNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.IsPosted = 1 and psocpt.PostDate = @PostDate Order By pso.FinalDate, pso.PanelSetId, a.AccessionTime";

            cmd.Parameters.Add("@PostDate", SqlDbType.DateTime).Value = postDate;
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByPositiveHPylori(DateTime startDate, DateTime endDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct pso.[MasterAccessionNo], pso.[ReportNo], a.AccessionTime [AccessionDate],  pso.[PanelSetId], a.[PFirstName] + ' ' + a.[PLastName] AS [PatientName], " +
                "a.[PLastName], a.[PFirstName], a.[ClientName], a.[PhysicianName], a.[PBirthdate], pso.[OriginatingLocation], pso.[FinalDate], pso.PanelSetName, su.UserName as [OrderedBy], " +
                "a.ColorCode, '' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a " +
                "join tblPanelSetOrder pso ON a.[MasterAccessionNo] = pso.[MasterAccessionNo] " +
                "join tblPanelOrder po on pso.ReportNo = po.ReportNo " +
                "join tblTestOrder t on po.panelOrderId = t.panelOrderId " +
                "join tblStainResult sr on t.TestOrderId = sr.TestOrderId " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "where t.TestId = 107 and sr.Result = 'Positive' and a.AccessionDate between @StartDate and @EndDate";

            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate;
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByAutopsies()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.[MasterAccessionNo], pso.[ReportNo], a.AccessionTime [AccessionDate],  pso.[PanelSetId], a.[PFirstName] + ' ' + a.[PLastName] AS [PatientName], " +
                "a.[PLastName], a.[PFirstName], a.[ClientName], a.[PhysicianName], a.[PBirthdate], pso.[OriginatingLocation], pso.[FinalDate], pso.PanelSetName, su.UserName as [OrderedBy], " +
                "a.ColorCode, '' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a " +
                "join tblPanelSetOrder pso ON a.[MasterAccessionNo] = pso.[MasterAccessionNo] " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "where pso.PanelSetId = 35 Order By pso.OrderDate desc";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByClientAccessioned()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.[MasterAccessionNo], pso.[ReportNo], a.AccessionTime [AccessionDate],  pso.[PanelSetId], a.[PFirstName] + ' ' + a.[PLastName] AS [PatientName], " +
                "a.[PLastName], a.[PFirstName], a.[ClientName], a.[PhysicianName], a.[PBirthdate], pso.[OriginatingLocation], pso.[FinalDate], pso.PanelSetName, su.UserName as [OrderedBy], " +
                "a.ColorCode, '' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a " +
                "join tblPanelSetOrder pso ON a.[MasterAccessionNo] = pso.[MasterAccessionNo] " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "where a.ClientAccessioned = 1 Order By pso.OrderDate desc";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByDrKurtzman()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.[MasterAccessionNo], pso.[ReportNo], a.AccessionTime [AccessionDate],  pso.[PanelSetId], a.[PFirstName] + ' ' + a.[PLastName] AS [PatientName], " +
                "a.[PLastName], a.[PFirstName], a.[ClientName], a.[PhysicianName], a.[PBirthdate], pso.[OriginatingLocation], pso.[FinalDate], pso.PanelSetName, su.UserName as [OrderedBy], " +
                "a.ColorCode, '' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a " +
                "join tblPanelSetOrder pso ON a.[MasterAccessionNo] = pso.[MasterAccessionNo] " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "where a.ClientId = 1520 Order By pso.OrderDate desc";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListBySpecimenKeyword(string specimenDescription, DateTime startDate, DateTime endDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.[MasterAccessionNo], pso.[ReportNo], a.AccessionTime [AccessionDate],  pso.[PanelSetId], a.[PFirstName] + ' ' + a.[PLastName] AS [PatientName], " +
                "a.[PLastName], a.[PFirstName], a.[ClientName], a.[PhysicianName], a.[PBirthdate], pso.[OriginatingLocation], pso.[FinalDate], pso.PanelSetName, su.UserName as [OrderedBy], " +
                "a.ColorCode, '' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.[MasterAccessionNo] = pso.[MasterAccessionNo] " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE a.MasterAccessionNo in " +
                "(Select MasterAccessionNo from tblSpecimenOrder so where so.masterAccessionNo = a.masterAccessionNo " +
                "and charindex(@SpecimenDescription, so.Description) > 0) " +
                "and a.AccessionDate between @StartDate and @EndDate";

            cmd.Parameters.Add("@SpecimenDescription", SqlDbType.VarChar).Value = specimenDescription;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate;
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByAccessionDate(DateTime accessionDate, List<int> panelSetIdList)
        {
            string panelSetIdString = YellowstonePathology.Business.Helper.IdListHelper.ToIdString(panelSetIdList);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.[MasterAccessionNo], pso.[ReportNo], a.AccessionTime [AccessionDate],  pso.[PanelSetId], a.[PFirstName] + ' ' + a.[PLastName] AS [PatientName], " +
                "a.[PLastName], a.[PFirstName], a.[ClientName], a.[PhysicianName], a.[PBirthdate], pso.[OriginatingLocation], pso.[FinalDate], pso.PanelSetName, su.UserName as [OrderedBy], " +
                "a.ColorCode, '' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.[MasterAccessionNo] = pso.[MasterAccessionNo] " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE a.AccessionDate = @AccessionDate " +
                "And pso.PanelSetId in (" + panelSetIdString + ")" +
                "ORDER BY AccessionTime desc ";
            cmd.Parameters.Add("@AccessionDate", SqlDbType.VarChar).Value = accessionDate;
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByMasterAccessionNo(string masterAccessionNo)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "SELECT a.[MasterAccessionNo], pso.[ReportNo], a.AccessionTime [AccessionDate],  pso.[PanelSetId], a.[PFirstName] + ' ' + a.[PLastName] AS [PatientName], " +
                "a.[PLastName], a.[PFirstName], a.[ClientName], a.[PhysicianName], a.[PBirthdate], pso.[OriginatingLocation], pso.[FinalDate], pso.PanelSetName, su.UserName as [OrderedBy], " +
                "a.ColorCode, '' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a " +
                "Left outer JOIN tblPanelSetOrder pso ON a.[MasterAccessionNo] = pso.[MasterAccessionNo] " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE a.MasterAccessionNo = @MasterAccessionNo";
            cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
		}

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByAliquotOrderId(string aliquotOrderId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.[MasterAccessionNo], pso.[ReportNo], a.AccessionTime [AccessionDate],  pso.[PanelSetId], a.[PFirstName] + ' ' + a.[PLastName] AS [PatientName], " +
               "a.[PLastName], a.[PFirstName], a.[ClientName], a.[PhysicianName], a.[PBirthdate], pso.[OriginatingLocation], pso.[FinalDate], pso.PanelSetName, su.UserName as [OrderedBy], " +
               "a.ColorCode, '' ForeignAccessionNo, pso.IsPosted " +
               "FROM tblAccessionOrder a " +
               "JOIN tblPanelSetOrder pso ON a.[MasterAccessionNo] = pso.[MasterAccessionNo] " +
               "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
               "WHERE a.MasterAccessionNo in " +
               "(Select MasterAccessionNo from tblSpecimenOrder so join tblAliquotOrder ao on so.SpecimenOrderId = ao.SpecimenOrderId where ao.AliquotOrderId = @AliquotOrderId)";
            cmd.Parameters.Add("@AliquotOrderId", SqlDbType.VarChar).Value = aliquotOrderId;
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByITAudit(YellowstonePathology.Business.Test.ITAuditPriorityEnum itAuditPriority)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT a.[MasterAccessionNo], pso.[ReportNo], a.AccessionTime [AccessionDate],  pso.[PanelSetId], a.[PFirstName] + ' ' + a.[PLastName] AS [PatientName], " +
                "a.[PLastName], a.[PFirstName], a.[ClientName], a.[PhysicianName], a.[PBirthdate], pso.[OriginatingLocation], pso.[FinalDate], pso.PanelSetName, su.UserName as [OrderedBy], " +
                "a.ColorCode, '' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a " +
                "Left outer JOIN tblPanelSetOrder pso ON a.[MasterAccessionNo] = pso.[MasterAccessionNo] " +
                "Left Outer JOIN tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE a.ITAuditRequired = 1 and a.ITAudited = 0 and a.ITAuditPriority = @ITAuditPriority";
            cmd.Parameters.Add("@ITAuditPriority", SqlDbType.Int).Value = (int)itAuditPriority;
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByDateRange(List<object> parameters)
		{
			SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "gwAccessionOrderListByCurrentMonthFill_1";
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = parameters[0];
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = parameters[1];
            cmd.Parameters.Add("@PanelSetId", SqlDbType.VarChar).Value = parameters[2];
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
		}

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByReportNo(List<object> parameters)
		{
			SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "gwAccessionOrderListByAccessionNoFill_1";
            cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = parameters[0];
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
		}

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByNotDistributed(List<object> parameters)
		{
			SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "gwAccessionOrderListByUnDistributedFill_1";
            cmd.Parameters.Add("@PanelId", SqlDbType.Int).Value = parameters[0];
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
		}

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByNotFinalPanelId(List<object> parameters)
		{
			SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "gwAccessionOrderListByNotFinalFill_1";
            cmd.Parameters.Add("@PanelId", SqlDbType.Int).Value = parameters[0];
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
		}

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByInHouseMolecularPending()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "gwAccessionOrderListByInHouseMolecularPending_1";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByPatientName(List<object> parameters)
		{
			SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pReportSearchListByPatientName_1";
            cmd.Parameters.Add("@PLastName", SqlDbType.VarChar).Value = parameters[0].ToString();
            if (parameters[1] == null) cmd.Parameters.Add("@PFirstName", SqlDbType.VarChar).Value = DBNull.Value;
            else cmd.Parameters.Add("@PFirstName", SqlDbType.VarChar).Value = parameters[1].ToString();
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
		}

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByMasterAccessionNo(List<object> parameters)
		{
			SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "gwAccessionOrderListByMasterAccessionNoFill_1";
            cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = parameters[0];
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
		}

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByNotAudited(List<object> parameters)
		{
			SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "gwAccessionOrderListByNotAudited_1";
            cmd.Parameters.Add("CaseType", SqlDbType.VarChar).Value = parameters[0];
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
		}

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByPatientId(List<object> parameters)
		{
			SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "gwAccessionOrderListByPatientId_1";
            cmd.Parameters.Add("@PatientId", SqlDbType.VarChar).Value = parameters[0].ToString();
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
		}

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByPanelSetId(List<object> parameters)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "gwAccessionOrderListByPanelSetId_3";
            cmd.Parameters.Add("@PanelSetId", SqlDbType.Int).Value = parameters[0];
            cmd.Parameters.Add("@AccessionDate", SqlDbType.DateTime).Value = parameters[1];
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByTest(int panelSetId, DateTime startDate, DateTime endDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.[MasterAccessionNo], pso.[ReportNo], a.AccessionTime [AccessionDate],  pso.[PanelSetId], a.[PFirstName] + ' ' + a.[PLastName] AS [PatientName], " +
                "a.[PLastName], a.[PFirstName], a.[ClientName], a.[PhysicianName], a.[PBirthdate], pso.[OriginatingLocation], pso.[FinalDate], pso.PanelSetName, su.UserName as [OrderedBy], " +
                "a.ColorCode, '' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.[MasterAccessionNo] = pso.[MasterAccessionNo] " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.PanelSetId  =  @PanelSetId " +
                "and pso.OrderDate between @StartDate and @EndDate";
            cmd.Parameters.Add("@PanelSetId", SqlDbType.Int).Value = panelSetId;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate;
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static Test.ThinPrepPap.AcidWashList GetAcidWashList(DateTime startDate)
        {
            Test.ThinPrepPap.AcidWashList result = new Test.ThinPrepPap.AcidWashList();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select pso.MasterAccessionNo, po.ReportNo, po.OrderTime, po.Accepted, a.PLastName, a.PFirstName, a.PMiddleInitial, po.Comment from tblPanelOrder po join tblPanelSetOrder pso on po.ReportNo = pso.ReportNo " +
                " join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo where po.PanelId = 39 and po.OrderTime >= @StartDate  order by po.OrderTime Desc";
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Test.ThinPrepPap.AcidWashListItem acidWashLIstItem = new Test.ThinPrepPap.AcidWashListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(acidWashLIstItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(acidWashLIstItem);
                    }
                }
            }

            result.SetState();
            return result;
        }

        private static YellowstonePathology.Business.Search.ReportSearchList BuildReportSearchList(SqlCommand cmd)
        {
            Search.ReportSearchList result = new Search.ReportSearchList();

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Search.ReportSearchItem reportSearchItem = new Search.ReportSearchItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(reportSearchItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(reportSearchItem);
                    }
                }
            }
            return result;
        }
    }
}
