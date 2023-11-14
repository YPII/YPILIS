using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Reflection;
using System.IO;

namespace YellowstonePathology.Business.Gateway
{
    public class MangementReportsGateway
    {
        public static UI.ManagementReports.ManagementReportCollection GetDepartmentOfHealthDailyCOVID(DateTime reportDate)
        {
            UI.ManagementReports.ManagementReportCollection result = new UI.ManagementReports.ManagementReportCollection();
            
            string sql = File.ReadAllText(@"\\fileserver\YPI Shared Documents\Management Reports\COVID\DepartmentOfHealthDailyCOVIDCases\query.sql");
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ReportDate", reportDate);

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
                }
            }            
                        
            return result;
        }
    }
}
