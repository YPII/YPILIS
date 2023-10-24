using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.UI
{
    public class ImportADRData
    {
        public ImportADRData()
        {

        }

        public void Run()
        {
            string [] files = System.IO.Directory.GetFiles("d:\\gi");
            foreach(string file in files)
            {
                Microsoft.Office.Interop.Excel.Application xlapp;
                Microsoft.Office.Interop.Excel.Workbook xlworkbook;
                Microsoft.Office.Interop.Excel._Worksheet xlworksheet;

                xlapp = new Microsoft.Office.Interop.Excel.Application();
                xlworkbook = xlapp.Workbooks.Open(file, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlworksheet = ((Microsoft.Office.Interop.Excel.Worksheet)xlworkbook.Sheets[1]);
                xlapp.Visible = true;

                int row = 2;
                while (true)
                {
                    string mrn = (string)(xlworksheet.Cells[row, 1] as Microsoft.Office.Interop.Excel.Range).Value;
                    if (string.IsNullOrEmpty(mrn) == true) break;
                    string providerName = (string)(xlworksheet.Cells[row, 2] as Microsoft.Office.Interop.Excel.Range).Value;
                    string patientName = (string)(xlworksheet.Cells[row, 3] as Microsoft.Office.Interop.Excel.Range).Value;
                    char[] delimeter = { ',' };
                    string [] nameSplit = patientName.Split(delimeter);
                    string firstName = nameSplit[1];
                    string lastName = nameSplit[0];
                    DateTime birthDate = (xlworksheet.Cells[row, 5] as Microsoft.Office.Interop.Excel.Range).Value;
                    DateTime screeningDate = (xlworksheet.Cells[row, 6] as Microsoft.Office.Interop.Excel.Range).Value;
                    string sql = "Insert tblADR (MRN, ProviderName, FirstName, LastName, BirthDate, ScreeningDate) values (";
                    sql = sql + "'" + mrn + "', '" + providerName + "', '" + firstName + "', '" + lastName + "', '" + birthDate.ToString("yyyy-MM-dd") + "', '" + screeningDate.ToString("yyyy-MM-dd") + "');";

                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
                    cmd.CommandText = sql;
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.ExecuteNonQuery();
                    }

                    row += 1;
                }

                xlworkbook.Close();
                xlapp.Quit();
            }
        }
    }
}
