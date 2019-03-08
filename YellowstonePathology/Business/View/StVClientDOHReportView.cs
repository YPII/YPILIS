using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.View
{
    public class StVClientDOHReportView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_ReportNo;
        private string m_ClientName;
        private string m_ReportedTo;
        private DateTime? m_TimeOfLastDistribution;

        public StVClientDOHReportView() { }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        [PersistentProperty()]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                if(this.m_ReportNo != value)
                {
                    this.m_ReportNo = value;
                    NotifyPropertyChanged("ReportNo");
                }
            }
        }

        [PersistentProperty()]
        public string ClientName
        {
            get { return this.m_ClientName; }
            set
            {
                if (this.m_ClientName != value)
                {
                    this.m_ClientName = value;
                    NotifyPropertyChanged("ClientName");
                }
            }
        }

        [PersistentProperty()]
        public string ReportedTo
        {
            get { return this.m_ReportedTo; }
            set
            {
                if (this.m_ReportedTo != value)
                {
                    this.m_ReportedTo = value;
                    NotifyPropertyChanged("ReportedTo");
                }
            }
        }

        [PersistentProperty()]
        public DateTime? TimeOfLastDistribution
        {
            get { return this.m_TimeOfLastDistribution; }
            set
            {
                if (this.m_TimeOfLastDistribution != value)
                {
                    this.m_TimeOfLastDistribution = value;
                    NotifyPropertyChanged("TimeOfLastDistribution");
                }
            }
        }
    }
}
