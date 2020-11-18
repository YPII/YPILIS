using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.UI.COVID
{	
	public class COVIDCaseCollection : ObservableCollection<COVIDCase>
    {
        public const string COVID_TESTING_DIR = @"c:\covid_testing_v2\";
        public const string LC96A_DIR = @"c:\covid_testing_v2\lc96a";
        public const string LC96A_SAMPLE_FILES_DIR = @"c:\covid_testing_v2\lc96a\sample_files\";
        public const string LC96A_RESULT_FILES_DIR = @"c:\covid_testing_v2\lc96a\result_files\";
        public const string LC460A_Dir = @"c:\covid_testing_v2\lc460a";
        public const string LC460A_SAMPLE_FILES_DIR = @"c:\covid_testing_v2\lc460a\sample_files\";
        public const string LC460A_RESULT_FILES_DIR = @"c:\covid_testing_v2\lc460a\result_files\";        

        public COVIDCaseCollection()
		{
            
		}        

        public static COVIDCaseCollection GetRecentCOVIDCases()
        {            
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate, pso.PanelSetId, s.Result, co.ClientOrderId, co.FirstTest, co.EmployedInHealthcare, co.Symptomatic, " +
                "co.DateOfSymptomaticOnset, co.Hospitalized, co.ICU, co.ResidentInCongregateCare, co.Pregnant, co.PRace, co.PEthnicity, a.HighPriority, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted, co.ClinicalHistory, co.specialInstructions " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "join tblClientOrder co on a.MasterAccessionNo = co.MasterAccessionNo " +
                "Join tblSARSCoV2TestOrder s on pso.ReportNo = s.ReportNo " +
                "WHERE a.AccessionDate >= Date_Add(a.AccessionDate, interval - 30 DAY) " +
                "And pso.PanelSetId in ('400') " +
                "ORDER BY a.AccessionTime desc;";
            COVIDCaseCollection result = BuildReportSearchList(cmd);
            return result;
        }

        public static COVIDCaseCollection GetNotRunCOVIDCases()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate, pso.PanelSetId, s.Result, co.ClientOrderId, co.FirstTest, co.EmployedInHealthcare, co.Symptomatic, " +
                "co.DateOfSymptomaticOnset, co.Hospitalized, co.ICU, co.ResidentInCongregateCare, co.Pregnant, co.PRace, co.PEthnicity, a.HighPriority, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted, co.ClinicalHistory, co.specialInstructions " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "join tblClientOrder co on a.MasterAccessionNo = co.MasterAccessionNo " +
                "Join tblSARSCoV2TestOrder s on pso.ReportNo = s.ReportNo " +
                "WHERE a.AccessionDate >= Date_Add(a.AccessionDate, interval - 30 DAY) " +
                "And pso.PanelSetId in ('400') and s.Result is null " +
                "ORDER BY a.HighPriority desc, a.AccessionTime;";
            COVIDCaseCollection result = BuildReportSearchList(cmd);
            return result;
        }

        public static COVIDCaseCollection GetDetectedCases()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate, pso.PanelSetId, s.Result, co.ClientOrderId, co.FirstTest, co.EmployedInHealthcare, co.Symptomatic, " +
                "co.DateOfSymptomaticOnset, co.Hospitalized, co.ICU, co.ResidentInCongregateCare, co.Pregnant, co.PRace, co.PEthnicity, a.HighPriority, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted, co.ClinicalHistory, co.specialInstructions " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "join tblClientOrder co on a.MasterAccessionNo = co.MasterAccessionNo " +
                "Join tblSARSCoV2TestOrder s on pso.ReportNo = s.ReportNo " +
                "WHERE a.AccessionDate >= Date_Add(a.AccessionDate, interval - 30 DAY) " +
                "And pso.PanelSetId in ('400') and s.Result = 'Detected' " +
                "ORDER BY a.AccessionTime desc;";
            COVIDCaseCollection result = BuildReportSearchList(cmd);
            return result;
        }

        public static COVIDCaseCollection GetLateCases()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate, pso.PanelSetId, s.Result, co.ClientOrderId, co.FirstTest, co.EmployedInHealthcare, co.Symptomatic, " +
                "co.DateOfSymptomaticOnset, co.Hospitalized, co.ICU, co.ResidentInCongregateCare, co.Pregnant, co.PRace, co.PEthnicity, a.HighPriority, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalTime, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted, co.ClinicalHistory, co.specialInstructions " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "join tblClientOrder co on a.MasterAccessionNo = co.MasterAccessionNo " +
                "Join tblSARSCoV2TestOrder s on pso.ReportNo = s.ReportNo " +
                "WHERE a.AccessionDate >= Date_Add(a.AccessionDate, interval - 30 DAY) " +
                "And pso.PanelSetId in ('400') and pso.ExpectedFinalTime >= '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' and pso.final = 0 " +
                "ORDER BY a.AccessionTime desc;";
            COVIDCaseCollection result = BuildReportSearchList(cmd);
            return result;
        }

        public COVIDCase GetByReportNo(string reportNo)
        {
            COVIDCase result = null;
            foreach(COVIDCase item in this)
            {
                if(item.ReportNo == reportNo)
                {
                    result = item;
                    break;
                }
            }
            return result;
        }

        public bool ReportNoExists(string reportNo)
        {
            bool result = false;
            foreach (COVIDCase item in this)
            {
                if (item.ReportNo == reportNo)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public static COVIDCaseCollection BuildReportSearchList(MySqlCommand cmd)
        {
            COVIDCaseCollection result = new COVIDCaseCollection();

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        COVIDCase covidCase = new COVIDCase();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter(covidCase, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(covidCase);
                    }
                }
            }
            return result;
        }

        public static void HandleFolderStructure()
        {            
            if (System.IO.Directory.Exists(COVIDCaseCollection.COVID_TESTING_DIR) == false) System.IO.Directory.CreateDirectory(COVIDCaseCollection.COVID_TESTING_DIR);
            if (System.IO.Directory.Exists(COVIDCaseCollection.LC96A_DIR) == false) System.IO.Directory.CreateDirectory(COVIDCaseCollection.LC96A_DIR);
            if (System.IO.Directory.Exists(COVIDCaseCollection.LC96A_SAMPLE_FILES_DIR) == false) System.IO.Directory.CreateDirectory(COVIDCaseCollection.LC96A_SAMPLE_FILES_DIR);
            if (System.IO.Directory.Exists(COVIDCaseCollection.LC96A_RESULT_FILES_DIR) == false) System.IO.Directory.CreateDirectory(COVIDCaseCollection.LC96A_RESULT_FILES_DIR);
            if (System.IO.Directory.Exists(COVIDCaseCollection.LC460A_Dir) == false) System.IO.Directory.CreateDirectory(COVIDCaseCollection.LC460A_Dir);
            if (System.IO.Directory.Exists(COVIDCaseCollection.LC460A_SAMPLE_FILES_DIR) == false) System.IO.Directory.CreateDirectory(COVIDCaseCollection.LC460A_SAMPLE_FILES_DIR);
            if (System.IO.Directory.Exists(COVIDCaseCollection.LC460A_RESULT_FILES_DIR) == false) System.IO.Directory.CreateDirectory(COVIDCaseCollection.LC460A_RESULT_FILES_DIR);            
        }
    }
}
