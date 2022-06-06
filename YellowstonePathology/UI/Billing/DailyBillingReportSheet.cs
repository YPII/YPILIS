using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;

namespace YellowstonePathology.UI.Billing
{
    internal class DailyBillingReportSheet
    {
        public DailyBillingReportSheet(DateTime postDateStart, DateTime postDateEnd, int clientGroupId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select ao.MasterAccessionNo, pso.ReportNo, pso.PanelSetName, ao.ClientId, ao.CollectionDate DateOfService, ao.PFirstName, ao.PLastName, ao.PMiddleInitial, ao.PBirthdate, " +
                "ao.ClientName, ao.PhysicianName, ao.SvhAccount, ao.SvhMedicalRecord, ao.PatientType, ao.PrimaryInsurance, ao.SecondaryInsurance, pso.NoCharge, pso.Ordered14DaysPostDischarge, pso.BillingType, psob.Quantity, psob.CptCode  " +
                "from tblAccessionOrder ao  " +
                "join tblClientGroupClient cgc on ao.ClientId = cgc.ClientId  " +
                "join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo  " +
                "join tblPanelSetOrderCPTCodeBill psob on pso.ReportNo = psob.ReportNo  " +
                "where psob.PostDate between @StartDate and @EndDate and pso.IsBillable = 1 and cgc.ClientGroupId = @ClientGroupId;";

            cmd.Parameters.AddWithValue("StartDate", postDateStart);
            cmd.Parameters.AddWithValue("EndDate", postDateEnd);
            cmd.Parameters.AddWithValue("ClientGroupId", clientGroupId);

            using (var package = new ExcelPackage())
            {
                package.Workbook.Properties.Title = "YPI Client Billing Detail report";
                package.Workbook.Properties.Author = "Sid Harder";
                package.Workbook.Properties.Comments = "Daily Billing Report";                

                using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
                {
                    cn.Open();
                    cmd.Connection = cn;
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var worksheet = package.Workbook.Worksheets.Add("Today");
                            for (int i = 0; i < dr.FieldCount; i++)
                            {
                                worksheet.Cells[1, i + 1].Value = dr.GetName(i);
                                worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                            }

                            int r = 2;
                            while (dr.Read())
                            {
                                for (int c = 0; c < dr.FieldCount; c++)
                                {
                                    worksheet.Cells[r, c + 1].Value = dr.GetValue(c);
                                    worksheet.Cells[r, c + 1].Style.Font.Bold = false;
                                }
                                r += 1;
                            }

                            worksheet.Cells.AutoFitColumns();
                        }
                    }
                }
                
                string path = $@"c:\temp\billing_report.xlsx";
                System.IO.Stream stream = System.IO.File.Create(path);
                package.SaveAs(stream);
                stream.Close();
            }            
        }
    }
}
