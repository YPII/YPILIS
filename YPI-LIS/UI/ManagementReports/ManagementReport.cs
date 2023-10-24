using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.UI.ManagementReports
{
    public class ManagementReport : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_ReportId;
        private string m_ReportName;

        private List<int> m_WebServiceAccountIds;

        public ManagementReport()
        {
            this.m_WebServiceAccountIds = new List<int>();
        }

        public List<int> WebServiceAccounts
        {
            get { return this.m_WebServiceAccountIds; }
        }

        public string ReportId
        {
            get { return this.m_ReportId; }
            set
            {
                if (this.m_ReportId != value)
                {
                    this.m_ReportId = value;
                    this.NotifyPropertyChanged("ReportId");
                }
            }
        }

        public string ReportName
        {
            get { return this.m_ReportName; }
            set
            {
                if (this.m_ReportName != value)
                {
                    this.m_ReportName = value;
                    this.NotifyPropertyChanged("ReportName");
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
