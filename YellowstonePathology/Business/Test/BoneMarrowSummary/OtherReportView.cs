using System;
using System.Collections.Generic;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;


namespace YellowstonePathology.Business.Test.BoneMarrowSummary
{
    public class OtherReportView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_ReportNo;
        private string m_MasterAccessionNo;
        private string m_PanelSetName;
        private DateTime? m_FinalDate;
        private string m_SummaryReportNo;
        private int m_PanelSetId;
        private bool m_IncludeOnSummaryReport;

        public OtherReportView() { }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        [PersistentProperty(true)]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                if (this.m_ReportNo != value)
                {
                    this.m_ReportNo = value;
                    this.NotifyPropertyChanged("ReportNo");
                }
            }
        }

        [PersistentProperty(true)]
        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set
            {
                if (this.m_MasterAccessionNo != value)
                {
                    this.m_MasterAccessionNo = value;
                    this.NotifyPropertyChanged("MasterAccessionNo");
                }
            }
        }

        [PersistentProperty(true)]
        public string PanelSetName
        {
            get { return this.m_PanelSetName; }
            set
            {
                if (this.m_PanelSetName != value)
                {
                    this.m_PanelSetName = value;
                    this.NotifyPropertyChanged("PanelSetName");
                }
            }
        }

        [PersistentProperty(true)]
        public DateTime? FinalDate
        {
            get { return this.m_FinalDate; }
            set
            {
                if (this.m_FinalDate != value)
                {
                    this.m_FinalDate = value;
                    this.NotifyPropertyChanged("FinalDate");
                }
            }
        }

        [PersistentProperty(true)]
        public string SummaryReportNo
        {
            get { return this.m_SummaryReportNo; }
            set
            {
                if (this.m_SummaryReportNo != value)
                {
                    this.m_SummaryReportNo = value;
                    this.NotifyPropertyChanged("SummaryReportNo");
                }
            }
        }

        [PersistentProperty(true)]
        public int PanelSetId
        {
            get { return this.m_PanelSetId; }
            set
            {
                if (this.m_PanelSetId != value)
                {
                    this.m_PanelSetId = value;
                    this.NotifyPropertyChanged("PanelSetId");
                }
            }
        }

        [PersistentProperty(true)]
        public bool IncludeOnSummaryReport
        {
            get { return this.m_IncludeOnSummaryReport; }
            set
            {
                if (this.m_IncludeOnSummaryReport != value)
                {
                    this.m_IncludeOnSummaryReport = value;
                    this.NotifyPropertyChanged("IncludeOnSummaryReport");
                }
            }
        }
    }
}
