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
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace YellowstonePathology.UI.Billing
{    
    public partial class SVHBillingDataImportDialog : Window, INotifyPropertyChanged
    {
        private const string ImportFolderPath = @"\\ypiiinterface1\FTPData\SVBBilling";
        private const string ProcessedFolderPath = @"\\ypiiinterface1\FTPData\SVBBilling\Processed";

        public event PropertyChangedEventHandler PropertyChanged;
        private YellowstonePathology.Business.Patient.Model.SVHImportFolder m_SVHImportFolder;
		private DateTime m_ImportForDate;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        public SVHBillingDataImportDialog()
        {
			this.m_ImportForDate = DateTime.Today;
            this.m_SystemIdentity = Business.User.SystemIdentity.Instance;
            
            this.m_SVHImportFolder = new Business.Patient.Model.SVHImportFolder();                    
            InitializeComponent();
            this.DataContext = this;
        }

        public Business.Patient.Model.SVHImportFolder SVHImportFolder
        {
            get { return this.m_SVHImportFolder; }
        }

		public DateTime ImportForDate
		{
			get { return this.m_ImportForDate; }
			set { this.m_ImportForDate = value; }
		}
        
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
            this.Close();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void HyperLinkOpenImportFolder_Click(object sender, RoutedEventArgs e)
        {            
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", ImportFolderPath);
            p.StartInfo = info;
            p.Start(); 
        }

        private void ButtonStartImport_Click(object sender, RoutedEventArgs e)
        {            
            this.m_SVHImportFolder.Process(this.m_ImportForDate, this.m_SystemIdentity);
            YellowstonePathology.Business.Gateway.BillingGateway.UpdateAccessionBillingInformationFromSVHBillingData(this.m_ImportForDate);
            this.m_SVHImportFolder = new Business.Patient.Model.SVHImportFolder();
            this.NotifyPropertyChanged("SVHImportFolder");
			MessageBox.Show("The files have been processed.");
        }		    
    }
}
