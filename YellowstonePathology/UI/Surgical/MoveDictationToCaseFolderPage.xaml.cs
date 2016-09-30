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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Surgical
{	
	public partial class MoveDictationToCaseFolderPage : PageFunction<Boolean>
	{
        string m_ReportNo;
        string m_DictationFileName;

        public MoveDictationToCaseFolderPage(string dictationFileName)
		{
            this.m_DictationFileName = dictationFileName;
			InitializeComponent();
			DataContext = this;
            this.Loaded += new RoutedEventHandler(MoveDictationToCaseFolderPage_Loaded);
		}

        private void MoveDictationToCaseFolderPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.TextBoxReportNo.Focus();
        }		

        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }		

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            if (string.IsNullOrEmpty(this.m_ReportNo) == false)
            {
				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_ReportNo.ToUpper());
				string filePath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
                //string dictationFileName = System.IO.Path.GetFileName(this.m_DictationFileName);
                string dictationFileName = System.IO.Path.GetFileName(this.m_ReportNo.ToUpper() + ".dct");
                string newFullPath = System.IO.Path.Combine(filePath, dictationFileName);
                System.IO.File.Copy(this.m_DictationFileName, newFullPath, true);
                Window.GetWindow(this).Close();
            }
            else
            {
                MessageBox.Show("The ReportNo entered is not valid.");
            }
		}

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();            
        }        
	}
}