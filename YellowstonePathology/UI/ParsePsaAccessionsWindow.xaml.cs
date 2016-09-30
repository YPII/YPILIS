using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for ParsePsaAccessionsWindow.xaml
    /// </summary>
    public partial class ParsePsaAccessionsWindow : Window
    {
        public ParsePsaAccessionsWindow()
        {
            InitializeComponent();
        }

        private void ButtonParse_Click(object sender, RoutedEventArgs e)
        {
            string text = this.TextBoxText.Text;

            string[] rows = text.Split('\n');
            StringBuilder insertStatements = new StringBuilder();

            char[] delimeters = new char[2];
            delimeters[0] = '\t';
            delimeters[1] = ' ';            

            List<PsaImport> importList = new List<PsaImport>();
            foreach(string row in rows)
            {
                string [] cols = row.Split(delimeters);
                if (cols.Length == 2)
                {
                    if (string.IsNullOrEmpty(cols[0]) == false)
                    {
                        if(cols[0] != "NO ACC" && cols[0] != ".NO-" && cols[0] != "NO")
                        {
                            string reportNo = cols[0].Insert(2, "-");
                            int indexOfFirstChar = this.FindFirstLetter(reportNo);
                            reportNo = reportNo.Insert(indexOfFirstChar, ".");
                            DateTime postDate = DateTime.Parse(cols[1].Trim());
                            importList.Add(new PsaImport(reportNo, postDate));
                            string insert = "Insert tblPsaImport (ReportNo, PostDate) values ('" + reportNo + "', '" + postDate.ToShortDateString() + "');";
                            insertStatements.AppendLine(insert);
                        }						                        
                    }
                }
                else
                {
                    Console.WriteLine("Row Not valid: " + row);
                }
            }
            this.TextBoxText.Text = insertStatements.ToString();
        }

        private int FindFirstLetter(string str)
        {
            for (int ctr = 0; ctr < str.Length; ctr++)
            {
                if (Char.IsLetter(str[ctr]))
                {
                    return ctr;
                }
            }
            return -1;
        }
    }    

    public class PsaImport
    {
        private string m_ReportNo;
        private DateTime m_PostDate;

        public PsaImport(string reportNo , DateTime postDate)
        {
            this.m_ReportNo = reportNo;
            this.m_PostDate = postDate;
        }

        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }

        public DateTime PostDate
        {
            get { return this.m_PostDate; }
            set { this.m_PostDate = value; }
        }
    }
}
