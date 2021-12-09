using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.Data;
using MySql.Data.MySqlClient;
using System.Reflection;
using System.IO;


namespace YellowstonePathology.UI.ManagementReports
{    
    public partial class DepartmentOfHealthDailyCOVID : UserControl
    {
        private ReportSelection m_ReportSelection;
        private DateTime m_SelectedDate;

        public DepartmentOfHealthDailyCOVID(ReportSelection reportSelection)
        {
            this.m_SelectedDate = DateTime.Today;
            this.m_ReportSelection = reportSelection;

            InitializeComponent();
            this.DataContext = this;
        }
        public void CreateReport()
        {
            using (var package = new ExcelPackage())
            {
                package.Workbook.Properties.Title = "Department Of Health COVID Daily Cases";
                package.Workbook.Properties.Author = "Sid Harder";
                package.Workbook.Properties.Comments = "Dailyl Report";

                string sql = File.ReadAllText(@"\\fileserver\YPI Shared Documents\Management Reports\COVID\DepartmentOfHealthDailyCOVIDCases\query.sql");
                MySqlCommand cmd = new MySqlCommand(sql);
                cmd.CommandType = CommandType.Text;                
                cmd.Parameters.AddWithValue("@FinalDate", this.m_SelectedDate);

                using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
                {
                    cn.Open();
                    cmd.Connection = cn;
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        var worksheet = package.Workbook.Worksheets.Add("Today");
                        for(int i=0; i<dr.FieldCount; i++)
                        {
                            worksheet.Cells[1, i+1].Value = dr.GetName(i);
                            worksheet.Cells[1, i+1].Style.Font.Bold = true;
                        }

                        int r = 2;
                        while (dr.Read())
                        {
                            for (int c = 0; c < dr.FieldCount; c++)
                            {
                                worksheet.Cells[r, c+1].Value = dr.GetValue(c);
                                worksheet.Cells[r, c+1].Style.Font.Bold = false;
                            }
                            r += 1;
                        }

                        worksheet.Cells.AutoFitColumns();                        

                        int endRowNumber = worksheet.Dimension.End.Row;
                        if(endRowNumber > 1)
                        {
                            string columnC = "C2:C" + endRowNumber.ToString();
                            worksheet.Cells[columnC].Style.Numberformat.Format = "yyyy-mm-dd";

                            string columnAD = "AD2:AD" + endRowNumber.ToString();
                            worksheet.Cells[columnAD].Style.Numberformat.Format = "yyyy-mm-dd";

                            string columnAG = "AG2:AG" + endRowNumber.ToString();
                            worksheet.Cells[columnAG].Style.Numberformat.Format = "yyyy-mm-dd";

                            string columnAB = "AB2:AB" + endRowNumber.ToString();
                            worksheet.Cells[columnAB].Style.Numberformat.Format = "yyyy-mm-dd";

                            string columnAL = "AL2:AL" + endRowNumber.ToString();
                            worksheet.Cells[columnAL].Style.Numberformat.Format = "yyyy-mm-dd";
                        }                        
                    }
                }                                             
               
                string path = $@"\\fileserver\YPI Shared Documents\Management Reports\COVID\DepartmentOfHealthDailyCOVIDCases\{DateTime.Now.ToString("MM-dd-yyy_HH-mm-ss")}.xlsx";
                System.IO.Stream stream = System.IO.File.Create(path);
                package.SaveAs(stream);
                stream.Close();
            }
            this.m_ReportSelection.BuilDocumentList();
            
        }

        public DateTime SelectedDate
        {
            get { return this.m_SelectedDate; }
            set { this.m_SelectedDate = value; }
        }

        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
            this.CreateReport();
        }
    }
}
