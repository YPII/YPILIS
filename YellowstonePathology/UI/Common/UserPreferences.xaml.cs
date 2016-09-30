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
        private YellowstonePathology.Business.Facility.Model.LocationCollection m_LocationCollection;		

        private YellowstonePathology.Business.Label.Model.LabelFormatCollection m_MolecularLabelFormatCollection;
        private System.Printing.PrintQueueCollection m_PrintQueueCollection;
		private YellowstonePathology.Business.ApplicationVersion m_ApplicationVersion;
        private YellowstonePathology.Business.Surgical.ProcessorRunCollection m_ProcessorRunCollection;

		public UserPreferences()
		{
            this.m_MolecularLabelFormatCollection = YellowstonePathology.Business.Label.Model.LabelFormatCollection.GetMolecularLabelCollection();        
			this.m_UserPreference = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference;			

            this.m_FacilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();
            this.m_LocationCollection = new Business.Facility.Model.LocationCollection();

            System.Printing.LocalPrintServer printServer = new System.Printing.LocalPrintServer();            
            this.m_PrintQueueCollection = printServer.GetPrintQueues(new[] { System.Printing.EnumeratedPrintQueueTypes.Local, System.Printing.EnumeratedPrintQueueTypes.Connections });

			this.m_ApplicationVersion = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.GetApplicationVersion(this);			
            this.m_ProcessorRunCollection = Business.Surgical.ProcessorRunCollection.GetAll(true);

			InitializeComponent();            

			this.DataContext = this;
			this.Loaded += new RoutedEventHandler(UserPreferences_Loaded);
            this.Closing += UserPreferences_Closing;
		}

        private void UserPreferences_Closing(object sender, CancelEventArgs e)
        {
            if(this.DialogResult == true)
            {
                YellowstonePathology.Business.User.UserPreferenceInstance.Instance.Save();
            }
        }

        private void UserPreferences_Loaded(object sender, RoutedEventArgs e)
		{
            this.m_Twain = new Business.Twain.Twain(new WpfWindowMessageHook(Window.GetWindow(this)));
            if(Environment.OSVersion.VersionString != "Microsoft Windows NT 6.2.9200.0")
            {
                this.PageScannerCollection = new Business.Common.PageScannerCollection(this.m_Twain);
            }
            else
            {
                this.m_PageScannerCollection = new Business.Common.PageScannerCollection();
                MessageBox.Show("Unable to initialize the TWAIN Scanner Collection.");
            }
			
		}

        public YellowstonePathology.Business.Surgical.ProcessorRunCollection ProcessorRunCollection
        {
            get { return this.m_ProcessorRunCollection; }
        }

        public YellowstonePathology.Business.Label.Model.LabelFormatCollection MolecularLabelFormatCollection
        {
            get { return this.m_MolecularLabelFormatCollection; }
        }

        public YellowstonePathology.Business.Facility.Model.FacilityCollection FacilityCollection
        {
            get { return this.m_FacilityCollection; }
        }

        public YellowstonePathology.Business.Facility.Model.LocationCollection LocationCollection
        {
            get { return this.m_LocationCollection; }
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

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
            this.DialogResult = true;
            this.Close();
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
            this.DialogResult = false;
			this.Close();
		}

		private void ButtonTestBlockPrinter_Click(object sender, RoutedEventArgs e)
		{            
			YellowstonePathology.Business.Common.Block block = new Business.Common.BlockV1();
			block.CassetteColumn = 3;
			block.ReportNo = "XXX-9999";
			block.BlockTitle = "XX";
			block.PatientInitials = "XX";
			block.BlockId = "999999";
			block.PrintRequested = true;

			YellowstonePathology.Business.Common.BlockCollection blockCollection = new Business.Common.BlockCollection();
			blockCollection.Add(block);
			YellowstonePathology.Business.Common.PrintMate.Print(blockCollection);            
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
			if (this.ComboBoxFacility.SelectedItem != null)
			{                
				YellowstonePathology.Business.Facility.Model.Facility facility = (YellowstonePathology.Business.Facility.Model.Facility)this.ComboBoxFacility.SelectedItem;
                this.m_LocationCollection = facility.Locations;
                this.NotifyPropertyChanged("LocationCollection");
			}
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
