using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.User
{
    [PersistentClass("tblUserPreference", "YPIDATA")]
	public class UserPreference : INotifyPropertyChanged
    {
        protected delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_UserPreferenceId;
        private string m_HostName;
        private bool m_BarcodeScanEnabled;
        private string m_BarcodeScanPort;
        private Nullable<DateTime> m_LastLocalDataUpdate;
		private string m_LockMode;
        private string m_ContainerLabelPrinter;
        private string m_HistologySlideLabelPrinter;
		private string m_CytologySlideLabelPrinter;
		private string m_RequisitionPrinter;
		private string m_CassettePrinter;		
		private string m_SpecialStainAcknowledgementPrinter;
		private string m_StartupPage;
		private string m_MolecularLabelPrinter;
        private string m_MolecularLabelFormat;
		private string m_PageScanner;
        private string m_FacilityId;
		private bool m_ActivateNotificationAlert;
        private string m_AlertWaveFileName;
        private string m_AcknowledgeTasksFor;
        private string m_TecanImportExportPath;
        private string m_SlideMatePrinterPath;
		private string m_WeekdayProcessorRunId;
		private string m_WeekendProcessorRunId;
        private string m_CytologySlidePrinter;
        private string m_LastReportNo;
        private string m_ThermoFisherSlidePrinter;
        private string m_LaserCassettePrinter;
        private bool m_UseLaserCassettePrinter;
        private Nullable<int> m_GPathologistId;
        private string m_FedExLabelPrinter;
        private bool m_Administrator;

        public UserPreference()
        {

        }

        public bool IsFacilitySet()
        {
            bool result = true;
            if (string.IsNullOrEmpty(this.m_FacilityId) == true) result = false;
            return result;
        }

        [PersistentPrimaryKeyProperty(false)]
        [PersistentDataColumnProperty(false, "200", "null", "varchar")]
        public string HostName
        {
            get { return this.m_HostName; }
            set
            {
                if (this.m_HostName != value)
                {
                    this.m_HostName = value;
                    this.NotifyPropertyChanged("HostName");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "50", "null", "varchar")]
        public string UserPreferenceId
        {
            get { return this.m_UserPreferenceId; }
            set
            {
                if (this.m_UserPreferenceId != value)
                {
                    this.m_UserPreferenceId = value;
                    this.NotifyPropertyChanged("UserPreferenceId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool BarcodeScanEnabled
        {
            get { return this.m_BarcodeScanEnabled; }
            set
            {
                if (this.m_BarcodeScanEnabled != value)
                {
                    this.m_BarcodeScanEnabled = value;
                    this.NotifyPropertyChanged("BarcodeScanEnabled");					
				}
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "'COM3'", "varchar")]
        public string BarcodeScanPort
        {
            get { return this.m_BarcodeScanPort; }
            set 
            {
                if (this.m_BarcodeScanPort != value)
                {                    
                    this.m_BarcodeScanPort = value;
                    this.NotifyPropertyChanged("BarcodeScanPort");					
				}
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> LastLocalDataUpdate
        {
            get { return this.m_LastLocalDataUpdate; }
            set
            {
                if (this.m_LastLocalDataUpdate != value)
                {
                    this.m_LastLocalDataUpdate = value;
                    this.NotifyPropertyChanged("LastLocalDataUpdate");					
				}
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "'Always Attempt Lock'", "varchar")]
		public string LockMode
		{
			get { return this.m_LockMode ; }
			set
			{
				if (this.m_LockMode != value)
				{
					this.m_LockMode = value;
					this.NotifyPropertyChanged("LockMode");					
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ContainerLabelPrinter
        {
            get { return this.m_ContainerLabelPrinter; }
            set
            {
                if (this.m_ContainerLabelPrinter != value)
                {
                    this.m_ContainerLabelPrinter = value;
                    this.NotifyPropertyChanged("ContainerLabelPrinter");					
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string HistologySlideLabelPrinter
        {
			get { return this.m_HistologySlideLabelPrinter; }
            set
            {
				if (this.m_HistologySlideLabelPrinter != value)
                {
					this.m_HistologySlideLabelPrinter = value;
					this.NotifyPropertyChanged("HistologySlideLabelPrinter");					
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string CytologySlideLabelPrinter
		{
			get { return this.m_CytologySlideLabelPrinter; }
			set
			{
				if (this.m_CytologySlideLabelPrinter != value)
				{
					this.m_CytologySlideLabelPrinter = value;
					this.NotifyPropertyChanged("CytologySlideLabelPrinter");					
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string RequisitionPrinter
		{
			get { return this.m_RequisitionPrinter; }
			set
			{
				if (this.m_RequisitionPrinter != value)
				{
					this.m_RequisitionPrinter = value;
					this.NotifyPropertyChanged("RequisitionPrinter");					
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string CassettePrinter
		{
			get { return this.m_CassettePrinter; }
			set
			{
				if (this.m_CassettePrinter != value)
				{
					this.m_CassettePrinter = value;
					this.NotifyPropertyChanged("CassettePrinter");					
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string LaserCassettePrinter
        {
            get { return this.m_LaserCassettePrinter; }
            set
            {
                if (this.m_LaserCassettePrinter != value)
                {
                    this.m_LaserCassettePrinter = value;
                    this.NotifyPropertyChanged("LaserCassettePrinter");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string SpecialStainAcknowledgementPrinter
        {
            get { return this.m_SpecialStainAcknowledgementPrinter; }
            set
            {
                if (this.m_SpecialStainAcknowledgementPrinter != value)
                {
                    this.m_SpecialStainAcknowledgementPrinter = value;
                    this.NotifyPropertyChanged("SpecialStainAcknowledgementPrinter");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string StartupPage
		{
			get { return this.m_StartupPage; }
			set
			{
				if (this.m_StartupPage != value)
				{
					this.m_StartupPage = value;
					this.NotifyPropertyChanged("StartupPage");					
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string MolecularLabelPrinter
		{
			get { return this.m_MolecularLabelPrinter; }
			set
			{
				if (this.m_MolecularLabelPrinter != value)
				{
					this.m_MolecularLabelPrinter = value;
					this.NotifyPropertyChanged("MolecularLabelPrinter");					
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string MolecularLabelFormat
        {
            get { return this.m_MolecularLabelFormat; }
            set
            {
                if (this.m_MolecularLabelFormat != value)
                {
                    this.m_MolecularLabelFormat = value;
                    this.NotifyPropertyChanged("MolecularLabelFormat");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string PageScanner
		{
			get { return this.m_PageScanner; }
			set
			{
				if (this.m_PageScanner != value)
				{
					this.m_PageScanner = value;
					this.NotifyPropertyChanged("PageScanner");					
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "50", "null", "varchar")]
        public string FacilityId
        {
            get { return this.m_FacilityId; }
            set
            {
                if (this.m_FacilityId != value)
                {
                    this.m_FacilityId = value;
                    this.NotifyPropertyChanged("FacilityId");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "tinyint")]
		public bool ActivateNotificationAlert
		{
			get { return this.m_ActivateNotificationAlert; }
			set
			{
				if (this.m_ActivateNotificationAlert != value)
				{
					this.m_ActivateNotificationAlert = value;
					this.NotifyPropertyChanged("ActivateNotificationAlert");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string AlertWaveFileName
        {
            get { return this.m_AlertWaveFileName; }
            set
            {
                if (this.m_AlertWaveFileName != value)
                {
                    this.m_AlertWaveFileName = value;
                    this.NotifyPropertyChanged("AlertWaveFileName");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string AcknowledgeTasksFor
        {
            get { return this.m_AcknowledgeTasksFor; }
            set
            {
                if (this.m_AcknowledgeTasksFor != value)
                {
                    this.m_AcknowledgeTasksFor = value;
                    this.NotifyPropertyChanged("AcknowledgeTasksFor");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string TecanImportExportPath
        {
            get { return this.m_TecanImportExportPath; }
            set
            {
                if (this.m_TecanImportExportPath != value)
                {
                    this.m_TecanImportExportPath = value;
                    this.NotifyPropertyChanged("TecanImportExportPath");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string SlideMatePrinterPath
        {
            get { return this.m_SlideMatePrinterPath; }
            set
            {
                if (this.m_SlideMatePrinterPath != value)
                {
                    this.m_SlideMatePrinterPath = value;
                    this.NotifyPropertyChanged("SlideMatePrinterPath");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string WeekdayProcessorRunId
		{
			get { return this.m_WeekdayProcessorRunId; }
			set
			{
				if (this.m_WeekdayProcessorRunId != value)
				{
					this.m_WeekdayProcessorRunId = value;
					this.NotifyPropertyChanged("WeekdayProcessorRunId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string WeekendProcessorRunId
		{
			get { return this.m_WeekendProcessorRunId; }
			set
			{
				if (this.m_WeekendProcessorRunId != value)
				{
					this.m_WeekendProcessorRunId = value;
					this.NotifyPropertyChanged("WeekendProcessorRunId");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string CytologySlidePrinter
        {
            get { return this.m_CytologySlidePrinter; }
            set
            {
                if (this.m_CytologySlidePrinter != value)
                {
                    this.m_CytologySlidePrinter = value;
                    this.NotifyPropertyChanged("CytologySlidePrinter");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string LastReportNo
        {
            get { return this.m_LastReportNo; }
            set
            {
                if (this.m_LastReportNo != value)
                {
                    this.m_LastReportNo = value;
                    this.NotifyPropertyChanged("LastReportNo");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ThermoFisherSlidePrinter
        {
            get { return this.m_ThermoFisherSlidePrinter; }
            set
            {
                if (this.m_ThermoFisherSlidePrinter != value)
                {
                    this.m_ThermoFisherSlidePrinter = value;
                    this.NotifyPropertyChanged("ThermoFisherSlidePrinter");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool UseLaserCassettePrinter
        {
            get { return this.m_UseLaserCassettePrinter; }
            set
            {
                if (this.m_UseLaserCassettePrinter != value)
                {
                    this.m_UseLaserCassettePrinter = value;
                    this.NotifyPropertyChanged("UseLaserCassettePrinter");
                }
            }
        }
        
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "null", "int")]
        public Nullable<int> GPathologistId
        {
            get { return this.m_GPathologistId; }
            set
            {
                if (this.m_GPathologistId != value)
                {
                    this.m_GPathologistId = value;
                    this.NotifyPropertyChanged("GPathologistId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string FedExLabelPrinter
        {
            get { return this.m_FedExLabelPrinter; }
            set
            {
                if (this.m_FedExLabelPrinter != value)
                {
                    this.m_FedExLabelPrinter = value;
                    this.NotifyPropertyChanged("FedExLabelPrinter");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool Administrator
        {
            get { return this.m_Administrator; }
            set
            {
                if (this.m_Administrator != value)
                {
                    this.m_Administrator = value;
                    this.NotifyPropertyChanged("Administrator");
                }
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }	
	}
}
