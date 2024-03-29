﻿using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
	public class ReportSearchGateway
	{
		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByReportNo(string reportNo)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.ReportNo = @ReportNo;";
            cmd.Parameters.AddWithValue("@ReportNo", reportNo);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByPanelSetFinalDate(DateTime finalDate)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.FinalDate = @FinalDate order by pso.ReportNo;";
            cmd.Parameters.AddWithValue("@FinalDate", finalDate);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByNotPosted()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.Final = 1 and pso.panelsetid not in (400,415) and pso.IsPosted = 0 and pso.OrderDate >= '2014-1-1' and pso.IsBillable = 1 " +
                "Order By pso.FinalDate, pso.PanelSetId, a.AccessionTime;";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByChangesNotPosted()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate, pso.PanelSetId, " +
	            "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy,  " +            
	            "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.Final = 1 and pso.IsPosted = 1 and pso.OrderDate >= '2018-06-01' and pso.IsBillable = 1 " +
                "and exists (select null from tblPanelSetOrderCPTCode where postdate is null and ReportNo = pso.ReportNo) " +
                "Order By pso.FinalTime, pso.PanelSetId, a.AccessionTime;";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListBySVHFinalNotPosted()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, ao.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(ao.PFirstName, ' ', ao.PLastName) AS PatientName, " +
                "ao.PLastName, ao.PFirstName, ao.ClientName, ao.PhysicianName, ao.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "from tblAccessionOrder ao " +
                "join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "where pso.Final = 1 " +
                "and ao.SvhMedicalRecord like 'V%' " +
                "and exists (Select null from tblPanelSetOrderCPTCode where ReportNo = pso.ReportNo and postDate is null) " +
                "and pso.panelSetId = 13 " +
                "and pso.OrderDate >= '2014-1-1' " +
                "Order By pso.FinalTime, pso.PanelSetId, ao.AccessionTime;";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListBySVHPosted(DateTime postDate)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a  " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblPanelSetOrderCPTCodeBill psocpt on pso.ReportNo = psocpt.ReportNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +                
                "WHERE pso.IsPosted = 1 and a.SvhMedicalRecord like 'V%' and pso.PanelSetId = 13 and psocpt.PostDate = @PostDate Order By pso.FinalTime, pso.PanelSetId, a.AccessionTime;";

            cmd.Parameters.AddWithValue("@PostDate", postDate);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByCOVIDCases()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a  " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +                
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.PanelSetId = 400 Order By a.AccessionTime desc;";

            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByYPICOVIDCases()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a  " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.PanelSetId = 400 and a.clientId in (280,1134) Order By a.AccessionTime desc;";

            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListBySCLCOVIDCases()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a  " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.PanelSetId = 400 and a.clientId in (558) Order By a.AccessionTime desc;";

            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByCOVIDPatientDistributionCases()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a  " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.PanelSetId = 400 and a.DistributeToPatient = 1 Order By a.AccessionTime desc;";

            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByDurdenCOVIDCases()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a  " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.PanelSetId = 400 and a.PhysicianId = 2797 and a.DistributeToPatient = 0 Order By a.AccessionTime desc;";

            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByCasesWithNotes(string year)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted, ocl.LogDate " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblOrderCommentLog ocl on a.MasterAccessionno = ocl.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "where year(ocl.LogDate) >= @Year " +
                "Order By ocl.LogDate desc;";

            cmd.Parameters.AddWithValue("@Year", year);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByASCCPCases()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted, pso.HoldDistribution " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblWomensHealthProfileTestOrder whp on pso.ReportNo = whp.ReportNo " + 
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "where a.AccessionDate >= '2020-01-01' and (whp.ManagePerASCCP = 1 or whp.ManagePerASCCPWithCotest = 1) " +
                "Order by a.AccessionDate desc";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }



        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByPostDate(DateTime postDate)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a  " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblPanelSetOrderCPTCodeBill psocpt on pso.ReportNo = psocpt.ReportNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.IsPosted = 1 and psocpt.PostDate = @PostDate Order By pso.FinalTime, pso.PanelSetId, a.AccessionTime;";

            cmd.Parameters.AddWithValue("@PostDate", postDate);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByPositiveHPylori(DateTime startDate, DateTime endDate)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a " +
                "join tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblPanelOrder po on pso.ReportNo = po.ReportNo " +
                "join tblTestOrder t on po.panelOrderId = t.panelOrderId " +
                "join tblStainResult sr on t.TestOrderId = sr.TestOrderId " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "where t.TestId = 107 and sr.Result = 'Positive' and a.AccessionDate between @StartDate and @EndDate;";

            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByAutopsies()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a " +
                "join tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "where pso.PanelSetId = 35 Order By pso.OrderDate desc;";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByClientAccessioned()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a " +
                "join tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "where a.ClientAccessioned = 1 Order By pso.OrderDate desc;";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByDrKurtzman()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a " +
                "join tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "where a.ClientId = 1520 Order By pso.OrderDate desc;";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListBySpecimenKeyword(string specimenDescription, DateTime startDate, DateTime endDate)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE a.MasterAccessionNo in " +
                "(Select MasterAccessionNo from tblSpecimenOrder so where so.masterAccessionNo = a.masterAccessionNo " +
                "and locate(@SpecimenDescription, so.Description) > 0) " +
                "and a.AccessionDate between @StartDate and @EndDate;";

            cmd.Parameters.AddWithValue("@SpecimenDescription", specimenDescription);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByNeo(DateTime accessionDate)
        {            
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE a.AccessionDate = @AccessionDate and pso.TechnicalComponentFacilityid in ('NEOGNMCIRVN','NEOGNMCNSHVLL') " +                
                "ORDER BY AccessionTime desc;";
            cmd.Parameters.AddWithValue("@AccessionDate", accessionDate);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);            
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByAccessionDate(DateTime accessionDate, List<int> panelSetIdList)
        {
            string panelSetIdString = Business.Helper.IdListHelper.ToIdString(panelSetIdList);
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE a.AccessionDate = @AccessionDate " +
                "And pso.PanelSetId in (" + panelSetIdString + ")" +
                "ORDER BY AccessionTime desc;";
            cmd.Parameters.AddWithValue("@AccessionDate", accessionDate);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByPatientInfo(string lastName, string firstName, DateTime dateOfBirth)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                $"WHERE a.PFirstName = '{firstName}' and a.PLastName = '{lastName}' and a.PBirthdate = '{dateOfBirth.ToString("yyyyMMdd")}' " +                
                "ORDER BY AccessionTime desc;";            
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetRecentCOVIDOrders()
        {            
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE a.AccessionDate >= Date_Add(a.AccessionDate, interval -30 DAY) " +
                "And pso.PanelSetId in ('400')" +
                "ORDER BY AccessionTime desc;";            
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByNoADT(DateTime accessionDate)
        {            
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, a.SVHMedicalRecord, a.PatientId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE a.AccessionDate = @AccessionDate " +        
                "ORDER BY AccessionTime desc;";
            cmd.Parameters.AddWithValue("@AccessionDate", accessionDate);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetPossibleRetrospectiveReviews(DateTime finalDate)
        {            
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.FinalDate = @FinalDate and pso.PanelSetId = 13 " +
                "and exists (select null from tblPanelSetOrderCPTCode where reportNo = pso.ReportNo and cptCode = '88305') " +
                "and exists (select null from tblSpecimenOrder where masterAccessionNo = a.MasterAccessionNo and LOWER(description) REGEXP 'esophagus|ge junction|gastroesophageal junction|stomach|small bowel|duodenum|jejunum|ampulla|common bile duct|ileum|terminal ileum|ileocecal valve|cecum|colon|rectum|anus|cervix|endocervix|endometrium|vagina|vulva|perineum|labia majora|labia minora|ovary|fallopian tube|skin|lung|bronchus|larynx|vocal cord|oral mucosa|oral cavity|tongue|gingiva|pharynx|epiglottis|kidney|nasopharynx|oropharynx|peritoneum|pleura|tonsil|trachea|ureter|urethra|bladder') " +
                "ORDER BY AccessionTime desc;";
            cmd.Parameters.AddWithValue("@FinalDate", finalDate);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.UI.RetrospectiveReviewList GetRetrospectiveReviews(DateTime finalDate)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT a.MasterAccessionNo, pso.ReportNo, pso.PanelSetId, pso.OrderDate, pso.FinalDate, pso.PanelSetName, psos.ReportNo as SurgicalReportNo, sus.DisplayName as SurgicalFinaledBy, psos.FinalDate as SurgicalFinalDate " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblPanelSetOrder psos on a.MasterAccessionNo = psos.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "join tblSystemUser sus on psos.FinaledById = sus.UserId " +
                "WHERE pso.PanelSetId = 262 and psos.PanelSetId = 13 " +
                "and exists (select null from tblPanelSetOrder where MasterAccessionNo = a.MasterAccessionNo and PanelSetId = 13 and FinalDate = @FinalDate) " +
	            "ORDER BY AccessionTime desc; ";
            cmd.Parameters.AddWithValue("@FinalDate", finalDate);

            UI.RetrospectiveReviewList result = new UI.RetrospectiveReviewList();
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        UI.RetrospectiveReviewListItem item = new UI.RetrospectiveReviewListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(item, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(item);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.UI.RetrospectiveReviewList GetRetrospectiveReviewKillList()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT a.MasterAccessionNo, pso.ReportNo, pso.PanelSetId, pso.FinalDate, pso.PanelSetName, psos.ReportNo as SurgicalReportNo, " +
                "sus.DisplayName as SurgicalFinaledBy, psos.FinalDate as SurgicalFinalDate " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblPanelSetOrder psos on a.MasterAccessionNo = psos.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "join tblSystemUser sus on psos.FinaledById = sus.UserId " +
                "WHERE pso.PanelSetId = 262 and psos.PanelSetId = 13 " +
                "and pso.Final = 0 and pso.ExpectedFinalTime <= curdate() " +
                "ORDER BY AccessionTime desc; ";            

            UI.RetrospectiveReviewList result = new UI.RetrospectiveReviewList();
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        UI.RetrospectiveReviewListItem item = new UI.RetrospectiveReviewListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(item, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(item);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByMasterAccessionNo(string masterAccessionNo)
		{
            MySqlCommand cmd = new MySqlCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "SELECT a.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a " +
                "Left outer JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE a.MasterAccessionNo = @MasterAccessionNo;";
            cmd.Parameters.AddWithValue("@MasterAccessionNo", masterAccessionNo);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
		}

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByAliquotOrderId(string aliquotOrderId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
               "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
               "'' ForeignAccessionNo, pso.IsPosted " +
               "FROM tblAccessionOrder a " +
               "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
               "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
               "WHERE a.MasterAccessionNo in " +
               "(Select MasterAccessionNo from tblSpecimenOrder so join tblAliquotOrder ao on so.SpecimenOrderId = ao.SpecimenOrderId " +
               "where ao.AliquotOrderId = @AliquotOrderId);";
            cmd.Parameters.AddWithValue("@AliquotOrderId", aliquotOrderId);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByITAudit(YellowstonePathology.Business.Test.ITAuditPriorityEnum itAuditPriority)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT a.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a " +
                "Left outer JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer JOIN tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE a.ITAuditRequired = 1 and a.ITAudited = 0 and a.ITAuditPriority = @ITAuditPriority;";
            cmd.Parameters.AddWithValue("@ITAuditPriority", (int)itAuditPriority);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByDateRange(List<object> parameters)
		{
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "gwAccessionOrderListByCurrentMonthFill_2";
            cmd.Parameters.AddWithValue("StartDate", parameters[0]);
            cmd.Parameters.AddWithValue("EndDate", parameters[1]);
            cmd.Parameters.AddWithValue("PanelSetId", parameters[2]);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
		}

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByReportNo(List<object> parameters)
		{
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "gwAccessionOrderListByAccessionNoFill_2";
            cmd.Parameters.AddWithValue("ReportNo", parameters[0]);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
		}

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByNotDistributed(List<object> parameters)
		{
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "gwAccessionOrderListByUnDistributedFill_2";
            cmd.Parameters.AddWithValue("PanelId", parameters[0]);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
		}

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByNotFinalPanelId(List<object> parameters)
		{
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "gwAccessionOrderListByNotFinalFill_2";
            cmd.Parameters.AddWithValue("PanelId", parameters[0]);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
		}

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByInHouseMolecularPending()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "gwAccessionOrderListByInHouseMolecularPending_2";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByPatientName(List<object> parameters)
		{
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pReportSearchListByPatientName_2";
            cmd.Parameters.AddWithValue("PLastName", parameters[0].ToString());
            if (parameters[1] == null) cmd.Parameters.AddWithValue("PFirstName", DBNull.Value);
            else cmd.Parameters.AddWithValue("PFirstName", parameters[1].ToString());
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
		}

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByMasterAccessionNo(List<object> parameters)
		{
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "gwAccessionOrderListByMasterAccessionNoFill_2";
            cmd.Parameters.AddWithValue("MasterAccessionNo", parameters[0]);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
		}

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByNotAudited(List<object> parameters)
		{
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "gwAccessionOrderListByNotAudited_2";
            cmd.Parameters.AddWithValue("CaseType", parameters[0]);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
		}

		public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByPatientId(List<object> parameters)
		{
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "gwAccessionOrderListByPatientId_2";
            cmd.Parameters.AddWithValue("PatientId", parameters[0].ToString());
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
		}

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByPanelSetId(List<object> parameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "gwAccessionOrderListByPanelSetId_4";
            cmd.Parameters.AddWithValue("PanelSetId", parameters[0]);
            cmd.Parameters.AddWithValue("AccessionDate", parameters[1]);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByTest(int panelSetId, DateTime startDate, DateTime endDate)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.PanelSetId  =  @PanelSetId " +
                "and pso.OrderDate between @StartDate and @EndDate;";
            cmd.Parameters.AddWithValue("@PanelSetId", panelSetId);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }        

        public static Test.ThinPrepPap.AcidWashList GetAcidWashList(DateTime startDate)
        {
            Test.ThinPrepPap.AcidWashList result = new Test.ThinPrepPap.AcidWashList();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select pso.MasterAccessionNo, po.ReportNo, po.OrderTime, po.Accepted, a.PLastName, a.PFirstName, a.PMiddleInitial, po.Comment, su.UserName " +
                "from tblPanelOrder po " +
                "join tblSystemUser su on po.OrderedById = su.UserId " +
                "join tblPanelSetOrder pso on po.ReportNo = pso.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo where po.PanelId = 39 and po.OrderTime >= @StartDate " +
                "order by po.OrderTime Desc;";

            cmd.Parameters.AddWithValue("@StartDate", startDate);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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

        public static YellowstonePathology.Business.Search.ReportSearchList BuildReportSearchList(MySqlCommand cmd)
        {
            Search.ReportSearchList result = new Search.ReportSearchList();

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
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

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByInvalidFinal()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.Final = 1 and pso.FinaledById = 0 and pso.Signature is null order by pso.PanelSetId, pso.ReportNo;";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByPendingTests()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.Final = 0 and pso.ExpectedFinalTime < curdate() order by pso.PanelSetId, pso.ReportNo;";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByClient(string clientId, DateTime startDate, DateTime endDate)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE a.clientId = '" + clientId + "' and a.AccessionDate >= '" + startDate.ToString("yyyy-MM-dd") + "' and a.AccessionDate <= '" + endDate.ToString("yyyy-MM-dd") + "' order by pso.PanelSetId, pso.ReportNo;";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByBillingDelayed()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, pso.IsPosted, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.BillingDelayed = 1;";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByDistributionDelayed()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, pso.IsPosted, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.DistributionDelayed = 1;";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListBySVHNotFinalMultipleOrders()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT a.ITAudited, pso.MasterAccessionNo, pso.HoldDistribution, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, pso.IsPosted, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo " +
                "from tblAccessionOrder a " +                
                "join tblPanelSetOrder pso on a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "where pso.final = 0 and patientId in " +
                "(select ao.PatientId " +
                "from tblAccessionOrder ao " +
                "join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.masterAccessionNo " +
                "where pso.final = 0 and ao.ClientId in (select clientId from tblClientGroupClient where clientGroupId = 1)  " +
                "group by ao.PatientId having count(*) > 1) order by a.PatientId;";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByProvationFinal(DateTime finalDate)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct a.ITAudited, pso.MasterAccessionNo, pso.HoldDistribution, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, pso.IsPosted, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, null as OrderedBy, " +
                "'' ForeignAccessionNo " +
                "from tblAccessionOrder a " +                
                "join tblPanelSetOrder pso on a.MasterAccessionNo = pso.masterAccessionNo " +
                "where a.clientId in (54) " +
                //$"and a.AccessionDate >= '{finalDate.ToString("yyyy-MM-dd")}';";
                $"and pso.FinalDate = '{finalDate.ToString("yyyy-MM-dd")}';";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }

        public static YellowstonePathology.Business.Search.ReportSearchList GetReportSearchListByNMHFinal(DateTime finalDate)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct a.ITAudited, pso.MasterAccessionNo, pso.HoldDistribution, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, pso.IsPosted, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, null as OrderedBy, " +
                "'' ForeignAccessionNo " +
                "from tblAccessionOrder a " +                
                "join tblPanelSetOrder pso on a.MasterAccessionNo = pso.masterAccessionNo " +
                $"where a.clientid in (select clientId from tblClientGroupClient where clientgroupid = 42) and a.AccessionDate >= '2023-06-01' order by a.accessiontime desc;";
            Search.ReportSearchList reportSearchList = BuildReportSearchList(cmd);
            return reportSearchList;
        }
    }
}
