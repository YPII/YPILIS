using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.WebService
{
    [PersistentClass("tblWebServiceAccount", "YPIDATA")]
    public class WebServiceAccount : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private WebServiceAccountClientCollection m_WebServiceAccountClientCollection;

        private int m_WebServiceAccountId;
        private string m_UserName;
        private string m_Password;
        private string m_DisplayName;
        private int m_PrimaryClientId;
        private string m_DownloadFileType;
        private string m_InitialPage;
        private int m_ApplicationTimeoutMinutes;
        private string m_RemoteFileDownloadDirectory;
        private string m_RemoteFileUploadDirectory;
        private string m_AlertEmailAddress;
        private bool m_SaveUserNameLocal;
        private bool m_SavePasswordLocal;
        private bool m_EnableApplicationTimeout;
        private bool m_EnableSaveSettings;
        private bool m_EnableFileUpload;
        private bool m_EnableFileDownload;
        private bool m_EnableOrderEntry;
        private bool m_EnableReportBrowser;
        private bool m_EnableBillingBrowser;
        private bool m_EnableEmailAlert;
        private string m_VersionCurrentlyUsing;
        private int m_SystemUserId;
        private string m_Signature;
        private string m_FacilityId;
        private string m_ObjectId;
        private string m_PublicAddress;

        public WebServiceAccount()
        {
            this.m_WebServiceAccountClientCollection = new WebService.WebServiceAccountClientCollection();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void SetDefaults()
        {
            this.m_DownloadFileType = "XPS";
            this.m_InitialPage = "ReportBrowser";
            this.m_ApplicationTimeoutMinutes = 40;
            this.m_RemoteFileDownloadDirectory = null;
            this.m_RemoteFileUploadDirectory = null;
            this.m_AlertEmailAddress = null;
            this.m_SaveUserNameLocal = true;
            this.m_SavePasswordLocal = true;
            this.m_EnableApplicationTimeout = true;
            this.m_EnableSaveSettings = true;
            this.m_EnableFileUpload = false;
            this.m_EnableFileDownload = false;
            this.m_EnableOrderEntry = false;
            this.m_EnableReportBrowser = true;
            this.m_EnableBillingBrowser = false;
            this.m_EnableEmailAlert = false;
            this.m_VersionCurrentlyUsing = "3.0.0.0";
            this.m_SystemUserId = 0;
        }

        [PersistentCollection]
        public WebServiceAccountClientCollection WebServiceAccountClientCollection
        {
            get { return this.m_WebServiceAccountClientCollection; }
            set { this.m_WebServiceAccountClientCollection = value; }
        }

        [PersistentPrimaryKeyProperty(false)]
        public int WebServiceAccountId
        {
            get { return this.m_WebServiceAccountId; }
            set
            {
                if (this.m_WebServiceAccountId != value)
                {
                    this.m_WebServiceAccountId = value;
                    this.NotifyPropertyChanged("WebServiceAccountId");
                }
            }
        }

        [PersistentProperty()]
        public string UserName
        {
            get { return this.m_UserName; }
            set
            {
                if (this.m_UserName != value)
                {
                    this.m_UserName = value;
                    this.NotifyPropertyChanged("UserName");
                }
            }
        }

        [PersistentProperty()]
        public string Password
        {
            get { return this.m_Password; }
            set
            {
                if (this.m_Password != value)
                {
                    this.m_Password = value;
                    this.NotifyPropertyChanged("Password");
                }
            }
        }

        [PersistentProperty()]
        public string DisplayName
        {
            get { return this.m_DisplayName; }
            set
            {
                if (this.m_DisplayName != value)
                {
                    this.m_DisplayName = value;
                    this.NotifyPropertyChanged("DisplayName");
                }
            }
        }

        [PersistentProperty()]
        public int PrimaryClientId
        {
            get { return this.m_PrimaryClientId; }
            set
            {
                if (this.m_PrimaryClientId != value)
                {
                    this.m_PrimaryClientId = value;
                    this.NotifyPropertyChanged("PrimaryClientId");
                }
            }
        }

        [PersistentProperty()]
        public string DownloadFileType
        {
            get { return this.m_DownloadFileType; }
            set
            {
                if (this.m_DownloadFileType != value)
                {
                    this.m_DownloadFileType = value;
                    this.NotifyPropertyChanged("DownloadFileType");
                }
            }
        }

        [PersistentProperty()]
        public string InitialPage
        {
            get { return this.m_InitialPage; }
            set
            {
                if (this.m_InitialPage != value)
                {
                    this.m_InitialPage = value;
                    this.NotifyPropertyChanged("InitialPage");
                }
            }
        }

        [PersistentProperty()]
        public int ApplicationTimeoutMinutes
        {
            get { return this.m_ApplicationTimeoutMinutes; }
            set
            {
                if (this.m_ApplicationTimeoutMinutes != value)
                {
                    this.m_ApplicationTimeoutMinutes = value;
                    this.NotifyPropertyChanged("ApplicationTimeoutMinutes");
                }
            }
        }

        [PersistentProperty()]
        public string RemoteFileDownloadDirectory
        {
            get { return this.m_RemoteFileDownloadDirectory; }
            set
            {
                if (this.m_RemoteFileDownloadDirectory != value)
                {
                    this.m_RemoteFileDownloadDirectory = value;
                    this.NotifyPropertyChanged("RemoteFileDownloadDirectory");
                }
            }
        }

        [PersistentProperty()]
        public string RemoteFileUploadDirectory
        {
            get { return this.m_RemoteFileUploadDirectory; }
            set
            {
                if (this.m_RemoteFileUploadDirectory != value)
                {
                    this.m_RemoteFileUploadDirectory = value;
                    this.NotifyPropertyChanged("RemoteFileUploadDirectory");
                }
            }
        }

        [PersistentProperty()]
        public string AlertEmailAddress
        {
            get { return this.m_AlertEmailAddress; }
            set
            {
                if (this.m_AlertEmailAddress != value)
                {
                    this.m_AlertEmailAddress = value;
                    this.NotifyPropertyChanged("AlertEmailAddress");
                }
            }
        }

        [PersistentProperty()]
        public bool SaveUserNameLocal
        {
            get { return this.m_SaveUserNameLocal; }
            set
            {
                if (this.m_SaveUserNameLocal != value)
                {
                    this.m_SaveUserNameLocal = value;
                    this.NotifyPropertyChanged("SaveUserNameLocal");
                }
            }
        }

        [PersistentProperty()]
        public bool SavePasswordLocal
        {
            get { return this.m_SavePasswordLocal; }
            set
            {
                if (this.m_SavePasswordLocal != value)
                {
                    this.m_SavePasswordLocal = value;
                    this.NotifyPropertyChanged("SavePasswordLocal");
                }
            }
        }

        [PersistentProperty()]
        public bool EnableApplicationTimeout
        {
            get { return this.m_EnableApplicationTimeout; }
            set
            {
                if (this.m_EnableApplicationTimeout != value)
                {
                    this.m_EnableApplicationTimeout = value;
                    this.NotifyPropertyChanged("EnableApplicationTimeout");
                }
            }
        }

        [PersistentProperty()]
        public bool EnableSaveSettings
        {
            get { return this.m_EnableSaveSettings; }
            set
            {
                if (this.m_EnableSaveSettings != value)
                {
                    this.m_EnableSaveSettings = value;
                    this.NotifyPropertyChanged("EnableSaveSettings");
                }
            }
        }

        [PersistentProperty()]
        public bool EnableFileUpload
        {
            get { return this.m_EnableFileUpload; }
            set
            {
                if (this.m_EnableFileUpload != value)
                {
                    this.m_EnableFileUpload = value;
                    this.NotifyPropertyChanged("EnableFileUpload");
                }
            }
        }

        [PersistentProperty()]
        public bool EnableFileDownload
        {
            get { return this.m_EnableFileDownload; }
            set
            {
                if (this.m_EnableFileDownload != value)
                {
                    this.m_EnableFileDownload = value;
                    this.NotifyPropertyChanged("EnableFileDownload");
                }
            }
        }

        [PersistentProperty()]
        public bool EnableOrderEntry
        {
            get { return this.m_EnableOrderEntry; }
            set
            {
                if (this.m_EnableOrderEntry != value)
                {
                    this.m_EnableOrderEntry = value;
                    this.NotifyPropertyChanged("EnableOrderEntry");
                }
            }
        }

        [PersistentProperty()]
        public bool EnableReportBrowser
        {
            get { return this.m_EnableReportBrowser; }
            set
            {
                if (this.m_EnableReportBrowser != value)
                {
                    this.m_EnableReportBrowser = value;
                    this.NotifyPropertyChanged("EnableReportBrowser");
                }
            }
        }

        [PersistentProperty()]
        public bool EnableBillingBrowser
        {
            get { return this.m_EnableBillingBrowser; }
            set
            {
                if (this.m_EnableBillingBrowser != value)
                {
                    this.m_EnableBillingBrowser = value;
                    this.NotifyPropertyChanged("EnableBillingBrowser");
                }
            }
        }

        [PersistentProperty()]
        public bool EnableEmailAlert
        {
            get { return this.m_EnableEmailAlert; }
            set
            {
                if (this.m_EnableEmailAlert != value)
                {
                    this.m_EnableEmailAlert = value;
                    this.NotifyPropertyChanged("EnableEmailAlert");
                }
            }
        }

        [PersistentProperty()]
        public string VersionCurrentlyUsing
        {
            get { return this.m_VersionCurrentlyUsing; }
            set
            {
                if (this.m_VersionCurrentlyUsing != value)
                {
                    this.m_VersionCurrentlyUsing = value;
                    this.NotifyPropertyChanged("VersionCurrentlyUsing");
                }
            }
        }

        [PersistentProperty()]
        public int SystemUserId
        {
            get { return this.m_SystemUserId; }
            set
            {
                if (this.m_SystemUserId != value)
                {
                    this.m_SystemUserId = value;
                    this.NotifyPropertyChanged("SystemUserId");
                }
            }
        }

        [PersistentProperty()]
        public string Signature
        {
            get { return this.m_Signature; }
            set
            {
                if (this.m_Signature != value)
                {
                    this.m_Signature = value;
                    this.NotifyPropertyChanged("Signature");
                }
            }
        }

        [PersistentProperty()]
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
        public string ObjectId
        {
            get { return this.m_ObjectId; }
            set
            {
                if (this.m_ObjectId != value)
                {
                    this.m_ObjectId = value;
                    this.NotifyPropertyChanged("ObjectId");
                }
            }
        }

        [PersistentProperty()]
        public string PublicAddress
        {
            get { return this.m_PublicAddress; }
            set
            {
                if (this.m_PublicAddress != value)
                {
                    this.m_PublicAddress = value;
                    this.NotifyPropertyChanged("PublicAddress");
                }
            }
        }
    }
}
