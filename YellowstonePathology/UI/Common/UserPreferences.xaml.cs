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
using System.ComponentModel;

namespace YellowstonePathology.UI.Common
{
    public partial class UserPreferences : Window, INotifyPropertyChanged
	{
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.Business.User.UserPreference m_UserPreference;
		private YellowstonePathology.Business.Twain.Twain m_Twain;
		private YellowstonePathology.Business.Common.PageScannerCollection m_PageScannerCollection;
        private YellowstonePathology.Business.Facility.Model.FacilityCollection m_FacilityCollection;
        private List<YellowstonePathology.Business.User.UserPreference> m_UserPreferenceList;

        private YellowstonePathology.Business.Label.Model.LabelFormatCollection m_MolecularLabelFormatCollection;
        private System.Printing.PrintQueueCollection m_PrintQueueCollection;
		private YellowstonePathology.Business.ApplicationVersion m_ApplicationVersion;
        private Business.Label.Model.CassettePrinterCollection m_CassettePrinterCollection;
        private bool m_HostNameEnabled;

        public UserPreferences(YellowstonePathology.Business.User.UserPreference userPreference)
		{
            this.m_UserPreferenceList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAllUserPreferences();
            this.m_MolecularLabelFormatCollection = YellowstonePathology.Business.Label.Model.LabelFormatCollection.GetMolecularLabelCollection();
            this.m_CassettePrinterCollection = new Business.Label.Model.CassettePrinterCollection();
            this.m_FacilityCollection = Business.Facility.Model.FacilityCollection.Instance;

            System.Printing.LocalPrintServer printServer = new System.Printing.LocalPrintServer();            
            this.m_PrintQueueCollection = printServer.GetPrintQueues(new[] { System.Printing.EnumeratedPrintQueueTypes.Local, System.Printing.EnumeratedPrintQueueTypes.Connections });

			this.m_ApplicationVersion = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.GetApplicationVersion(this);			            

			InitializeComponent();

            if (userPreference == null)
            {
                this.m_UserPreference = new Business.User.UserPreference();
                this.m_HostNameEnabled = true;
            }
            else
            {
                this.m_UserPreference = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullUserPreference(userPreference.HostName, this);
                this.m_HostNameEnabled = false;
            }

            this.m_PageScannerCollection = new Business.Common.PageScannerCollection();
            this.DataContext = this;
			this.Loaded += new RoutedEventHandler(UserPreferences_Loaded);
            this.Closing += UserPreferences_Closing;
		}

        private void UserPreferences_Closing(object sender, CancelEventArgs e)
        {
            if (this.DialogResult.HasValue && this.DialogResult == true)
            {
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
            }
            YellowstonePathology.Business.User.UserPreferenceInstance.Instance.Refresh();
        }

        private void UserPreferences_Loaded(object sender, RoutedEventArgs e)
		{
            this.m_Twain = new Business.Twain.Twain(new WpfWindowMessageHook(Window.GetWindow(this)));            
        }    
        
        public Business.Label.Model.CassettePrinterCollection CassettePrinterCollection
        {
            get { return this.m_CassettePrinterCollection; }
        }    

        public YellowstonePathology.Business.Label.Model.LabelFormatCollection MolecularLabelFormatCollection
        {
            get { return this.m_MolecularLabelFormatCollection; }
        }

        public YellowstonePathology.Business.Facility.Model.FacilityCollection FacilityCollection
        {
            get { return this.m_FacilityCollection; }
        }

        public YellowstonePathology.Business.User.UserPreference UserPreference
        {
            get { return this.m_UserPreference; }
        }

        public System.Printing.PrintQueueCollection PrintQueueCollection
        {
            get { return this.m_PrintQueueCollection; }
        }

		public YellowstonePathology.Business.Common.PageScannerCollection PageScannerCollection
		{
			get { return this.m_PageScannerCollection; }
			private set
			{
				this.m_PageScannerCollection = value;
				this.NotifyPropertyChanged("PageScannerCollection");
			}
		}

		public string CurrentVersion
		{
			get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
		}

		public YellowstonePathology.Business.ApplicationVersion ApplicationVersion
		{
			get { return this.m_ApplicationVersion; }
		}

        public bool HostNameEnabled
        {
            get { return this.m_HostNameEnabled; }
        }

        private bool CanSave()
        {
            bool result = true;
            if (string.IsNullOrEmpty(this.m_UserPreference.HostName) == true)
            {
                result = false;
            }
            else
            {
                foreach (Business.User.UserPreference userPreference in this.m_UserPreferenceList)
                {
                    if (this.m_UserPreference.HostName == userPreference.HostName
                        && this.m_UserPreference.UserPreferenceId != userPreference.UserPreferenceId
                        )
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
            if(this.CanSave() == true)
            {
                Business.User.UserPreference userPreference = this.m_UserPreferenceList.SingleOrDefault(item => item.UserPreferenceId == this.m_UserPreference.UserPreferenceId);
                if(userPreference == null)
                {
                    this.m_UserPreference.UserPreferenceId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(this.m_UserPreference, this);
                }
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Enter a unique Host Name.");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
            this.DialogResult = false;
			this.Close();
		}		

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

		private void ComboBoxFacility_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			/*if (this.ComboBoxFacility.SelectedItem != null)
			{                
				YellowstonePathology.Business.Facility.Model.Facility facility = (YellowstonePathology.Business.Facility.Model.Facility)this.ComboBoxFacility.SelectedItem;
                this.m_LocationCollection = facility.Locations;
                this.NotifyPropertyChanged("LocationCollection");
			}*/
		}				

        private void ButtonAlertWaveFileNameBrowse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".wav";
            dlg.Filter = "WAV Files (*.wav)|*.wav";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                this.m_UserPreference.AlertWaveFileName = filename;
            }
        }

        private void ButtonSlideMatePrinterPathBrowse_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.Description = "Custom Description";

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.m_UserPreference.SlideMatePrinterPath = fbd.SelectedPath;
                this.NotifyPropertyChanged("SlideMatePrinterPath");
            }
        }
    }
}
